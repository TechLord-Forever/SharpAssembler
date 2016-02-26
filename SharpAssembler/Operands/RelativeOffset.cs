using System;
using System.Linq.Expressions;

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// A relative offset.
    /// </summary>
    /// <remarks>In the Intel manuals, a relative offset is
    /// denoted as rel8, rel16, rel32 or rel64.</remarks>
    public class RelativeOffset : Operand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeOffset"/> class.
        /// </summary>
        /// <param name="value">The expression describing the jump target.</param>
        public RelativeOffset(Expression<Func<Context, ReferenceOffset>> value)
            : this(value, DataSize.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeOffset"/> class.
        /// </summary>
        /// <param name="value">The expression describing the jump target.</param>
        /// <param name="size">The size of the offset; or <see cref="DataSize.None"/> to specify no size.</param>
        public RelativeOffset(Expression<Func<Context, ReferenceOffset>> value, DataSize size)
            : base(size)
        {
            Expression = value;
        }

        /// <summary>
        /// Gets or sets the expression evaluating to the jump target.
        /// </summary>
        /// <value>A function taking a <see cref="Context"/> and returning a <see cref="ReferenceOffset"/>.</value>
        public Expression<Func<Context, ReferenceOffset>> Expression { get; set; }

        DataSize operandSize = DataSize.None;
        /// <summary>
        /// Gets the actual size of the relative offset value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration; or <see cref="DataSize.None"/>.</value>
        public override DataSize Size => operandSize;

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        public override void Construct(Context context, EncodedInstruction instruction)
        {
            // Determine the size of the immediate operand. Otherwise the length is not calculated correctly.
            DataSize size = PreferredSize;
            if (size == DataSize.None)
                size = context.AddressingMode;
            if (size >= DataSize.Bit64)
            {
                var bit = ((int)size) << 3;
                throw new AssemblerException($"{bit}-bit operands cannot be encoded.");
            }

            else if (size == DataSize.None)
                throw new AssemblerException("The operand size is not specified.");

            instruction.SetOperandSize(context.AddressingMode, size);
            instruction.ImmediateSize = size;

            // Let's evaluate the expression.
            var result = Expression?.Compile()(context);
            result = new ReferenceOffset(result.Reference, result.Constant - ((long)context.Address + instruction.Length));
            instruction.Immediate = result;
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
                case OperandType.RelativeOffset:
                    return Size == DataSize.None || Size <= descriptor.Size;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adjusts this <see cref="Operand"/> based on the specified <see cref="OperandDescriptor"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> used to adjust.</param>
        /// <remarks>
        /// Only <see cref="OperandDescriptor"/> instances for which <see cref="IsMatch"/> returns
        /// <see langword="true"/> may be used as a parameter to this method.
        /// </remarks>
        public override void Adjust(OperandDescriptor descriptor)
        {
            // Nothing to do.
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Expression?.ToString();
        }
    }
}
