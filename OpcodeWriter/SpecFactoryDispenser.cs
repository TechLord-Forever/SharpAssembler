﻿using System.Collections.Generic;

namespace OpcodeWriter
{
    /// <summary>
    /// Dispenses <see cref="SpecFactory"/> objects for specific platforms.
    /// </summary>
    public class SpecFactoryDispenser
    {
        /// <summary>
        /// A dictionary with factories.
        /// </summary>
        Dictionary<string, SpecFactory> factories = new Dictionary<string, SpecFactory>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecFactoryDispenser"/> class.
        /// </summary>
        public SpecFactoryDispenser()
        {
        }

        /// <summary>
        /// Registers a new factory with the specified platform ID.
        /// </summary>
        /// <param name="platformID">The ID of the platform.</param>
        /// <param name="factory">The factory instance.</param>
        public void Register(string platformID, SpecFactory factory)
        {
            factories.Add(platformID, factory);
        }

        /// <summary>
        /// Gets a factory with the specified platform ID.
        /// </summary>
        /// <param name="platformID">The platform ID.</param>
        /// <returns>The factory instance.</returns>
        public SpecFactory Get(string platformID)
        {
            return factories[platformID];
        }
    }
}
