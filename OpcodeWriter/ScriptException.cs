using System;
using System.Runtime.Serialization;

namespace OpcodeWriter
{
    [Serializable]
    public class ScriptException : Exception
    {
        public ScriptException()
        {
        }

        public ScriptException(string message)
            : base(message)
        {
        }

        public ScriptException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ScriptException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}