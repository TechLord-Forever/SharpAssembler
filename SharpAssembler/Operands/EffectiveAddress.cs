using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An effective address.
    /// </summary>
    public partial class EffectiveAddress : Operand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EffectiveAddress"/> class.
        /// </summary>
        public EffectiveAddress()
            : this(DataSize.Bit32, Register.None, Register.None, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectiveAddress"/> class.
        /// </summary>
        /// <param name="operandSize">The <see cref="DataSize"/> of the operand.</param>
        /// <param name="addressSize">The address size.</param>
        /// <param name="displacement">The displacement expression.</param>
        public EffectiveAddress(DataSize operandSize, DataSize addressSize, Expression<Func<Context, ReferenceOffset>> displacement)
            : this(operandSize, Register.None, Register.None, 0, displacement)
        {
            AddressSize = addressSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectiveAddress"/> class.
        /// </summary>
        /// <param name="operandSize">The <see cref="DataSize"/> of the operand.</param>
        /// <param name="baseRegister">The base <see cref="Register"/>; or <see langword="Register.None"/>.</param>
        /// <param name="indexRegister">The index <see cref="Register"/>; or <see langword="Register.None"/>.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="displacement">The displacement expression.</param>
        public EffectiveAddress(DataSize operandSize, Register baseRegister, Register indexRegister, int scale,
            Expression<Func<Context, ReferenceOffset>> displacement)
            : base(operandSize)
        {
            if (scale > 8)
                throw new ArgumentOutOfRangeException(nameof(scale));

            BaseRegister = baseRegister;
            IndexRegister = indexRegister;
            Scale = scale;
            Displacement = displacement;
        }

        /// <summary>
        /// Gets the base register.
        /// </summary>
        /// <value>A <see cref="Register"/>.</value>
        public Register BaseRegister { get; set; }

        /// <summary>
        /// Gets the index register.
        /// </summary>
        /// <value>A <see cref="Register"/>.</value>
        public Register IndexRegister { get; set; }

        /// <summary>
        /// Gets or sets the scaling factor.
        /// </summary>
        /// <value>The scaling factor, which must be zero, or a positive power of two less than or equal to 8.</value>
        public int Scale { get; private set; }

        /// <summary>
        /// Gets or sets the expression specifying the displacement of the effective address.
        /// </summary>
        /// <value>The displacement <see cref="Func{Context, ReferenceOffset}"/>; or <see langword="null"/> to specify
        /// no displacement.</value>
        public Expression<Func<Context, ReferenceOffset>> Displacement { get; set; }

        /// <summary>
        /// Gets or sets the address size of the effective address.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration;
        /// or <see cref="DataSize.None"/> to specify no particular address size.</value>
        /// <remarks>
        /// When set to a value other than <see cref="DataSize.None"/>, the specified address
        /// size is used. The registers are checked to have the same size. When set to
        /// <see cref="DataSize.None"/>, the address size is determined from the registers. When
        /// no registers are set, the address size is determined from the <see cref="Context"/>.
        /// </remarks>
        public DataSize AddressSize { get; set; }

        /// <summary>
        /// Gets or sets whether the effective address is RIP-relative.
        /// </summary>
        /// <value><see langword="true"/> to encode a RIP-relative address; <see langword="false"/> to encode an
        /// absolute address; otherwise, <see langword="null"/> to use the <see cref="Context"/>'s default.
        /// The default is <see langword="null"/>.</value>
        /// <remarks>
        /// <para>The value of this property may only be <see langword="true"/> in 64-bit addressing mode.</para>
        /// <para>When this property's value is <see langword="null"/> and the default is to use RIP-relative
        /// addressing, it is not guaranteed that the effective address is encoded using RIP-relative addressing.
        /// There are some effective addresses for which RIP-relative addressing is not possible.</para>
        /// </remarks>
        public bool? RelativeAddress { get; set; }

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        public override void Construct(Context context, EncodedInstruction instruction)
        {
            DataSize addressSize = GetAddressSize(context);

            instruction.SetOperandSize(context.AddressingMode, Size);

            if (context.AddressingMode != DataSize.Bit64 && Size == DataSize.Bit64)
                throw new AssemblerException("A 64-bit operand cannot be used with non-64-bit operand sizes.");

            if (context.AddressingMode != DataSize.Bit64 && addressSize == DataSize.Bit64)
                throw new AssemblerException("A 64-bit effective address cannot be used with non-64-bit address sizes.");

            EncodeDisplacement(context, instruction, addressSize);

            switch(addressSize)
            {
                case DataSize.Bit16:
                    Encode16BitEffectiveAddress(instruction);
                    break;
                case DataSize.Bit32:
                    Encode32BitEffectiveAddress(instruction);
                    break;
                case DataSize.Bit64:
                    Encode64BitEffectiveAddress(context, instruction);
                    break;
                default:
                    throw new NotSupportedException();
            }

            // Address size prefix.
            // When the registers have a width different from the current
            // operating mode width, then we have to add an address size prefix.
            // At this point, we know that the widths are valid.
            instruction.SetAddressSize(context.AddressingMode, addressSize);
        }

        /// <summary>
        /// Determines the address size used by this instruction.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the register width is determined.</param>
        /// <returns>The width of the registers as a <see cref="DataSize"/>.</returns>
        DataSize GetAddressSize(Context context)
        {
            DataSize addressSize = AddressSize;
            DataSize baseWidth = BaseRegister.Size;
            DataSize indexWidth = IndexRegister.Size;
            DataSize contextAddressSize = context.AddressingMode;

            if (addressSize == DataSize.None)
                addressSize = baseWidth;
            if (addressSize == DataSize.None)
                addressSize = indexWidth;
            if (addressSize == DataSize.None)
                addressSize = contextAddressSize;


            if (baseWidth != DataSize.None && baseWidth != addressSize)
                throw new AssemblerException($"BASE register {BaseRegister} has a different width than the determined address size.");

            if (baseWidth != DataSize.None && indexWidth != DataSize.None && baseWidth != indexWidth)
                throw new AssemblerException($"BASE register {BaseRegister} and INDEX register {IndexRegister} have different widths.");

            if (!IsValidRegisterWidthForMode(baseWidth, contextAddressSize))
                throw new AssemblerException($"The {BaseRegister} BASE register is not valid for the {(int)contextAddressSize << 3}-bit address size.");

            if (!IsValidRegisterWidthForMode(indexWidth, contextAddressSize))
                throw new AssemblerException($"The {IndexRegister} INDEX register is not valid for the {(int)contextAddressSize << 3}-bit address size.");

            if (indexWidth != DataSize.None && indexWidth != addressSize)
                throw new AssemblerException($"INDEX register {IndexRegister} has a different width than the determined address size.");

            return addressSize;
        }

        /// <summary>
        /// Encodes the displacement in the <see cref="EncodedInstruction"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instr">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        /// <param name="addressSize">The address size used by the effective address.</param>
        void EncodeDisplacement(Context context, EncodedInstruction instr, DataSize addressSize)
        {
            DataSize displacementSize = DataSize.None;
            ReferenceOffset displacementExpression = null;

            if (Displacement != null)
            {
                // Let's evaluate the displacement expression.
                displacementExpression = Displacement.Compile()(context);

                // Determine the size of the displacement.
                displacementSize = addressSize;
                if (displacementSize == DataSize.None)
                {
                    // Does the result have a (resolved or not resolved) reference?
                    if (displacementExpression.Reference != null && !displacementExpression.Reference.Resolved)
                        // When the result has a reference, use the architecture's address size.
                        displacementSize = context.AddressingMode;
                    else
                        // Otherwise, use the most efficient word size.
                        displacementSize = Extensions.GetSizeOfValue(displacementExpression.Evaluate(context));
                }
            }

            instr.DisplacementSize = displacementSize;
            instr.Displacement = displacementExpression;
        }

        /// <summary>
        /// Returns whether the effective address register's width is valid for the given mode.
        /// </summary>
        /// <param name="registerWidth">The register's <see cref="DataSize"/>.</param>
        /// <param name="mode">The <see cref="DataSize"/> of the mode.</param>
        /// <returns><see langword="true"/> when the register may be used in an effective address in the specified
        /// mode; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// When <paramref name="registerWidth"/> equals <see cref="DataSize.None"/>, this method returns
        /// <see langword="true"/>. A register with no size is valid for any mode.
        /// </remarks>
        static bool IsValidRegisterWidthForMode(DataSize registerWidth, DataSize mode)
        {
            switch (mode)
            {
                case DataSize.Bit16:
                case DataSize.Bit32:
                    return registerWidth == DataSize.Bit16
                        || registerWidth == DataSize.Bit32
                        || registerWidth == DataSize.None;
                case DataSize.Bit64:
                    return registerWidth == DataSize.Bit32
                        || registerWidth == DataSize.Bit64
                        || registerWidth == DataSize.None;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="OperandDescriptor"/> matches this
        /// <see cref="Operand"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> to match.</param>
        /// <returns><see langword="true"/> when the specified descriptor matches this operand;
        /// otherwise, <see langword="false"/>.</returns>
        public override bool IsMatch(OperandDescriptor descriptor)
        {
            switch (descriptor.OperandType)
            {
                case OperandType.RegisterOrMemoryOperand:
                    return Size == descriptor.RegisterType.GetSize();
                case OperandType.MemoryOperand:
                    return Size == descriptor.Size;
                default:
                    return false;
            }
        }
    }
}
