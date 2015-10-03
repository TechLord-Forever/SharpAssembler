using System;
using System.Collections.Generic;
using SharpAssembler.Symbols;
using System.Linq.Expressions;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Declares data.
    /// </summary>
    public class DeclareData : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="expression">The expression of the value.</param>
        /// <param name="size">The size of the result.</param>
        public DeclareData(Expression<Func<Context, ReferenceOffset>> expression, DataSize size)
        {
            Expression = expression;
            Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="reference">A symbol reference.</param>
        /// <param name="size">The size of the result.</param>
        public DeclareData(Reference reference, DataSize size)
            : this(c => new ReferenceOffset(reference), size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 8-bit signed integer value.</param>
        [CLSCompliant(false)]
        public DeclareData(sbyte value)
            : this(c => new ReferenceOffset(value), DataSize.Bit8)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 8-bit unsigned integer value.</param>
        public DeclareData(byte value)
            : this(c => new ReferenceOffset(value), DataSize.Bit8)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 16-bit signed integer value.</param>
        public DeclareData(short value)
            : this(c => new ReferenceOffset(value), DataSize.Bit16)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 16-bit unsigned integer value.</param>
        [CLSCompliant(false)]
        public DeclareData(ushort value)
            : this(c => new ReferenceOffset(value), DataSize.Bit16)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 32-bit signed integer value.</param>
        public DeclareData(int value)
            : this(c => new ReferenceOffset(value), DataSize.Bit32)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 32-bit unsigned integer value.</param>
        [CLSCompliant(false)]
        public DeclareData(uint value)
            : this(c => new ReferenceOffset(value), DataSize.Bit32)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 64-bit signed integer value.</param>
        public DeclareData(long value)
            : this(c => new ReferenceOffset(value), DataSize.Bit64)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData"/> class.
        /// </summary>
        /// <param name="value">A 64-bit unsigned integer value.</param>
        [CLSCompliant(false)]
        public DeclareData(ulong value)
            : this(c => new ReferenceOffset((long)value), DataSize.Bit64)
        {
        }

        /// <summary>
        /// Gets or sets the expression that will be declared.
        /// </summary>
        /// <value>A function accepting a <see cref="Context"/> and returning a <see cref="ReferenceOffset"/>.</value>
        public Expression<Func<Context, ReferenceOffset>> Expression { get; private set; }

        /// <summary>
        /// Gets or sets the size of the declared data.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.</value>
        public DataSize Size { get; private set; }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            yield return new ExpressionEmittable(Expression.Compile()(context), Size);
        }
    }
}
