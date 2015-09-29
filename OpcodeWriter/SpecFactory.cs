namespace OpcodeWriter
{
    /// <summary>
    /// Factory for specifications.
    /// </summary>
    public abstract class SpecFactory
    {
        /// <summary>
        /// Creates a new <see cref="OpcodeSpec"/> object.
        /// </summary>
        /// <returns>The created object.</returns>
        public virtual OpcodeSpec CreateOpcodeSpec()
        {
            return new OpcodeSpec();
        }

        /// <summary>
        /// Creates a new <see cref="OpcodeVariantSpec"/> object.
        /// </summary>
        /// <returns>The created object.</returns>
        public virtual OpcodeVariantSpec CreateOpcodeVariantSpec()
        {
            return new OpcodeVariantSpec();
        }

        /// <summary>
        /// Creates a new <see cref="OperandSpec"/> object.
        /// </summary><param name="type">The operand type.</param>
        /// <param name="defaultValue">The default value; or <see langword="null"/>.</param>
        /// <returns>The created object.</returns>
        public abstract OperandSpec CreateOperandSpec(string type, object defaultValue);
    }
}
