﻿using System;

namespace SharpAssembler.Architectures.X86.Operands
{
    partial class EffectiveAddress
    {
        /// <summary>
        /// Encodes a 64-bit effective address.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instr">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        void Encode64BitEffectiveAddress(Context context, EncodedInstruction instr)
        {
            instr.SetModRMByte();

            bool ripRelative = RelativeAddress ?? context.UseRIPRelativeAddressing;
            bool forceRipRelative = RelativeAddress.HasValue && RelativeAddress == true;

            if (BaseRegister.IsNone && IndexRegister.IsNone)
            {
                if (ripRelative)
                {
                    // [RIP+disp32]

                    instr.ModRM.RM = 0x05;
                    instr.ModRM.Mod = 0x00;
                }
                else
                {
                    // [disp32]

                    instr.ModRM.RM = 0x04;
                    instr.ModRM.Mod = 0x00;

                    instr.SetSIBByte();
                    instr.Sib.Base = 0x05;
                    instr.Sib.Index = 0x04;
                    instr.Sib.Scale = 0x00;
                }

                // Only 32-bit displacements can be encoded without a base and index register.
                instr.DisplacementSize = DataSize.Bit32;
                if (instr.Displacement == null)
                    instr.Displacement = new ReferenceOffset(0);
            }
            else
            {
                if (forceRipRelative)
                    throw new AssemblerException("The effective address cannot be encoded with RIP-relative addressing.");

                if (BaseRegister != Register.RSP && IndexRegister.IsNone)
                {
                    // [REG+...]

                    instr.ModRM.RM = BaseRegister.Value;
                }
                else
                {
                    // [REG+REG*s+...]

                    // Encode the SIB byte too.
                    instr.SetSIBByte();

                    // R/M
                    instr.ModRM.RM = 0x04;

                    // Base
                    if (!BaseRegister.IsNone)
                        instr.Sib.Base = BaseRegister.Value;
                    else
                        instr.Sib.Base = 0x05;

                    // Index
                    if (!IndexRegister.IsNone)
                        instr.Sib.Index = IndexRegister.Value;
                    else
                        instr.Sib.Index = 0x20;

                    // Scale
                    instr.Sib.Scale = (byte)((int)Math.Log(Scale, 2));
                }

                if (instr.Displacement == null && BaseRegister == Register.RBP)
                {
                    // [RBP] will be represented as [RBP+disp8].
                    // [RBP+REG*s] will be represented as [RBP+REG*s+disp8].
                    instr.DisplacementSize = DataSize.Bit8;
                    instr.Displacement = new ReferenceOffset(0);
                }

                switch (instr.DisplacementSize)
                {
                    case DataSize.None:
                        instr.ModRM.Mod = 0x00;
                        break;
                    case DataSize.Bit8:
                        instr.ModRM.Mod = 0x01;
                        break;
                    case DataSize.Bit16:
                    case DataSize.Bit32:
                        instr.ModRM.Mod = 0x02;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
