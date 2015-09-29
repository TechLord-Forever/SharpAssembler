using System.Collections.Generic;
using System.IO;

namespace OpcodeWriter
{
    /// <summary>
    /// Reads scripts.
    /// </summary>
    public interface IScriptInterpreter
    {
        /// <summary>
        /// Reads the script from the specified file
        /// and returns the opcode specifications it contains.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> ReadFrom(string path);

        /// <summary>
        /// Reads the script from the specified <see cref="Stream"/>
        /// and returns the opcode specifications it contains.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/>.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> ReadFrom(Stream stream);

        /// <summary>
        /// Reads the script from the specified <see cref="Stream"/>
        /// and returns the opcode specifications it contains.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/>.</param>
        /// <param name="basePath">The base path of the script; or <see langword="null"/> to specify none.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> ReadFrom(Stream stream, string basePath);

        /// <summary>
        /// Reads the script from the specified <see cref="TextReader"/>
        /// and returns the opcode specifications it contains.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/>.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> ReadFrom(TextReader reader);

        /// <summary>
        /// Reads the script from the specified <see cref="TextReader"/>
        /// and returns the opcode specifications it contains.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/>.</param>
        /// <param name="basePath">The base path of the script; or <see langword="null"/> to specify none.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> ReadFrom(TextReader reader, string basePath);

        /// <summary>
        /// Reads the specified script.
        /// </summary>
        /// <param name="script">The script to read.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> Read(string script);

        /// <summary>
        /// Reads the specified script.
        /// </summary>
        /// <param name="script">The script to read.</param>
        /// <param name="basePath">The base path of the script; or <see langword="null"/> to specify none.</param>
        /// <returns>An enumerable collection of <see cref="OpcodeSpec"/> objects.</returns>
        IEnumerable<OpcodeSpec> Read(string script, string basePath);
    }
}
