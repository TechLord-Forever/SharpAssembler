using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SharpAssembler.Symbols;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// Represents an encoded instruction.
    /// </summary>
    public sealed partial class EncodedInstruction : IEmittable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedInstruction"/> class.
        /// </summary>
        public EncodedInstruction(byte[] opcode, byte fixedReg, bool lockPrefix)
        {
            Opcode   = opcode;
            FixedReg = fixedReg;
            Prefix1  = lockPrefix ? PrefixLockRepeat.Lock : PrefixLockRepeat.None;
        }

        /// <summary>
        /// Gets or sets the group 1 prefix used.
        /// </summary>
        /// <value>A member of the <see cref="PrefixLockRepeat"/> enumeration; or
        /// <see cref="PrefixLockRepeat.None"/> to specify no prefix.
        /// The default is <see cref="PrefixLockRepeat.None"/>.</value>
        public PrefixLockRepeat Prefix1 { get; set; } = PrefixLockRepeat.None;

        /// <summary>
        /// Gets or sets the group 2 prefix used.
        /// </summary>
        /// <value>A member of the <see cref="PrefixSegmentBranch"/> enumeration; or
        /// <see cref="PrefixSegmentBranch.None"/> to specify no prefix.
        /// The default is <see cref="PrefixSegmentBranch.None"/>.</value>
        public PrefixSegmentBranch Prefix2 { get; set; } = PrefixSegmentBranch.None;

        /// <summary>
        /// Gets or sets the group 3 prefix used.
        /// </summary>
        /// <value>A member of the <see cref="PrefixAddressSizeOverride"/> enumeration; or
        /// <see cref="PrefixAddressSizeOverride.None"/> to specify no prefix.
        /// The default is <see cref="PrefixAddressSizeOverride.None"/>.</value>
        public PrefixAddressSizeOverride Prefix3 { get; set; } = PrefixAddressSizeOverride.None;

        /// <summary>
        /// Gets or sets the group 4 prefix used.
        /// </summary>
        /// <value>A member of the <see cref="PrefixOperandSizeOverride"/> enumeration; or
        /// <see cref="PrefixOperandSizeOverride.None"/> to specify no prefix.
        /// The default is <see cref="PrefixOperandSizeOverride.None"/>.</value>
        public PrefixOperandSizeOverride Prefix4 { get; set; } = PrefixOperandSizeOverride.None;

        /// <summary>
        /// Gets or sets the mandatory prefix.
        /// </summary>
        /// <value>The mandatory prefix as an array of bytes. The default is an empty array.</value>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public byte[] MandatoryPrefix { get; set; } = new byte[0];

        /// <summary>
        /// Gets or sets the opcode of the encoded instruction.
        /// </summary>
        /// <value>The opcode bytes of the instruction. The default is an empty array.</value>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public byte[] Opcode { get; private set; } = new byte[0];

        byte opcodeReg = 0;
        /// <summary>
        /// Gets or sets a value which is added to the opcode byte.
        /// </summary>
        /// <value>The 4-bit opcode REG value. The default is 0.</value>
        /// <remarks>
        /// The least significant three bits are encoded by OR-ing them with the last opcode byte, while the fourth bit
        /// is encoded in the REX.B bit.
        /// </remarks>
        public byte OpcodeReg
        {
            get { return opcodeReg; }
            set
            {
                if (value > 0xf)
                    throw new Exception("Only the first 4 bits may be set.");
                opcodeReg = value;
            }
        }

        /// <summary>
        /// Gets the ModR/M byte to encode.
        /// </summary>
        /// <value>A <see cref="ModRMByte"/>; or <see langword="null"/> to not encode a ModR/M byte. The default is
        /// <see langword="null"/>.</value>
        /// <remarks>
        /// To ensure that a ModR/M byte will be created (i.e. that this property is not <see langword="null"/>), call
        /// the <see cref="SetModRMByte"/> method.
        /// </remarks>
        public ModRMByte ModRM { get; private set; }

        /// <summary>
        /// Gets the SIB byte to encode.
        /// </summary>
        /// <value>A <see cref="SibByte"/>; or <see langword="null"/> to not encode a SIB byte. The default is
        /// <see langword="null"/>.</value>
        /// <remarks>
        /// To ensure that a ModR/M byte will be created (i.e. that this property is not <see langword="null"/>), call
        /// the <see cref="SetSIBByte"/> method.
        /// </remarks>
        public SibByte Sib { get; private set; }

        byte fixedReg;
        /// <summary>
        /// Gets or sets the fixed value of the REG part of the ModR/M byte, when a ModR/M is used.
        /// </summary>
        /// <value>The 3-bit fixed REG value.</value>
        /// <remarks>
        /// <para>When no operands require a ModR/M byte, or when the encoding of an operand puts a value into the
        /// ModR/M's REG field, this property is ignored.</para>
        /// <para>Specify this value before the first call to <see cref="SetModRMByte"/>; otherwise the REG field is
        /// not set.</para>
        /// </remarks>
        public byte FixedReg
        {
            get { return fixedReg; }
            set
            {
                if (value > 0x7)
                    throw new Exception("Only the first 3 bits may be set.");
                fixedReg = value;
            }
        }

        /// <summary>
        /// Gets or sets whether 64-bit operands are used, and whether a REX prefix is used.
        /// </summary>
        /// <value><see langword="true"/> to use 64-bit operands; otherwise, <see langword="false"/>;
        /// or <see langword="null"/> when this does not apply. The default is <see langword="null"/>.</value>
        /// <remarks>
        /// When this property is not <see langword="null"/>, a REX byte is encoded. When this property is
        /// <see langword="null"/>, no REX byte is encoded, regardless of the fourth bit of the SIB byte's BASE o
        /// INDEX fields, or the fourth bit of the ModR/M byte's RM or REG fields.
        /// </remarks>
        public bool? Use64BitOperands { get; set; } = null;

        /// <summary>
        /// Gets or sets the displacement value.
        /// </summary>
        /// <value>An <see cref="ReferenceOffset"/> specifying the displacement value or symbol;
        /// or <see langword="null"/> to use no displacement. The default is <see langword="null"/>.</value>
        public ReferenceOffset Displacement { get; set; } = null;

        /// <summary>
        /// Gets or sets the actual size of the displacement value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration. The default is
        /// <see cref="DataSize.None"/>.</value>
        public DataSize DisplacementSize { get; set; } = DataSize.None;

        /// <summary>
        /// Gets or sets the immediate value.
        /// </summary>
        /// <value>An <see cref="ReferenceOffset"/> specifying the immediate value or symbol;
        /// or <see langword="null"/> to use no immediate. The default is <see langword="null"/>.</value>
        /// <remarks>
        /// The immediate value may be used as third opcode byte for AMD 3DNow! instructions.
        /// </remarks>
        public ReferenceOffset Immediate { get; set; }// FIXME: Where do AMD 3DNow! bytes go in here?

        /// <summary>
        /// Gets or sets the actual size of the immediate value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.
        /// The default is <see cref="DataSize.None"/>.</value>
        public DataSize ImmediateSize { get; set; } = DataSize.None;

        /// <summary>
        /// Gets or sets the immediate extra value.
        /// </summary>
        /// <value>An <see cref="ReferenceOffset"/> specifying the extra immediate value or symbol;
        /// or <see langword="null"/> to use no extra immediate. The default is <see langword="null"/>.</value>
        public ReferenceOffset ExtraImmediate { get; set; }

        /// <summary>
        /// Gets or sets the actual size of the extra immediate value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.
        /// The default is <see cref="DataSize.None"/>.</value>
        public DataSize ExtraImmediateSize { get; set; } = DataSize.None;

        #region Methods
        /// <summary>
        /// Gets the length of the encoded instruction.
        /// </summary>
        /// <returns>The length of the encoded instruction, in bytes.</returns>
        public int GetLength()
        {
            int length = 0;

            // Legacy prefixes
            if (Prefix1 != PrefixLockRepeat.None)
                length++;
            if (Prefix2 != PrefixSegmentBranch.None)
                length++;
            if (Prefix3 != PrefixAddressSizeOverride.None)
                length++;
            if (Prefix4 != PrefixOperandSizeOverride.None)
                length++;

            // Mandatory prefix
            if (MandatoryPrefix != null)
                length += MandatoryPrefix.Length;

            // REX prefix
            if (Use64BitOperands.HasValue)
                length++;

            // Opcode
            if (Opcode != null)
                length += Opcode.Length;

            // ModR/M byte
            if (ModRM != null)
                length++;

            // SIB byte
            if (Sib != null)
                length++;

            // Displacement
            length += (int)DisplacementSize;

            // Immediate
            length += (int)ImmediateSize;

            // Extra Immediate
            length += (int)ExtraImmediateSize;

            return length;
        }

        #region Emitting
        /// <summary>
        /// Modifies the context and emits the binary representation of this emittable.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the emittable will be emitted.</param>
        /// <param name="context">The <see cref="Context"/> in which the emittable will be emitted.</param>
        /// <returns>The number of emitted bytes.</returns>
        public int Emit(BinaryWriter writer, Context context)
        {
            long instructionOffset = writer.BaseStream.Position;

            EmitLegacyPrefixes(writer);
            EmitMandatoryPrefix(writer);
            EmitREXPrefix(writer);

            EmitOpcode(writer);

            EmitModRMByte(writer);
            EmitSIBByte(writer);

            EmitReferenceOffset(writer, instructionOffset, context, Displacement, DisplacementSize);
            EmitReferenceOffset(writer, instructionOffset, context, Immediate, ImmediateSize);
            EmitReferenceOffset(writer, instructionOffset, context, ExtraImmediate, ExtraImmediateSize);

            return checked((int)(writer.BaseStream.Position - instructionOffset));
        }

        /// <summary>
        /// Emits the legacy prefixes.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitLegacyPrefixes(BinaryWriter writer)
        {
            if (Prefix1 != PrefixLockRepeat.None)
                writer.Write((byte)Prefix1);
            if (Prefix2 != PrefixSegmentBranch.None)
                writer.Write((byte)Prefix2);
            if (Prefix3 != PrefixAddressSizeOverride.None)
                writer.Write((byte)Prefix3);
            if (Prefix4 != PrefixOperandSizeOverride.None)
                writer.Write((byte)Prefix4);
        }

        /// <summary>
        /// Emits the mandatory prefix.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitMandatoryPrefix(BinaryWriter writer)
        {
            writer.Write(MandatoryPrefix);
        }

        /// <summary>
        /// Emits the REX-prefix, if used.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitREXPrefix(BinaryWriter writer)
        {
            if (!Use64BitOperands.HasValue)
                return;

            byte rex = 0x40;
            if (Use64BitOperands.Value)
                rex |= 0x08;
            if (ModRM != null && Sib != null)
            {
                rex |= (byte)((Sib.Base & 0x08) >> 3);        // REX.B
                rex |= (byte)((Sib.Index & 0x08) >> 2);        // REX.X
                rex |= (byte)((ModRM.Reg & 0x08) >> 1);        // REX.R
            }
            else if (ModRM != null)
            {
                rex |= (byte)((ModRM.RM & 0x08) >> 3);        // REX.B
                rex |= (byte)((ModRM.Reg & 0x08) >> 1);        // REX.R
            }
            else
            {
                // No ModR/M or SIB bytes, but a reg-value anyway.
                rex |= (byte)((opcodeReg & 0x08) >> 3);        // REX.B
            }

            writer.Write(rex);
        }

        /// <summary>
        /// Emits the opcode of the instruction.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitOpcode(BinaryWriter writer)
        {
            // We OR the least siginificant 3 bits of the opcode REG with the last byte of the opcode, if necessary.
            byte[] actualOpcode;
            if (opcodeReg > 0)
            {
                actualOpcode = (byte[])Opcode.Clone();
                actualOpcode[actualOpcode.Length - 1] |= (byte)(opcodeReg & 0x7);
            }
            else
                actualOpcode = Opcode;

            writer.Write(actualOpcode);
        }

        /// <summary>
        /// Emits the MOD-R/M byte of the instruction, if any.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitModRMByte(BinaryWriter writer)
        {
            if (ModRM == null)
                return;

            byte modrmbyte = 0;
            modrmbyte |= (byte)((ModRM.RM & 0x07));
            modrmbyte |= (byte)((ModRM.Reg & 0x07) << 3);
            modrmbyte |= (byte)(ModRM.Mod << 6);

            writer.Write(modrmbyte);
        }

        /// <summary>
        /// Emits the SIB byte of the instruction, if any.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        void EmitSIBByte(BinaryWriter writer)
        {
            if (Sib == null)
                return;

            byte sibbyte = 0;
            sibbyte |= (byte)((Sib.Base & 0x07));
            sibbyte |= (byte)((Sib.Index & 0x07) << 3);
            sibbyte |= (byte)(Sib.Scale << 6);

            writer.Write(sibbyte);
        }

        /// <summary>
        /// Emits the immediate value as part of the instruction, if any.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        /// <param name="instructionOffset">The offset of the instruction in the stream underlying
        /// <paramref name="writer"/>.</param>
        /// <param name="context">The <see cref="Context"/> of the instruction.</param>
        /// <param name="expression">The <see cref="ReferenceOffset"/> to emit.</param>
        /// <param name="size">The size of the value to emit.</param>
        void EmitReferenceOffset(BinaryWriter writer, long instructionOffset, Context context, ReferenceOffset expression, DataSize size)
        {
            if (expression == null)
                return;

            // Number of bytes before the expression.
            var relocationDiff = (ulong)(writer.BaseStream.Position - instructionOffset);
            Relocation relocation = null;

            var actualValue = expression.Evaluate(context);
            if (expression.Reference != null)
            {
                relocation = new Relocation(
                    expression.Reference.Symbol,
                    //context.Section,
                    (long)context.Address,
                    actualValue,
                    RelocationType.Default32);
            }

            // Emit the expression's value.
            EmitConstant(writer, size, actualValue);

            // Add the relocation to the context.
            if (relocation != null)
            {
                relocation.Offset += (long)relocationDiff;
                context.RelocationTable.Add(relocation);
            }
        }

        /// <summary>
        /// Emits a constant value.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        /// <param name="size">The size of the constant to emit.</param>
        /// <param name="constant">The constant value to emit.</param>
        void EmitConstant(BinaryWriter writer, DataSize size, long constant)
        {
            try
            {
                switch (size)
                {
                    case DataSize.Bit8:
                        writer.Write(checked((byte)constant));
                        break;
                    case DataSize.Bit16:
                        writer.Write(checked((ushort)constant));
                        break;
                    case DataSize.Bit32:
                        writer.Write(checked((uint)constant));
                        break;
                    case DataSize.Bit64:
                        writer.Write(checked((ulong)constant));
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            catch (OverflowException ex)
            {
                throw new AssemblerException("The value to emit does not fit in the specified width.", ex);
            }
        }
        #endregion

        /// <summary>
        /// Sets that the ModR/M byte gets encoded.
        /// </summary>
        public void SetModRMByte()
        {
            if (ModRM == null)
                ModRM = new ModRMByte(0, fixedReg, 0);
        }

        /// <summary>
        /// Sets that the SIB byte gets encoded.
        /// </summary>
        public void SetSIBByte()
        {
            if (Sib == null)
                Sib = new SibByte();
        }

        /// <summary>
        /// Gets the address size based on the address size prefix and the current assembler mode.
        /// </summary>
        /// <param name="assemblerMode">The assembler mode.</param>
        /// <returns>The address size.</returns>
        public DataSize GetAddressSize(DataSize assemblerMode)
        {
            if (Prefix3 == PrefixAddressSizeOverride.None)
                return assemblerMode;

            switch (assemblerMode)
            {
                case DataSize.Bit16:
                    return DataSize.Bit32;
                case DataSize.Bit32:
                    return DataSize.Bit16;
                case DataSize.Bit64:
                    return DataSize.Bit32;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Sets the address size prefix when needed.
        /// </summary>
        /// <param name="assemblerMode">The assembler mode.</param>
        /// <param name="addressSize">The address size.</param>
        /// <remarks>
        /// This method assumes that only valid combinations of address size and mode are given.
        /// </remarks>
        public void SetAddressSize(DataSize assemblerMode, DataSize addressSize)
        {
            if (addressSize != DataSize.None && assemblerMode != addressSize)
                Prefix3 = PrefixAddressSizeOverride.AddressSizeOverride;
            else
                Prefix3 = PrefixAddressSizeOverride.None;
        }

        // Note currently used and not tested. Probably has a bug with 64-bit instructions
        // that don't require a REX prefix.

        /// <summary>
        /// Gets the operand size based on the operand size prefix and the current assembler mode.
        /// </summary>
        /// <param name="assemblerMode">The assembler mode.</param>
        /// <returns>The operand size.</returns>
        public DataSize GetOperandSize(DataSize assemblerMode)
        {
            switch (assemblerMode)
            {
                case DataSize.Bit16:
                    if (Prefix4 == PrefixOperandSizeOverride.OperandSizeOverride)
                        return DataSize.Bit32;
                    else
                        return DataSize.Bit16;
                case DataSize.Bit32:
                    if (Prefix4 == PrefixOperandSizeOverride.OperandSizeOverride)
                        return DataSize.Bit16;
                    else
                        return DataSize.Bit32;
                case DataSize.Bit64:
                    if (Use64BitOperands.HasValue && Use64BitOperands.Value)
                        return DataSize.Bit64;
                    else if (Prefix4 == PrefixOperandSizeOverride.OperandSizeOverride)
                        return DataSize.Bit16;
                    else
                        return DataSize.Bit32;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Sets the operand size prefix when needed.
        /// </summary>
        /// <param name="assemblerMode">The assembler mode.</param>
        /// <param name="operandSize">The operand size.</param>
        public void SetOperandSize(DataSize assemblerMode, DataSize operandSize)
        {
            if (assemblerMode == DataSize.Bit16 &&
                operandSize == DataSize.Bit32)
                Prefix4 = PrefixOperandSizeOverride.OperandSizeOverride;
            else if ((assemblerMode == DataSize.Bit32 ||
                assemblerMode == DataSize.Bit64) &&
                operandSize == DataSize.Bit16)
                Prefix4 = PrefixOperandSizeOverride.OperandSizeOverride;

            if (assemblerMode == DataSize.Bit64 &&
                operandSize == DataSize.Bit64)
            {
                // Setting this to anything other than null causes a REX prefix to be encoded.
                Use64BitOperands = true;
            }
        }
        #endregion
    }
}
