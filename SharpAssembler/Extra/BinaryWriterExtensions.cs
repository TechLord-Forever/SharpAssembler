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
using System.Text;

namespace SharpAssembler
{
    /// <summary>
    /// Extension methods to the <see cref="BinaryWriter"/> class.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes a value to the <see cref="BinaryWriter"/> as a value with the specified size.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="size">The size of the value to write.</param>
        /// <returns>The number of written bytes.</returns>
        [CLSCompliant(false)]
        public static int Write(this BinaryWriter writer, ulong value, DataSize size)
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

        #region Write()
        /// <summary>
        /// Writes a value to the <see cref="BinaryWriter"/> as a value with the specified size.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="size">The size of the value to write.</param>
        /// <returns>The number of written bytes.</returns>
        public static int Write(this BinaryWriter writer, long value, DataSize size)
        {
            return Write(writer, (ulong)value, size);
        }

        #endregion
    }
}
