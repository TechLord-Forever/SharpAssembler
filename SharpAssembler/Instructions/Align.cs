using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Emits padding bytes up to a specified boundary.
    /// </summary>
    public class Align : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Align"/> class.
        /// </summary>
        /// <param name="boundary">The boundary to align to. Must be a power of two.</param>
        public Align(int boundary)
            : this(boundary, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Align"/> class.
        /// </summary>
        /// <param name="boundary">The boundary to align to. Must be a power of two.</param>
        /// <param name="paddingbyte">The padding byte value used.</param>
        public Align(int boundary, byte paddingbyte)
        {
            Boundary = boundary;
            PaddingByte = paddingbyte;
        }

        /// <summary>
        /// Gets or sets the boundary to which is aligned.
        /// </summary>
        /// <value>The boundary to align to, which must be a power of two.</value>
        public int Boundary { get; private set; }

        /// <summary>
        /// Gets or sets the byte value used to pad.
        /// </summary>
        /// <value>A byte value. The default is 0x00.</value>
        public byte PaddingByte { get; private set; }

        /// <summary>
        /// Generates an array of bytes which represent the padding bytes used for the align instruction.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the padding will be generated.</param>
        /// <param name="length">The length of the padding, in bytes.</param>
        /// <returns>A byte array which has a length of <paramref name="length"/>.</returns>
        /// <remarks>
        /// The default operation is to generate a sequence of bytes with the value <see cref="PaddingByte"/>.
        /// Architectures may provide their own implementation of this method, which may generate more appropriate
        /// padding sequences (depending on the processor used and other factors).
        /// </remarks>
        protected virtual byte[] GeneratePadding(Context context, int length)
        {
            byte[] paddingbytes = new byte[length];
            // Because an empty array is automatically initialized with 0x00 bytes,
            // we only need to extend the array with padding bytes for other values.
            if (PaddingByte != 0x00)
            {
                for (int i = 0; i < paddingbytes.Length; i++)
                    paddingbytes[i] = PaddingByte;
            }
            return paddingbytes;
        }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            int padding = (int)Extensions.CalculatePadding(context.Address, Boundary);
            return new IEmittable[]{ new RawEmittable(GeneratePadding(context, padding)) };
        }
    }
}
