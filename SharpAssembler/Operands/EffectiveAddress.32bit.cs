using System;

namespace SharpAssembler.Architectures.X86.Operands
{
    partial class EffectiveAddress
    {
        /// <summary>
        /// Encodes a 32-bit effective address.
        /// </summary>
        /// <param name="instr">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        void Encode32BitEffectiveAddress(EncodedInstruction instr)
        {
            instr.SetModRMByte();

            if (BaseRegister == Register.None && IndexRegister == Register.None)
            {
                // R/M
                instr.ModRM.RM = 0x05;
                // Mod
                instr.ModRM.Mod = 0x00;

                // Only 32-bit displacements can be encoded without a base and index register.
                instr.DisplacementSize = DataSize.Bit32;
                if (instr.Displacement == null)
                    instr.Displacement = new ReferenceOffset(0);
            }
            else if (BaseRegister != Register.ESP && IndexRegister == Register.None)
            {
                // R/M
//#warning instr.ModRM.RM = (byte)((int)BaseRegister & 0x07);
                instr.ModRM.RM = (byte)(BaseRegister.Full & 0x07);

                // Displacement.
                if (instr.Displacement == null && BaseRegister == Register.EBP)
                {
                    // [EBP] will be represented as [EBP+disp8].
                    instr.DisplacementSize = DataSize.Bit8;
                    instr.Displacement = new ReferenceOffset(0);
                }

                // Mod
                if (instr.DisplacementSize == DataSize.None)
                    instr.ModRM.Mod = 0x00;
                else if (instr.DisplacementSize == DataSize.Bit8)
                    instr.ModRM.Mod = 0x01;
                else if (instr.DisplacementSize <= DataSize.Bit32)
                    instr.ModRM.Mod = 0x02;
            }
            else
            {
                // Encode the SIB byte too.
                instr.SetSIBByte();

                // R/M
                instr.ModRM.RM = 0x04;

                // Displacement
                if (instr.Displacement == null && BaseRegister == Register.EBP)
                {
                    // [EBP+REG*s] will be represented as [EBP+REG*s+disp8].
                    instr.DisplacementSize = DataSize.Bit8;
                    instr.Displacement = new ReferenceOffset(0);
                }

                // Mod
                if (instr.DisplacementSize == DataSize.None)
                    instr.ModRM.Mod = 0x00;
                else if (instr.DisplacementSize == DataSize.Bit8)
                    instr.ModRM.Mod = 0x01;
                else if (instr.DisplacementSize <= DataSize.Bit32)
                    instr.ModRM.Mod = 0x02;

                // Base
//#warning instr.Sib.Base = (byte)((int)BaseRegister & 0x07);
                instr.Sib.Base = (byte)(BaseRegister.Full & 0x07);
                if (BaseRegister == Register.None)
                    instr.Sib.Base = 0x05;

                // Index
//#warning instr.Sib.Index = (byte)((int)IndexRegister & 0x07);
                instr.Sib.Index = (byte)(IndexRegister.Full & 0x07);
                if (IndexRegister == Register.None)
                    instr.Sib.Index = 0x20;

                // Scale
                instr.Sib.Scale = (byte)((int)Math.Log(Scale, 2));
            }
        }
    }
}
