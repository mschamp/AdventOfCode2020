using System.Collections.Generic;

namespace Interfaces.Extensions
{
    public static class DictionairyExtensions
    {
        public static IEnumerable<(int, int)> Neighbors<T>(this Dictionary<(int, int), T> dict, (int, int) CurrentPosition, bool includeDiagonal = false)
        {
            if (dict.ContainsKey((CurrentPosition.Item1 + 1, CurrentPosition.Item2))) yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2);
            if (dict.ContainsKey((CurrentPosition.Item1 - 1, CurrentPosition.Item2))) yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2);
            if (dict.ContainsKey((CurrentPosition.Item1, CurrentPosition.Item2 + 1))) yield return (CurrentPosition.Item1, CurrentPosition.Item2 + 1);
            if (dict.ContainsKey((CurrentPosition.Item1, CurrentPosition.Item2 - 1))) yield return (CurrentPosition.Item1, CurrentPosition.Item2 - 1);

            if (includeDiagonal)
            {
                if (dict.ContainsKey((CurrentPosition.Item1 + 1, CurrentPosition.Item2 + 1))) yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2 + 1);
                if (dict.ContainsKey((CurrentPosition.Item1 - 1, CurrentPosition.Item2 + 1))) yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2 + 1);
                if (dict.ContainsKey((CurrentPosition.Item1 - 1, CurrentPosition.Item2 - 1))) yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2 - 1);
                if (dict.ContainsKey((CurrentPosition.Item1 + 1, CurrentPosition.Item2 - 1))) yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2 - 1);
            }
        }

        public static IEnumerable<(int, int)> Neighbors(this (int, int) CurrentPosition, bool includeDiagonal = false)
        {
            yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2);
            yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2);
            yield return (CurrentPosition.Item1, CurrentPosition.Item2 + 1);
            yield return (CurrentPosition.Item1, CurrentPosition.Item2 - 1);

            if (includeDiagonal)
            {
                yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2 + 1);
                yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2 + 1);
                yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2 - 1);
                yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2 - 1);
            }
        }

        public static IEnumerable<(int, int)> Neighbors(this (int x, int y) current, (int x, int y) min, (int x, int y) max, bool includeDiagonal = false)
        {
            if (current.x > min.x) yield return (current.x - 1, current.y);
            if (current.y > min.y) yield return (current.x, current.y - 1);
            if (current.x < max.x) yield return (current.x + 1, current.y);
            if (current.y < max.y) yield return (current.x, current.y + 1);

            if (includeDiagonal)
            {
                if (current.x > min.x && current.y > min.y) yield return (current.x - 1, current.y - 1);
                if (current.x < max.x && current.y > min.y) yield return (current.x + 1, current.y - 1);
                if (current.x < max.x && current.y < max.y) yield return (current.x + 1, current.y + 1);
                if (current.x > min.x && current.y < max.y) yield return (current.x - 1, current.y + 1);
            }
        }
    }
}
