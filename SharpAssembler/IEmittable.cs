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
using System.Diagnostics.Contracts;
using System.IO;

namespace SharpAssembler
{
    /// <summary>
    /// An interface for a constructed representation of a <see cref="Constructable"/>.
    /// </summary>
    public interface IEmittable
	{
		/// <summary>
		/// Modifies the context and emits the binary representation of this emittable to the specified
		/// <see cref="BinaryWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="BinaryWriter"/> to which the encoded instruction is written.</param>
		/// <param name="context">The <see cref="Context"/> in which the emittable will be emitted.</param>
		/// <returns>The number of emitted bytes.</returns>
		int Emit(BinaryWriter writer, Context context);

		/// <summary>
		/// Gets the length of the emittable.
		/// </summary>
		/// <returns>The length of the emittable, in bytes.</returns>
		[Pure]
		int GetLength();

		// TODO:
		// - Add a list of symbols used by this emittable.
		// - Let Emit throw an exception when there are unresolved symbols or something.
	}
}