using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpcodeWriter
{
    /// <summary>
    /// Describes a single opcode.
    /// </summary>
    public class OpcodeSpec
    {
        /// <summary>
        /// Gets the platform for which this <see cref="OpcodeSpec"/> is written.
        /// </summary>
        /// <value>A platform identifier; or <see langword="null"/> when not specified.</value>
        public virtual string Platform
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets the mnemonic of the opcode.
        /// </summary>
        /// <value>The opcode's mnemonic; or <see langword="null"/> to specify none.
        /// The default is <see langword="null"/>.</value>
        public string Mnemonic { get; set; }

        string name;
        /// <summary>
        /// Gets or sets the name of the opcode as used in classes and identifiers in the code.
        /// </summary>
        /// <value>The name to use; or <see langword="null"/> to use the mnemonic.
        /// The default is <see langword="null"/>.</value>
        public string Name
        {
            get
            {
                if (name != null)
                    return name;
                else
                    return char.ToUpperInvariant(Mnemonic[0]).ToString() + Mnemonic.Substring(1).ToLowerInvariant();
            }
            set { name = value; }
        }

        /// <summary>
        /// Gets a list of names that are aliases for the opcode.
        /// </summary>
        /// <value>A list of strings.</value>
        public List<string> Aka { get; private set; } = new List<string>();

        /// <summary>
        /// Gets or sets a short description of the opcode.
        /// </summary>
        /// <value>A one-sentence description of the opcode; or <see langword="null"/> to specify none.
        /// The default is <see langword="null"/>.</value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets a collection of opcode variants.
        /// </summary>
        /// <value>A collection of opcode variants.</value>
        public Collection<OpcodeVariantSpec> Variants { get; private set; } = new Collection<OpcodeVariantSpec>();
    }
}
