using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asocjacje
{
    public static class Extentions
    {

        public static IEnumerable<IEnumerable<T>>
            Permutations<T>( this IEnumerable<T>  list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return Permutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
