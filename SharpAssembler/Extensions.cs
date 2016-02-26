using System;
using System.IO;

namespace SharpAssembler
{
    /// <summary>
    /// Extra math functions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Calculates the padding from the specified value to the next boundary.
        /// </summary>
        /// <param name="value">The value from which to calculate the padding.</param>
        /// <param name="boundary">The boundary, which is a power of two.</param>
        /// <returns>The padding from the value to the next boundary.</returns>
        public static ulong CalculatePadding(ulong value, int boundary)
        {
            return Align(value, boundary) - value;
        }

        /// <summary>
        /// Aligns the value to the next specified boundary.
        /// </summary>
        /// <param name="value">The value to align.</param>
        /// <param name="boundary">The boundary, which is a power of two.</param>
        /// <returns>The aligned value.</returns>
        public static ulong Align(ulong value, int boundary)
        {
            return ((ulong)boundary + ((value - 1) & ~((ulong)boundary - 1)));
        }

        /// <summary>
        /// Determines the minimum width that can fit the specified (signed or unsigned) value.
        /// </summary>
        /// <param name="value">The value to fit.</param>
        /// <param name="signed">Whether to fit the value signed or unsigned.</param>
        /// <returns>A member of the <see cref="DataSize"/> enumeration.</returns>
        public static DataSize GetSizeOfValue(long value, bool signed = true)
        {
            if (signed && value < 0)
                value = -value;

            if ((value & 0x7FFFFFFF00000000) != 0) return DataSize.Bit64;
            if ((value & 0x00000000FFFF0000) != 0) return DataSize.Bit32;
            if ((value & 0x000000000000FF00) != 0) return DataSize.Bit16;
            return DataSize.Bit8;
        }
    }
}
