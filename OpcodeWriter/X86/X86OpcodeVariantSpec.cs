using System;

namespace OpcodeWriter.X86
{
    /// <summary>
    ///
    /// </summary>
    public class X86OpcodeVariantSpec : OpcodeVariantSpec
    {
        private byte fixedReg = 0;
        /// <summary>
        /// Gets or sets the fixed value of the REG part of the ModR/M byte.
        /// </summary>
        /// <value>The 3-bit fixed REG value. The default value is 0.</value>
        public byte FixedReg
        {
            get { return fixedReg; }
            set
            {
                if (value > 0x7)
                    throw new Exception("Only the least significant 3 bits may be set.");
                fixedReg = value;
            }
        }

        /// <summary>
        /// Gets or sets the operand size of the opcode variant.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration;
        /// or <see cref="OpcodeWriter.DataSize.None"/> to specify no operand size.
        /// The default is <see cref="OpcodeWriter.DataSize.None"/>.</value>
        public DataSize OperandSize { get; set; } = DataSize.None;

        /// <summary>
        /// Gets or sets the processor modes in which this opcode variant is supported.
        /// </summary>
        /// <value>A bitwise combination of members of the <see cref="ProcessorModes"/> enumeration.
        /// The default is <see cref="SharpAssembler.Architectures.X86.ProcessorModes.LongProtectedReal"/>.</value>
        public ProcessorModes SupportedModes { get; set; } = ProcessorModes.LongProtectedReal;

        /// <summary>
        /// Gets or sets whether this opcode variant requires REX prefix.
        /// </summary>
        /// <value><see langword="true"/> when the opcode variant requires no REX prefix;
        /// otherwise, <see langword="false"/>. The default is <see langword="false"/>.</value>
        public bool NoRexPrefix { get; set; } = false;
    }
}
