﻿using System.Linq;

namespace General.Extensions
{
	public static class ArrayExtensions
    {
        public static T[] getRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
            .Select(x => matrix[rowNumber, x])
            .ToArray();
        }

        public static T[] getColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
            .Select(x => matrix[x, columnNumber])
            .ToArray();
        }
    }
}
