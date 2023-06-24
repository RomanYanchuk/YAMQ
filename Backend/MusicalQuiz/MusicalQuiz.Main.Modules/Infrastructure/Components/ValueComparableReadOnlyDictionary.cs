using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicalQuiz.Main.Modules.Infrastructure.Components;

public class ValueComparableReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>
{
    public ValueComparableReadOnlyDictionary(IDictionary<TKey, TValue> dict) : base(dict)
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

        return obj is ValueComparableReadOnlyDictionary<TKey, TValue> collection &&
               collection.Keys.SequenceEqual(Keys) && collection.Values.SequenceEqual(Values);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var item in Dictionary)
        {
            hashCode.Add(item.Key);
            hashCode.Add(item.Value);
        }

        return hashCode.ToHashCode();
    }
}