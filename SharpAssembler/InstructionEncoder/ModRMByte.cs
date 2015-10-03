using System;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// Represents the ModR/M byte.
    /// </summary>
    public class ModRMByte : SubStructure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModRMByte"/> class.
        /// </summary>
        public ModRMByte()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModRMByte"/> class.
        /// </summary>
        /// <param name="rm">The 4-bit R/M part.</param>
        /// <param name="reg">The 4-bit REG part.</param>
        /// <param name="mod">The 2-bit MOD part.</param>
        public ModRMByte(byte rm, byte reg, byte mod)
        {
            if (rm > 0x0F)
                throw new ArgumentOutOfRangeException(nameof(rm),  "Only the first 4 bits may be set.");
            if (reg > 0x0F)
                throw new ArgumentOutOfRangeException(nameof(reg), "Only the first 4 bits may be set.");
            if (mod > 0x03)
                throw new ArgumentOutOfRangeException(nameof(mod), "Only the first 2 bits may be set.");

            this.rm  = rm;
            this.reg = reg;
            this.mod = mod;
        }

        #region Properties
        private byte rm;
        /// <summary>
        /// Gets or sets the value of the R/M part of the ModR/M byte.
        /// </summary>
        /// <value>The 4-bit R/M value.</value>
        /// <remarks>
        /// The least significant three bits are encoded in the ModR/M byte, while the fourth bit is encoded as the
        /// REX.B bit when there is no SIB byte.
        /// </remarks>
        public byte RM
        {
            get    { return rm; }
            set
            {
                if (value > 0x0F)
                    throw new Exception("Only the first 4 bits may be set.");
                rm = value;
            }
        }

        private byte reg;
        /// <summary>
        /// Gets or sets the value of the REG part of the ModR/M byte.
        /// </summary>
        /// <value>The 4-bit REG value.</value>
        /// <remarks>
        /// The least significant three bits are encoded in the ModR/M byte, while the fourth bit is encoded in the
        /// REX.R bit.
        /// </remarks>
        public byte Reg
        {
            get { return reg; }
            set
            {
                if (value > 0x0F)
                    throw new Exception("Only the first 4 bits may be set.");
                reg = value;
            }
        }

        private byte mod;
        /// <summary>
        /// Gets or sets the value of the MOD part of the ModR/M byte.
        /// </summary>
        /// <value>The 2-bit MOD value.</value>
        public byte Mod
        {
            get { return mod; }
            set
            {
                if (value > 0x03)
                    throw new Exception("Only the first 2 bits may be set.");
                mod = value;
            }
        }
        #endregion

        /// <summary>
        /// Returns a byte array representation of this sub structure.
        /// </summary>
        /// <returns>A byte array.</returns>
        public override byte[] ToBytes()
        {
            byte result = 0x00;
            result |= (byte)((rm  & 0x07));
            result |= (byte)((reg & 0x07) << 3);
            result |= (byte)((mod & 0x03) << 6);
            return new[] { result };
        }
    }
}
