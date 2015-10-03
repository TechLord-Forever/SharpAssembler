#region Copyright and License
/*
 * SharpAssembler
 * Library for .NET that assembles a predetermined list of
 * instructions into machine code.
 *
 * Copyright (C) 2011-2012 Daniël Pelsmaeker
 *
 * This file is part of SharpAssembler.
 *
 * SharpAssembler is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * SharpAssembler is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with SharpAssembler.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
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
        [CLSCompliant(false)]
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
        [CLSCompliant(false)]
        public static ulong Align(ulong value, int boundary)
        {
            return ((ulong)boundary + ((value - 1) & ~((ulong)boundary - 1)));
        }

        /// <summary>
        /// Aligns the value to the next specified boundary.
        /// </summary>
        /// <param name="value">The value to align.</param>
        /// <param name="boundary">The boundary, which is a power of two.</param>
        /// <returns>The aligned value.</returns>
        public static long Align(long value, int boundary)
        {
            return (boundary + ((value - 1) & ~(boundary - 1)));
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

        /// <summary>
        /// Writes a value to the <see cref="BinaryWriter"/> as a value with the specified size.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="size">The size of the value to write.</param>
        /// <returns>The number of written bytes.</returns>
        public static int Write(this BinaryWriter writer, long value, DataSize size)
        {
            switch (size)
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

            return (int)size;
        }
    }
}
