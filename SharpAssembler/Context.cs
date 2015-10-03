using System.Collections.ObjectModel;
using SharpAssembler.Symbols;
using System;

namespace SharpAssembler
{
    /// <summary>
    /// A class for architecture-specific assembler context information.
    /// </summary>
    /// <remarks>
    /// An instance of the <see cref="Context"/> class (or a derived class) is created at the start of the assembling,
    /// and updated as the instructions are processed.
    /// </remarks>
    public class Context
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
        {
        }

        /// <summary>
        /// Gets the architecture for which this object file was created.
        /// </summary>
        /// <value>An <see cref="IArchitecture"/>.</value>
        public IArchitecture Architecture { get; private set; }

        /// <summary>
        /// Gets or sets the current virtual address.
        /// </summary>
        /// <value>The current virtual address.</value>
        [CLSCompliant(false)]
        public ulong Address { get; set; }

        /// <summary>
        /// Gets a collection of relocations in the current context.
        /// </summary>
        /// <value>A <see cref="Collection{Relocation}"/> object.</value>
        public Collection<Relocation> RelocationTable { get; private set; } = new Collection<Relocation>();

        /// <summary>
        /// Gets a dictionary of defined symbols in the current context.
        /// </summary>
        /// <value>A <see cref="SymbolTable"/> object.</value>
        public SymbolTable SymbolTable { get; private set; } = new SymbolTable();

        /// <summary>
        /// Resets this context without clearing the symbol or relocation tables.
        /// </summary>
        /// <remarks>
        /// The context's address is reset to zero, the curent section is set to <see langword="null"/>. Derived
        /// classes may override this method and reset other fields and properties. The symbol and relocation tables
        /// are never cleared.
        /// </remarks>
        public virtual void Reset()
        {
            Address = 0;
        }
    }
}

