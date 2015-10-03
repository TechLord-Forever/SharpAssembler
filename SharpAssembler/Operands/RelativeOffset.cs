using System;
using System.Globalization;
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

        private DataSize operandSize = DataSize.None;
        /// <summary>
        /// Gets the actual size of the relative offset value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration; or <see cref="DataSize.None"/>.</value>
        public override DataSize Size { get { return operandSize; } }

        #region Methods
        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instr">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        internal override void Construct(Context context, EncodedInstruction instr)
        {
            // Determine the size of the immediate operand. Otherwise the length is not calculated correctly.
            DataSize size = PreferredSize;
            if (size == DataSize.None)
                size = context.Architecture.OperandSize;
            if (size >= DataSize.Bit64)
                throw new AssemblerException(string.Format(CultureInfo.InvariantCulture,
                    "{0}-bit operands cannot be encoded.",
                    ((int)size) << 3));
            else if (size == DataSize.None)
                throw new AssemblerException("The operand size is not specified.");
            instr.SetOperandSize(context.Architecture.OperandSize, size);
            instr.ImmediateSize = size;

            // Let's evaluate the expression.
            ReferenceOffset result = Expression?.Compile()(context);
            result = new ReferenceOffset(result.Reference, result.Constant - ((long)context.Address + instr.GetLength()));
            instr.Immediate = result;

            // FIXME: Remove this:
#if false
            // Determine the size of the immediate operand.
            DataSize size = PreferredSize;
            if (size == DataSize.None)
            {
                // Does the result have a (resolved or not resolved) reference?
                if (result.Reference != null)
                    // When the result has a reference, use the architecture's operand size.
                    size = context.Representation.Architecture.OperandSize;
                else
                    // Otherwise, use the most efficient word size.
                    size = MathExt.GetSizeOfValue(result.Constant);
            }
            if (size >= DataSize.Bit64)
                throw new AssemblerException(String.Format(CultureInfo.InvariantCulture,
                    "{0}-bit operands cannot be encoded.",
                    ((int)size) << 3));
            else if (size == DataSize.None)
                throw new AssemblerException("The operand size is not specified.");
#endif
        }

        /// <summary>
        /// Determines whether the specified <see cref="OperandDescriptor"/> matches this
        /// <see cref="Operand"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> to match.</param>
        /// <returns><see langword="true"/> when the specified descriptor matches this operand;
        /// otherwise, <see langword="false"/>.</returns>
        internal override bool IsMatch(OperandDescriptor descriptor)
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
        internal override void Adjust(OperandDescriptor descriptor)
        {
            // Nothing to do.
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Expression?.ToString();
        }
        #endregion
    }
}
