using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicalQuiz.Main.Modules.Infrastructure.Components
{
    public class ValueComparableReadOnlyCollection<T> : ReadOnlyCollection<T>
    {
        public ValueComparableReadOnlyCollection(IList<T> list) : base(list)
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is ValueComparableReadOnlyCollection<T> collection &&
                   collection.Items.SequenceEqual(Items);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            foreach (T item in Items)
            {
                hashCode.Add(item);
            }

            return hashCode.ToHashCode();
        }
    }
}