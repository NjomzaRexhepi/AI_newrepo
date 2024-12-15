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

            { 5, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 4 },
            { 0, 0, 0, 0, 8, 0, 0, 0, 0 },
            { 0, 0, 4, 0, 0, 0, 0, 0, 0 },
            { 0, 6, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 9, 0, 0, 0, 0 },
            { 0, 0, 0, 4, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 5, 0 }

        };

            List<Cage> cages = new List<Cage>
        {

            new Cage(15, new List<(int, int)> { (0, 0), (0, 1), (1, 0) }),
            new Cage(10, new List<(int, int)> { (0, 2), (1, 1), (1, 2) }),
            new Cage(20, new List<(int, int)> { (2, 0), (2, 1), (2, 2) }),
            new Cage(7, new List<(int, int)> { (3, 0), (3, 1) }),
            new Cage(16, new List<(int, int)> { (4, 4), (5, 4), (6, 4) }),
            new Cage(18, new List<(int, int)> { (8, 7), (8, 8), (7, 8) }),
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
