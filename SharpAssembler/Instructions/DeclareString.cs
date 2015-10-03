using System.Text;
using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Declares a string.
    /// </summary>
    public class DeclareString : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareString"/> class declaring the specified string
        /// with UTF-8 encoding.
        /// </summary>
        /// <param name="data">The data.</param>
        public DeclareString(string data)
            : this(data, Encoding.UTF8)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareString"/> class declaring the specified string
        /// with the specified encoding.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="encoding">The encoding of the string.</param>
        public DeclareString(string data, Encoding encoding)
        {
            Data = data;
            Encoding = encoding;
        }

        /// <summary>
        /// Gets or sets the string that will be declared.
        /// </summary>
        /// <value>A string.</value>
        public string Data { get; private set; }

        /// <summary>
        /// Gets or sets the encoding of the declared string.
        /// </summary>
        /// <value>An <see cref="Encoding"/>.</value>
        public Encoding Encoding { get; private set; }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            yield return new RawEmittable(Encoding.GetBytes(Data));
        }
    }
}
