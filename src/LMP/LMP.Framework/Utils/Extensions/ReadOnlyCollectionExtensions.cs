﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LMP.Utils.Extensions {
    public static class ReadOnlyCollectionExtensions {
        public static IList<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable) {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }
    }
}