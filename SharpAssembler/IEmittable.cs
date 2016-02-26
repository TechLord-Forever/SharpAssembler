using System.IO;

namespace SharpAssembler
{
    /// <summary>
    /// An interface for a constructed representation of a <see cref="Constructable"/>.
    /// </summary>
    public interface IEmittable
    {
        /// <summary>
        /// Modifies the context and emits the binary representation of this emittable to the specified
        /// <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
        /// <param name="context">The <see cref="Context"/> in which the emittable will be emitted.</param>
        /// <returns>The number of emitted bytes.</returns>
        int Emit(BinaryWriter writer, Context context);

        /// <summary>
        /// Gets the length of the emittable.
        /// </summary>
        /// <returns>The length of the emittable, in bytes.</returns>
        int Length { get; }
    }
}
