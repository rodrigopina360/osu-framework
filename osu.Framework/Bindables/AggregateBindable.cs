// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Framework.Bindables
{
    /// <summary>
    /// Combine multiple bindables into one aggregate bindable result.
    /// </summary>
    /// <typeparam name="T">The type of values.</typeparam>
    public class AggregateBindable<T>
    {
        private readonly Func<T, T, T> aggregateFunction;

        /// <summary>
        /// THe final result after aggregating all added sources..
        /// </summary>
        public IBindable<T> Result => result;

        private readonly Bindable<T> result;

        /// <summary>
        /// Create a new aggregate bindable.
        /// </summary>
        /// <param name="aggregateFunction">The function to be used for aggregation, taking two input <see cref="T"/> values and returning one output.</param>
        /// <param name="defaultValue">The value used for the initial <see cref="Result"/> and for the first step of aggregation.</param>
        public AggregateBindable(Func<T, T, T> aggregateFunction, T defaultValue = default)
        {
            this.aggregateFunction = aggregateFunction;
            result = new Bindable<T>(defaultValue) { Default = defaultValue };
        }

        private readonly Dictionary<WeakReference, IBindable<T>> sourceMapping = new Dictionary<WeakReference, IBindable<T>>();

        public void AddSource(IBindable<T> bindable)
        {
            if (findExistingWeak(bindable) != null)
                return;

            var boundCopy = bindable.GetBoundCopy();
            sourceMapping.Add(new WeakReference(bindable), boundCopy);
            boundCopy.BindValueChanged(recalculateAggregate, true);
        }

        public void RemoveSource(IBindable<T> bindable)
        {
            var weak = findExistingWeak(bindable);
            if (weak != null)
            {
                sourceMapping[weak].UnbindAll();
                sourceMapping.Remove(weak);
            }

            recalculateAggregate();
        }

        private WeakReference findExistingWeak(IBindable<T> bindable) => sourceMapping.Keys.FirstOrDefault(k => k.Target == bindable);

        private void recalculateAggregate(ValueChangedEvent<T> obj = null)
        {
            T calculated = result.Default;

            foreach (var dead in sourceMapping.Keys.Where(k => !k.IsAlive).ToArray())
                sourceMapping.Remove(dead);

            foreach (var s in sourceMapping.Values)
                calculated = aggregateFunction(calculated, s.Value);

            result.Value = calculated;
        }
    }
}
