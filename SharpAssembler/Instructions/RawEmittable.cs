using System.IO;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// An emittable which emits raw bytes.
    /// </summary>
    public class RawEmittable : IEmittable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawEmittable"/> class.
        /// </summary>
        /// <param name="content">The content of the emittable.</param>
        public RawEmittable(byte[] content)
        {
            Content = content;
        }

        /// <summary>
        /// Gets or sets the content of the raw emittable.
        /// </summary>
        /// <value>An array of bytes.</value>
        public byte[] Content { get; set; }


        /// <summary>
        /// Modifies the context and emits the binary representation of this emittable.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        /// <param name="context">The <see cref="Context"/> in which the emittable will be emitted.</param>
        /// <returns>The number of emitted bytes.</returns>
        public int Emit(BinaryWriter writer, Context context)
        {
            writer.Write(Content);
            return Content.Length;
        }

        /// <summary>
        /// Gets the length of the emittable.
        /// </summary>
        /// <returns>The length of the emittable, in bytes.</returns>
        public int GetLength()
        {
            return Content.Length;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="object"/>.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
