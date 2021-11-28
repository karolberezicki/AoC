using System.Collections.Generic;

namespace AoC._2018.Day06
{
    public static class MatrixExtensions
    {
        public static T[] GetRow<T>(this T[,] matrix, int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new T[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }

        public static T[] GetCol<T>(this T[,] matrix, int col)
        {
            var colLength = matrix.GetLength(0);
            var colVector = new T[colLength];

            for (var i = 0; i < colLength; i++)
                colVector[i] = matrix[i, col];

            return colVector;
        }

        public static List<T> ToList<T>(this T[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var result = new List<T>(width * height);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    result.Add(matrix[i, j]);
                }
            }

            return result;
        }

        public static void FillArray<T>(this T[,] array, T value)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}
