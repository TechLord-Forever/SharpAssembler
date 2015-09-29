@echo off
cd ..\..\Tools\OpcodeWriter
Source\bin\Debug\OpcodeWriter.exe -y "Assembler\yasm64.exe" -oc "..\..\SharpAssembler.Architectures.X86\Source\Opcodes" -ot "..\..\SharpAssembler.Architectures.X86\Tests\Opcodes" -r "..\..\Input\X86 Opcodes"
pause