using System;

namespace OpcodeWriter.X86
{
    /// <summary>
    /// Writes code based on an opcode specification.
    /// </summary>
    public partial class X86SpecWriter : SpecWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X86SpecWriter"/> class.
        /// </summary>
        /// <param name="yasmExecutablePath">The path to the YASM executable.</param>
        public X86SpecWriter(string yasmExecutablePath)
        {
            this.yasmExecutablePath = yasmExecutablePath;
        }

        protected override string SubNamespace
        {
            get { return "X86"; }
        }

        /// <inheritdoc />
        protected override string GetOpcodeBaseClassName()
        {
            return "X86Opcode";
        }

        /// <inheritdoc />
        protected override string GetOpcodeVariantClassName()
        {
            return "X86OpcodeVariant";
        }

        /// <inheritdoc />
        protected override string GetOpcodeStaticClassName()
        {
            return "X86Opcode";
        }

        /// <inheritdoc />
        protected override string GetTestBaseClassName()
        {
            return "OpcodeTestBase";
        }

        /// <summary>
        /// Gets the name of the operand as used in the manuals.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <returns>The operand name.</returns>
        string GetOperandManualName(X86OperandSpec operand)
        {
            switch (operand.Type)
            {
                case X86OperandType.RegisterOperand:
                    return "reg" + operand.Size.GetBitCount();
                case X86OperandType.FixedRegister:
                    return Enum.GetName(typeof(Register), operand.FixedRegister).ToUpperInvariant();
                case X86OperandType.Immediate:
                    return "imm" + operand.Size.GetBitCount();
                case X86OperandType.MemoryOperand:
                    return "mem" + operand.Size.GetBitCount();
                case X86OperandType.MemoryOffset:
                    return "moffset" + operand.Size.GetBitCount();
                case X86OperandType.FarPointer:
                    return "pntr16:" + operand.Size.GetBitCount();
                case X86OperandType.RegisterOrMemoryOperand:
                    return "reg/mem" + operand.Size.GetBitCount();
                case X86OperandType.RelativeOffset:
                    return "rel" + operand.Size.GetBitCount() + "off";
                default:
                    throw new NotSupportedException("The operand type is not supported.");
            }
        }
    }
}
