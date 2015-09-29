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
                throw new ArgumentOutOfRangeException("rm",  "Only the first 4 bits may be set.");
            if (reg > 0x0F)
                throw new ArgumentOutOfRangeException("reg", "Only the first 4 bits may be set.");
            if (mod > 0x03)
                throw new ArgumentOutOfRangeException("mod", "Only the first 2 bits may be set.");

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
