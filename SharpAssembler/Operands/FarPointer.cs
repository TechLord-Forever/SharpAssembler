using System;
using System.Globalization;
using System.Linq.Expressions;

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// A relative offset.
    /// </summary>
    /// <remarks>
    /// In the Intel manuals, a far pointer is denoted as <c>ptr16:16</c>, <c>ptr16:32</c> or <c>ptr16:64</c>.
    /// In the AMD manuals, a far pointer is denoted as <c>pntr16:16</c> and <c>pntr16:32</c>.
    /// </remarks>
    public class FarPointer : Operand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarPointer"/> class.
        /// </summary>
        /// <param name="selector">The 16-bit selector expression.</param>
        /// <param name="offset">The offset expression.</param>
        /// <param name="size">The size of the offset; or <see cref="DataSize.None"/> to specify no size.</param>
        public FarPointer(Expression<Func<Context, ReferenceOffset>> selector, Expression<Func<Context, ReferenceOffset>> offset, DataSize size)
            : base(size)
        {
            Selector = selector;
            Offset = offset;
            PreferredSize = size;
        }

        /// <summary>
        /// Gets or sets the expression evaluating to the 16-bit selector.
        /// </summary>
        /// <value>A function taking a <see cref="Context"/> and returning a <see cref="ReferenceOffset"/>.</value>
        public Expression<Func<Context, ReferenceOffset>> Selector { get; set; }

        /// <summary>
        /// Gets or sets the expression evaluating to the offset.
        /// </summary>
        /// <value>A function taking a <see cref="Context"/> and returning a <see cref="ReferenceOffset"/>.</value>
        public Expression<Func<Context, ReferenceOffset>> Offset { get; set; }

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        internal override void Construct(Context context, EncodedInstruction instruction)
        {
            ReferenceOffset offsetResult = Offset.Compile()(context);
            ReferenceOffset selectorResult = Selector?.Compile()(context);

            // Determine the size of the immediate operand.
            DataSize size = PreferredSize;
            if (size == DataSize.None)
            {
                // Does the result have a (resolved or not resolved) reference?
                if (offsetResult.Reference != null)
                    // When the result has a reference, use the architecture's operand size.
                    size = context.AddressingMode;
                else
                    // Otherwise, use the most efficient word size.
                    size = Extensions.GetSizeOfValue(offsetResult.Constant);
            }
            if (size <= DataSize.Bit8)
                size = DataSize.Bit16;

            if (size > DataSize.Bit64)
                throw new AssemblerException("The operand cannot be encoded.");
            else if (size == DataSize.None)
                throw new AssemblerException("The operand size is not specified.");



            // Set the parameters.
            instruction.Immediate = offsetResult;
            instruction.ImmediateSize = size;
            instruction.ExtraImmediate = selectorResult;
            instruction.ExtraImmediateSize = (DataSize)2;
            instruction.SetOperandSize(context.AddressingMode, size);
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
                case OperandType.FarPointer:
                    return Size == descriptor.Size;
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
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", Selector, Offset);
        }
    }
}
