using System;

namespace OpcodeWriter
{
    /// <summary>
    /// An annotation.
    /// </summary>
    public sealed class Annotation
    {
        /// <summary>
        /// Gets the name of the property to be set.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the new value of the property.
        /// </summary>
        /// <value>A value, which may be <see langword="null"/>.</value>
        public object Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Annotation"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public Annotation(string name, object value)
        {
            PropertyName = name;
            Value = value;
        }

        /// <summary>
        /// Sets a property on the specified object.
        /// </summary>
        /// <param name="target">The target object.</param>
        public void SetOn(object target)
        {
            var type = target.GetType();
            var property = type.GetProperty(PropertyName);

            if (property == null)
                throw new ScriptException(string.Format("A property {0} could not be found on {1}.", PropertyName, type));

            property.SetValue(target, ConvertToType(property.PropertyType, Value), null);
        }

        /// <summary>
        /// Converts the specified value to a value that fits the target type.
        /// </summary>
        /// <param name="targetType">The target <see cref="Type"/>.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        private static object ConvertToType(Type targetType, object value)
        {
            if (targetType.IsAssignableFrom(value.GetType()))
                return value;

            if (targetType.IsEnum)
            {
                if (!Enum.IsDefined(targetType, value.ToString()))
                    throw new ScriptException(string.Format("The value '{0}' is not a member of {1}.", value, targetType));

                return Enum.Parse(targetType, value.ToString(), true);
            }

            if (targetType.Equals(typeof(byte)))
                return Convert.ToByte(value);
            if (targetType.Equals(typeof(sbyte)))
                return Convert.ToSByte(value);
            if (targetType.Equals(typeof(short)))
                return Convert.ToInt16(value);
            if (targetType.Equals(typeof(ushort)))
                return Convert.ToUInt16(value);
            if (targetType.Equals(typeof(int)))
                return Convert.ToInt32(value);
            if (targetType.Equals(typeof(uint)))
                return Convert.ToUInt32(value);
            if (targetType.Equals(typeof(long)))
                return Convert.ToInt64(value);
            if (targetType.Equals(typeof(ulong)))
                return Convert.ToUInt64(value);

            throw new ScriptException(string.Format("Could not convert from {0} to {1}.", value.GetType(), targetType));
        }
    }
}
