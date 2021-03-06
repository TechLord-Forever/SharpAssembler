﻿//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using SharpAssembler.Architectures.X86.Opcodes;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Opcodes
{
    /// <summary>
    /// The IN (Input from Port) instruction opcode.
    /// </summary>
    public class InOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InOpcode"/> class.
        /// </summary>
        public InOpcode()
            : base("in", GetOpcodeVariants())
        {
        }
        #endregion

        /// <summary>
        /// Returns the opcode variants of this opcode.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="X86OpcodeVariant"/> objects.</returns>
        private static IEnumerable<X86OpcodeVariant> GetOpcodeVariants()
        {
            return new X86OpcodeVariant[]{
                // IN AL, imm8
                new X86OpcodeVariant(
                    new byte[] { 0xE4 },
                    new OperandDescriptor(Register.AL),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // IN AX, imm8
                new X86OpcodeVariant(
                    new byte[] { 0xE5 },
                    new OperandDescriptor(Register.AX),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // IN EAX, imm8
                new X86OpcodeVariant(
                    new byte[] { 0xE5 },
                    new OperandDescriptor(Register.EAX),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // IN AL, DX
                new X86OpcodeVariant(
                    new byte[] { 0xEC },
                    new OperandDescriptor(Register.AL),
                    new OperandDescriptor(Register.DX)),
                // IN AX, DX
                new X86OpcodeVariant(
                    new byte[] { 0xED },
                    new OperandDescriptor(Register.AX),
                    new OperandDescriptor(Register.DX)),
                // IN EAX, DX
                new X86OpcodeVariant(
                    new byte[] { 0xED },
                    new OperandDescriptor(Register.EAX),
                    new OperandDescriptor(Register.DX)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new IN (Input from Port) instruction.
        /// </summary>
        /// <param name="dest">A register.</param>
        /// <param name="port">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction In(Register dest, byte port)
        {
            return X86Opcode.In.CreateInstruction(new RegisterOperand(dest), new Immediate(port, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new IN (Input from Port) instruction.
        /// </summary>
        /// <param name="dest">A register.</param>
        /// <param name="port">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        [CLSCompliant(false)]
        public static X86Instruction In(Register dest, sbyte port)
        {
            return X86Opcode.In.CreateInstruction(new RegisterOperand(dest), new Immediate(port, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new IN (Input from Port) instruction.
        /// </summary>
        /// <param name="dest">A register.</param>
        /// <param name="port">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction In(Register dest, Register port)
        {
            return X86Opcode.In.CreateInstruction(new RegisterOperand(dest), new RegisterOperand(port));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The IN (Input from Port) instruction opcode.
        /// </summary>
        public static readonly X86Opcode In = new InOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
