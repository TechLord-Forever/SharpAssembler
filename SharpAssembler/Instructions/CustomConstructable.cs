using System.Linq;
using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Base class for custom constructables.
    /// </summary>
    public abstract class CustomConstructable : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomConstructable"/> class.
        /// </summary>
        protected CustomConstructable()
        {
        }

        /// <summary>
        /// Gets all the constructables that represent this custom constructable.
        /// </summary>
        /// <param name="context">The context in which the constructables are retrieved.</param>
        /// <returns>An enumerable collection of <see cref="Constructable"/> objects.</returns>
        /// <remarks>
        /// Elements of the returned enumerable may be <see langword="null"/>.
        /// </remarks>
        protected abstract IEnumerable<Constructable> GetContent(Context context);

        /// <inheritdoc />
        public sealed override IEnumerable<IEmittable> Construct(Context context)
        {
            var constructables = GetContent(context);
            var emittables = constructables.SelectMany(c => c.Construct(context));
            return emittables;
        }
    }
}
