using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku;
public class KillerSudokuClass
{
    public static bool SolveKillerSudoku(int[,] grid, List<Cage> cages)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[row, col] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (IsValidMove(grid, row, col, num, cages))
                        {
                            grid[row, col] = num;

                            if (SolveKillerSudoku(grid, cages))
                            {
                                return true;
                            }

                            grid[row, col] = 0;
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    public static bool IsValidMove(int[,] grid, int row, int col, int num, List<Cage> cages)
    {
        for (int i = 0; i < 9; i++)
        {
            if (grid[row, i] == num || grid[i, col] == num)
            {
                return false;
            }
        }

        int startRow = (row / 3) * 3;
        int startCol = (col / 3) * 3;
        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startCol; j < startCol + 3; j++)
            {
                if (grid[i, j] == num)
                {
                    return false;
                }
            }
        }

        foreach (var cage in cages)
        {
            if (cage.Cells.Contains((row, col)))
            {
                int currentSum = 0;
                int emptyCells = 0;

                foreach (var cell in cage.Cells)
                {
                    int r = cell.Item1;
                    int c = cell.Item2;
                    if (grid[r, c] == 0)
                    {
                        emptyCells++;
                    }
                    currentSum += grid[r, c];
                }

                if (currentSum + num > cage.Sum)
                {
                    return false;
                }

                if (emptyCells == 1 && currentSum + num != cage.Sum)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void PrintGrid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Console.Write(grid[row, col] + " ");
            }
            Console.WriteLine();
        }
    }
}
