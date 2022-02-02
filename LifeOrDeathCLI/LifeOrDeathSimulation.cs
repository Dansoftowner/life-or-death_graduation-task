using System;
using System.Collections.Generic;
using System.Text;

namespace LifeOrDeathCLI
{
    class LifeOrDeathSimulation
    {
        private readonly int ColumnsCount;
        private readonly int RowsCount;

        private int[,] Matrix;

        private List<int> rowEdgeCases;
        private List<int> columnEdgeCases;

        public LifeOrDeathSimulation(int columnsCount, int rowsCount, Action<int[,]> initMatrix)
        {
            ColumnsCount = columnsCount;
            RowsCount = rowsCount;
            Matrix = new int[RowsCount + 2, ColumnsCount + 2];
            rowEdgeCases = new List<int>() { 0, RowsCount - 1 };
            columnEdgeCases = new List<int>() { 0, ColumnsCount - 1 };
            initMatrix(Matrix);
        }

        public LifeOrDeathSimulation(int columnsCount, int rowsCount) : this(columnsCount, rowsCount, m => { })
        {
            InitializeMatrix(Matrix);
        }

        private void InitializeMatrix(int[,] matrix)
        {
            var random = new Random();
            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColumnsCount; col++)
                {
                    matrix[row, col] = random.Next(2);
                }
            }
            matrix[0, 0] = 0;
            matrix[0, ColumnsCount - 1] = 0;
            matrix[RowsCount - 1, 0] = 0;
            matrix[RowsCount - 1, ColumnsCount - 1] = 0;
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public void NextState()
        {
            var temp = new int[RowsCount + 2, ColumnsCount + 2];

            for (int row = 1; row < RowsCount - 1; row++)
            {
                for (int col = 1; col < ColumnsCount - 1; col++)
                {
                    var neighbours = GetNeighboursCount(row, col);

                    var cell = Matrix[row, col];
                    if (cell == 1)
                    {
                        bool remains = neighbours == 2 || neighbours == 3;
                        if (remains)
                            temp[row, col] = 1;
                    }
                    else
                    {
                        bool reincarnates = neighbours == 3;
                        if (reincarnates)
                            temp[row, col] = 1;
                    }
                }
            }
            Matrix = temp;
        }

        public void Run()
        {
            Print();
            NextState();
            Thread.Sleep(500);
        }

        private int GetNeighboursCount(int row, int col)
        {
            return new List<int?>() {
                GetNeighbourSafely(row, col -1), // left
                GetNeighbourSafely(row, col + 1), // right
                GetNeighbourSafely(row -1, col), // up
                GetNeighbourSafely(row + 1, col), // down
                GetNeighbourSafely(row -1, col + 1), // up right
                GetNeighbourSafely(row -1, col - 1), // up left
                GetNeighbourSafely(row +1, col -1), // down left
                GetNeighbourSafely(row +1, col +1) // down right
            }.Count(it => it == 1);
        }

        private int? GetNeighbourSafely(int row, int col)
        {
            try
            {
                if (IsEdgeCase(row, col))
                {
                    throw new Exception();
                }

                return Matrix[row, col];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsEdgeCase(int row, int col)
        {
            return rowEdgeCases.Contains(row) || columnEdgeCases.Contains(col);
        }

        public override string ToString()
        {
            Dictionary<int, string> mappings = new Dictionary<int, string>()
            {
                {0, " "},
                {1, "S"}
            };

            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColumnsCount; col++)
                    if (IsEdgeCase(row, col))
                        sb.Append("X");
                    else
                        sb.Append(mappings[Matrix[row, col]]);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
