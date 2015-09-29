using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// A description for an instruction.
    /// </summary>
    public abstract partial class X86Opcode : IOpcode
    {
        private readonly string mnemonic;
        /// <inheritdoc />
        public virtual string Mnemonic
        {
            get { return mnemonic; }
        }

        /// <summary>
        /// Gets a read-only ordered collection of opcode variants.
        /// </summary>
        /// <value>A collection of <see cref="X86OpcodeVariant"/> objects.</value>
        public ReadOnlyCollection<X86OpcodeVariant> Variants { get; private set; }

        /// <summary>
        /// Gets whether the instruction can lock so that the read-modify-write
        /// operation is executed atomically.
        /// </summary>
        /// <value><see langword="true"/> when the instruction can lock;
        /// otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// The default implementation returns <see langword="false"/>.
        /// </remarks>
        public virtual bool CanLock
        {
            get { return false; }
        }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="X86Opcode"/> class.
        /// </summary>
        /// <param name="mnemonic">The mnemonic of the opcode.</param>
        /// <param name="variants">The opcode variants.</param>
        protected X86Opcode(string mnemonic, IEnumerable<X86OpcodeVariant> variants)
        {
            Contract.Requires<ArgumentNullException>(mnemonic != null);
            Contract.Requires<ArgumentNullException>(variants != null);
            this.mnemonic = mnemonic;
            Variants = variants.ToList().AsReadOnly();
        }
        #endregion

        /// <summary>
        /// Creates a new instruction for this opcode.
        /// </summary>
        /// <param name="operands">The operands of the instruction.</param>
        /// <returns>The created instruction.</returns>
        public X86Instruction CreateInstruction(params Operand[] operands)
        {
            Contract.Requires<ArgumentNullException>(operands != null);
            return CreateInstruction((IList<Operand>)operands);
        }

        /// <summary>
        /// Creates a new instruction for this opcode.
        /// </summary>
        /// <param name="operands">The operands of the instruction.</param>
        /// <returns>The created instruction.</returns>
        public virtual X86Instruction CreateInstruction(IList<Operand> operands)
        {
            Contract.Requires<ArgumentNullException>(operands != null);
            X86Instruction instr;
            if (CanLock)
                instr = new LockInstruction(this, operands);
            else
                instr = new X86Instruction(this, operands);

            return instr;
        }
    }
}
