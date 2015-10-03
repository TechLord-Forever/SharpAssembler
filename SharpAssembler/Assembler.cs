using SharpAssembler.Instructions;

namespace SharpAssembler
{
    partial class Assembler { /* stuff */ }

    /// <summary>
    ///
    /// </summary>
    public partial class Assembler
    {
        /// <summary>
        ///
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Assembler()
        {
            Context = new Context(DataSize.Bit64, 0, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public Label DefineLabel(string identifier = null)
        {
            return new Label(identifier);
        }
    }
}
