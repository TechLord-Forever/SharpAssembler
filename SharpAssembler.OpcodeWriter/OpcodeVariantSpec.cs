﻿using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;

namespace SharpAssembler.OpcodeWriter
{
    /// <summary>
    /// A specification for a single variant of an opcode.
    /// </summary>
    public class OpcodeVariantSpec
	{
		private bool hasNoMnemonic = false;
		/// <summary>
		/// Gets or sets whether this opcode variant has no associated mnemonic.
		/// </summary>
		/// <value><see langword="true"/> when the opcode variant has no associated mnemonic;
		/// otherwise, <see langword="false"/>. The default is <see langword="false"/>.</value>
		public bool HasNoMnemonic
		{
			get { return hasNoMnemonic; }
			set { hasNoMnemonic = value; }
		}

		private byte[] opcodeBytes = null;
		/// <summary>
		/// Gets or sets the bytes that are used for the encoded opcode part of the instruction.
		/// </summary>
		/// <value>An array of bytes; or <see langword="null"/> to specify no opcode.
		/// The default is <see langword="null"/>.</value>
		public byte[] OpcodeBytes
		{
			get { return opcodeBytes; }
			set { opcodeBytes = value; }
		}

		private readonly Collection<OperandSpec> operands = new Collection<OperandSpec>();
		/// <summary>
		/// Gets a collection of operands.
		/// </summary>
		/// <value>A collection of operands.</value>
		public Collection<OperandSpec> Operands
		{
			get
			{
				#region Contract
				Contract.Ensures(Contract.Result<Collection<OperandSpec>>() != null);
				#endregion
				return operands;
			}
		}
	}
}
