using SharpAssembler.Symbols;
using System;
using System.Collections.ObjectModel;

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
        /// Gets or sets whether to use RIP-relative addressing by default.
        /// </summary>
        /// <value><see langword="true"/> to use RIP-relative addressing by default;
        /// otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// This property's value may only be <see langword="true"/> in 64-bit addressing mode.
        /// </remarks>
        public bool UseRIPRelativeAddressing { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DataSize AddressingMode { get; set; }

        /// <summary>
        /// Gets or sets the current virtual address.
        /// </summary>
        /// <value>The current virtual address.</value>
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
        ///
        /// </summary>
        public Context(DataSize addressingMode, ulong address, bool useRIPRelativeAddressing)
        {
            AddressingMode = addressingMode;
            Address = address;
            UseRIPRelativeAddressing = useRIPRelativeAddressing;
        }
    }
}