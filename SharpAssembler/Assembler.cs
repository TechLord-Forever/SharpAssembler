using SharpAssembler.Instructions;

namespace SharpAssembler
{
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
            Context = new Context();
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
