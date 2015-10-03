

namespace SharpAssembler.Architectures.X86.Instructions
{
    partial class Prefetchl
    {
        /// <summary>
        /// Specifies the prefetch level for the <see cref="Prefetchl"/> instruction.
        /// </summary>
        public enum PrefetchLevel
        {
            /// <summary>
            /// No prefetch level specified.
            /// </summary>
            None = 0,
            /// <summary>
            /// Non-Temporal Access.
            /// </summary>
            NonTemporalAccess = 0x100 | 0,
            /// <summary>
            /// All cache levels.
            /// </summary>
            T0 = 0x100 | 1,
            /// <summary>
            /// Level 2 and higher.
            /// </summary>
            T1 = 0x100 | 2,
            /// <summary>
            /// Level 3 and higher.
            /// </summary>
            T2 = 0x100 | 3,
        }
    }
}
