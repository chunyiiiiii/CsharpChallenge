using System;
using System.Diagnostics;

namespace MatrixMultiplication
{
    class Strassen
    {
        static void Main(string[] args)
        {
            // 定义两个128x128的矩阵，用于对比两种算法效率
            int n = 256;
            int[,] matrixA = GenerateRandomMatrix(n);
            int[,] matrixB = GenerateRandomMatrix(n);

            //定义两个矩阵，用于测试两种算法结果是否都正确
            //int[,] matrixA = {
            //    {1, 2, 3, 4},
            //    {5, 6, 7, 8},
            //    {9, 10, 11, 12},
            //    {13, 14, 15, 16}
            //};

            //int[,] matrixB = {
            //    {17, 18, 19, 20},
            //    {21, 22, 23, 24},
            //    {25, 26, 27, 28},
            //    {29, 30, 31, 32}
            //};

            //打印输入矩阵(可选, 矩阵较大时不建议打印)
            Console.WriteLine("矩阵 A:");
            PrintMatrix(matrixA);
            Console.WriteLine("\n矩阵 B:");
            PrintMatrix(matrixB);

            int numTests = 10;
            int[,] resultConventional;
            int[,] resultStrassen;

            // 常规算法测试
            long totalTimeConventional = 0;
            for (int i = 0; i < numTests; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                resultConventional = ConventionalMatrixMultiplication(matrixA, matrixB);
                stopwatch.Stop();
                totalTimeConventional += stopwatch.ElapsedMilliseconds;
                
            }
            // 打印结果矩阵 (可选, 矩阵较大时不建议打印)
            resultConventional = ConventionalMatrixMultiplication(matrixA, matrixB);
            Console.WriteLine("\n常规算法结果:");
            PrintMatrix(resultConventional);

            long averageTimeConventional = totalTimeConventional / numTests;
            Console.WriteLine($"\n常规算法平均耗时：{averageTimeConventional} 毫秒");

            // Strassen算法测试
            long totalTimeStrassen = 0;
            for (int i = 0; i < numTests; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                resultStrassen = StrassenMatrixMultiplication(matrixA, matrixB);
                stopwatch.Stop();
                totalTimeStrassen += stopwatch.ElapsedMilliseconds;
            }
            // 打印结果矩阵 (可选, 矩阵较大时不建议打印)
            resultStrassen = StrassenMatrixMultiplication(matrixA, matrixB);
            Console.WriteLine("\nStrassen算法结果:");
            PrintMatrix(resultStrassen);
            long averageTimeStrassen = totalTimeStrassen / numTests;
            Console.WriteLine($"\nStrassen算法平均耗时：{averageTimeStrassen} 毫秒");

            Console.ReadKey();
        }

        // 生成随机矩阵
        static int[,] GenerateRandomMatrix(int n)
        {
            Random random = new Random();
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }
            return matrix;
        }

        // 常规矩阵乘法算法
        static int[,] ConventionalMatrixMultiplication(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }

        // Strassen矩阵乘法算法
        static int[,] StrassenMatrixMultiplication(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);

            if (n <= 16) // 调整阈值，根据实际情况选择
            {
                return ConventionalMatrixMultiplication(A, B);
            }

            int[,] A11 = GetSubMatrix(A, 0, 0, n / 2);
            int[,] A12 = GetSubMatrix(A, 0, n / 2, n / 2);
            int[,] A21 = GetSubMatrix(A, n / 2, 0, n / 2);
            int[,] A22 = GetSubMatrix(A, n / 2, n / 2, n / 2);
            int[,] B11 = GetSubMatrix(B, 0, 0, n / 2);
            int[,] B12 = GetSubMatrix(B, 0, n / 2, n / 2);
            int[,] B21 = GetSubMatrix(B, n / 2, 0, n / 2);
            int[,] B22 = GetSubMatrix(B, n / 2, n / 2, n / 2);

            int[,] P1 = StrassenMatrixMultiplication(AddMatrices(A11, A22), AddMatrices(B11, B22));
            int[,] P2 = StrassenMatrixMultiplication(AddMatrices(A21, A22), B11);
            int[,] P3 = StrassenMatrixMultiplication(A11, SubtractMatrices(B12, B22));
            int[,] P4 = StrassenMatrixMultiplication(A22, SubtractMatrices(B21, B11));
            int[,] P5 = StrassenMatrixMultiplication(AddMatrices(A11, A12), B22);
            int[,] P6 = StrassenMatrixMultiplication(SubtractMatrices(A21, A11), AddMatrices(B11, B12));
            int[,] P7 = StrassenMatrixMultiplication(SubtractMatrices(A12, A22), AddMatrices(B21, B22));

            int[,] C11 = AddMatrices(SubtractMatrices(AddMatrices(P1, P4), P5), P7);
            int[,] C12 = AddMatrices(P3, P5);
            int[,] C21 = AddMatrices(P2, P4);
            int[,] C22 = AddMatrices(SubtractMatrices(AddMatrices(P1, P3), P2), P6);

            return CombineSubMatrices(C11, C12, C21, C22);
        }

        // 获取子矩阵
        static int[,] GetSubMatrix(int[,] M, int rowStart, int colStart, int size)
        {
            int[,] subMatrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    subMatrix[i, j] = M[rowStart + i, colStart + j];
                }
            }
            return subMatrix;
        }

        // 合并子矩阵
        static int[,] CombineSubMatrices(int[,] C11, int[,] C12, int[,] C21, int[,] C22)
        {
            int n = C11.GetLength(0) * 2;
            int[,] C = new int[n, n];
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    C[i, j] = C11[i, j];
                    C[i, j + n / 2] = C12[i, j];
                    C[i + n / 2, j] = C21[i, j];
                    C[i + n / 2, j + n / 2] = C22[i, j];
                }
            }
            return C;
        }

        // 矩阵加法
        static int[,] AddMatrices(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }
            return C;
        }

        // 矩阵减法
        static int[,] SubtractMatrices(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }
            return C;
        }

        // 打印矩阵 (可选, 矩阵较大时不建议打印)
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}