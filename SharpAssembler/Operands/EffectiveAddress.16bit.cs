namespace SharpAssembler.Architectures.X86.Operands
{
    partial class EffectiveAddress
    {
        /// <summary>
        /// Encodes a 16-bit effective address.
        /// </summary>
        /// <param name="instr">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        void Encode16BitEffectiveAddress(EncodedInstruction instr)
        {
            instr.SetModRMByte();

            // We order the registers in such way that reg1 has the register with the highest number,
            // and reg2 has the register with the lowest number. When a register is not provided, it is put in reg2.
            // This simplifies the following tests, for which the order does not matter.
            var baseReg  = BaseRegister;
            var indexReg = (Scale == 1 ? IndexRegister : Register.None);
            var reg1     = (baseReg.Value >= indexReg.Value ? baseReg : indexReg);
            var reg2     = (baseReg.Value < indexReg.Value ? baseReg : indexReg);

            if (Scale != 1 && Scale != 0)
                throw new AssemblerException("The specified scaling factor is not supported in a 16-bit effective address.");

            // Two cases together deviate from the standard MOD encoding.
            if (reg1 == Register.BP && reg2.IsNone)
            {
                // [BP+...]
                instr.ModRM.RM = 0x06;
                instr.ModRM.Mod = (byte)(instr.DisplacementSize == DataSize.Bit8 ? 0x01 : 0x02);
            }
            else if (reg1.IsNone && reg2.IsNone)
            {
                // [...]
                instr.ModRM.RM = 0x06;
                instr.ModRM.Mod = 0x00;
            }
            else
            {
                // The other cases are straight forward.
                if (reg1 == Register.DI && reg2 == Register.BP)
                    // [BP+DI+...]
                    instr.ModRM.RM = 0x03;
                else if (reg1 == Register.DI && reg2 == Register.BX)
                    // [BX+DI+...]
                    instr.ModRM.RM = 0x01;
                else if (reg1 == Register.DI && reg2.IsNone)
                    // [DI+...]
                    instr.ModRM.RM = 0x05;
                else if (reg1 == Register.SI && reg2 == Register.BP)
                    // [BP+SI+...]
                    instr.ModRM.RM = 0x02;
                else if (reg1 == Register.SI && reg2 == Register.BX)
                    // [BX+SI+...]
                    instr.ModRM.RM = 0x00;
                else if (reg1 == Register.SI && reg2.IsNone)
                    // [SI+...]
                    instr.ModRM.RM = 0x04;
                else if (reg1 == Register.BX && reg2.IsNone)
                    // [BX+...]
                    instr.ModRM.RM = 0x06;
                else
                    throw new AssemblerException("The effective address cannot be encoded");

                switch (instr.DisplacementSize)
                {
                    case DataSize.None:
                        instr.ModRM.Mod = 0x00;
                        break;
                    case DataSize.Bit8:
                        instr.ModRM.Mod = 0x01;
                        break;
                    default:
                        // The default is 16-bit, so larger values get truncated.
                        instr.ModRM.Mod = 0x02;
                        break;
                }
            }
        }
    }
}
