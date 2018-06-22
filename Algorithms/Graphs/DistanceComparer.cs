using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public sealed class DistanceComparer : IComparer<int?>
    {
        public int Compare(int? x, int? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            return Comparer<int>.Default.Compare(x.Value, y.Value);
        }
    }
}