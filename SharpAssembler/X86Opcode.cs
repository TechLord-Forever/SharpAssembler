using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// A description for an instruction.
    /// </summary>
    public abstract partial class X86Opcode
    {
        /// <inheritdoc />
        public string Mnemonic { get; protected set; }

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
        public virtual bool CanLock { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="X86Opcode"/> class.
        /// </summary>
        /// <param name="mnemonic">The mnemonic of the opcode.</param>
        /// <param name="variants">The opcode variants.</param>
        protected X86Opcode(string mnemonic, IEnumerable<X86OpcodeVariant> variants)
        {
            Mnemonic = mnemonic;
            Variants = variants.ToList().AsReadOnly();
        }

        /// <summary>
        /// Creates a new instruction for this opcode.
        /// </summary>
        /// <param name="operands">The operands of the instruction.</param>
        /// <returns>The created instruction.</returns>
        public X86Instruction CreateInstruction(params Operand[] operands)
        {
            return new X86Instruction(this, operands) { Lock = CanLock };
        }
    }
}
