﻿#region Copyright and License
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

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An operand for an instruction.
    /// </summary>
	public interface IOperand
	{
		/// <summary>
		/// Gets or sets the preferred size of the operand.
		/// </summary>
		/// <value>A member of the <see cref="DataSize"/> enumeration;
		/// or <see cref="SharpAssembler.DataSize.None"/> to specify no size.</value>
		DataSize PreferredSize
		{ get; set; }

		/// <summary>
		/// Gets the actual size of operand.
		/// </summary>
		/// <value>A member of the <see cref="DataSize"/> enumeration;
		/// or <see cref="SharpAssembler.DataSize.None"/>.</value>
		DataSize Size
		{ get; }
	}
}
