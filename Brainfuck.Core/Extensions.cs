namespace Brainfuck
{
    using System.Collections.Generic;

    public static class Extensions
    {
        public static bool InBounds<T>(this ICollection<T> ts, int index)
        {
            return index >= 0 & index < ts.Count;
        }
    }
}