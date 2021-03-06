﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace OpcodeWriter.X86
{
    partial class X86SpecWriter
    {
        /// <inheritdoc />
        protected override void WriteCodeOpcodeClassProperties(OpcodeSpec spec, TextWriter writer)
        {
            X86OpcodeSpec x86spec = (X86OpcodeSpec)spec;

            WriteCanLock(x86spec, writer);

            base.WriteCodeOpcodeClassProperties(spec, writer);
        }

        /// <inheritdoc />
        protected override void WriteCodeUsingDirectives(TextWriter writer)
        {
            base.WriteCodeUsingDirectives(writer);
            writer.WriteLine("using SharpAssembler.Architectures.X86.Opcodes;");
            writer.WriteLine("using SharpAssembler.Architectures.X86.Operands;");
        }


        /// <summary>
        /// Writes the <c>CanLock</c> property.
        /// </summary>
        /// <param name="spec">The opcode specification.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected void WriteCanLock(X86OpcodeSpec spec, TextWriter writer)
        {
            if (!spec.CanLock)
                return;

            writer.WriteLine(T + T + "/// <inheritdoc />");
            writer.WriteLine(T + T + "public override bool CanLock");
            writer.WriteLine(T + T + "{");
            writer.WriteLine(T + T + T + "get { return true; }");
            writer.WriteLine(T + T + "}");
            writer.WriteLine();
        }

        /// <inheritdoc />
        protected override void WriteCodeOpcodeVariant(OpcodeSpec spec, OpcodeVariantSpec variant, TextWriter writer)
        {
            var x86variant = (X86OpcodeVariantSpec)variant;
            var operands = x86variant.Operands.Cast<X86OperandSpec>();
            var operandNames = from o in operands select GetOperandManualName(o);

            writer.Write(T + T + T + T + "// {0}", spec.Mnemonic.ToUpperInvariant());
            if (operandNames.Any())
                writer.WriteLine(" {0}", string.Join(", ", operandNames));
            else
                writer.WriteLine();
            writer.WriteLine(T + T + T + T + "new X86OpcodeVariant(");

            string opcodeBytes = string.Join(", ", from b in x86variant.OpcodeBytes select string.Format("0x{0:X2}", b));

            writer.Write(T + T + T + T + T + "new byte[] {{ {0} }}", opcodeBytes);

            // Either the fixed REG is not 0, or the opcode has a spot for the REG available as it uses the
            // ModRM byte (because it has a reg/mem operand) but does not specify the REG part (because it does not
            // have a reg operand), or the OperandSize is specified, which requires the REG also to be specified.
            if (x86variant.FixedReg != 0 || (x86variant.OperandSize != DataSize.None && operands.Any()) ||
                (operands.Any(o => o.Type == X86OperandType.RegisterOrMemoryOperand) &&
                !operands.Any(o => o.Type == X86OperandType.RegisterOperand)))
            {
                writer.Write(", {0}", x86variant.FixedReg);
            }

            if (x86variant.OperandSize != DataSize.None)
            {
                writer.Write(", DataSize.{0}", Enum.GetName(typeof(DataSize), x86variant.OperandSize));
            }

            foreach (var operand in operands)
            {
                writer.WriteLine(",");
                WriteOperandDescriptor(operand, writer);
            }
            writer.Write(")");

            if (x86variant.NoRexPrefix || x86variant.SupportedModes != ProcessorModes.LongProtectedReal)
            {
                writer.WriteLine();
                writer.Write(T + T + T + T + T + "{ ");

                List<string> properties = new List<string>();
                if (x86variant.NoRexPrefix)
                    properties.Add("NoRexPrefix = true");

                if (x86variant.SupportedModes != ProcessorModes.LongProtectedReal)
                    properties.Add("SupportedModes = ProcessorModes." + Enum.GetName(typeof(ProcessorModes), x86variant.SupportedModes));

                writer.Write(string.Join(", ", properties));

                writer.Write(" }");
            }
            writer.WriteLine(",");
        }

        /// <summary>
        /// Writes the operand descriptor.
        /// </summary>
        /// <param name="operand">The operand specification.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        void WriteOperandDescriptor(X86OperandSpec operand, TextWriter writer)
        {
            string encodingStr = string.Empty;
            if (operand.Encoding != X86OperandEncoding.Default)
                encodingStr = ", OperandEncoding." + Enum.GetName(typeof(X86OperandEncoding), operand.Encoding);

            switch (operand.Type)
            {
                case X86OperandType.RegisterOperand:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose{0}Bit" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.FixedRegister:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(Register.{0})",
                        Enum.GetName(typeof(Register), operand.FixedRegister).ToUpperInvariant());
                    break;
                case X86OperandType.Immediate:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.Immediate, DataSize.Bit{0}" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.MemoryOperand:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit{0}" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.MemoryOffset:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.MemoryOffset, DataSize.Bit{0}" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.FarPointer:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.FarPointer, DataSize.Bit{0}" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.RegisterOrMemoryOperand:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose{0}Bit" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                case X86OperandType.RelativeOffset:
                    writer.Write(T + T + T + T + T + "new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit{0}" + encodingStr + ")",
                        operand.Size.GetBitCount());
                    break;
                default:
                    throw new NotSupportedException("The operand type is not supported.");
            }
        }

        /// <summary>
        /// Writes the documentation for the opcode variant method.
        /// </summary>
        /// <param name="spec">The opcode specification.</param>
        /// <param name="variant">The opcode variant.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected void WriteCodeInstrOpcodeVariantMethodDocumentation(X86OpcodeSpec spec,
            IEnumerable<Tuple<X86OperandSpec, string, string>> operands, TextWriter writer)
        {
            writer.WriteLine(T + T + "/// <summary>");
            writer.WriteLine(T + T + "/// Creates a new {0} ({1}) instruction.", spec.Mnemonic.ToUpperInvariant(), spec.ShortDescription);
            writer.WriteLine(T + T + "/// </summary>");

            foreach (var operand in operands)
            {
                WriteOperandDocumentation(operand, writer);
            }

            writer.WriteLine(T + T + "/// <returns>The created instruction.</returns>");
        }

        /// <inheritdoc />
        protected override void WriteCodeInstrOpcodeVariantMethods(OpcodeSpec spec, string mnemonic, TextWriter writer)
        {
            var x86spec = (X86OpcodeSpec)spec;

            // Determine all possible combinations of parameters.
            var operandArguments = x86spec.Variants
                .SelectMany(v => CartesianProduct(
                    from o in v.Operands.Cast<X86OperandSpec>()
                    select GetOperandArguments(o)));
            var operandTuples = operandArguments.Distinct(new OperandEnumerationEqualityComparer());


            if (operandTuples.Any())
            {
                WriteCodeInstrOpcodeVariantMethod(x86spec, mnemonic, operandTuples.First(), writer);
                foreach (var operands in operandTuples.Skip(1))
                {
                    writer.WriteLine();
                    WriteCodeInstrOpcodeVariantMethod(x86spec, mnemonic, operands, writer);
                }
            }
        }

        /// <summary>
        /// Writes a single opcode variant method.
        /// </summary>
        /// <param name="spec">The opcode specification.</param>
        /// <param name="mnemonic">The mnemonic to use.</param>
        /// <param name="operands">The operands.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        void WriteCodeInstrOpcodeVariantMethod(X86OpcodeSpec spec, string mnemonic, IEnumerable<Tuple<X86OperandSpec, string, string>> operands, TextWriter writer)
        {
            WriteCodeInstrOpcodeVariantMethodDocumentation(spec, operands, writer);

            var operandsString = string.Join(", ", operands.Select(o => o.Item2 + " " + AsValidIdentifier(o.Item1.Name)));
            writer.WriteLine(T + T + "public static X86Instruction {0}({1})", AsValidIdentifier(mnemonic), operandsString);

            var argumentsString = string.Join(", ", operands.Select(o => string.Format(o.Item3, AsValidIdentifier(o.Item1.Name))));
            writer.WriteLine(T + T + "{");
            writer.WriteLine(T + T + T + "return X86Opcode.{0}.CreateInstruction({1});", AsValidIdentifier(spec.Name), argumentsString);
            writer.WriteLine(T + T + "}");
        }

        /// <summary>
        /// Returns the carthesian product of a sequence of sequences.
        /// </summary>
        /// <typeparam name="T">The type of object in the sequences.</typeparam>
        /// <param name="sequences">The sequences.</param>
        /// <returns>The cartesian product of the sequences.</returns>
        static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            // This implementation is based on the code published by Eric Lippert on his blog at:
            // https://blogs.msdn.com/b/ericlippert/archive/2010/06/28/computing-a-cartesian-product-with-linq.aspx

            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item }));
        }

        /// <summary>
        /// Gets the arguments for the specified operand.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <param name="clsCompliant">Whether the resulting method is CLS compliant.</param>
        /// <returns>An array of tuples. Each tuple specifies the type of the argument and the
        /// implementation as an operand. The latter uses <c>{0}</c> in place of the argument name.</returns>
        IEnumerable<Tuple<X86OperandSpec, string, string>> GetOperandArguments(X86OperandSpec operand)
        {
            switch (operand.Type)
            {
                case X86OperandType.Immediate:
                    if (operand.Size == DataSize.Bit8)
                    {
                        return new Tuple<X86OperandSpec, string, string>[]{
                            new Tuple<X86OperandSpec, string, string>(operand, "byte", "new Immediate({0}, DataSize.Bit8)"),
                            new Tuple<X86OperandSpec, string, string>(operand, "sbyte", "new Immediate({0}, DataSize.Bit8)"),
                        };
                    }
                    else if (operand.Size == DataSize.Bit16)
                    {
                        return new Tuple<X86OperandSpec, string, string>[]{
                            new Tuple<X86OperandSpec, string, string>(operand, "short", "new Immediate({0}, DataSize.Bit16)"),
                            new Tuple<X86OperandSpec, string, string>(operand, "ushort", "new Immediate({0}, DataSize.Bit16)"),
                        };
                    }
                    else if (operand.Size == DataSize.Bit32)
                    {
                        return new Tuple<X86OperandSpec, string, string>[]{
                            new Tuple<X86OperandSpec, string, string>(operand, "int", "new Immediate({0}, DataSize.Bit32)"),
                            new Tuple<X86OperandSpec, string, string>(operand, "uint", "new Immediate({0}, DataSize.Bit32)"),
                        };
                    }
                    else if (operand.Size == DataSize.Bit64)
                    {
                        return new Tuple<X86OperandSpec, string, string>[]{
                            new Tuple<X86OperandSpec, string, string>(operand, "long", "new Immediate({0}, DataSize.Bit64)"),
                            new Tuple<X86OperandSpec, string, string>(operand, "ulong", "new Immediate({0}, DataSize.Bit64)"),
                        };
                    }
                    else
                        throw new NotSupportedException("The operand size is not supported.");
                case X86OperandType.MemoryOperand:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "EffectiveAddress", "{0}"),
                    };
                case X86OperandType.MemoryOffset:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "MemoryOffset", "{0}"),
                    };
                case X86OperandType.FarPointer:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "FarPointer", "{0}"),
                        new Tuple<X86OperandSpec, string, string>(operand, "EffectiveAddress", "{0}"),
                    };
                case X86OperandType.RegisterOrMemoryOperand:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "Register", "new RegisterOperand({0})"),
                        new Tuple<X86OperandSpec, string, string>(operand, "EffectiveAddress", "{0}"),
                    };
                case X86OperandType.RelativeOffset:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "RelativeOffset", "{0}"),
                    };
                case X86OperandType.RegisterOperand:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "Register", "new RegisterOperand({0})"),
                    };
                case X86OperandType.FixedRegister:
                    return new Tuple<X86OperandSpec, string, string>[]{
                        new Tuple<X86OperandSpec, string, string>(operand, "Register", "new RegisterOperand({0})"),
                    };
                //return Enumerable.Empty<Tuple<X86OperandSpec, String, String>>();
                default:
                    throw new NotSupportedException("The operand type is not supported.");
            }
        }

        /// <summary>
        /// Writes the operand documentation.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        void WriteOperandDocumentation(Tuple<X86OperandSpec, string, string> operand, TextWriter writer)
        {
            switch (operand.Item1.Type)
            {
                case X86OperandType.FixedRegister:
                case X86OperandType.RegisterOperand:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A register.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.Immediate:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">An immediate value.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.MemoryOperand:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A memory operand.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.MemoryOffset:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A memory offset.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.FarPointer:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A far pointer.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.RegisterOrMemoryOperand:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A register or memory operand.</param>",
                        operand.Item1.Name);
                    break;
                case X86OperandType.RelativeOffset:
                    writer.WriteLine(T + T + "/// <param name=\"{0}\">A relative offset.</param>",
                        operand.Item1.Name);
                    break;
                default:
                    throw new NotSupportedException("The operand type is not supported.");
            }
        }

        private class OperandEnumerationEqualityComparer : IEqualityComparer<IEnumerable<Tuple<X86OperandSpec, string, string>>>
        {

            public bool Equals(IEnumerable<Tuple<X86OperandSpec, string, string>> x, IEnumerable<Tuple<X86OperandSpec, string, string>> y)
            {
                return Enumerable.SequenceEqual(x, y, new OperandEqualityComparer());
            }

            public int GetHashCode(IEnumerable<Tuple<X86OperandSpec, string, string>> obj)
            {
                // NOTE: Equality is often determined by looking at the hash codes.
                unchecked
                {
                    int hash = 17;
                    foreach (var e in obj)
                        hash = hash * 29 + e.Item2.GetHashCode();
                    return hash;
                }
            }
        }

        class OperandEqualityComparer : IEqualityComparer<Tuple<X86OperandSpec, string, string>>
        {

            public bool Equals(Tuple<X86OperandSpec, string, string> x, Tuple<X86OperandSpec, string, string> y)
            {
                return x.Item2.Equals(y.Item2);
            }

            public int GetHashCode(Tuple<X86OperandSpec, string, string> obj)
            {
                return obj.Item2.GetHashCode();
            }
        }
    }
}
