﻿using System;
using SharpAssembler.Symbols;
using System.Linq.Expressions;

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An immediate value.
    /// </summary>
    public class Immediate : Operand
    {
        /// <summary>
        /// Whether this <see cref="Immediate"/> is encoded as the 'extra' immediate value.
        /// </summary>
        bool asExtraImmediate = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="constant">A constant.</param>
        public Immediate(long constant)
            : this(constant, DataSize.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="constant">A constant.</param>
        /// <param name="size">The size of the resulting value.</param>
        public Immediate(long constant, DataSize size)
            : this(c => new ReferenceOffset(constant), size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="reference">A reference.</param>
        public Immediate(Reference reference)
            : this(reference, DataSize.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="reference">A reference.</param>
        /// <param name="size">The size of the resulting value.</param>
        public Immediate(Reference reference, DataSize size)
            : this(c => new ReferenceOffset(reference), size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public Immediate(Expression<Func<Context, ReferenceOffset>> expression)
            : this(expression, DataSize.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Immediate"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="size">The size of the resulting value.</param>
        public Immediate(Expression<Func<Context, ReferenceOffset>> expression, DataSize size)
            : base(size)
        {
            Expression = expression;
        }

        /// <summary>
        /// Gets or sets the expression evaluating to the immediate value.
        /// </summary>
        /// <value>A function taking a <see cref="Context"/> and returning a <see cref="ReferenceOffset"/>.</value>
        public Expression<Func<Context, ReferenceOffset>> Expression { get; set; }

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        public override void Construct(Context context, EncodedInstruction instruction)
        {
            // Let's evaluate the expression.
            var result = Expression?.Compile()(context);

            // Determine the size of the immediate operand.
            DataSize size = PreferredSize;
            if (size == DataSize.None)
            {
                // Does the result have a (resolved or not resolved) reference?
                if (result.Reference != null)
                    // When the result has a reference, use the architecture's operand size.
                    size = context.AddressingMode;
                else
                    // Otherwise, use the most efficient word size.
                    size = Extensions.GetSizeOfValue(result.Constant);
            }
            if (size > DataSize.Bit64)
                throw new AssemblerException("Operands with more than 64 bits cannot be encoded.");
            else if (size == DataSize.None)
                throw new AssemblerException("The operand size is not specified.");

            // Set the parameters.
            if (!asExtraImmediate)
            {
                instruction.Immediate = result;
                instruction.ImmediateSize = size;
            }
            else
            {
                instruction.ExtraImmediate = result;
                instruction.ExtraImmediateSize = size;
            }
            instruction.SetOperandSize(context.AddressingMode, size);
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
                case OperandType.Immediate:
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
            asExtraImmediate = (descriptor.OperandEncoding == OperandEncoding.ExtraImmediate);
            PreferredSize = descriptor.Size;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            // TODO: Implement.
            return base.ToString();
        }

        /// <summary>
        /// Converts a 64-bit signed integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(long value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts a 64-bit unsigned integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(ulong value)
        {
            return new Immediate((long)value);
        }

        /// <summary>
        /// Converts a 32-bit signed integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(int value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts a 32-bit unsigned integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(uint value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts a 16-bit signed integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(short value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts a 16-bit unsigned integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(ushort value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts an 8-bit signed integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(sbyte value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts an 8-bit unsigned integer to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(byte value)
        {
            return new Immediate(value);
        }

        /// <summary>
        /// Converts a reference to an <see cref="Immediate"/> value.
        /// </summary>
        /// <param name="reference">The reference to convert.</param>
        /// <returns>The resulting <see cref="Immediate"/>.</returns>
        public static implicit operator Immediate(Reference reference)
        {
            return new Immediate(reference);
        }
    }
}
