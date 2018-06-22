using System.Collections.Generic;

namespace Algorithms
{
    public static class Extensions
    {
        public static int IndexOfMediane<T>(this IList<T> items, int index1, int index2, int index3, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            var a = items[index1];
            var b = items[index2];
            var c = items[index3];
            if (comparer.Compare(a, b) > 0) // a > b
            {
                if (comparer.Compare(a, c) < 0) // a < c
                {
                    return index1; // a
                }
                if (comparer.Compare(b, c) > 0) // b > c
                {
                    return index2; // b
                }
                return index3; // c
            }
            if (comparer.Compare(b, c) < 0) // b < c
            {
                return index2; // b
            }
            if (comparer.Compare(a, c) > 0) // a > c
            {
                return index1; // a
            }
            return index3; // c
        }

        public static void Swap<T>(this IList<T> items, int index1, int index2)
        {
            var tmp = items[index1];
            items[index1] = items[index2];
            items[index2] = tmp;
        }

        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            for (int i = 0, n = array.GetLength(0); i < n; i++)
            {
                yield return array[row, i];
            }
        }
    }
}