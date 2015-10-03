namespace SharpAssembler.Architectures.X86.Operands
{
    partial class RegisterOperand
    {
        /// <summary>
        /// Specifies how the <see cref="RegisterOperand"/> gets encoded.
        /// </summary>
        internal enum OperandEncoding
        {
            /// <summary>
            /// The default encoding of the register.
            /// </summary>
            /// <remarks>
            /// This is used when the operand is part of a 'reg' operand, but encoded
            /// in the ModR/M byte.
            /// </remarks>
            Default,
            /// <summary>
            /// Add the register value to the opcode.
            /// </summary>
            /// <remarks>
            /// This is used when the operand is part of a 'reg' operand, but encoded
            /// in the last opcode byte.
            /// </remarks>
            AddToOpcode,
            /// <summary>
            /// Reg/mem encoding.
            /// </summary>
            /// <remarks>
            /// This is used when the operand is part of a 'reg/mem' operand, encoded
            /// in the ModR/M byte.
            /// </remarks>
            ModRm,
            /// <summary>
            /// The operand is not encoded.
            /// </summary>
            /// <remarks>
            /// This is used for operands which are implicitly part of the instruction.
            /// </remarks>
            Ignore,
        }
    }
}
