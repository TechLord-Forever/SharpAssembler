using System.Collections.ObjectModel;

namespace SharpAssembler
{
    /// <summary>
    /// An instruction.
    /// </summary>
	public interface IInstruction
	{
		/// <summary>
		/// Gets the opcode of the instruction.
		/// </summary>
		/// <value>The <see cref="IOpcode"/> of the instruction,
		/// which describes the semantics of the instruction.</value>
		IOpcode Opcode
		{ get; }

		/// <summary>
		/// Returns the operands to the instruction.
		/// </summary>
		/// <returns>An ordered list of operands.</returns>
		ReadOnlyCollection<IOperand> GetOperands();
	}
}
