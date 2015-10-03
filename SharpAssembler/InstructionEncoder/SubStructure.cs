using System;
using System.Linq;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// A base class for sub structures of the <see cref="EncodedInstruction"/>.
    /// </summary>
    public abstract class SubStructure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubStructure"/> class.
        /// </summary>
        protected SubStructure()
        {
        }

        /// <summary>
        /// Returns a byte array representation of this sub structure.
        /// </summary>
        /// <returns>A byte array.</returns>
        public abstract byte[] ToBytes();

        /// <summary>
        /// Copies the representation of this sub structure to the specified array.
        /// </summary>
        /// <param name="target">The array to copy to.</param>
        /// <param name="index">The index at which copying starts.</param>
        /// <returns>The number of bytes copied.</returns>
        public int CopyTo(byte[] target, int index)
        {
            return CopyTo(target, index, -1);
        }

        /// <summary>
        /// Copies the representation of this sub structure to the specified array.
        /// </summary>
        /// <param name="target">The array to copy to.</param>
        /// <param name="index">The index at which copying starts.</param>
        /// <param name="count">The maximum number of bytes to copy; or -1 to specify no limit.</param>
        /// <returns>The number of bytes copied.</returns>
        public int CopyTo(byte[] target, int index, int count)
        {
            var source = ToBytes();
            int length = source.Length;
            if (count >= 0 && count < length)
                length = count;

            Array.Copy(source, 0, target, index, length);

            return length;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="object"/>.</returns>
        public override string ToString()
        {
            return "{" + string.Join(", ", ToBytes().Select(n => "0x" + n.ToString("X2"))) + "}";
        }
    }
}