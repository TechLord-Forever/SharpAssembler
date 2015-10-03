using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// An arithmetic instruction.
    /// </summary>
    public abstract class ArithmeticInstruction : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate operand.</param>
        protected ArithmeticInstruction(RegisterOperand destination, Immediate source)
            : this((IRegisterOrMemoryOperand)destination, (ISourceOperand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate operand.</param>
        protected ArithmeticInstruction(EffectiveAddress destination, Immediate source)
            : this((IRegisterOrMemoryOperand)destination, (ISourceOperand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source register operand.</param>
        protected ArithmeticInstruction(RegisterOperand destination, RegisterOperand source)
            : this((IRegisterOrMemoryOperand)destination, (ISourceOperand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source register operand.</param>
        protected ArithmeticInstruction(EffectiveAddress destination, RegisterOperand source)
            : this((IRegisterOrMemoryOperand)destination, (ISourceOperand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source register or memory operand.</param>
        protected ArithmeticInstruction(RegisterOperand destination, EffectiveAddress source)
            : this((IRegisterOrMemoryOperand)destination, (ISourceOperand)source)
        {
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticInstruction"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private ArithmeticInstruction(IRegisterOrMemoryOperand destination, ISourceOperand source)
        {
            this.destination = destination;
            this.source = source;
        }
        #endregion

        #region Properties
        private IRegisterOrMemoryOperand destination;
        /// <summary>
        /// Gets the destination operand of the instruction.
        /// </summary>
        /// <value>An <see cref="IRegisterOrMemoryOperand"/>.</value>
        public IRegisterOrMemoryOperand Destination
        {
            get
            {
                return destination;
            }
        }

        private ISourceOperand source;
        /// <summary>
        /// Gets the source operand of the instruction.
        /// </summary>
        /// <value>An <see cref="ISourceOperand"/>.</value>
        public ISourceOperand Source
        {
            get
            {
                return source;
            }
        }

        private bool lockInstruction = false;
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
        public bool Lock
        {
            get { return lockInstruction; }
            set { lockInstruction = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enumerates an ordered list of operands used by this instruction.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Operand"/> objects.</returns>
        public override IEnumerable<Operand> GetOperands()
        {
            // The order is important here!
            yield return (Operand)this.destination;
            yield return (Operand)this.source;
        }
        #endregion
    }
}
