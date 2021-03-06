﻿using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace SharpAssembler.Architectures.X86
{
    /// <summary>
    /// Describes a single operand.
    /// </summary>
    public struct OperandDescriptor : IEquatable<OperandDescriptor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="operandType">The type of operand.</param>
        /// <param name="size">The size of the operand.</param>
        public OperandDescriptor(OperandType operandType, DataSize size)
            : this(operandType, RegisterType.None, size, OperandEncoding.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="operandType">The type of operand.</param>
        /// <param name="registerType">The type of register.</param>
        public OperandDescriptor(OperandType operandType, RegisterType registerType)
            : this(operandType, registerType, OperandEncoding.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="operandType">The type of operand.</param>
        /// <param name="size">The size of the operand.</param>
        /// <param name="encoding">How the operand is encoded.</param>
        public OperandDescriptor(OperandType operandType, DataSize size, OperandEncoding encoding)
            : this(operandType, RegisterType.None, size, encoding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="operandType">The type of operand.</param>
        /// <param name="registerType">The type of register.</param>
        /// <param name="encoding">How the operand is encoded.</param>
        public OperandDescriptor(OperandType operandType, RegisterType registerType,    OperandEncoding encoding)
            : this(operandType, registerType, DataSize.None, encoding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="operandType">The type of operand.</param>
        /// <param name="registerType">A bitwise combination of valid types of the register.</param>
        /// <param name="size">The size of the operand.</param>
        /// <param name="encoding">Specifies how the operand is encoded.</param>
        OperandDescriptor(OperandType operandType, RegisterType registerType, DataSize size, OperandEncoding encoding)
        {
            OperandType = operandType;
            RegisterType = registerType;
            Size = (size != DataSize.None ? size : registerType.GetSize());
            FixedRegister = Register.None;
            OperandEncoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperandDescriptor"/> structure.
        /// </summary>
        /// <param name="register">The fixed <see cref="Register"/> value of the operand.</param>
        public OperandDescriptor(Register register)
        {
            OperandType = OperandType.FixedRegister;
            RegisterType = register.Type;
            Size = register.Size;
            FixedRegister = register;
            OperandEncoding = OperandEncoding.Default;
        }

        /// <summary>
        /// Gets the type of operand.
        /// </summary>
        /// <value>A member of the <see cref="OperandType"/> enumeration.</value>
        public OperandType OperandType { get; private set; }

        /// <summary>
        /// Gets the type of registers allowed for this operand.
        /// </summary>
        /// <value>A bitwise combination of members of the <see cref="RegisterType"/> enumeration;
        /// or <see cref="RegisterType.None"/> when this does not apply.</value>
        /// <remarks>
        /// This property is only valid for operands which take a register.
        /// </remarks>
        public RegisterType RegisterType { get; private set; }

        /// <summary>
        /// Gets how the operand is encoded.
        /// </summary>
        /// <value>A member of the <see cref="OperandEncoding"/> enumeration.</value>
        public OperandEncoding OperandEncoding { get; private set; }

        /// <summary>
        /// Gets the size of the operand.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration; or <see cref="DataSize.None"/> when the size
        /// of the operand does not matter.</value>
        /// <remarks>
        /// This property returns the size of <see cref="FixedRegister"/> when <see cref="OperandType"/> equals
        /// <see cref="OperandType.FixedRegister"/>.
        /// </remarks>
        public DataSize Size { get; private set; }

        /// <summary>
        /// Gets the <see cref="Register"/> which must be set when <see cref="OperandType"/> equals
        /// <see cref="OperandType.FixedRegister"/>.
        /// </summary>
        /// <value>A <see cref="Register"/>; or <see cref="Register.None"/> when it does not apply.</value>
        public Register FixedRegister { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter;
        /// otherwise, <see langword="false"/>.</returns>
        public bool Equals(OperandDescriptor other)
        {
            return other.OperandType == OperandType
                && other.RegisterType == RegisterType
                && other.Size == Size
                && other.FixedRegister == FixedRegister;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> and this instance are the same type and
        /// represent the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is OperandDescriptor))
                return false;
            return Equals((OperandDescriptor)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            // TODO: Better GetHashCode() method implementation.
            return OperandType.GetHashCode() ^ RegisterType.GetHashCode() ^ Size.GetHashCode() ^ FixedRegister.GetHashCode();
        }

        // TODO: Implement these in the operand types.
#if false
            /// <summary>
            /// Checks whether the specified operand is a match to this <see cref="OperandDescriptor"/>.
            /// </summary>
            /// <param name="operand">The <see cref="Operand"/> to test.</param>
            /// <returns><see langword="true"/> when <paramref name="operand"/> is a match to this
            /// <see cref="OperandDescriptor"/>; otherwise, <see langword="false"/>.</returns>
            [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
            public bool Match(Operand operand)
            {
                switch (operandType)
                {
                    case OperandType.FixedRegister:
                        {
                            RegisterOperand castoperand = operand as RegisterOperand;
                            if (castoperand != null)
                                return (castoperand.Register == fixedRegister);
                            return false;
                        }
                    case OperandType.Immediate:
                        {
                            Immediate castoperand = operand as Immediate;
                            if (castoperand != null)
                                return (castoperand.Size == DataSize.None || castoperand.Size == size);
                            return false;
                        }
                    case OperandType.RegisterOperand:
                        {
                            RegisterOperand castoperand = operand as RegisterOperand;
                            if (castoperand != null)
                                return (/*castoperand.Size == size &&*/ registerType.HasFlag(castoperand.Register.GetRegisterType()));
                            return false;
                        }
                    case OperandType.RegisterOrMemoryOperand:
                        {
                            RegisterOperand castoperand1 = operand as RegisterOperand;
                            EffectiveAddress castoperand2 = operand as EffectiveAddress;
                            if (castoperand1 != null)
                                return (/*castoperand1.Size == size &&*/ registerType.HasFlag(castoperand1.Register.GetRegisterType()));
                            if (castoperand2 != null)
                                return (/*castoperand2.Size == size*/ registerType.HasFlag(castoperand2.Size));
                            return false;
                        }
                    case OperandType.MemoryOperand:
                        {
                            EffectiveAddress castoperand = operand as EffectiveAddress;
                            if (castoperand != null)
                                return (castoperand.Size == size);
                            return false;
                        }
                    case OperandType.MemoryOffset:
                        // TODO: Implement!
                        return false;
                    case OperandType.FarPointer:
                        {
                            FarPointer castoperand = operand as FarPointer;
                            if (castoperand != null)
                                return (castoperand.Size == size);
                            return false;
                        }

                    case OperandType.RelativeOffset:
                        {
                            // A relative offset matches when the Size of the operand
                            // equals DataSize.None. The Size will be set when the
                            // operand has been preprocessed, after which it is tested
                            // again.
                            RelativeOffset castoperand = operand as RelativeOffset;
                            if (castoperand != null)
                                return (castoperand.Size == DataSize.None || castoperand.Size == size);
                            return false;
                        }
                    case OperandType.None:
                        return (operand == null);
                    default:
                        throw new NotSupportedException();
                }
            }

            /// <summary>
            /// Adjusts the operand to reflect the encoding differences
            /// between variants.
            /// </summary>
            /// <param name="operand">The operand to adjust to this descriptor.</param>
            public void Adjust(Operand operand)
            {
                switch (operandType)
                {
                    case OperandType.RegisterOrMemoryOperand:
                        {
                            RegisterOperand castoperand = operand as RegisterOperand;
                            if (castoperand != null)
                                // When the operand is a register (and not a memory reference), it needs to be encoded as part of the reg/mem.
                                castoperand.Encoding = RegisterOperandEncoding.ModRm;
                            break;
                        }
                    case OperandType.RegisterOperand:
                        {
                            RegisterOperand castoperand = operand as RegisterOperand;
                            if (castoperand != null)
                                // When the operand needs to be added to the opcode, set it as such.
                                castoperand.Encoding = (operandEncoding == OperandEncoding.OpcodeAdd ? RegisterOperandEncoding.AddToOpcode : RegisterOperandEncoding.Default);
                            break;
                        }
                    case OperandType.Immediate:
                        {
                            Immediate castoperand = operand as Immediate;
                            if (castoperand != null)
                                castoperand.AsExtraImmediate = (operandEncoding == OperandEncoding.ExtraImmediate);
                            break;
                        }
                    case OperandType.FixedRegister:
                    case OperandType.MemoryOperand:
                    case OperandType.MemoryOffset:
                    case OperandType.FarPointer:
                    case OperandType.RelativeOffset:
                    case OperandType.None:
                    default:
                        break;
                }
            }
#endif

#if false
        /// <summary>
        /// Checks whether the specified object is valid to be assigned to
        /// the <see cref="OperandType"/>.
        /// </summary>
        /// <param name="operandType">A bitwise combination of <see cref="OperandType"/> members
        /// for which the operand must be valid.</param>
        /// <param name="registerType">A bitwise combination of <see cref="RegisterType"/> members
        /// for which the operand must be valid. The lower 8 bits are ignored.</param>
        /// <param name="operand">The <see cref="Operand"/> to check.</param>
        /// <returns><see langword="true"/> when an <paramref name="operand"/> may be assigned
        /// to an operand with the specified operand type and register type; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// When <paramref name="operand"/> is <see langword="null"/>, this method always returns <see langword="true"/>
        /// because there is no flag in <see cref="OperandType"/> to indicate that <see langword="null"/> operands are
        /// (dis)allowed.
        /// </remarks>
        public static bool IsValidArgument(OperandType operandType, RegisterType registerType, Operand operand)
        {
            // Null operands are implicitly allowed.
            if (operand == null)
                return true;

            // Ignore the lower 8 bits.
            registerType = (RegisterType)((int)registerType & ~0xFF);

            // TODO: Implement.
            if (operandType.HasFlag(OperandType.MemoryOffset))
                throw new NotImplementedException();

            // Check the type of operand.
            if (operand is Immediate)
                return operandType.HasFlag(OperandType.Immediate);
            if (operand is EffectiveAddress)
                return operandType.HasFlag(OperandType.MemoryOperand)
                    || operandType.HasFlag(OperandType.RegisterOrMemoryOperand);
            if (operand is RelativeOffset)
                return operandType.HasFlag(OperandType.RelativeOffset);
            if (operand is FarPointer)
                return operandType.HasFlag(OperandType.FarPointer);
            if (operand is RegisterOperand)
            {
                if (!operandType.HasFlag(OperandType.RegisterOperand) &&
                    !operandType.HasFlag(OperandType.FixedRegister) &&
                    !operandType.HasFlag(OperandType.RegisterOrMemoryOperand))
                    return false;
                if (operandType.HasFlag(OperandType.FixedRegister))
                    return true;

                // Check the type of register.
                return (registerType.HasFlag((RegisterType)((int)(operand as RegisterOperand).Register.GetRegisterType() & ~0xFF)));
            }

            return false;
        }
#endif

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            switch (OperandType)
            {
                case OperandType.FixedRegister:
                    return Enum.GetName(typeof(Register), FixedRegister);
                case OperandType.Immediate:
                    return "imm" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                case OperandType.MemoryOperand:
                    return "mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "");
                case OperandType.MemoryOffset:
                    return "moffset" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                case OperandType.FarPointer:
                    return "pntr16:" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                case OperandType.RegisterOperand:
                    switch (RegisterType)
                    {
                        case RegisterType.FloatingPoint:
                            return "ST(i)";
                        case RegisterType.Simd64Bit:
                            return "mmx";
                        case RegisterType.Simd128Bit:
                            return "xmm";
                        case RegisterType.Segment:
                            return "segReg";
                        case RegisterType.Control:
                            return "cReg";
                        case RegisterType.Debug:
                            return "dReg";
                        //case RegisterType.GeneralPurpose8Bit:
                        //case RegisterType.GeneralPurpose16Bit:
                        //case RegisterType.GeneralPurpose32Bit:
                        //case RegisterType.GeneralPurpose64Bit:
                        //case RegisterType.None:
                        default:
                            return "reg" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                    }
                case OperandType.RegisterOrMemoryOperand:
                    switch (RegisterType)
                    {
                        case RegisterType.FloatingPoint:
                            return "ST(i)/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        case RegisterType.Simd64Bit:
                            return "mmx/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        case RegisterType.Simd128Bit:
                            return "xmm/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        case RegisterType.Segment:
                            return "segReg/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        case RegisterType.Control:
                            return "cReg/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        case RegisterType.Debug:
                            return "dReg/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                        //case RegisterType.GeneralPurpose8Bit:
                        //case RegisterType.GeneralPurpose16Bit:
                        //case RegisterType.GeneralPurpose32Bit:
                        //case RegisterType.GeneralPurpose64Bit:
                        //case RegisterType.None:
                        default:
                            return "reg/mem" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?");
                    }
                case OperandType.RelativeOffset:
                    return "rel" + (Size != DataSize.None ? ((int)Size * 8).ToString(CultureInfo.InvariantCulture) : "?") + "off";
                case OperandType.None:
                    // Nothing to do.
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Determines whether two specified <see cref="OperandDescriptor"/> objects have the same value.
        /// </summary>
        /// <param name="first">An <see cref="OperandDescriptor"/>.</param>
        /// <param name="second">An <see cref="OperandDescriptor"/>.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="first"/> is the same as the value of
        /// <paramref name="second"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(OperandDescriptor first, OperandDescriptor second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Determines whether two specified <see cref="OperandDescriptor"/> objects have different values.
        /// </summary>
        /// <param name="first">An <see cref="OperandDescriptor"/>.</param>
        /// <param name="second">An <see cref="OperandDescriptor"/>.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="first"/> is different from the value of
        /// <paramref name="second"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(OperandDescriptor first, OperandDescriptor second)
        {
            return !(first == second);
        }
    }
}
