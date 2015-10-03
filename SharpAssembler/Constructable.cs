using System.Collections;
using System.Collections.Generic;

namespace SharpAssembler
{
    /// <summary>
    /// An interface for entities which can be contained can create a
    /// representation of themselves.
    /// </summary>
    public abstract class Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Constructable"/> class.
        /// </summary>
        protected Constructable()
        {
        }

        /// <summary>
        /// Gets a dictionary which may be used to store data specific to this object.
        /// </summary>
        /// <value>An implementation of the <see cref="IDictionary"/> interface.</value>
        /// <remarks>
        /// This property is not serialized or deserialized.
        /// </remarks>
        public IDictionary Annotations { get; private set; } = new Hashtable();

        /// <summary>
        /// Modifies the context and constructs an emittable representing this constructable.
        /// </summary>
        /// <param name="context">The mutable <see cref="Context"/> in which the emittable will be constructed.</param>
        /// <returns>A list of constructed emittables; or an empty list.</returns>
        public abstract IEnumerable<IEmittable> Construct(Context context);
    }
}
