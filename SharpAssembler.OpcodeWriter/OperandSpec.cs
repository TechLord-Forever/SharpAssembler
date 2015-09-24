namespace SharpAssembler.OpcodeWriter
{
    /// <summary>
    /// Specification for an operand to an opcode invariant.
    /// </summary>
    public class OperandSpec
	{
		private string name;
		/// <summary>
		/// Gets or sets the name of the opcode.
		/// </summary>
		/// <value>The name of the opcode; or <see langword="null"/> to specify none.
		/// The default is <see langword="null"/>.</value>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}
}
