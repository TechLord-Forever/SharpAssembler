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
using System.Collections;
using System.Collections.Generic;

namespace SharpAssembler
{
    /// <summary>
    /// An interface for entities which can be contained can create a
    /// representation of themselves.
    /// </summary>
    public abstract class Constructable// : IAnnotatable//, IObjectFileVisitable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Constructable"/> class.
        /// </summary>
        protected Constructable()
        {
        }

        /// <summary>
        /// Gets a dictionary which may be used to store data specific to this object.
        /// </summary>
        /// <value>An implementation of the <see cref="IDictionary"/> interface.</value>
        /// <remarks>
        /// This property is not serialized or deserialized.
        /// </remarks>
        public IDictionary Annotations { get; private set; } = new Hashtable();

        /// <summary>
        /// Modifies the context and constructs an emittable representing this constructable.
        /// </summary>
        /// <param name="context">The mutable <see cref="Context"/> in which the emittable will be constructed.</param>
        /// <returns>A list of constructed emittables; or an empty list.</returns>
        public abstract IEnumerable<IEmittable> Construct(Context context);
    }
}
