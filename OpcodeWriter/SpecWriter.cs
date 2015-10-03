using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace OpcodeWriter
{
    /// <summary>
    /// Writes code based on an opcode specification.
    /// </summary>
    public abstract partial class SpecWriter
    {
        /// <summary>
        /// Represents a tab.
        /// </summary>
        protected const string T = "    ";

        /// <summary>
        /// Gets the base namespace.
        /// </summary>
        protected virtual string BaseNamespace
        {
            get { return string.Format("SharpAssembler.Architectures.{0}", SubNamespace); }
        }

        /// <summary>
        /// Gets the opcode namespace.
        /// </summary>
        protected virtual string OpcodeNamespace
        {
            get { return BaseNamespace + ".Opcodes"; }
        }

        /// <summary>
        /// Gets the opcode test namespace.
        /// </summary>
        protected virtual string OpcodeTestNamespace
        {
            get { return BaseNamespace + ".Tests.Opcodes"; }
        }

        /// <summary>
        /// Gets or sets the sub namespace to use.
        /// </summary>
        /// <value>The sub namespace to use.</value>
        /// <remarks>
        /// The sub namespace is the part in brackets in the following example namespace identifier:
        /// <example>
        /// SharpAssembler.Architectures.&lt;subnamespace&gt;.Opcodes
        /// </example>
        /// </remarks>
        protected abstract string SubNamespace { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecWriter"/> class.
        /// </summary>
        public SpecWriter()
        {
        }

        #endregion

        /// <summary>
        /// Writes the specification in code to the specified code file,
        /// and tests for it to the specified test file.
        /// </summary>
        /// <param name="spec">The specification to write.</param>
        /// <param name="codeFile">The full path to the code file to write to;
        /// or <see langword="null"/> to write no code.</param>
        /// <param name="testFile">The full path to the code file to write to;
        /// or <see langword="null"/> to write no tests.</param>
        /// <remarks>
        /// Any data that is already in the files is overwritten and discarded.
        /// </remarks>
        public void Write(OpcodeSpec spec, string codeFile, string testFile)
        {
            using (var codeStream = File.Create(codeFile))
            using (var testStream = File.Create(testFile))
                Write(spec, codeStream, testStream);
        }

        /// <summary>
        /// Writes the specification in code to the specified code stream,
        /// and tests for it to the specified test stream.
        /// </summary>
        /// <param name="spec">The specification to write.</param>
        /// <param name="codeStream">The code <see cref="Stream"/> to write to;
        /// or <see langword="null"/> to write no code.</param>
        /// <param name="testStream">The test <see cref="Stream"/> to write to;
        /// or <see langword="null"/> to write no tests.</param>
        public void Write(OpcodeSpec spec, Stream codeStream, Stream testStream)
        {
            using (var codeWriter = new StreamWriter(codeStream, Encoding.UTF8))
            using (var testWriter = new StreamWriter(testStream, Encoding.UTF8))
                Write(spec, codeWriter, testWriter);
        }

        /// <summary>
        /// Writes the specification in code to the specified code writer,
        /// and tests for it to the specified test writer.
        /// </summary>
        /// <param name="spec">The specification to write.</param>
        /// <param name="codeWriter">The code writer to use;
        /// or <see langword="null"/> to write no code.</param>
        /// <param name="testWriter">The test writer to use;
        /// or <see langword="null"/> to write no tests.</param>
        public void Write(OpcodeSpec spec, TextWriter codeWriter, TextWriter testWriter)
        {
            if (codeWriter != null)
                WriteCode(spec, codeWriter);

            if (testWriter != null)
                WriteTest(spec, testWriter);
        }

        /// <summary>
        /// Writes a warning about the file being generated.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected void WriteWarning(TextWriter writer)
        {
            writer.WriteLine("//////////////////////////////////////////////////////");
            writer.WriteLine("//                     WARNING                      //");
            writer.WriteLine("//     The contents of this file is generated.      //");
            writer.WriteLine("//    DO NOT MODIFY, your changes will be lost!     //");
            writer.WriteLine("//////////////////////////////////////////////////////");
        }

        protected static readonly Regex IdentifierValidation = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]", RegexOptions.Compiled);

        static readonly System.CodeDom.Compiler.CodeDomProvider CSharpCodeCompiler = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("C#");

        /// <summary>
        /// Returns the given identifier as a valid C# identifier.
        /// </summary>
        /// <param name="identifier">The identifier to return.</param>
        /// <returns>The identifier made safe for use in C#.</returns>
        public static string AsValidIdentifier(string identifier)
        {
            // Based on the code by Visual T4 posted here:
            // http://blog.visualt4.com/2009/02/creating-valid-c-identifiers.html

            // Compliant with item 2.4.2 of the C# specification
            identifier = IdentifierValidation.Replace(identifier, "_");

            if (!char.IsLetter(identifier, 0))
                identifier = "_" + identifier;

            return CSharpCodeCompiler.CreateEscapedIdentifier(identifier);
        }
    }
}
