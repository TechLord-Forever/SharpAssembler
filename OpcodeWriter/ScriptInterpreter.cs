﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace OpcodeWriter
{
    /// <summary>
    /// Reads a test script.
    /// </summary>
    public class ScriptInterpreter : IScriptInterpreter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInterpreter"/> class.
        /// </summary>
        /// <param name="tokenizer">The tokenizer to use.</param>
        /// <param name="factoryDispenser">The factory dispenser to use.</param>
        public ScriptInterpreter(ScriptTokenizer tokenizer, SpecFactoryDispenser factoryDispenser)
        {
            this.tokenizer = tokenizer;
            this.factoryDispenser = factoryDispenser;
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> ReadFrom(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return ReadFrom(stream, Path.GetDirectoryName(path));
            }
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> ReadFrom(Stream stream)
        {
            return ReadFrom(stream, null);
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> ReadFrom(Stream stream, string basePath)
        {
            // TODO: Ensure that 'stream' is not closed afterwards.
            using (var reader = new StreamReader(stream))
            {
                return ReadFrom(reader, basePath);
            }
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> ReadFrom(TextReader reader)
        {
            return ReadFrom(reader, null);
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> ReadFrom(TextReader reader, string basePath)
        {
            return Read(reader.ReadToEnd(), basePath);
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> Read(string script)
        {
            return Read(script, null);
        }

        /// <inheritdoc />
        public IEnumerable<OpcodeSpec> Read(string script, string basePath)
        {
            opcodespecs = new List<OpcodeSpec>();
            reader = new ScriptReader(tokenizer.Tokenize(script));

            this.basePath = basePath != null ? basePath : Directory.GetCurrentDirectory();

            Execute();
            return opcodespecs;
        }

        /// <summary>
        /// The base path to the script, never <see langword="null"/>.
        /// </summary>
        string basePath;

        /// <summary>
        /// The tokenizer to use.
        /// </summary>
        ScriptTokenizer tokenizer;

        /// <summary>
        /// A list to which all read <see cref="OpcodeSpec"/> objects are added.
        /// </summary>
        List<OpcodeSpec> opcodespecs;

        /// <summary>
        /// A script reader.
        /// </summary>
        ScriptReader reader;

        /// <summary>
        /// The <see cref="SpecFactoryDispenser"/>.
        /// </summary>
        readonly SpecFactoryDispenser factoryDispenser;

        /// <summary>
        /// The current <see cref="SpecFactory"/>.
        /// </summary>
        SpecFactory factory;

        /// <summary>
        /// Aliases.
        /// </summary>
        Dictionary<string, string> aliases = new Dictionary<string, string>();

        /// <summary>
        /// Annotations applied to a specific object.
        /// </summary>
        Dictionary<object, IList<Annotation>> annotationAssociations = new Dictionary<object, IList<Annotation>>();

        /// <summary>
        /// Annotations that were read, but not yet applied to an object.
        /// </summary>
        IList<Annotation> readAnnotations = new List<Annotation>();


        /// <summary>
        /// Executes the state machine.
        /// </summary>
        void Execute()
        {
            aliases = new Dictionary<string, string>();
            annotationAssociations = new Dictionary<object, IList<Annotation>>();
            readAnnotations = new List<Annotation>();

            while (reader.Peek() != null)
            {
                string keyword = reader.Peek();
                switch (keyword)
                {
                    case "include":
                        ReadInclude();
                        break;
                    case "opcode":
                        ReadOpcodeDefinition();
                        break;
                    case "alias":
                        ReadAlias();
                        break;
                    case "[":
                        ReadAnnotations();
                        break;
                    default:
                        throw new ScriptException(string.Format("Unknown keyword {0}, expected one of: opcode, alias, [", keyword));
                }
            }

            ApplyAllAnnotations();
        }

        /// <summary>
        /// Reads an include.
        /// </summary>
        void ReadInclude()
        {
            ExpectRead("include");

            var path = this.reader.ReadString();

            ExpectRead(";");

            if (Path.GetExtension(path) == string.Empty)
            {
                path += ".inc";
            }

            // Process the include right at this spot.
            using (var stream = File.OpenRead(Path.Combine(basePath, path)))
            using (var reader = new StreamReader(stream))
            {
                this.reader.Prepend(tokenizer.Tokenize(reader.ReadToEnd()));
            }
        }

        /// <summary>
        /// Reads an alias.
        /// </summary>
        void ReadAlias()
        {
            ExpectRead("alias");

            var from = ReadIdentifier();

            ExpectRead("=");

            var to = ReadIdentifier();

            ExpectRead(";");

            if (from != to)
                aliases[from] = to;
        }

        /// <summary>
        /// Reads an identifier, and applies the alias if possible.
        /// </summary>
        /// <returns>The identifier.</returns>
        string ReadIdentifier()
        {
            string oldIdentifier = reader.ReadIdentifier();
            string newIdentifier;
            if (!aliases.TryGetValue(oldIdentifier, out newIdentifier))
                newIdentifier = oldIdentifier;
            return newIdentifier;
        }

        /// <summary>
        /// Sets the currently used <see cref="SpecFactory"/> to the correct implementation
        /// based on the platform ID.
        /// </summary>
        /// <param name="platform">The platform ID.</param>
        void SetFactoryForPlatform(string platform)
        {
            factory = factoryDispenser.Get(platform);
        }

        /// <summary>
        /// Reads an opcode definition from the file.
        /// </summary>
        void ReadOpcodeDefinition()
        {
            reader.Read();

            string platform = ReadIdentifier();
            SetFactoryForPlatform(platform);
            var opcodeSpec = factory.CreateOpcodeSpec();
            opcodespecs.Add(opcodeSpec);
            opcodeSpec.Mnemonic = ReadIdentifier();

            string asKeyword = reader.Peek();
            if (asKeyword == "as")
            {
                ExpectRead("as");
                opcodeSpec.Name = ReadIdentifier();
            }
            string akaKeyword = reader.Peek();
            if (akaKeyword == "aka")
            {
                ExpectRead("aka");
                if (reader.Peek() == "(")
                {
                    reader.ReadListInRegion(ScriptReader.RegionType.Parentheses, ",", () =>
                    {
                        opcodeSpec.Aka.Add(ReadIdentifier());
                    });
                }
                else
                    opcodeSpec.Aka.Add(ReadIdentifier());
            }

            ApplyAnnotations(opcodeSpec);

            reader.ReadRegionAndRepeat(ScriptReader.RegionType.Body, () =>
            {
                string keyword = reader.Peek();
                switch (keyword)
                {
                    case "var":
                        ReadOpcodeVariant(opcodeSpec);
                        break;
                    case "[":
                        ReadAnnotations();
                        break;
                    default:
                        throw new ScriptException(string.Format("Unknown keyword {0}, expected one of: var, [", keyword));
                }
            });
        }

        #region Annotations
        /// <summary>
        /// Reads an annotation.
        /// </summary>
        Annotation ReadAnnotation()
        {
            string name = ReadIdentifier();
            ExpectRead("=");
            object value = ReadAnyValue();

            return new Annotation(name, value);
        }

        /// <summary>
        /// Reads annotations, if they are specified.
        /// </summary>
        void ReadAnnotations()
        {
            if (reader.Peek() == "[")
            {
                reader.ReadListInRegion(ScriptReader.RegionType.SquareBrackets, ",", () =>
                {
                    readAnnotations.Add(ReadAnnotation());
                });
            }
        }

        /// <summary>
        /// Adds the annotations to the specified object.
        /// </summary>
        /// <param name="target">The target object.</param>
        void ApplyAnnotations(object target)
        {
            var annotations = readAnnotations;
            readAnnotations = new List<Annotation>();

            IList<Annotation> existingAnnotations;
            if (!annotationAssociations.TryGetValue(target, out existingAnnotations))
                annotationAssociations[target] = annotations;
            else
            {
                foreach (var annotation in annotations)
                    existingAnnotations.Add(annotation);
            }
        }

        /// <summary>
        /// Applies all annotations.
        /// </summary>
        void ApplyAllAnnotations()
        {
            foreach (var annotationAssociation in annotationAssociations)
            {
                foreach (var annotation in annotationAssociation.Value)
                {
                    annotation.SetOn(annotationAssociation.Key);
                }
            }
        }
        #endregion

        /// <summary>
        /// Reads an opcode variant.
        /// </summary>
        void ReadOpcodeVariant(OpcodeSpec opcodeSpec)
        {
            ExpectRead("var");

            var opcodeVariantSpec = factory.CreateOpcodeVariantSpec();
            opcodeSpec.Variants.Add(opcodeVariantSpec);

            string opcodeBytesStr = ReadIdentifier();
            opcodeVariantSpec.OpcodeBytes = ReadByteArray(opcodeBytesStr);

            ApplyAnnotations(opcodeVariantSpec);

            reader.ReadListInRegion(ScriptReader.RegionType.Parentheses, ",", () =>
            {
                ReadAnnotations();

                string operandType = ReadIdentifier();
                string operandName = ReadIdentifier();
                object defaultValue = null;

                if (reader.Peek().Equals("="))
                {
                    reader.Read();
                    defaultValue = ReadAnyValue();
                }

                var operandSpec = factory.CreateOperandSpec(operandType, defaultValue);
                opcodeVariantSpec.Operands.Add(operandSpec);
                operandSpec.Name = operandName;

                ApplyAnnotations(operandSpec);
            });

            ExpectRead(";");
        }

        /// <summary>
        /// Reads an array of bytes.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>The array of bytes.</returns>
        static byte[] ReadByteArray(string str)
        {
            var byteStrings = str.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[byteStrings.Length];
            int index = 0;
            foreach (var s in byteStrings)
            {
                int value;
                if (!int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                {
                    throw new ScriptException("Malformed opcode variant identifier. Expected a space-separated list " +
                        "of hexadecimal byte values.");
                }
                bytes[index++] = (byte)value;
            }
            return bytes;
        }

        /// <summary>
        /// Expects a particular value.
        /// </summary>
        /// <param name="expected">The value that is expected.</param>
        void ExpectRead(string expected)
        {
            string actual = reader.Read();
            if (actual != expected)
                throw new ScriptException(string.Format("Expected \"{0}\", got \"{1}\".", expected, actual));
        }

        /// <summary>
        /// Reads a value of any type.
        /// </summary>
        /// <returns>The read value.</returns>
        object ReadAnyValue()
        {
            string keyword = reader.Peek();
            switch (keyword)
            {
                case "true": reader.Read(); return true;
                case "false": reader.Read(); return false;
            }

            if (reader.PeekString() != null)
                return reader.ReadString();

            if (reader.PeekInteger().HasValue)
                return reader.ReadInteger();

            if (reader.PeekIdentifier() != null)
                return (Identifier)ReadIdentifier();

            throw new ScriptException("Could not determine type of value.");
        }
    }
}
