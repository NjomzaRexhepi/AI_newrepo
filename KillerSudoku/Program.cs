using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[,] grid = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

            List<Cage> cages = new List<Cage>
        {
            new Cage(15, new List<(int, int)> { (0, 0), (0, 1), (1, 0) }),
            new Cage(10, new List<(int, int)> { (0, 2), (1, 1), (1, 2) }),
        };

            if (KillerSudokuClass.SolveKillerSudoku(grid, cages))
            {
                Console.WriteLine("Killer Sudoku solved successfully:");
                KillerSudokuClass.PrintGrid(grid);
            }
            else
            {
                Console.WriteLine("No solution found for the given Killer Sudoku puzzle.");
            }
        }
    }
}
