﻿using System.IO;

namespace OpcodeWriter
{
    partial class SpecWriter
    {
        /// <summary>
        /// Writes the specification tests to the specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="spec">The specification to write.</param>
        /// <param name="writer">The write to use.</param>
        protected virtual void WriteTest(OpcodeSpec spec, TextWriter writer)
        {
            WriteWarning(writer);
            WriteTestUsingDirectives(writer);
            writer.WriteLine();
            WriteTestClassStart(spec, writer);
            WriteTestClassTests(spec, writer);
            WriteTestClassEnd(writer);
            WriteWarning(writer);
        }

        /// <summary>
        /// Writes the <c>using</c>-directives.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        /// <remarks>
        /// Override this method to add additional using directives.
        /// Call the base method in the process.
        /// </remarks>
        protected virtual void WriteTestUsingDirectives(TextWriter writer)
        {
            // NOTE: Writing more using-directives than strictly needed is neither a problem,
            // nor a cause for warnings or errors.
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("using System.Linq;");
            writer.WriteLine("using Moq;");
            writer.WriteLine("using NUnit.Framework;");
        }

        /// <summary>
        /// Writes the namespace declaration start and the test class declaration start.
        /// </summary>
        /// <param name="spec">The opcode specification.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected void WriteTestClassStart(OpcodeSpec spec, TextWriter writer)
        {
            writer.WriteLine("namespace {0}", OpcodeTestNamespace);
            writer.WriteLine("{");
            writer.WriteLine(T + "/// <summary>");
            writer.WriteLine(T + "/// Tests all variants of the {0} opcode.", spec.Mnemonic.ToUpperInvariant());
            writer.WriteLine(T + "/// </summary>");
            writer.WriteLine(T + "[TestFixture]");
            writer.WriteLine(T + "public class {0} : {1}", AsValidIdentifier(spec.Name + "Tests"), GetTestBaseClassName());
            writer.WriteLine(T + "{");
        }

        /// <summary>
        /// Writes the end of the test class declaration and its namespace.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected void WriteTestClassEnd(TextWriter writer)
        {
            writer.WriteLine(T + "}");
            writer.WriteLine("}");
            writer.WriteLine();
        }

        /// <summary>
        /// Writes the tests.
        /// </summary>
        /// <param name="spec">The opcode specification.</param>
        /// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
        protected abstract void WriteTestClassTests(OpcodeSpec spec, TextWriter writer);

        /// <summary>
        /// Returns the class name of the base class for the opcode test classes.
        /// </summary>
        /// <returns>The opcode test base class name.</returns>
        protected abstract string GetTestBaseClassName();
    }
}
