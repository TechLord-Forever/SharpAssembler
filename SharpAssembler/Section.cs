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
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using SharpAssembler.Symbols;

namespace SharpAssembler
{
    /// <summary>
    /// A section in an object file.
    /// </summary>
    /// <remarks>
    /// To create and add a new instance of this class to an object file,
    /// call <see cref="SectionCollection.AddNew(SectionType, string)"/>.
    /// </remarks>
    [Serializable]
    public sealed class Section
        : IIdentifiable, IAnnotatable, IObjectFileVisitable
    {
        /// <summary>
        /// A list of <see cref="IEmittable"/> objects representing the constructed <see cref="Constructable"/>
        /// objects in this section.
        /// </summary>
        private IEnumerable<IEmittable> emittables = null;
        /// <summary>
        /// Total length of all the emittables.
        /// </summary>
        private int emittableLength = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of the section.</param>
        /// <remarks>
        /// <para>Not all possible characters are valid in a section's identifier in all implementations and file
        /// formats. It is recommended that only the characters a-z, A-Z and 0-9 are used for the identifier, and the
        /// identifier does not start with a digit. Use the <see cref="SectionCollection.AddNew(SectionType, string)"/>
        /// method to ensure that the identifier is valid for the file format.</para>
        /// <para>Some identifiers have a special meaning in certain implementations and file formats.</para>
        /// </remarks>
        internal Section(string identifier)
        {
            Identifier = identifier;
            AssociatedSymbol = new Symbol(SymbolType.None);
        }

        /// <summary>
        /// Gets the identifier of this section.
        /// </summary>
        /// <value>An identifier.</value>
        /// <remarks>
        /// <para>Not all possible characters are valid in a section's identifier in all implementations and file
        /// formats. It is recommended that only the characters a-z, A-Z and 0-9 are used for the identifier, and the
        /// identifier does not start with a digit. However, it may start with a dot. Use the
        /// <see cref="SectionCollection.AddNew(SectionType, string)"/> method to ensure that the identifier is valid
        /// for the file format.</para>
        /// <para>Some identifiers have a special meaning in certain implementations and file formats.</para>
        /// </remarks>
        public string Identifier { get; private set; }

        private Symbol associatedSymbol;
        /// <inheritdoc />
        public Symbol AssociatedSymbol
        {
            get { return associatedSymbol; }
            set { Symbol.SetAssociation(this, value); }
        }

        /// <summary>
        /// Gets or sets flags which apply to this <see cref="Section"/>.
        /// </summary>
        /// <value>A bitwise combination of members of the <see cref="SectionFlags"/> enumeration.</value>
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
        public SectionFlags Flags { get; set; }

        #region Flags
        /// <summary>
        /// Gets or sets whether the contents of this section is copied from the object file to memory on execution.
        /// </summary>
        /// <value><see langword="true"/> to set the <see cref="SectionFlags.Allocated"/> flag;
        /// otherwise, <see langword="false"/> to clear it.</value>
        public bool Allocate
        {
            get { return Flags.HasFlag(SectionFlags.Allocated); }
            set { Flags = Flags.SetFlag(SectionFlags.Allocated, value); }
        }

        /// <summary>
        /// Gets or sets whether the contents of this section requires writing permission.
        /// </summary>
        /// <value><see langword="true"/> to set the <see cref="SectionFlags.Writable"/> flag;
        /// otherwise, <see langword="false"/> to clear it.</value>
        public bool Writable
        {
            get { return Flags.HasFlag(SectionFlags.Writable); }
            set { Flags = Flags.SetFlag(SectionFlags.Writable, value); }
        }

        /// <summary>
        /// Gets or sets whether the contents of this section requires execute permission.
        /// </summary>
        /// <value><see langword="true"/> to set the <see cref="SectionFlags.Executable"/> flag;
        /// otherwise, <see langword="false"/> to clear it.</value>
        public bool Executable
        {
            get { return Flags.HasFlag(SectionFlags.Executable); }
            set { Flags = Flags.SetFlag(SectionFlags.Executable, value); }
        }

        /// <summary>
        /// Gets or sets whether the contents of this section is zeroed. It is assumed to be present in memory on
        /// execution, but is not written in the file.
        /// </summary>
        /// <value><see langword="true"/> to set the <see cref="SectionFlags.Virtual"/> flag;
        /// otherwise, <see langword="false"/> to clear it.</value>
        public bool NoBits
        {
            get { return Flags.HasFlag(SectionFlags.Virtual); }
            set { Flags = Flags.SetFlag(SectionFlags.Virtual, value); }
        }
        #endregion

        /// <summary>
        /// Gets or sets the required alignment of this section.
        /// </summary>
        /// <value>The alignment of this section, as a power of two. The values 0 and 1 both indicate no alignment
        /// constraints. The default is 16.</value>
        /// <remarks>
        /// The effect of setting this property depends on the <see cref="ObjectFile"/> used to store this section.
        /// For example, in the BIN object file format, this property determines the padding used between the end of
        /// the previous section and the start of this one, while in the ELF32 object file format, this property's
        /// value is stored in the metadata and has no influence on the placement of the section in the file or the
        /// memory references used.
        /// </remarks>
        public int Alignment { get; set; } = 16;


        /// <summary>
        /// Gets or sets the virtual address where this section starts.
        /// </summary>
        /// <value>The virtual address used for all memory references within this section;
        /// or <see langword="null"/> to specify no virtual start address.</value>
        /// <remarks>
        /// <para>Not all file formats and architectures support setting this member to a value other than
        /// <see langword="null"/>.</para>
        /// <para>This member is similar to the 'vstart' NASM section attribute.</para>
        /// </remarks>
        public UInt128? Address { get; set; }

        /// <summary>
        /// Gets a dictionary which may be used to store data specific to this object.
        /// </summary>
        /// <value>An implementation of the <see cref="IDictionary"/> interface.</value>
        /// <remarks>
        /// This property is not serialized or deserialized.
        /// </remarks>
        public IDictionary Annotations { get; private set; } = new Hashtable();

        /// <summary>
        /// Gets whether the section has been constructed.
        /// </summary>
        /// <value><see langword="true"/> when the section's <see cref="Construct"/> method has
        /// been called; otherwise, <see langword="false"/>.</value>
        public bool IsConstructed
        {
            get { return emittables != null; }
        }

        /// <summary>
        /// Gets a collection with the contents of this section.
        /// </summary>
        /// <value>A <see cref="ConstructableCollection"/> with the contents of this section.</value>
        public ConstructableCollection Contents { get; private set; } = new ConstructableCollection();

        #region Methods
        /// <summary>
        /// Constructs the <see cref="IEmittable"/> sequence and symbol table for this section.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> used.</param>
        public void Construct(Context context)
        {
            // Set the current section and its offset.
            context.Section = this;
            //context.SectionOffset = 0;
            emittableLength = 0;

            // When a virtual address has been set, use it.
            if (Address.HasValue)
                context.Address = Address.Value;

            // When there are alignment constraints, align the virtual address.
            if (Alignment > 1)
                context.Address = context.Address.Align(Alignment);

            // Construct all emittables.
            var emittables = new List<IEmittable>();
            foreach (var constructable in Contents.WhereNotNull())
            {
                var result = constructable.Construct(context);
                emittables.AddRange(result.WhereNotNull());
            }
            this.emittables = emittables;

            long totalLength = this.emittables.Sum(e => (long)e.GetLength());
            emittableLength = checked((int)(emittableLength + totalLength));
            context.Address += totalLength;

            // Reset the current section.
            context.Section = null;
        }

        /// <summary>
        /// Emits this section into its binary representation.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the section is written.</param>
        /// <param name="context">The <see cref="Context"/> used.</param>
        /// <returns>The number of bytes in this section which were written to <paramref name="writer"/>.</returns>
        public long Emit(BinaryWriter writer, Context context)
        {
            // Set the current section and its offset.
            context.Section = this;

            // When a virtual address has been set, use it.
            if (Address.HasValue)
                context.Address = Address.Value;

            // When there are alignment constraints, align the virtual address.
            if (Alignment > 1)
                context.Address = context.Address.Align(Alignment);

            // Encode each emittable.
            long totalEmitted = 0;
            foreach (IEmittable e in emittables)
            {
                int emitted = e.Emit(writer, context);
                context.Address += (ulong)emitted;
                totalEmitted += emitted;
                emittableLength -= emitted;
            }

            // Reset the current section.
            context.Section = null;

            return totalEmitted;
        }

        /// <summary>
        /// Resets the section's encoding process.
        /// </summary>
        public void Reset()
        {
            emittables = null;
            emittableLength = 0;
        }

        /// <inheritdoc />
        void IAssociatable.SetAssociatedSymbol(Symbol symbol)
        {
            associatedSymbol = symbol;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The <see cref="IObjectFileVisitor"/> visiting.</param>
        public void Accept(IObjectFileVisitor visitor)
        {
            visitor.VisitSection(this);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", Identifier);
        }
        #endregion

        /// <summary>
        /// Gets the <see cref="ObjectFile"/> in which this section is declared.
        /// </summary>
        /// <value>An <see cref="ObjectFile"/>; or <see langword="null"/> when the section is not part of any object
        /// file.</value>
        public ObjectFile Parent { get; internal set; }

        /// <summary>
        /// Gets the <see cref="IFile"/> in which this
        /// object is defined.
        /// </summary>
        /// <value>A <see cref="IFile"/>.</value>
        IFile IAssociatable.ParentFile
        {
            get { return ((IAssociatable)Parent).ParentFile; }
        }
    }
}
