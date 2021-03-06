﻿using System;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// Represents the SIB byte.
    /// </summary>
    public class SibByte
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SibByte"/> class.
        /// </summary>
        public SibByte()
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SibByte"/> class.
        /// </summary>
        /// <param name="baseReg">The 4-bit BASE part.</param>
        /// <param name="index">The 4-bit INDEX part.</param>
        /// <param name="scale">The 2-bit SCALE part.</param>
        public SibByte(byte baseReg, byte index, byte scale)
        {
            @base = (byte)(baseReg & 0x0F);
            this.index = (byte)(index & 0x0F);
            this.scale = (byte)(scale & 0x03);
        }

        byte @base;
        /// <summary>
        /// Gets or sets the value of the BASE part of the SIB byte.
        /// </summary>
        /// <value>The 4-bit BASE value.</value>
        /// <remarks>
        /// The least significant three bits are encoded in the SIB byte, while the fourth bit is encoded as the
        /// REX.B bit when there is a SIB byte.
        /// </remarks>
        public byte Base
        {
            get { return @base; }
            set
            {
                if (value > 0x0F)
                    throw new ArgumentOutOfRangeException(nameof(value), "Only the first 4 bits may be set.");
                @base = value;
            }
        }

        byte index;
        /// <summary>
        /// Gets or sets the value of the INDEX part of the SIB byte.
        /// </summary>
        /// <value>The 4-bit INDEX value.</value>
        /// <remarks>
        /// The least significant three bits are encoded in the SIB byte, while the fourth bit is encoded in the
        /// REX.X bit.
        /// </remarks>
        public byte Index
        {
            get { return index; }
            set
            {
                if (value > 0x0F)
                    throw new ArgumentOutOfRangeException(nameof(value), "Only the first 4 bits may be set.");
                index = value;
            }
        }

        byte scale;
        /// <summary>
        /// Gets or sets the value of the SCALE part of the SIB byte.
        /// </summary>
        /// <value>The 2-bit SCALE value.</value>
        /// <remarks>
        /// The actual scale is two to the power of this value, so that 0 equals a scale of 1, 1 equals a scale of
        /// 2, 2 equals a scale of 4 and 3 equals a scale of 8.
        /// </remarks>
        public byte Scale
        {
            get { return scale; }
            set
            {
                if (value > 0x03)
                    throw new ArgumentOutOfRangeException(nameof(value), "Only the first 2 bits may be set.");
                scale = value;
            }
        }
    }
}
