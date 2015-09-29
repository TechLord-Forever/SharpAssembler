using SharpAssembler.Architectures.X86;

namespace SharpAssembler
{
    /// <summary>
    ///
    /// </summary>
    public class Register_
    {
        /// <summary>
        ///
        /// </summary>
        public byte Value { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public RegisterType Type { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DataSize Size { get { return (DataSize)((int)Type & 0x3F); } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public Register_(byte value, RegisterType type)
        {
            Value = value;
            Type  = type;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class Reg
    {
        /// <summary>
        /// The lower 8-bits of the accumulator register.
        /// </summary>
        public static Register_ AL { get; } = new Register_(0x00, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the accumulator register.
        /// </summary>
        public static Register_ AX { get; } = new Register_(0x00, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the accumulator register.
        /// </summary>
        public static Register_ EAX { get; } = new Register_(0x00, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the accumulator register.
        /// </summary>
        public static Register_ RAX { get; } = new Register_(0x00, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The first 64-bit SIMD register.
        /// </summary>
        public static Register_ MM0 { get; } = new Register_(0x00, RegisterType.Simd64Bit);
        /// <summary>
        /// The first 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM0 { get; } = new Register_(0x00, RegisterType.Simd128Bit);
        /// <summary>
        /// The extra segment register.
        /// </summary>
        public static Register_ ES { get; } = new Register_(0x00, RegisterType.Segment);
        /// <summary>
        /// The first control register.
        /// </summary>
        public static Register_ CR0 { get; } = new Register_(0x00, RegisterType.Control);
        /// <summary>
        /// The first debug register.
        /// </summary>
        public static Register_ DR0 { get; } = new Register_(0x00, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 0.
        /// </summary>
        public static Register_ ST0 { get; } = new Register_(0x00, RegisterType.FloatingPoint);

        /// <summary>
        /// The lower 8-bits of the counter register.
        /// </summary>
        public static Register_ CL { get; } = new Register_(0x01, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the counter register.
        /// </summary>
        public static Register_ CX { get; } = new Register_(0x01, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the counter register.
        /// </summary>
        public static Register_ ECX { get; } = new Register_(0x01, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the counter register.
        /// </summary>
        public static Register_ RCX { get; } = new Register_(0x01, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The second 64-bit SIMD register.
        /// </summary>
        public static Register_ MM1 { get; } = new Register_(0x01, RegisterType.Simd64Bit);
        /// <summary>
        /// The second 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM1 { get; } = new Register_(0x01, RegisterType.Simd128Bit);
        /// <summary>
        /// The code segment register.
        /// </summary>
        public static Register_ CS { get; } = new Register_(0x01, RegisterType.Segment);
        /// <summary>
        /// The second control register.
        /// </summary>
        public static Register_ CR1 { get; } = new Register_(0x01, RegisterType.Control);
        /// <summary>
        /// The second debug register.
        /// </summary>
        public static Register_ DR1 { get; } = new Register_(0x01, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 1.
        /// </summary>
        public static Register_ ST1 { get; } = new Register_(0x01, RegisterType.FloatingPoint);

        /// <summary>
        /// The lower 8-bits of the data register.
        /// </summary>
        public static Register_ DL { get; } = new Register_(0x02, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the data register.
        /// </summary>
        public static Register_ DX { get; } = new Register_(0x02, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the data register.
        /// </summary>
        public static Register_ EDX { get; } = new Register_(0x02, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the data register.
        /// </summary>
        public static Register_ RDX { get; } = new Register_(0x02, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The third 64-bit SIMD register.
        /// </summary>
        public static Register_ MM2 { get; } = new Register_(0x02, RegisterType.Simd64Bit);
        /// <summary>
        /// The third 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM2 { get; } = new Register_(0x02, RegisterType.Simd128Bit);
        /// <summary>
        /// The stack segment register.
        /// </summary>
        public static Register_ SS { get; } = new Register_(0x02, RegisterType.Segment);
        /// <summary>
        /// The third control register.
        /// </summary>
        public static Register_ CR2 { get; } = new Register_(0x02, RegisterType.Control);
        /// <summary>
        /// The third debug register.
        /// </summary>
        public static Register_ DR2 { get; } = new Register_(0x02, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 2.
        /// </summary>
        public static Register_ ST2 { get; } = new Register_(0x02, RegisterType.FloatingPoint);

        /// <summary>
        /// The lower 8-bits of the base register.
        /// </summary>
        public static Register_ BL { get; } = new Register_(0x03, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the base register.
        /// </summary>
        public static Register_ BX { get; } = new Register_(0x03, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the base register.
        /// </summary>
        public static Register_ EBX { get; } = new Register_(0x03, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the base register.
        /// </summary>
        public static Register_ RBX { get; } = new Register_(0x03, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fourth 64-bit SIMD register.
        /// </summary>
        public static Register_ MM3 { get; } = new Register_(0x03, RegisterType.Simd64Bit);
        /// <summary>
        /// The fourth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM3 { get; } = new Register_(0x03, RegisterType.Simd128Bit);
        /// <summary>
        /// The data segment register.
        /// </summary>
        public static Register_ DS { get; } = new Register_(0x03, RegisterType.Segment);
        /// <summary>
        /// The fourth control register.
        /// </summary>
        public static Register_ CR3 { get; } = new Register_(0x03, RegisterType.Control);
        /// <summary>
        /// The fourth debug register.
        /// </summary>
        public static Register_ DR3 { get; } = new Register_(0x03, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 3.
        /// </summary>
        public static Register_ ST3 { get; } = new Register_(0x03, RegisterType.FloatingPoint);

        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the accumulator register.
        /// </summary>
        public static Register_ AH { get; } = new Register_(0x04, RegisterType.GeneralPurpose8Bit);

        /// <summary>
        /// The lower 8-bits of the stack pointer register.
        /// </summary>
        //SPL = AH | 0x10,    // Required REX prefix.

        /// <summary>
        /// The lower 16-bits of the stack pointer register.
        /// </summary>
        public static Register_ SP { get; } = new Register_(0x04, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the stack pointer register.
        /// </summary>
        public static Register_ ESP { get; } = new Register_(0x04, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the stack pointer register.
        /// </summary>
        public static Register_ RSP { get; } = new Register_(0x04, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fifth 64-bit SIMD register.
        /// </summary>
        public static Register_ MM4 { get; } = new Register_(0x04, RegisterType.Simd64Bit);
        /// <summary>
        /// The fifth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM4 { get; } = new Register_(0x04, RegisterType.Simd128Bit);
        /// <summary>
        /// The second extra segment register.
        /// </summary>
        public static Register_ FS { get; } = new Register_(0x04, RegisterType.Segment);
        /// <summary>
        /// The fifth control register.
        /// </summary>
        public static Register_ CR4 { get; } = new Register_(0x04, RegisterType.Control);
        /// <summary>
        /// The fifth debug register.
        /// </summary>
        public static Register_ DR4 { get; } = new Register_(0x04, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 4.
        /// </summary>
        public static Register_ ST4 { get; } = new Register_(0x04, RegisterType.FloatingPoint);

        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the counter register.
        /// </summary>
        public static Register_ CH { get; } = new Register_(0x05, RegisterType.GeneralPurpose8Bit);

        /// <summary>
        /// The lower 8-bits of the base pointer register.
        /// </summary>
        //BPL = CH | 0x10,    // Required REX prefix.

        /// <summary>
        /// The lower 16-bits of the base pointer register.
        /// </summary>
        public static Register_ BP { get; } = new Register_(0x05, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the base pointer register.
        /// </summary>
        public static Register_ EBP { get; } = new Register_(0x05, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the base pointer register.
        /// </summary>
        public static Register_ RBP { get; } = new Register_(0x05, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The sixth 64-bit SIMD register.
        /// </summary>
        public static Register_ MM5 { get; } = new Register_(0x05, RegisterType.Simd64Bit);
        /// <summary>
        /// The sixth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM5 { get; } = new Register_(0x05, RegisterType.Simd128Bit);
        /// <summary>
        /// The third extra segment register.
        /// </summary>
        public static Register_ GS { get; } = new Register_(0x05, RegisterType.Segment);
        /// <summary>
        /// The sixth control register.
        /// </summary>
        public static Register_ CR5 { get; } = new Register_(0x05, RegisterType.Control);
        /// <summary>
        /// The sixth debug register.
        /// </summary>
        public static Register_ DR5 { get; } = new Register_(0x05, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 5.
        /// </summary>
        public static Register_ ST5 { get; } = new Register_(0x05, RegisterType.FloatingPoint);



        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the data register.
        /// </summary>
        public static Register_ DH { get; } = new Register_(0x06, RegisterType.GeneralPurpose8Bit);

        /// <summary>
        /// The lower 8-bits of the source index register.
        /// </summary>
        //SIL = DH | 0x10,    // Required REX prefix.

        /// <summary>
        /// The lower 16-bits of the source index register.
        /// </summary>
        public static Register_ SI { get; } = new Register_(0x06, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the source index register.
        /// </summary>
        public static Register_ ESI { get; } = new Register_(0x06, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the source index register.
        /// </summary>
        public static Register_ RSI { get; } = new Register_(0x06, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The seventh 64-bit SIMD register.
        /// </summary>
        public static Register_ MM6 { get; } = new Register_(0x06, RegisterType.Simd64Bit);
        /// <summary>
        /// The seventh 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM6 { get; } = new Register_(0x06, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The seventh control register.
        /// </summary>
        public static Register_ CR6 { get; } = new Register_(0x06, RegisterType.Control);
        /// <summary>
        /// The seventh debug register.
        /// </summary>
        public static Register_ DR6 { get; } = new Register_(0x06, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 6.
        /// </summary>
        public static Register_ ST6 { get; } = new Register_(0x06, RegisterType.FloatingPoint);



        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the base register.
        /// </summary>
        public static Register_ BH { get; } = new Register_(0x07, RegisterType.GeneralPurpose8Bit);

        /// <summary>
        /// The lower 8-bits of the destination index register.
        /// </summary>
        //DIL = BH | 0x10,    // Required REX prefix.

        /// <summary>
        /// The lower 16-bits of the destination index register.
        /// </summary>
        public static Register_ DI { get; } = new Register_(0x07, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the destination index register.
        /// </summary>
        public static Register_ EDI { get; } = new Register_(0x07, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the destination index register.
        /// </summary>
        public static Register_ RDI { get; } = new Register_(0x07, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The eighth 64-bit SIMD register.
        /// </summary>
        public static Register_ MM7 { get; } = new Register_(0x07, RegisterType.Simd64Bit);
        /// <summary>
        /// The eighth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM7 { get; } = new Register_(0x07, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The eighth control register.
        /// </summary>
        public static Register_ CR7 { get; } = new Register_(0x07, RegisterType.Control);
        /// <summary>
        /// The eighth debug register.
        /// </summary>
        public static Register_ DR7 { get; } = new Register_(0x07, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 7.
        /// </summary>
        public static Register_ ST7 { get; } = new Register_(0x07, RegisterType.FloatingPoint);



        /// <summary>
        /// The lower 8-bits of the nineth general purpose register.
        /// </summary>
        public static Register_ R8L { get; } = new Register_(0x08, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the nineth general purpose register.
        /// </summary>
        public static Register_ R8W { get; } = new Register_(0x08, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the nineth general purpose register.
        /// </summary>
        public static Register_ R8D { get; } = new Register_(0x08, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the nineth general purpose register.
        /// </summary>
        public static Register_ R8 { get; } = new Register_(0x08, RegisterType.GeneralPurpose64Bit);
        // MM0
        /// <summary>
        /// The nineth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM8 { get; } = new Register_(0x08, RegisterType.Simd128Bit);
        // ES
        /// <summary>
        /// The nineth control register.
        /// </summary>
        public static Register_ CR8 { get; } = new Register_(0x08, RegisterType.Control);
        /// <summary>
        /// The nineth debug register.
        /// </summary>
        public static Register_ DR8 { get; } = new Register_(0x08, RegisterType.Debug);
        // Floating point stack register.


        /// <summary>
        /// The lower 8-bits of the tenth general purpose register.
        /// </summary>
        public static Register_ R9L { get; } = new Register_(0x09, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the tenth general purpose register.
        /// </summary>
        public static Register_ R9W { get; } = new Register_(0x09, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the tenth general purpose register.
        /// </summary>
        public static Register_ R9D { get; } = new Register_(0x09, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the tenth general purpose register.
        /// </summary>
        public static Register_ R9 { get; } = new Register_(0x09, RegisterType.GeneralPurpose64Bit);
        // MM1
        /// <summary>
        /// The tenth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM9 { get; } = new Register_(0x09, RegisterType.Simd128Bit);
        // CS
        /// <summary>
        /// The tenth control register.
        /// </summary>
        public static Register_ CR9 { get; } = new Register_(0x09, RegisterType.Control);
        /// <summary>
        /// The tenth debug register.
        /// </summary>
        public static Register_ DR9 { get; } = new Register_(0x09, RegisterType.Debug);
        // Floating point stack register.



        /// <summary>
        /// The lower 8-bits of the eleventh general purpose register.
        /// </summary>
        public static Register_ R10L { get; } = new Register_(0x0A, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the eleventh general purpose register.
        /// </summary>
        public static Register_ R10W { get; } = new Register_(0x0A, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the eleventh general purpose register.
        /// </summary>
        public static Register_ R10D { get; } = new Register_(0x0A, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the eleventh general purpose register.
        /// </summary>
        public static Register_ R10 { get; } = new Register_(0x0A, RegisterType.GeneralPurpose64Bit);
        // MM2
        /// <summary>
        /// The eleventh 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM10 { get; } = new Register_(0x0A, RegisterType.Simd128Bit);
        // SS
        /// <summary>
        /// The eleventh control register.
        /// </summary>
        public static Register_ CR10 { get; } = new Register_(0x0A, RegisterType.Control);
        /// <summary>
        /// The eleventh debug register.
        /// </summary>
        public static Register_ DR10 { get; } = new Register_(0x0A, RegisterType.Debug);
        // Floating point stack register.


        /// <summary>
        /// The lower 8-bits of the twelfth general purpose register.
        /// </summary>
        public static Register_ R11L { get; } = new Register_(0x0B, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the twelfth general purpose register.
        /// </summary>
        public static Register_ R11W { get; } = new Register_(0x0B, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the twelfth general purpose register.
        /// </summary>
        public static Register_ R11D { get; } = new Register_(0x0B, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the twelfth general purpose register.
        /// </summary>
        public static Register_ R11 { get; } = new Register_(0x0B, RegisterType.GeneralPurpose64Bit);
        // MM3
        /// <summary>
        /// The twelfth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM11 { get; } = new Register_(0x0B, RegisterType.Simd128Bit);
        // DS
        /// <summary>
        /// The twelfth control register.
        /// </summary>
        public static Register_ CR11 { get; } = new Register_(0x0B, RegisterType.Control);
        /// <summary>
        /// The twelfth debug register.
        /// </summary>
        public static Register_ DR11 { get; } = new Register_(0x0B, RegisterType.Debug);
        // Floating point stack register.




        /// <summary>
        /// The lower 8-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register_ R12L { get; } = new Register_(0x0C, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register_ R12W { get; } = new Register_(0x0C, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register_ R12D { get; } = new Register_(0x0C, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register_ R12 { get; } = new Register_(0x0C, RegisterType.GeneralPurpose64Bit);
        // MM4
        /// <summary>
        /// The thirteenth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM12 { get; } = new Register_(0x0C, RegisterType.Simd128Bit);
        // FS
        /// <summary>
        /// The thirteenth control register.
        /// </summary>
        public static Register_ CR12 { get; } = new Register_(0x0C, RegisterType.Control);
        /// <summary>
        /// The thirteenth debug register.
        /// </summary>
        public static Register_ DR12 { get; } = new Register_(0x0C, RegisterType.Debug);
        // Floating point stack register.



        /// <summary>
        /// The lower 8-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register_ R13L { get; } = new Register_(0x0D, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register_ R13W { get; } = new Register_(0x0D, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register_ R13D { get; } = new Register_(0x0D, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register_ R13 { get; } = new Register_(0x0D, RegisterType.GeneralPurpose64Bit);
        // MM5
        /// <summary>
        /// The fourteenth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM13 { get; } = new Register_(0x0D, RegisterType.Simd128Bit);
        // GS
        /// <summary>
        /// The fourteenth control register.
        /// </summary>
        public static Register_ CR13 { get; } = new Register_(0x0D, RegisterType.Control);
        /// <summary>
        /// The fourteenth debug register.
        /// </summary>
        public static Register_ DR13 { get; } = new Register_(0x0D, RegisterType.Debug);
        // Floating point stack register.



        /// <summary>
        /// The lower 8-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register_ R14L { get; } = new Register_(0x0E, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register_ R14W { get; } = new Register_(0x0E, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register_ R14D { get; } = new Register_(0x0E, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register_ R14 { get; } = new Register_(0x0E, RegisterType.GeneralPurpose64Bit);
        // MM6
        /// <summary>
        /// The fifteenth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM14 { get; } = new Register_(0x0E, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The fifteenth control register.
        /// </summary>
        public static Register_ CR14 { get; } = new Register_(0x0E, RegisterType.Control);
        /// <summary>
        /// The fifteenth debug register.
        /// </summary>
        public static Register_ DR14 { get; } = new Register_(0x0E, RegisterType.Debug);
        // Floating point stack register.



        /// <summary>
        /// The lower 8-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register_ R15L { get; } = new Register_(0x0F, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register_ R15W { get; } = new Register_(0x0F, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register_ R15D { get; } = new Register_(0x0F, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register_ R15 { get; } = new Register_(0x0F, RegisterType.GeneralPurpose64Bit);
        // MM7
        /// <summary>
        /// The sixteenth 128-bit SIMD register.
        /// </summary>
        public static Register_ XMM15 { get; } = new Register_(0x0F, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The sixteenth control register.
        /// </summary>
        public static Register_ CR15 { get; } = new Register_(0x0F, RegisterType.Control);
        /// <summary>
        /// The sixteenth debug register.
        /// </summary>
        public static Register_ DR15 { get; } = new Register_(0x0F, RegisterType.Debug);
        // Floating point stack register.
    }
}
