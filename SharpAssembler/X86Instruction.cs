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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharpAssembler.Architectures.X86.Operands;
using System.Collections.ObjectModel;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// An x86-64 instruction.
    /// </summary>
    public partial class X86Instruction : Constructable
    {
        /// <summary>
        /// Gets the opcode of the instruction.
        /// </summary>
        /// <value>The <see cref="X86Opcode"/> of the instruction,
        /// which describes the semantics of the instruction.</value>
        public X86Opcode Opcode { get; private set; }

        /// <summary>
        /// Gets or sets an explicit operand size for this instruction.
        /// </summary>
        /// <value>The explicit operand size for this instruction; or <see cref="DataSize.None"/> to determine it from
        /// the operands. The default is <see cref="DataSize.None"/>.</value>
        /// <remarks>
        /// This property is intended to be used with instructions which do not have any operand from which the operand
        /// size can be determined.
        /// </remarks>
        public DataSize ExplicitOperandSize { get; set; } = DataSize.None;

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
        public virtual bool Lock { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="X86Instruction"/> class.
        /// </summary>
        /// <param name="opcode">The opcode of this instruction.</param>
        /// <param name="operands">The operands to the instruction.</param>
        public X86Instruction(X86Opcode opcode, params Operand[] operands)
            : this(opcode, (IList<Operand>)operands)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X86Instruction"/> class.
        /// </summary>
        /// <param name="opcode">The opcode of this instruction.</param>
        /// <param name="operands">The operands to the instruction.</param>
        public X86Instruction(X86Opcode opcode, IList<Operand> operands)
            : base()
        {
            Opcode = opcode;
            Operands = new ReadOnlyCollection<Operand>(operands);
        }

        /// <summary>
        /// Returns the operands to the instruction.
        /// </summary>
        public ReadOnlyCollection<Operand> Operands { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}({1})",
                Opcode.Mnemonic,
                string.Join(", ", Operands));
        }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            // Get the most efficient instruction variant.
            X86OpcodeVariant variant = GetVariant(context);
            if (variant == null)
                throw new AssemblerException("No matching instruction variant was found.");

            // Construct the chosen variant.
            var instr = variant.Construct(context, Operands, Lock);

            return new IEmittable[] { instr };
        }

        /// <summary>
        /// Finds the most efficient variant for the instruction with the current operands.
        /// </summary>
        /// <param name="context">The <see cref="Context"/>.</param>
        /// <returns>The most efficient <see cref="X86OpcodeVariant"/>;
        /// or <see langword="null"/> when none was found.</returns>
        X86OpcodeVariant GetVariant(Context context)
        {
            var variants =
                from v in Opcode.Variants
                where v.Match(ExplicitOperandSize, context, Operands.ToList())
                select v;
            var variant = variants.FirstOrDefault();

            return variant;
        }
    }
}
