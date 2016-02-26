using System;
using System.IO;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// An emittable which emits an expression result.
    /// </summary>
    public class ExpressionEmittable : IEmittable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionEmittable"/> class.
        /// </summary>
        /// <param name="expression">The expression of the value.</param>
        /// <param name="size">The size of the result.</param>
        public ExpressionEmittable(ReferenceOffset expression, DataSize size)
        {
            Expression = expression;
            Size = size;
        }

        /// <summary>
        /// Gets or sets the expression result to emit.
        /// </summary>
        /// <value>A <see cref="ReferenceOffset"/>.</value>
        public ReferenceOffset Expression { get; set; }

        /// <summary>
        /// Gets or sets the size of the emitted data.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.</value>
        public DataSize Size { get; set; }

        /// <summary>
        /// Modifies the context and emits the binary representation of this emittable.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        /// <param name="context">The <see cref="Context"/> in which the emittable will be emitted.</param>
        /// <returns>The number of emitted bytes.</returns>
        public int Emit(BinaryWriter writer, Context context)
        {
            var value = Expression.Evaluate(context);
            switch (Size)
            {
                case DataSize.Bit8:
                    writer.Write((byte)value);
                    break;
                case DataSize.Bit16:
                    writer.Write((ushort)value);
                    break;
                case DataSize.Bit32:
                    writer.Write((uint)value);
                    break;
                case DataSize.Bit64:
                    writer.Write(value);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return (int)Size;
        }

        /// <summary>
        /// Gets the length of the emittable.
        /// </summary>
        /// <returns>The length of the emittable, in bytes.</returns>
        public int Length => (int)Size;
    }
}
