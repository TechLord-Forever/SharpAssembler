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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Declares data.
    /// </summary>
    /// <typeparam name="T">The type of the declared data.</typeparam>
    public class DeclareData<T> : Constructable
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData{T}"/> class.
        /// </summary>
        public DeclareData()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData{T}"/> class.
        /// </summary>
        /// <param name="data">The data to declare.</param>
        public DeclareData(T data)
        {
            Data = new List<T>();
            Data.Add(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData{T}"/> class.
        /// </summary>
        /// <param name="data">The data to declare.</param>
        public DeclareData(params T[] data)
        {
            Data = new List<T>(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareData{T}"/> class.
        /// </summary>
        /// <param name="data">An array of structures representing the data to declare.</param>
        public DeclareData(IEnumerable<T> data)
        {
            Data = new List<T>(data);
        }

        /// <summary>
        /// Gets the data that will be declared.
        /// </summary>
        /// <value>A <see cref="IList{T}"/> of structures.</value>
        public IList<T> Data { get; private set; }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            int totallength = 0;
            foreach (T value in Data)
            {
                totallength += GetSize(value);
            }

            byte[] databytes = new byte[totallength];
            int offset = 0;
            foreach (T value in Data)
            {
                offset += CopyBytes(value, databytes, offset);
            }

            yield return new RawEmittable(databytes);
        }

        /// <summary>
        /// Gets the size of the specified object.
        /// </summary>
        /// <param name="value">The object whose size to determine.</param>
        /// <returns>The size, in bytes.</returns>
        private int GetSize(T value)
        {
            return Marshal.SizeOf(value);
        }

        /// <summary>
        /// Copies the byte representation of the value to the specified array at the specified location.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="array">The array to copy the representation to.</param>
        /// <param name="offset">The offset in <paramref name="array"/> where to start copying.</param>
        /// <returns>The number of bytes copied.</returns>
        private int CopyBytes(T value, byte[] array, int offset)
        {
            int length = GetSize(value);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, array, offset, length);
            Marshal.FreeHGlobal(ptr);
            return length;
        }
    }
}
