using System.Collections.Generic;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// An instruction that can have a lock prefix.
    /// </summary>
    public class LockInstruction : X86Instruction, ILockInstruction
    {
        /// <summary>
        /// Gets or sets whether the lock prefix is used.
        /// </summary>
        /// <value><see langword="true"/> to enable the lock prefix; otherwise, <see langword="false"/>.
        /// The default is <see langword="false"/>.</value>
        /// <remarks>
        /// When this property is set to <see langword="true"/>, the lock signal is asserted before accessing the
        /// specified memory location. When the lock signal has already been asserted, the instruction must wait for it
        /// to be released. Instructions without the lock prefix do not check the lock signal, and will be executed
        /// even when the lock signal is asserted by some other instruction.
        /// </remarks>
        public bool Lock { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LockInstruction"/> class.
        /// </summary>
        /// <param name="opcode">The opcode of this instruction.</param>
        /// <param name="operands">The operands to the instruction.</param>
        public LockInstruction(X86Opcode opcode, params Operand[] operands)
            : this(opcode, (IList<Operand>)operands)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LockInstruction"/> class.
        /// </summary>
        /// <param name="opcode">The opcode of this instruction.</param>
        /// <param name="operands">The operands to the instruction.</param>
        public LockInstruction(X86Opcode opcode, IList<Operand> operands)
            : base(opcode, operands)
        {
        }

        /// <inheritdoc />
        protected override bool GetLockPrefix()
        {
            return Lock;
        }
    }
}
