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
            Data = new List<T> { data };
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
            int offset = 0;
            int size = Marshal.SizeOf(typeof(T));
            var array = new byte[size * Data.Count];

            foreach (T value in Data)
            {
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, array, offset, size);
                Marshal.FreeHGlobal(ptr);
                offset += size;
            }

            yield return new RawEmittable(array);
        }
    }
}
