using System.Collections.Generic;
using System.Linq;

namespace MusicalQuiz.Main.Modules.Infrastructure.Components;

public static class EnumerableExtensions
{
    public static ValueComparableReadOnlyCollection<T> ToReadonly<T>(this IEnumerable<T> enumerable)
    {
        return new ValueComparableReadOnlyCollection<T>(enumerable.ToList());
    }
}