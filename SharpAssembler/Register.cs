using SharpAssembler.Architectures.X86;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// An x86-64 register.
    /// </summary>
    public class Register
    {
        /// <summary>
        /// Return register name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Return register value.
        /// </summary>
        public byte Value { get; private set; }

        /// <summary>
        /// Return register type <see cref="RegisterType"/>
        /// </summary>
        public RegisterType Type { get; private set; }

        /// <summary>
        /// Returns the register size <see cref="DataSize"/>.
        /// </summary>
        public DataSize Size { get { return (DataSize)((int)Type & 0x3F); } }

        /// <summary>
        /// Return full value.
        /// </summary>
        public int Full { get { return Value | (int)Type << 5; } }

        /// <summary>
        /// Cretae new instance of <see cref="Register"/>
        /// </summary>
        /// <param name="name">Register name.</param>
        /// <param name="value">Register value.</param>
        /// <param name="type">Register type <see cref="RegisterType"/></param>
        public Register(string name, byte value, RegisterType type)
        {
            Name  = name;
            Value = value;
            Type  = type;
        }

        /// <summary>
        /// Returns whether the register is a general purpose register.
        /// </summary>
        public bool IsGeneralPurpose
        {
            get { return Type == RegisterType.GeneralPurpose; }
        }

        /// <summary>
        /// Returns whether the register is a control register.
        /// </summary>
        public bool IsControl
        {
            get { return Type == RegisterType.Control; }
        }

        /// <summary>
        /// Returns whether the register is a debug register.
        /// </summary>
        public bool IsDebug
        {
            get { return Type == RegisterType.Debug; }
        }

        /// <summary>
        /// Returns whether the register is an x87 floating-point register.
        /// </summary>
        public bool IsFloatingPoint
        {
            get { return Type == RegisterType.FloatingPoint; }
        }

        /// <summary>
        /// Returns whether the register is a segment register.
        /// </summary>
        public bool IsSegment
        {
            get { return Type == RegisterType.Segment; }
        }

        /// <summary>
        /// Returns whether the register is a SIMD register.
        /// </summary>
        public bool IsSimd
        {
            get { return Type == RegisterType.Simd64Bit || Type == RegisterType.Simd128Bit; }
        }

        /// <summary>
        /// Empty register.
        /// </summary>
        public readonly static Register None = new Register("<none>", 0, RegisterType.None);
        /// <summary>
        /// The lower 8-bits of the accumulator register.
        /// </summary>
        public static Register AL { get; } = new Register(nameof(AL), 0x00, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the accumulator register.
        /// </summary>
        public static Register AX { get; } = new Register(nameof(AX), 0x00, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the accumulator register.
        /// </summary>
        public static Register EAX { get; } = new Register(nameof(EAX), 0x00, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the accumulator register.
        /// </summary>
        public static Register RAX { get; } = new Register(nameof(RAX), 0x00, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The first 64-bit SIMD register.
        /// </summary>
        public static Register MM0 { get; } = new Register(nameof(MM0), 0x00, RegisterType.Simd64Bit);
        /// <summary>
        /// The first 128-bit SIMD register.
        /// </summary>
        public static Register XMM0 { get; } = new Register(nameof(XMM0), 0x00, RegisterType.Simd128Bit);
        /// <summary>
        /// The extra segment register.
        /// </summary>
        public static Register ES { get; } = new Register(nameof(ES), 0x00, RegisterType.Segment);
        /// <summary>
        /// The first control register.
        /// </summary>
        public static Register CR0 { get; } = new Register(nameof(CR0), 0x00, RegisterType.Control);
        /// <summary>
        /// The first debug register.
        /// </summary>
        public static Register DR0 { get; } = new Register(nameof(DR0), 0x00, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 0.
        /// </summary>
        public static Register ST0 { get; } = new Register(nameof(ST0), 0x00, RegisterType.FloatingPoint);
        /// <summary>
        /// The lower 8-bits of the counter register.
        /// </summary>
        public static Register CL { get; } = new Register(nameof(CL), 0x01, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the counter register.
        /// </summary>
        public static Register CX { get; } = new Register(nameof(CX), 0x01, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the counter register.
        /// </summary>
        public static Register ECX { get; } = new Register(nameof(ECX), 0x01, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the counter register.
        /// </summary>
        public static Register RCX { get; } = new Register(nameof(RCX), 0x01, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The second 64-bit SIMD register.
        /// </summary>
        public static Register MM1 { get; } = new Register(nameof(MM1), 0x01, RegisterType.Simd64Bit);
        /// <summary>
        /// The second 128-bit SIMD register.
        /// </summary>
        public static Register XMM1 { get; } = new Register(nameof(XMM1), 0x01, RegisterType.Simd128Bit);
        /// <summary>
        /// The code segment register.
        /// </summary>
        public static Register CS { get; } = new Register(nameof(CS), 0x01, RegisterType.Segment);
        /// <summary>
        /// The second control register.
        /// </summary>
        public static Register CR1 { get; } = new Register(nameof(CR1), 0x01, RegisterType.Control);
        /// <summary>
        /// The second debug register.
        /// </summary>
        public static Register DR1 { get; } = new Register(nameof(DR1), 0x01, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 1.
        /// </summary>
        public static Register ST1 { get; } = new Register(nameof(ST1), 0x01, RegisterType.FloatingPoint);
        /// <summary>
        /// The lower 8-bits of the data register.
        /// </summary>
        public static Register DL { get; } = new Register(nameof(DL), 0x02, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the data register.
        /// </summary>
        public static Register DX { get; } = new Register(nameof(DX), 0x02, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the data register.
        /// </summary>
        public static Register EDX { get; } = new Register(nameof(EDX), 0x02, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the data register.
        /// </summary>
        public static Register RDX { get; } = new Register(nameof(RDX), 0x02, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The third 64-bit SIMD register.
        /// </summary>
        public static Register MM2 { get; } = new Register(nameof(MM2), 0x02, RegisterType.Simd64Bit);
        /// <summary>
        /// The third 128-bit SIMD register.
        /// </summary>
        public static Register XMM2 { get; } = new Register(nameof(XMM2), 0x02, RegisterType.Simd128Bit);
        /// <summary>
        /// The stack segment register.
        /// </summary>
        public static Register SS { get; } = new Register(nameof(SS), 0x02, RegisterType.Segment);
        /// <summary>
        /// The third control register.
        /// </summary>
        public static Register CR2 { get; } = new Register(nameof(CR2), 0x02, RegisterType.Control);
        /// <summary>
        /// The third debug register.
        /// </summary>
        public static Register DR2 { get; } = new Register(nameof(DR2), 0x02, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 2.
        /// </summary>
        public static Register ST2 { get; } = new Register(nameof(ST2), 0x02, RegisterType.FloatingPoint);
        /// <summary>
        /// The lower 8-bits of the base register.
        /// </summary>
        public static Register BL { get; } = new Register(nameof(BL), 0x03, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the base register.
        /// </summary>
        public static Register BX { get; } = new Register(nameof(BX), 0x03, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the base register.
        /// </summary>
        public static Register EBX { get; } = new Register(nameof(EBX), 0x03, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the base register.
        /// </summary>
        public static Register RBX { get; } = new Register(nameof(RBX), 0x03, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fourth 64-bit SIMD register.
        /// </summary>
        public static Register MM3 { get; } = new Register(nameof(MM3), 0x03, RegisterType.Simd64Bit);
        /// <summary>
        /// The fourth 128-bit SIMD register.
        /// </summary>
        public static Register XMM3 { get; } = new Register(nameof(XMM3), 0x03, RegisterType.Simd128Bit);
        /// <summary>
        /// The data segment register.
        /// </summary>
        public static Register DS { get; } = new Register(nameof(DS), 0x03, RegisterType.Segment);
        /// <summary>
        /// The fourth control register.
        /// </summary>
        public static Register CR3 { get; } = new Register(nameof(CR3), 0x03, RegisterType.Control);
        /// <summary>
        /// The fourth debug register.
        /// </summary>
        public static Register DR3 { get; } = new Register(nameof(DR3), 0x03, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 3.
        /// </summary>
        public static Register ST3 { get; } = new Register(nameof(ST3), 0x03, RegisterType.FloatingPoint);
        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the accumulator register.
        /// </summary>
        public static Register AH { get; } = new Register(nameof(AH), 0x04, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the stack pointer register.
        /// </summary>
        public static Register SP { get; } = new Register(nameof(SP), 0x04, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the stack pointer register.
        /// </summary>
        public static Register ESP { get; } = new Register(nameof(ESP), 0x04, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the stack pointer register.
        /// </summary>
        public static Register RSP { get; } = new Register(nameof(RSP), 0x04, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fifth 64-bit SIMD register.
        /// </summary>
        public static Register MM4 { get; } = new Register(nameof(MM4), 0x04, RegisterType.Simd64Bit);
        /// <summary>
        /// The fifth 128-bit SIMD register.
        /// </summary>
        public static Register XMM4 { get; } = new Register(nameof(XMM4), 0x04, RegisterType.Simd128Bit);
        /// <summary>
        /// The second extra segment register.
        /// </summary>
        public static Register FS { get; } = new Register(nameof(FS), 0x04, RegisterType.Segment);
        /// <summary>
        /// The fifth control register.
        /// </summary>
        public static Register CR4 { get; } = new Register(nameof(CR4), 0x04, RegisterType.Control);
        /// <summary>
        /// The fifth debug register.
        /// </summary>
        public static Register DR4 { get; } = new Register(nameof(DR4), 0x04, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 4.
        /// </summary>
        public static Register ST4 { get; } = new Register(nameof(ST4), 0x04, RegisterType.FloatingPoint);
        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the counter register.
        /// </summary>
        public static Register CH { get; } = new Register(nameof(CH), 0x05, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the base pointer register.
        /// </summary>
        public static Register BP { get; } = new Register(nameof(BP), 0x05, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the base pointer register.
        /// </summary>
        public static Register EBP { get; } = new Register(nameof(EBP), 0x05, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the base pointer register.
        /// </summary>
        public static Register RBP { get; } = new Register(nameof(RBP), 0x05, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The sixth 64-bit SIMD register.
        /// </summary>
        public static Register MM5 { get; } = new Register(nameof(MM5), 0x05, RegisterType.Simd64Bit);
        /// <summary>
        /// The sixth 128-bit SIMD register.
        /// </summary>
        public static Register XMM5 { get; } = new Register(nameof(XMM5), 0x05, RegisterType.Simd128Bit);
        /// <summary>
        /// The third extra segment register.
        /// </summary>
        public static Register GS { get; } = new Register(nameof(GS), 0x05, RegisterType.Segment);
        /// <summary>
        /// The sixth control register.
        /// </summary>
        public static Register CR5 { get; } = new Register(nameof(CR5), 0x05, RegisterType.Control);
        /// <summary>
        /// The sixth debug register.
        /// </summary>
        public static Register DR5 { get; } = new Register(nameof(DR5), 0x05, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 5.
        /// </summary>
        public static Register ST5 { get; } = new Register(nameof(ST5), 0x05, RegisterType.FloatingPoint);
        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the data register.
        /// </summary>
        public static Register DH { get; } = new Register(nameof(DH), 0x06, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the source index register.
        /// </summary>
        public static Register SI { get; } = new Register(nameof(SI), 0x06, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the source index register.
        /// </summary>
        public static Register ESI { get; } = new Register(nameof(ESI), 0x06, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the source index register.
        /// </summary>
        public static Register RSI { get; } = new Register(nameof(RSI), 0x06, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The seventh 64-bit SIMD register.
        /// </summary>
        public static Register MM6 { get; } = new Register(nameof(MM6), 0x06, RegisterType.Simd64Bit);
        /// <summary>
        /// The seventh 128-bit SIMD register.
        /// </summary>
        public static Register XMM6 { get; } = new Register(nameof(XMM6), 0x06, RegisterType.Simd128Bit);
        /// <summary>
        /// The seventh control register.
        /// </summary>
        public static Register CR6 { get; } = new Register(nameof(CR6), 0x06, RegisterType.Control);
        /// <summary>
        /// The seventh debug register.
        /// </summary>
        public static Register DR6 { get; } = new Register(nameof(DR6), 0x06, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 6.
        /// </summary>
        public static Register ST6 { get; } = new Register(nameof(ST6), 0x06, RegisterType.FloatingPoint);
        /// <summary>
        /// The higher 8-bits of the lower 16-bits of the base register.
        /// </summary>
        public static Register BH { get; } = new Register(nameof(BH), 0x07, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the destination index register.
        /// </summary>
        public static Register DI { get; } = new Register(nameof(DI), 0x07, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the destination index register.
        /// </summary>
        public static Register EDI { get; } = new Register(nameof(EDI), 0x07, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the destination index register.
        /// </summary>
        public static Register RDI { get; } = new Register(nameof(RDI), 0x07, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The eighth 64-bit SIMD register.
        /// </summary>
        public static Register MM7 { get; } = new Register(nameof(MM7), 0x07, RegisterType.Simd64Bit);
        /// <summary>
        /// The eighth 128-bit SIMD register.
        /// </summary>
        public static Register XMM7 { get; } = new Register(nameof(XMM7), 0x07, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The eighth control register.
        /// </summary>
        public static Register CR7 { get; } = new Register(nameof(CR7), 0x07, RegisterType.Control);
        /// <summary>
        /// The eighth debug register.
        /// </summary>
        public static Register DR7 { get; } = new Register(nameof(DR7), 0x07, RegisterType.Debug);
        /// <summary>
        /// The floating point stack register index 7.
        /// </summary>
        public static Register ST7 { get; } = new Register(nameof(ST7), 0x07, RegisterType.FloatingPoint);
        /// <summary>
        /// The lower 8-bits of the nineth general purpose register.
        /// </summary>
        public static Register R8L { get; } = new Register(nameof(R8L), 0x08, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the nineth general purpose register.
        /// </summary>
        public static Register R8W { get; } = new Register(nameof(R8W), 0x08, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the nineth general purpose register.
        /// </summary>
        public static Register R8D { get; } = new Register(nameof(R8D), 0x08, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the nineth general purpose register.
        /// </summary>
        public static Register R8 { get; } = new Register(nameof(R8), 0x08, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The nineth 128-bit SIMD register.
        /// </summary>
        public static Register XMM8 { get; } = new Register(nameof(XMM8), 0x08, RegisterType.Simd128Bit);
        /// <summary>
        /// The nineth control register.
        /// </summary>
        public static Register CR8 { get; } = new Register(nameof(CR8), 0x08, RegisterType.Control);
        /// <summary>
        /// The nineth debug register.
        /// </summary>
        public static Register DR8 { get; } = new Register(nameof(DR8), 0x08, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the tenth general purpose register.
        /// </summary>
        public static Register R9L { get; } = new Register(nameof(R9L), 0x09, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the tenth general purpose register.
        /// </summary>
        public static Register R9W { get; } = new Register(nameof(R9W), 0x09, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the tenth general purpose register.
        /// </summary>
        public static Register R9D { get; } = new Register(nameof(R9D), 0x09, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the tenth general purpose register.
        /// </summary>
        public static Register R9 { get; } = new Register(nameof(R9), 0x09, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The tenth 128-bit SIMD register.
        /// </summary>
        public static Register XMM9 { get; } = new Register(nameof(XMM9), 0x09, RegisterType.Simd128Bit);
        /// <summary>
        /// The tenth control register.
        /// </summary>
        public static Register CR9 { get; } = new Register(nameof(CR9), 0x09, RegisterType.Control);
        /// <summary>
        /// The tenth debug register.
        /// </summary>
        public static Register DR9 { get; } = new Register(nameof(DR9), 0x09, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the eleventh general purpose register.
        /// </summary>
        public static Register R10L { get; } = new Register(nameof(R10L), 0x0A, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the eleventh general purpose register.
        /// </summary>
        public static Register R10W { get; } = new Register(nameof(R10W), 0x0A, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the eleventh general purpose register.
        /// </summary>
        public static Register R10D { get; } = new Register(nameof(R10D), 0x0A, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the eleventh general purpose register.
        /// </summary>
        public static Register R10 { get; } = new Register(nameof(R10), 0x0A, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The eleventh 128-bit SIMD register.
        /// </summary>
        public static Register XMM10 { get; } = new Register(nameof(XMM10), 0x0A, RegisterType.Simd128Bit);
        /// <summary>
        /// The eleventh control register.
        /// </summary>
        public static Register CR10 { get; } = new Register(nameof(CR10), 0x0A, RegisterType.Control);
        /// <summary>
        /// The eleventh debug register.
        /// </summary>
        public static Register DR10 { get; } = new Register(nameof(DR10), 0x0A, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the twelfth general purpose register.
        /// </summary>
        public static Register R11L { get; } = new Register(nameof(R11L), 0x0B, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the twelfth general purpose register.
        /// </summary>
        public static Register R11W { get; } = new Register(nameof(R11W), 0x0B, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the twelfth general purpose register.
        /// </summary>
        public static Register R11D { get; } = new Register(nameof(R11D), 0x0B, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the twelfth general purpose register.
        /// </summary>
        public static Register R11 { get; } = new Register(nameof(R11), 0x0B, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The twelfth 128-bit SIMD register.
        /// </summary>
        public static Register XMM11 { get; } = new Register(nameof(XMM11), 0x0B, RegisterType.Simd128Bit);
        /// <summary>
        /// The twelfth control register.
        /// </summary>
        public static Register CR11 { get; } = new Register(nameof(CR11), 0x0B, RegisterType.Control);
        /// <summary>
        /// The twelfth debug register.
        /// </summary>
        public static Register DR11 { get; } = new Register(nameof(DR11), 0x0B, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register R12L { get; } = new Register(nameof(R12L), 0x0C, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register R12W { get; } = new Register(nameof(R12W), 0x0C, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register R12D { get; } = new Register(nameof(R12D), 0x0C, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the thirteenth general purpose register.
        /// </summary>
        public static Register R12 { get; } = new Register(nameof(R12), 0x0C, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The thirteenth 128-bit SIMD register.
        /// </summary>
        public static Register XMM12 { get; } = new Register(nameof(XMM12), 0x0C, RegisterType.Simd128Bit);
        /// <summary>
        /// The thirteenth control register.
        /// </summary>
        public static Register CR12 { get; } = new Register(nameof(CR12), 0x0C, RegisterType.Control);
        /// <summary>
        /// The thirteenth debug register.
        /// </summary>
        public static Register DR12 { get; } = new Register(nameof(DR12), 0x0C, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register R13L { get; } = new Register(nameof(R13L), 0x0D, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register R13W { get; } = new Register(nameof(R13W), 0x0D, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register R13D { get; } = new Register(nameof(R13D), 0x0D, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the fourteenth general purpose register.
        /// </summary>
        public static Register R13 { get; } = new Register(nameof(R13), 0x0D, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fourteenth 128-bit SIMD register.
        /// </summary>
        public static Register XMM13 { get; } = new Register(nameof(XMM13), 0x0D, RegisterType.Simd128Bit);
        /// <summary>
        /// The fourteenth control register.
        /// </summary>
        public static Register CR13 { get; } = new Register(nameof(CR13), 0x0D, RegisterType.Control);
        /// <summary>
        /// The fourteenth debug register.
        /// </summary>
        public static Register DR13 { get; } = new Register(nameof(DR13), 0x0D, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register R14L { get; } = new Register(nameof(R14L), 0x0E, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register R14W { get; } = new Register(nameof(R14W), 0x0E, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register R14D { get; } = new Register(nameof(R14D), 0x0E, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the fifteenth general purpose register.
        /// </summary>
        public static Register R14 { get; } = new Register(nameof(R14), 0x0E, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The fifteenth 128-bit SIMD register.
        /// </summary>
        public static Register XMM14 { get; } = new Register(nameof(XMM14), 0x0E, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The fifteenth control register.
        /// </summary>
        public static Register CR14 { get; } = new Register(nameof(CR14), 0x0E, RegisterType.Control);
        /// <summary>
        /// The fifteenth debug register.
        /// </summary>
        public static Register DR14 { get; } = new Register(nameof(DR14), 0x0E, RegisterType.Debug);
        /// <summary>
        /// The lower 8-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register R15L { get; } = new Register(nameof(R15L), 0x0F, RegisterType.GeneralPurpose8Bit);
        /// <summary>
        /// The lower 16-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register R15W { get; } = new Register(nameof(R15W), 0x0F, RegisterType.GeneralPurpose16Bit);
        /// <summary>
        /// The lower 32-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register R15D { get; } = new Register(nameof(R15D), 0x0F, RegisterType.GeneralPurpose32Bit);
        /// <summary>
        /// The lower 64-bits of the sixteenth general purpose register.
        /// </summary>
        public static Register R15 { get; } = new Register(nameof(R15), 0x0F, RegisterType.GeneralPurpose64Bit);
        /// <summary>
        /// The sixteenth 128-bit SIMD register.
        /// </summary>
        public static Register XMM15 { get; } = new Register(nameof(XMM15), 0x0F, RegisterType.Simd128Bit);
        // Stack register
        /// <summary>
        /// The sixteenth control register.
        /// </summary>
        public static Register CR15 { get; } = new Register(nameof(CR15), 0x0F, RegisterType.Control);
        /// <summary>
        /// The sixteenth debug register.
        /// </summary>
        public static Register DR15 { get; } = new Register(nameof(DR15), 0x0F, RegisterType.Debug);
    }
}
