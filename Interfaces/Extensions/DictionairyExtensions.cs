using System.Collections.Generic;

namespace Interfaces.Extensions
{
    public static class DictionairyExtensions
    {
        public static IEnumerable<(int,int)> Neighbors<T>(this Dictionary<(int,int),T> dict, (int,int)CurrentPosition, bool includeDiagonal=false)
        {
            if (dict.ContainsKey((CurrentPosition.Item1 + 1, CurrentPosition.Item2))) yield return (CurrentPosition.Item1+1, CurrentPosition.Item2);
            if (dict.ContainsKey((CurrentPosition.Item1 - 1, CurrentPosition.Item2))) yield return (CurrentPosition.Item1-1, CurrentPosition.Item2);
            if (dict.ContainsKey((CurrentPosition.Item1 , CurrentPosition.Item2 + 1))) yield return (CurrentPosition.Item1, CurrentPosition.Item2+1);
            if (dict.ContainsKey((CurrentPosition.Item1 , CurrentPosition.Item2 -1 ))) yield return (CurrentPosition.Item1, CurrentPosition.Item2-1);

            if (includeDiagonal)
            {
                if (dict.ContainsKey((CurrentPosition.Item1 + 1, CurrentPosition.Item2+1))) yield return (CurrentPosition.Item1 + 1, CurrentPosition.Item2 + 1);
                if (dict.ContainsKey((CurrentPosition.Item1 - 1, CurrentPosition.Item2+1))) yield return (CurrentPosition.Item1 - 1, CurrentPosition.Item2 + 1);
                if (dict.ContainsKey((CurrentPosition.Item1-1, CurrentPosition.Item2 - 1))) yield return (CurrentPosition.Item1-1, CurrentPosition.Item2 - 1);
                if (dict.ContainsKey((CurrentPosition.Item1+1, CurrentPosition.Item2 - 1))) yield return (CurrentPosition.Item1+1, CurrentPosition.Item2 - 1);
            }
        }
    }
}
