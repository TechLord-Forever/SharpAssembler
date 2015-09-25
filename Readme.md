SharpAssembler is a library that assembles machine instructions (e.g. x86-64) represented as managed .NET objects. It provides an intuitive object model that can be manipulated and emitted to various object file formats (e.g. bin, elf) from C# and other .NET languages.

Current status
==============
There is support for:
* x86-64 general instructions
* BIN file format
Soon there will be support for:
* x86-64 special instructions
* ELF file format

Installation instructions
=========================
Extract the ZIP archive somewhere, and reference the DLL files from your project. You'll need to reference at least the `Core` library (`SharpAssembler Core.dll`), and add anything you may want to use. The following add-on libraries are included:
* x86-64 architecture (in `SharpAssembler x86-64.dll`)
* BIN file format (in `SharpAssembler Bin File Format.dll`)

The libraries have Code Contracts applied to them. You are not required to support them, and it will still work. However, if you want to support them, you can get the Code Contracts Development Tools from the [Microsoft DevLabs website](http://msdn.microsoft.com/en-us/devlabs/dd491992).

Support
=======
You can get support at the [SharpAssembler website](https://sourceforge.net/projects/sharpassembler/). For anything from bugs, feature requests, suggestions, documentation errors, inconsistencies and things that are not clear you may create a new ticker or start a new forum discussion topic.

License
=======
SharpAssembler is licensed under GPL version 3. The intention is to make this library a contribution to the open source community. If you or your open source project have any specific license requirements, please contact us and we can talk about it.

Opcode List
=======
 * AAA -> ASCII Adjust After Addition
 * AAD -> ASCII Adjust AX Before Division
 * AAM -> ASCII Adjust AX After Multiply
 * AAS -> ASCII Adjust AL After Subtraction
 * ADC -> Add with Carry
 * ADCX -> Unsigned Integer Addition of Two Operands with Carry Flag
 * ADD -> Add
 * ADDPD -> Add Packed Double-Precision Floating-Point Values
 * ADDPS -> Add Packed Single-Precision Floating-Point Values
 * ADDSD -> Add Scalar Double-Precision Floating-Point Values
 * ADDSS -> Add Scalar Single-Precision Floating-Point Values
 * ADDSUBPD -> Packed Double-FP Add/Subtract
 * ADDSUBPS -> Packed Single-FP Add/Subtract
 * ADOX -> Unsigned Integer Addition of Two Operands with Overflow Flag
 * AESDEC -> Perform One Round of an AES Decryption Flow
 * AESDECLAST -> Perform Last Round of an AES Decryption Flow
 * AESENC -> Perform One Round of an AES Encryption Flow
 * AESENCLAST -> Perform Last Round of an AES Encryption Flow
 * AESIMC -> Perform the AES InvMixColumn Transformation
 * AESKEYGENASSIST -> AES Round Key Generation Assist
 * AND -> Logical AND
 * ANDN -> Logical AND NOT
 * ANDPD -> Bitwise Logical AND of Packed Double-Precision Floating-Point Values
 * ANDPS -> Bitwise Logical AND of Packed Single-Precision Floating-Point Values
 * ANDNPD -> Bitwise Logical AND NOT of Packed Double-Precision Floating-Point Values
 * ANDNPS -> Bitwise Logical AND NOT of Packed Single-Precision Floating-Point Values
 * ARPL -> Adjust RPL Field of Segment Selector
 * BLENDPD -> Blend Packed Double Precision Floating-Point Values
 * BEXTR -> Bit Field Extract
 * BLENDPS -> Blend Packed Single Precision Floating-Point Values
 * BLENDVPD -> Variable Blend Packed Double Precision Floating-Point Values
 * BLENDVPS -> Variable Blend Packed Single Precision Floating-Point Values
 * BLSI -> Extract Lowest Set Isolated Bit
 * BLSMSK -> Get Mask Up to Lowest Set Bit
 * BLSR -> Reset Lowest Set Bit
 * BOUND -> Check Array Index Against Bounds
 * BSF -> Bit Scan Forward
 * BSR -> Bit Scan Reverse
 * BSWAP -> Byte Swap
 * BT -> Bit Test
 * BTC -> Bit Test and Complement
 * BTR -> Bit Test and Reset
 * BTS -> Bit Test and Set
 * BZHI -> Zero High Bits Starting with Specified Bit Position
 * CALL -> Call Procedure
 * CBW/CWDE/CDQE -> Convert Byte to Word/Convert Word to Doubleword/Convert Doubleword to Quadword
 * CLAC -> Clear AC Flag in EFLAGS Register
 * CLC -> Clear Carry Flag
 * CLD -> Clear Direction Flag
 * CLFLUSH -> Flush Cache Line
 * CLI -> Clear Interrupt Flag
 * CLTS -> Clear Task-Switched Flag in CR0
 * CMC -> Complement Carry Flag
 * CMOVcc -> Conditional Move
 * CMP -> Compare Two Operands
 * CMPPD -> Compare Packed Double-Precision Floating-Point Values
 * CMPPS -> Compare Packed Single-Precision Floating-Point Values
 * CMPS/CMPSB/CMPSW/CMPSD/CMPSQ -> Compare String Operands
 * CMPSD -> Compare Scalar Double-Precision Floating-Point Values
 * CMPSS -> Compare Scalar Single-Precision Floating-Point Values
 * CMPXCHG -> Compare and Exchange
 * CMPXCHG8B/CMPXCHG16B -> Compare and Exchange Bytes
 * COMISD -> Compare Scalar Ordered Double-Precision Floating-Point Values and Set EFLAGS
 * COMISS -> Compare Scalar Ordered Single-Precision Floating-Point Values and Set EFLAGS
 * CPUID -> CPU Identification
 * CRC32 -> Accumulate CRC32 Value
 * CVTDQ2PD -> Convert Packed Dword Integers to Packed Double-Precision FP Values
 * CVTDQ2PS -> Convert Packed Dword Integers to Packed Single-Precision FP Values
 * CVTPD2DQ -> Convert Packed Double-Precision FP Values to Packed Dword Integers
 * CVTPD2PI -> Convert Packed Double-Precision FP Values to Packed Dword Integers
 * CVTPD2PS -> Convert Packed Double-Precision FP Values to Packed Single-Precision FP Values
 * CVTPI2PD -> Convert Packed Dword Integers to Packed Double-Precision FP Values
 * CVTPI2PS -> Convert Packed Dword Integers to Packed Single-Precision FP Values
 * CVTPS2DQ -> Convert Packed Single-Precision FP Values to Packed Dword Integers
 * CVTPS2PD -> Convert Packed Single-Precision FP Values to Packed Double-Precision FP Values
 * CVTPS2PI -> Convert Packed Single-Precision FP Values to Packed Dword Integers
 * CVTSD2SI -> Convert Scalar Double-Precision FP Value to Integer
 * CVTSD2SS -> Convert Scalar Double-Precision FP Value to Scalar Single-Precision FP Value
 * CVTSI2SD -> Convert Dword Integer to Scalar Double-Precision FP Value
 * CVTSI2SS -> Convert Dword Integer to Scalar Single-Precision FP Value
 * CVTSS2SD -> Convert Scalar Single-Precision FP Value to Scalar Double-Precision FP Value
 * CVTSS2SI -> Convert Scalar Single-Precision FP Value to Dword Integer
 * CVTTPD2DQ -> Convert with Truncation Packed Double-Precision FP Values to Packed Dword Integers
 * CVTTPD2PI -> Convert with Truncation Packed Double-Precision FP Values to Packed Dword Integers
 * CVTTPS2DQ -> Convert with Truncation Packed Single-Precision FP Values to Packed Dword Integers
 * CVTTPS2PI -> Convert with Truncation Packed Single-Precision FP Values to Packed Dword Integers
 * CVTTSD2SI -> Convert with Truncation Scalar Double-Precision FP Value to Signed Integer
 * CVTTSS2SI -> Convert with Truncation Scalar Single-Precision FP Value to Dword Integer
 * CWD/CDQ/CQO -> Convert Word to Doubleword/Convert Doubleword to Quadword
 * DAA -> Decimal Adjust AL after Addition
 * DAS -> Decimal Adjust AL after Subtraction
 * DEC -> Decrement by 1
 * DIV -> Unsigned Divide
 * DIVPD -> Divide Packed Double-Precision Floating-Point Values
 * DIVPS -> Divide Packed Single-Precision Floating-Point Values
 * DIVSD -> Divide Scalar Double-Precision Floating-Point Values
 * DIVSS -> Divide Scalar Single-Precision Floating-Point Values
 * DPPD -> Dot Product of Packed Double Precision Floating-Point Values
 * DPPS -> Dot Product of Packed Single Precision Floating-Point Values
 * EMMS -> Empty MMX Technology State
 * ENTER -> Make Stack Frame for Procedure Parameters
 * EXTRACTPS -> Extract Packed Single Precision Floating-Point Value
 * F2XM1 -> Compute 2x–1
 * FABS -> Absolute Value
 * FADD/FADDP/FIADD -> Add
 * FBLD -> Load Binary Coded Decimal
 * FBSTP -> Store BCD Integer and Pop
 * FCHS -> Change Sign
 * FCLEX/FNCLEX -> Clear Exceptions
 * FCMOVcc -> Floating-Point Conditional Move
 * FCOM/FCOMP/FCOMPP -> Compare Floating Point Values
 * FCOMI/FCOMIP/ FUCOMI/FUCOMIP -> Compare Floating Point Values and Set EFLAGS
 * FCOS -> Cosine
 * FDECSTP -> Decrement Stack-Top Pointer
 * FDIV/FDIVP/FIDIV -> Divide
 * FDIVR/FDIVRP/FIDIVR -> Reverse Divide
 * FFREE -> Free Floating-Point Register
 * FICOM/FICOMP -> Compare Integer
 * FILD -> Load Integer
 * FINCSTP -> Increment Stack-Top Pointer
 * FINIT/FNINIT -> Initialize Floating-Point Unit
 * FIST/FISTP -> Store Integer
 * FISTTP -> Store Integer with Truncation
 * FLD -> Load Floating Point Value
 * FLD1/FLDL2T/FLDL2E/FLDPI/FLDLG2/FLDLN2/FLDZ -> Load Constant
 * FLDCW -> Load x87 FPU Control Word
 * FLDENV -> Load x87 FPU Environment
 * FMUL/FMULP/FIMUL -> Multiply
 * FNOP -> No Operation
 * FPATAN -> Partial Arctangent
 * FPREM -> Partial Remainder
 * FPREM1 -> Partial Remainder
 * FPTAN -> Partial Tangent
 * FRNDINT -> Round to Integer
 * FRSTOR -> Restore x87 FPU State
 * FSAVE/FNSAVE -> Store x87 FPU State
 * FSCALE -> Scale
 * FSIN -> Sine
 * FSINCOS -> Sine and Cosine
 * FSQRT -> Square Root
 * FST/FSTP -> Store Floating Point Value
 * FSTCW/FNSTCW -> Store x87 FPU Control Word
 * FSTENV/FNSTENV -> Store x87 FPU Environment
 * FSTSW/FNSTSW -> Store x87 FPU Status Word
 * FSUB/FSUBP/FISUB -> Subtract
 * FSUBR/FSUBRP/FISUBR -> Reverse Subtract
 * FTST -> TEST
 * FUCOM/FUCOMP/FUCOMPP -> Unordered Compare Floating Point Values
 * FXAM -> Examine ModR/M
 * FXCH -> Exchange Register Contents
 * FXRSTOR -> Restore x87 FPU, MMX, XMM, and MXCSR State
 * FXSAVE -> Save x87 FPU, MMX Technology, and SSE State
 * FXTRACT -> Extract Exponent and Significand
 * FYL2X -> Compute y * log2x
 * FYL2XP1 -> Compute y * log2(x +1)
 * HADDPD -> Packed Double-FP Horizontal Add
 * HADDPS -> Packed Single-FP Horizontal Add
 * HLT -> Halt
 * HSUBPD -> Packed Double-FP Horizontal Subtract
 * HSUBPS -> Packed Single-FP Horizontal Subtract
 * IDIV -> Signed Divide
 * IMUL -> Signed Multiply
 * IN -> Input from Port
 * INC -> Increment by 1
 * INS/INSB/INSW/INSD -> Input from Port to String
 * INSERTPS -> Insert Packed Single Precision Floating-Point Value
 * INT n/INTO/INT 3 -> Call to Interrupt Procedure
 * INVD -> Invalidate Internal Caches
 * INVLPG -> Invalidate TLB Entries
 * INVPCID -> Invalidate Process-Context Identifier
 * IRET/IRETD -> Interrupt Return
 * Jcc -> Jump if Condition Is Met
 * JMP -> Jump
 * LAHF -> Load Status Flags into AH Register
 * LAR -> Load Access Rights Byte
 * LDDQU -> Load Unaligned Integer 128 Bits
 * LDMXCSR -> Load MXCSR Register
 * LDS/LES/LFS/LGS/LSS -> Load Far Pointer
 * LEA -> Load Effective Address
 * LEAVE -> High Level Procedure Exit
 * LFENCE -> Load Fence
 * LGDT/LIDT -> Load Global/Interrupt Descriptor Table Register
 * LLDT -> Load Local Descriptor Table Register
 * LMSW -> Load Machine Status Word
 * LOCK -> Assert LOCK# Signal Prefix
 * LODS/LODSB/LODSW/LODSD/LODSQ -> Load String
 * LOOP/LOOPcc -> Loop According to ECX Counter
 * LSL -> Load Segment Limit
 * LTR -> Load Task Register
 * LZCNT -> Count the Number of Leading Zero Bits
 * MASKMOVDQU -> Store Selected Bytes of Double Quadword
 * MASKMOVQ -> Store Selected Bytes of Quadword
 * MAXPD -> Return Maximum Packed Double-Precision Floating-Point Values
 * MAXPS -> Return Maximum Packed Single-Precision Floating-Point Values
 * MAXSD -> Return Maximum Scalar Double-Precision Floating-Point Value
 * MAXSS -> Return Maximum Scalar Single-Precision Floating-Point Value
 * MFENCE -> Memory Fence
 * MINPD -> Return Minimum Packed Double-Precision Floating-Point Values
 * MINPS -> Return Minimum Packed Single-Precision Floating-Point Values
 * MINSD -> Return Minimum Scalar Double-Precision Floating-Point Value
 * MINSS -> Return Minimum Scalar Single-Precision Floating-Point Value
 * MONITOR -> Set Up Monitor Address
 * MOV -> Move
 * MOV -> Move to/from Control Registers
 * MOV -> Move to/from Debug Registers
 * MOVAPD -> Move Aligned Packed Double-Precision Floating-Point Values
 * MOVAPS -> Move Aligned Packed Single-Precision Floating-Point Values
 * MOVBE -> Move Data After Swapping Bytes
 * MOVD/MOVQ -> Move Doubleword/Move Quadword
 * MOVDDUP -> Move One Double-FP and Duplicate
 * MOVDQA -> Move Aligned Double Quadword
 * MOVDQU -> Move Unaligned Double Quadword
 * MOVDQ2Q -> Move Quadword from XMM to MMX Technology Register
 * MOVHLPS -> Move Packed Single-Precision Floating-Point Values High to Low
 * MOVHPD -> Move High Packed Double-Precision Floating-Point Value
 * MOVHPS -> Move High Packed Single-Precision Floating-Point Values
 * MOVLHPS -> Move Packed Single-Precision Floating-Point Values Low to High
 * MOVLPD -> Move Low Packed Double-Precision Floating-Point Value
 * MOVLPS -> Move Low Packed Single-Precision Floating-Point Values
 * MOVMSKPD -> Extract Packed Double-Precision Floating-Point Sign Mask
 * MOVMSKPS -> Extract Packed Single-Precision Floating-Point Sign Mask
 * MOVNTDQA -> Load Double Quadword Non-Temporal Aligned Hint
 * MOVNTDQ -> Store Double Quadword Using Non-Temporal Hint
 * MOVNTI -> Store Doubleword Using Non-Temporal Hint
 * MOVNTPD -> Store Packed Double-Precision Floating-Point Values Using Non-Temporal Hint
 * MOVNTPS -> Store Packed Single-Precision Floating-Point Values Using Non-Temporal Hint
 * MOVNTQ -> Store of Quadword Using Non-Temporal Hint
 * MOVQ -> Move Quadword
 * MOVQ2DQ -> Move Quadword from MMX Technology to XMM Register
 * MOVS/MOVSB/MOVSW/MOVSD/MOVSQ -> Move Data from String to String
 * MOVSD -> Move Scalar Double-Precision Floating-Point Value
 * MOVSHDUP -> Move Packed Single-FP High and Duplicate
 * MOVSLDUP -> Move Packed Single-FP Low and Duplicate
 * MOVSS -> Move Scalar Single-Precision Floating-Point Values
 * MOVSX/MOVSXD -> Move with Sign-Extension
 * MOVUPD -> Move Unaligned Packed Double-Precision Floating-Point Values
 * MOVUPS -> Move Unaligned Packed Single-Precision Floating-Point Values
 * MOVZX -> Move with Zero-Extend
 * MPSADBW -> Compute Multiple Packed Sums of Absolute Difference
 * MUL -> Unsigned Multiply
 * MULPD -> Multiply Packed Double-Precision Floating-Point Values
 * MULPS -> Multiply Packed Single-Precision Floating-Point Values
 * MULSD -> Multiply Scalar Double-Precision Floating-Point Values
 * MULSS -> Multiply Scalar Single-Precision Floating-Point Values
 * MULX -> Unsigned Multiply Without Affecting Flags
 * MWAIT -> Monitor Wait
 * NEG -> Two's Complement Negation
 * NOP -> No Operation
 * NOT -> One's Complement Negation
 * OR -> Logical Inclusive OR
 * ORPD -> Bitwise Logical OR of Double-Precision Floating-Point Values
 * ORPS -> Bitwise Logical OR of Single-Precision Floating-Point Values
 * OUT -> Output to Port
 * OUTS/OUTSB/OUTSW/OUTSD -> Output String to Port
 * PABSB/PABSW/PABSD -> Packed Absolute Value
 * PACKSSWB/PACKSSDW -> Pack with Signed Saturation
 * PACKUSDW -> Pack with Unsigned Saturation
 * PACKUSWB -> Pack with Unsigned Saturation
 * PADDB/PADDW/PADDD -> Add Packed Integers
 * PADDQ -> Add Packed Quadword Integers
 * PADDSB/PADDSW -> Add Packed Signed Integers with Signed Saturation
 * PADDUSB/PADDUSW -> Add Packed Unsigned Integers with Unsigned Saturation
 * PALIGNR -> Packed Align Right
 * PAND -> Logical AND
 * PANDN -> Logical AND NOT
 * PAUSE -> Spin Loop Hint
 * PAVGB/PAVGW -> Average Packed Integers
 * PBLENDVB -> Variable Blend Packed Bytes
 * PBLENDW -> Blend Packed Words
 * PCLMULQDQ - Carry-Less Multiplication Quadword 4-68
 * PCMPEQB/PCMPEQW/PCMPEQD -> Compare Packed Data for Equal
 * PCMPEQQ -> Compare Packed Qword Data for Equal
 * PCMPESTRI -> Packed Compare Explicit Length Strings, Return Index
 * PCMPESTRM -> Packed Compare Explicit Length Strings, Return Mask
 * PCMPGTB/PCMPGTW/PCMPGTD -> Compare Packed Signed Integers for Greater Than
 * PCMPGTQ -> Compare Packed Data for Greater Than
 * PCMPISTRI -> Packed Compare Implicit Length Strings, Return Index
 * PCMPISTRM -> Packed Compare Implicit Length Strings, Return Mask
 * PDEP -> Parallel Bits Deposit
 * PEXT -> Parallel Bits Extract
 * PEXTRB/PEXTRD/PEXTRQ -> Extract Byte/Dword/Qword
 * PEXTRW -> Extract Word
 * PHADDW/PHADDD -> Packed Horizontal Add
 * PHADDSW -> Packed Horizontal Add and Saturate
 * PHMINPOSUW -> Packed Horizontal Word Minimum
 * PHSUBW/PHSUBD -> Packed Horizontal Subtract
 * PHSUBSW -> Packed Horizontal Subtract and Saturate
 * PINSRB/PINSRD/PINSRQ -> Insert Byte/Dword/Qword
 * PINSRW -> Insert Word
 * PMADDUBSW -> Multiply and Add Packed Signed and Unsigned Bytes
 * PMADDWD -> Multiply and Add Packed Integers
 * PMAXSB -> Maximum of Packed Signed Byte Integers
 * PMAXSD -> Maximum of Packed Signed Dword Integers
 * PMAXSW -> Maximum of Packed Signed Word Integers
 * PMAXUB -> Maximum of Packed Unsigned Byte Integers
 * PMAXUD -> Maximum of Packed Unsigned Dword Integers
 * PMAXUW -> Maximum of Packed Word Integers
 * PMINSB -> Minimum of Packed Signed Byte Integers
 * PMINSD -> Minimum of Packed Dword Integers
 * PMINSW -> Minimum of Packed Signed Word Integers
 * PMINUB -> Minimum of Packed Unsigned Byte Integers
 * PMINUD -> Minimum of Packed Dword Integers
 * PMINUW -> Minimum of Packed Word Integers
 * PMOVMSKB -> Move Byte Mask
 * PMOVSX -> Packed Move with Sign Extend
 * PMOVZX -> Packed Move with Zero Extend
 * PMULDQ -> Multiply Packed Signed Dword Integers
 * PMULHRSW -> Packed Multiply High with Round and Scale
 * PMULHUW -> Multiply Packed Unsigned Integers and Store High Result
 * PMULHW -> Multiply Packed Signed Integers and Store High Result
 * PMULLD -> Multiply Packed Signed Dword Integers and Store Low Result
 * PMULLW -> Multiply Packed Signed Integers and Store Low Result
 * PMULUDQ -> Multiply Packed Unsigned Doubleword Integers
 * POP -> Pop a Value from the Stack
 * POPA/POPAD -> Pop All General-Purpose Registers
 * POPCNT -> Return the Count of Number of Bits Set to 1
 * POPF/POPFD/POPFQ -> Pop Stack into EFLAGS Register
 * POR -> Bitwise Logical OR
 * PREFETCHh -> Prefetch Data Into Caches
 * PREFETCHW -> Prefetch Data into Caches in Anticipation of a Write
 * PREFETCHWT1 -> Prefetch Vector Data Into Caches with Intent to Write and T1 Hint
 * PSADBW -> Compute Sum of Absolute Differences
 * PSHUFB -> Packed Shuffle Bytes
 * PSHUFD -> Shuffle Packed Doublewords
 * PSHUFHW -> Shuffle Packed High Words
 * PSHUFLW -> Shuffle Packed Low Words
 * PSHUFW -> Shuffle Packed Words
 * PSIGNB/PSIGNW/PSIGND -> Packed SIGN
 * PSLLDQ -> Shift Double Quadword Left Logical
 * PSLLW/PSLLD/PSLLQ -> Shift Packed Data Left Logical
 * PSRAW/PSRAD -> Shift Packed Data Right Arithmetic
 * PSRLDQ -> Shift Double Quadword Right Logical
 * PSRLW/PSRLD/PSRLQ -> Shift Packed Data Right Logical
 * PSUBB/PSUBW/PSUBD -> Subtract Packed Integers
 * PSUBQ -> Subtract Packed Quadword Integers
 * PSUBSB/PSUBSW -> Subtract Packed Signed Integers with Signed Saturation
 * PSUBUSB/PSUBUSW -> Subtract Packed Unsigned Integers with Unsigned Saturation
 * PTEST- Logical Compare 4-255
 * PUNPCKHBW/PUNPCKHWD/PUNPCKHDQ/PUNPCKHQDQ -> Unpack High Data
 * PUNPCKLBW/PUNPCKLWD/PUNPCKLDQ/PUNPCKLQDQ -> Unpack Low Data
 * PUSH -> Push Word, Doubleword or Quadword Onto the Stack
 * PUSHA/PUSHAD -> Push All General-Purpose Registers
 * PUSHF/PUSHFD -> Push EFLAGS Register onto the Stack
 * PXOR -> Logical Exclusive OR
 * RCL/RCR/ROL/ROR- -> Rotate
 * RCPPS -> Compute Reciprocals of Packed Single-Precision Floating-Point Values
 * RCPSS -> Compute Reciprocal of Scalar Single-Precision Floating-Point Values
 * RDFSBASE/RDGSBASE -> Read FS/GS Segment Base
 * RDMSR -> Read from Model Specific Register
 * RDPKRU -> Read Protection Key Rights for User Pages
 * RDPMC -> Read Performance-Monitoring Counters
 * RDRAND -> Read Random Number
 * RDSEED -> Read Random SEED
 * RDTSC -> Read Time-Stamp Counter
 * RDTSCP -> Read Time-Stamp Counter and Processor ID
 * REP/REPE/REPZ/REPNE/REPNZ -> Repeat String Operation Prefix
 * RET -> Return from Procedure
 * RORX -> Rotate Right Logical Without Affecting Flags
 * ROUNDPD -> Round Packed Double Precision Floating-Point Values
 * ROUNDPS -> Round Packed Single Precision Floating-Point Values
 * ROUNDSD -> Round Scalar Double Precision Floating-Point Values
 * ROUNDSS -> Round Scalar Single Precision Floating-Point Values
 * RSM -> Resume from System Management Mode
 * RSQRTPS -> Compute Reciprocals of Square Roots of Packed Single-Precision Floating-Point Values
 * RSQRTSS -> Compute Reciprocal of Square Root of Scalar Single-Precision Floating-Point Value
 * SAHF -> Store AH into Flags
 * SAL/SAR/SHL/SHR -> Shift
 * SARX/SHLX/SHRX -> Shift Without Affecting Flags
 * SBB -> Integer Subtraction with Borrow
 * SCAS/SCASB/SCASW/SCASD -> Scan String
 * SETcc -> Set Byte on Condition
 * SFENCE -> Store Fence
 * SGDT -> Store Global Descriptor Table Register
 * SHLD -> Double Precision Shift Left
 * SHRD -> Double Precision Shift Right
 * SHUFPD -> Shuffle Packed Double-Precision Floating-Point Values
 * SHUFPS -> Shuffle Packed Single-Precision Floating-Point Values
 * SIDT -> Store Interrupt Descriptor Table Register
 * SLDT -> Store Local Descriptor Table Register
 * SMSW -> Store Machine Status Word
 * SQRTPD -> Compute Square Roots of Packed Double-Precision Floating-Point Values
 * SQRTPS -> Compute Square Roots of Packed Single-Precision Floating-Point Values
 * SQRTSD -> Compute Square Root of Scalar Double-Precision Floating-Point Value
 * SQRTSS -> Compute Square Root of Scalar Single-Precision Floating-Point Value
 * STAC -> Set AC Flag in EFLAGS Register
 * STC -> Set Carry Flag
 * STD -> Set Direction Flag
 * STI -> Set Interrupt Flag
 * STMXCSR -> Store MXCSR Register State
 * STOS/STOSB/STOSW/STOSD/STOSQ -> Store String
 * STR -> Store Task Register
 * SUB -> Subtract
 * SUBPD -> Subtract Packed Double-Precision Floating-Point Values
 * SUBPS -> Subtract Packed Single-Precision Floating-Point Values
 * SUBSD -> Subtract Scalar Double-Precision Floating-Point Values
 * SUBSS -> Subtract Scalar Single-Precision Floating-Point Values
 * SWAPGS -> Swap GS Base Register
 * SYSCALL -> Fast System Call
 * SYSENTER -> Fast System Call
 * SYSEXIT -> Fast Return from Fast System Call
 * SYSRET -> Return From Fast System Call
 * TEST -> Logical Compare
 * TZCNT -> Count the Number of Trailing Zero Bits
 * UCOMISD -> Unordered Compare Scalar Double-Precision Floating-Point Values and Set EFLAGS
 * UCOMISS -> Unordered Compare Scalar Single-Precision Floating-Point Values and Set EFLAGS
 * UD2 -> Undefined Instruction
 * UNPCKHPD -> Unpack and Interleave High Packed Double-Precision Floating-Point Values
 * UNPCKHPS -> Unpack and Interleave High Packed Single-Precision Floating-Point Values
 * UNPCKLPD -> Unpack and Interleave Low Packed Double-Precision Floating-Point Values
 * UNPCKLPS -> Unpack and Interleave Low Packed Single-Precision Floating-Point Values
 * VBROADCAST -> Broadcast Floating-Point Data
 * VCVTPH2PS -> Convert 16-bit FP Values to Single-Precision FP Values
 * VCVTPS2PH -> Convert Single-Precision FP value to 16-bit FP value
 * VERR/VERW -> Verify a Segment for Reading or Writing
 * VEXTRACTF128 -> Extract Packed Floating-Point Values
 * VEXTRACTI128 -> Extract packed Integer Values
 * VFMADD132PD/VFMADD213PD/VFMADD231PD -> Fused Multiply-Add of Packed Double-Precision Floating-Point Values
 * VFMADD132PS/VFMADD213PS/VFMADD231PS -> Fused Multiply-Add of Packed Single-Precision Floating-Point Values
 * VFMADD132SD/VFMADD213SD/VFMADD231SD -> Fused Multiply-Add of Scalar Double-Precision Floating-Point Values
 * VFMADD132SS/VFMADD213SS/VFMADD231SS -> Fused Multiply-Add of Scalar Single-Precision Floating-Point Values
 * VFMADDSUB132PD/VFMADDSUB213PD/VFMADDSUB231PD -> Fused Multiply-Alternating Add/Subtract of Packed Double-Precision Floating-Point Values
 * VFMADDSUB132PS/VFMADDSUB213PS/VFMADDSUB231PS -> Fused Multiply-Alternating Add/Subtract of Packed Single-Precision Floating-Point Values
 * VFMSUBADD132PD/VFMSUBADD213PD/VFMSUBADD231PD -> Fused Multiply-Alternating Subtract/Add of Packed Double-Precision Floating-Point Values
 * VFMSUBADD132PS/VFMSUBADD213PS/VFMSUBADD231PS -> Fused Multiply-Alternating Subtract/Add of Packed Single-Precision Floating-Point Values
 * VFMSUB132PD/VFMSUB213PD/VFMSUB231PD -> Fused Multiply-Subtract of Packed Double-Precision Floating-Point Values
 * VFMSUB132PS/VFMSUB213PS/VFMSUB231PS -> Fused Multiply-Subtract of Packed Single-Precision Floating-Point Values
 * VFMSUB132SD/VFMSUB213SD/VFMSUB231SD -> Fused Multiply-Subtract of Scalar Double-Precision Floating-Point Values
 * VFMSUB132SS/VFMSUB213SS/VFMSUB231SS -> Fused Multiply-Subtract of Scalar Single-Precision Floating-Point Values
 * VFNMADD132PD/VFNMADD213PD/VFNMADD231PD -> Fused Negative Multiply-Add of Packed Double-Precision Floating-Point Values
 * VFNMADD132PS/VFNMADD213PS/VFNMADD231PS -> Fused Negative Multiply-Add of Packed Single-Precision Floating-Point Values
 * VFNMADD132SD/VFNMADD213SD/VFNMADD231SD -> Fused Negative Multiply-Add of Scalar Double-Precision Floating-Point Values
 * VFNMADD132SS/VFNMADD213SS/VFNMADD231SS -> Fused Negative Multiply-Add of Scalar Single-Precision Floating-Point Values
 * VFNMSUB132PD/VFNMSUB213PD/VFNMSUB231PD -> Fused Negative Multiply-Subtract of Packed Double-Precision Floating-Point Values
 * VFNMSUB132PS/VFNMSUB213PS/VFNMSUB231PS -> Fused Negative Multiply-Subtract of Packed Single-Precision Floating-Point Values
 * VFNMSUB132SD/VFNMSUB213SD/VFNMSUB231SD -> Fused Negative Multiply-Subtract of Scalar Double-Precision Floating-Point Values
 * VFNMSUB132SS/VFNMSUB213SS/VFNMSUB231SS -> Fused Negative Multiply-Subtract of Scalar Single-Precision Floating-Point Values
 * VGATHERDPD/VGATHERQPD -> Gather Packed DP FP Values Using Signed Dword/Qword Indices
 * VGATHERDPS/VGATHERQPS -> Gather Packed SP FP values Using Signed Dword/Qword Indices
 * VPGATHERDD/VPGATHERQD -> Gather Packed Dword Values Using Signed Dword/Qword Indices
 * VPGATHERDQ/VPGATHERQQ -> Gather Packed Qword Values Using Signed Dword/Qword Indices
 * VINSERTF128 -> Insert Packed Floating-Point Values
 * VINSERTI128 -> Insert Packed Integer Values
 * VMASKMOV -> Conditional SIMD Packed Loads and Stores
 * VPBLENDD -> Blend Packed Dwords
 * VPBROADCAST -> Broadcast Integer Data
 * VPERMD -> Full Doublewords Element Permutation
 * VPERMPD -> Permute Double-Precision Floating-Point Elements
 * VPERMPS -> Permute Single-Precision Floating-Point Elements
 * VPERMQ -> Qwords Element Permutation
 * VPERM2I128 -> Permute Integer Values
 * VPERMILPD -> Permute Double-Precision Floating-Point Values
 * VPERMILPS -> Permute Single-Precision Floating-Point Values
 * VPERM2F128 -> Permute Floating-Point Values
 * VPMASKMOV -> Conditional SIMD Integer Packed Loads and Stores
 * VPSLLVD/VPSLLVQ -> Variable Bit Shift Left Logical
 * VPSRAVD -> Variable Bit Shift Right Arithmetic
 * VPSRLVD/VPSRLVQ -> Variable Bit Shift Right Logical
 * VTESTPD/VTESTPS -> Packed Bit Test
 * VZEROALL -> Zero All YMM Registers
 * VZEROUPPER -> Zero Upper Bits of YMM Registers
 * WAIT/FWAIT -> Wait
 * WBINVD -> Write Back and Invalidate Cache
 * WRFSBASE/WRGSBASE -> Write FS/GS Segment Base
 * WRMSR -> Write to Model Specific Register
 * WRPKRU -> Write Data to User Page Key Register
 * XACQUIRE/XRELEASE -> Hardware Lock Elision Prefix Hints
 * XABORT -> Transactional Abort
 * XADD -> Exchange and Add
 * XBEGIN -> Transactional Begin
 * XCHG -> Exchange Register/Memory with Register
 * XEND -> Transactional End
 * XGETBV -> Get Value of Extended Control Register
 * XLAT/XLATB -> Table Look-up Translation
 * XOR -> Logical Exclusive OR
 * XORPD -> Bitwise Logical XOR for Double-Precision Floating-Point Values
 * XORPS -> Bitwise Logical XOR for Single-Precision Floating-Point Values
 * XRSTOR -> Restore Processor Extended States
 * XRSTORS -> Restore Processor Extended States Supervisor
 * XSAVE -> Save Processor Extended States
 * XSAVEC -> Save Processor Extended States with Compaction
 * XSAVEOPT -> Save Processor Extended States Optimized
 * XSAVES -> Save Processor Extended States Supervisor
 * XSETBV -> Set Extended Control Register
 * XTEST -> Test If In Transactional Execution