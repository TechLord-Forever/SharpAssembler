using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// A group of constructables.
    /// </summary>
    public class Group : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        public Group()
            : this(new Collection<Constructable>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class with the specified list
        /// of <see cref="Constructable"/> objects.
        /// </summary>
        /// <param name="constructables">The list to use.</param>
        protected Group(IList<Constructable> constructables)
        {
            Content = constructables;
        }

        /// <summary>
        /// Gets an ordered list of <see cref="Constructable"/> objects in this group.
        /// </summary>
        /// <value>A <see cref="Collection{T}"/> of <see cref="Constructable"/> objects.</value>
        public IList<Constructable> Content { get; private set; }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            return Content.SelectMany(c => c.Construct(context));
        }
    }
}
