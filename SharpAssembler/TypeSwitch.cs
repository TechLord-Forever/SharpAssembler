﻿namespace SharpAssembler.Architectures.X86
{
    using System;

    namespace SharpAssembler.Languages.Nasm
    {
        /// <summary>
        /// Executes a particular piece of code based on the type of the argument.
        /// </summary>
        public static class TypeSwitch
        {
            /// <summary>
            /// Executes a particular piece of code based on the type of the argument.
            /// </summary>
            /// <typeparam name="TSource">The argument's type.</typeparam>
            /// <param name="value">The switch argument.</param>
            /// <returns>An object on which the switch cases can be specified.</returns>
            public static Switch<TSource> On<TSource>(TSource value)
            {
                return new Switch<TSource>(value);
            }

            /// <summary>
            /// Internal class used by the <see cref="TypeSwitch"/> static class.
            /// </summary>
            /// <typeparam name="TSource">The source type.</typeparam>
            public class Switch<TSource>
            {
                /// <summary>
                /// The source value.
                /// </summary>
                private TSource value;
                /// <summary>
                /// Whether a switch case handled the value.
                /// </summary>
                private bool handled = false;

                /// <summary>
                /// Initializes a new instance of the <see cref="TypeSwitch"/> class.
                /// </summary>
                /// <param name="value">The switch value.</param>
                internal Switch(TSource value)
                {
                    this.value = value;
                }

                /// <summary>
                /// Executes the specified piece of code when the type of the argument is assignable to the
                /// specified type.
                /// </summary>
                /// <typeparam name="TTarget">The target type.</typeparam>
                /// <param name="action">The action to execute.</param>
                /// <returns>An object on which further switch cases can be specified.</returns>
                public Switch<TSource> Case<TTarget>(Action action)
                    where TTarget : TSource
                {
                    if (action == null)
                        throw new ArgumentNullException("action");
                    if (!handled)
                    {
                        var sourceType = value.GetType();
                        var targetType = typeof(TTarget);
                        if (targetType.IsAssignableFrom(sourceType))
                        {
                            action();
                            handled = true;
                        }
                    }

                    return this;
                }

                /// <summary>
                /// Executes the specified piece of code when the type of the argument is assignable to the
                /// specified type.
                /// </summary>
                /// <typeparam name="TTarget">The target type.</typeparam>
                /// <param name="action">The action to execute.</param>
                /// <returns>An object on which further switch cases can be specified.</returns>
                public Switch<TSource> Case<TTarget>(Action<TTarget> action)
                    where TTarget : TSource
                {
                    if (action == null)
                        throw new ArgumentNullException("action");

                    if (!handled)
                    {
                        var sourceType = value.GetType();
                        var targetType = typeof(TTarget);
                        if (targetType.IsAssignableFrom(sourceType))
                        {
                            action((TTarget)value);
                            handled = true;
                        }
                    }

                    return this;
                }

                /// <summary>
                /// Executes the specified piece of code when none of the other cases handles the specified type.
                /// </summary>
                /// <param name="action">The action to execute.</param>
                public void Default(Action action)
                {
                    if (action == null)
                        throw new ArgumentNullException("action");

                    if (!handled)
                        action();
                }

                /// <summary>
                /// Executes the specified piece of code when none of the other cases handles the specified type.
                /// </summary>
                /// <param name="action">The action to execute.</param>
                public void Default(Action<TSource> action)
                {
                    if (action == null)
                        throw new ArgumentNullException("action");

                    if (!handled)
                        action(value);
                }
            }
        }
    }

}
