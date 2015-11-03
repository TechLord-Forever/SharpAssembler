using System;
using SharpAssembler.Symbols;

namespace SharpAssembler
{
    /// <summary>
    /// A simple expression of (reference + offset).
    /// </summary>
    public class ReferenceOffset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceOffset"/> class
        /// with the specified (reference + offset) expression.
        /// </summary>
        /// <param name="reference">The reference whose address is used.</param>
        /// <param name="constant">A constant value added to the reference address.</param>
        public ReferenceOffset(Reference reference, long constant)
        {
            Reference = reference;
            Constant = constant;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceOffset"/> class
        /// with the specified value.
        /// </summary>
        /// <param name="constant">A constant value.</param>
        public ReferenceOffset(long constant)
            : this(null, constant)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceOffset"/> class
        /// with the specified reference address.
        /// </summary>
        /// <param name="reference">The reference whose address is used.</param>
        public ReferenceOffset(Reference reference)
            : this(reference, 0)
        {
        }

        /// <summary>
        /// Gets the <see cref="Reference"/> to a symbol whose address is used as the value for the expression.
        /// </summary>
        /// <value>A <see cref="Reference"/>; or <see langword="null"/> when no reference is specified.</value>
        public Reference Reference { get; private set; }

        /// <summary>
        /// Gets the constant added to reference address value (or 0 when no reference is provided).
        /// </summary>
        /// <value>An <see cref="long"/>.</value>
        public long Constant { get; private set; }

        /// <summary>
        /// Resolves any unresolved symbol references and returns the actual value of the expression.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the unresolved symbol references are
        /// resolved.</param>
        /// <returns>The result of the expression.</returns>
        /// <exception cref="InvalidOperationException">
        /// The symbol referenced could not be resolved.
        /// </exception>
        public long Evaluate(Context context)
        {
            // Attempts to resolve any unresolved symbol references.
            if (Reference != null && !Reference.Resolve(context))
                throw new InvalidOperationException("The expression still contains an unresolved symbol reference.");

            long value = Constant;
            if (Reference != null)
                value += Reference.Symbol.Value;

            return value;
        }

        /// <summary>
        /// Converts the specified <see cref="string"/> to a symbol reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(string reference)
        {
            return new ReferenceOffset(new Reference(reference));
        }

        /// <summary>
        /// Converts the specified <see cref="Reference"/> to a simple expression.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(Reference reference)
        {
            return new ReferenceOffset(reference);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(sbyte value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(byte value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(short value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(ushort value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(int value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(uint value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(long value)
        {
            return new ReferenceOffset(value);
        }

        /// <summary>
        /// Converts the specified constant to a simple expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="ReferenceOffset"/>.</returns>
        public static implicit operator ReferenceOffset(ulong value)
        {
            return new ReferenceOffset((long)value);
        }

        /// <summary>
        /// Takes two reference offsets and adds the left one to the right one.
        /// </summary>
        /// <param name="left">The left reference offset to use.</param>
        /// <param name="right">The right reference offset to use.</param>
        /// <returns>The resulting reference offset.</returns>
        public static ReferenceOffset operator +(ReferenceOffset left, ReferenceOffset right)
        {
            if (left.Reference != null && right.Reference != null)
                throw new Exception("Cannot add two symbol-relative expressions.");

            else if (left.Reference != null)
                return new ReferenceOffset(left.Reference, left.Constant + right.Constant);
            else
                return new ReferenceOffset(right.Reference, left.Constant + right.Constant);
        }

        /// <summary>
        /// Takes two reference-offsets and subtracts the right one from the left one.
        /// </summary>
        /// <param name="left">The left reference-offset to use.</param>
        /// <param name="right">The right reference-offset to use.</param>
        /// <returns>The resulting reference-offset.</returns>
        public static ReferenceOffset operator -(ReferenceOffset left, ReferenceOffset right)
        {
            if (left.Reference != null && !left.Reference.Resolved)
                throw new Exception("The left-hand reference-offset is not resolved.");
            if (right.Reference != null && !right.Reference.Resolved)
                throw new Exception("The right-hand reference-offset is not resolved.");

            if (left.Reference != null && right.Reference != null)
            {
                var value = left.Reference.Symbol.Value - right.Reference.Symbol.Value + left.Constant - right.Constant;
                return new ReferenceOffset(null, value);
            }
            throw new NotImplementedException();
        }
    }
}
