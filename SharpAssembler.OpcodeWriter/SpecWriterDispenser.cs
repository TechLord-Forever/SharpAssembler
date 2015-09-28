using System.Collections.Generic;

namespace SharpAssembler.OpcodeWriter
{
    /// <summary>
    /// Dispenses <see cref="SpecWriter"/> objects for specific platforms.
    /// </summary>
    public class SpecWriterDispenser
    {
        /// <summary>
        /// A dictionary with writers.
        /// </summary>
        private Dictionary<string, SpecWriter> writers = new Dictionary<string, SpecWriter>();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecWriterDispenser"/> class.
        /// </summary>
        public SpecWriterDispenser()
        { /* Nothing to do. */ }
        #endregion

        /// <summary>
        /// Registers a new writer with the specified platform ID.
        /// </summary>
        /// <param name="platformID">The ID of the platform.</param>
        /// <param name="writer">The writer instance.</param>
        public void Register(string platformID, SpecWriter writer)
        {
            writers.Add(platformID, writer);
        }

        /// <summary>
        /// Gets a writer with the specified platform ID.
        /// </summary>
        /// <param name="platformID">The platform ID.</param>
        /// <returns>The writer instance.</returns>
        public SpecWriter Get(string platformID)
        {
            return writers[platformID];
        }
    }
}
