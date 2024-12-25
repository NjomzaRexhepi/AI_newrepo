using System;
using System.Collections.Generic;

namespace KillerSudoku
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Select Difficulty Level (1: Easy, 2: Medium, 3: Hard):");
            int difficultyLevel = int.Parse(Console.ReadLine());

            int[,] grid;
            List<Cage> cages;

            switch (difficultyLevel)
            {
                case 1:
                    grid = GetEasyGrid();
                    cages = GetEasyCages();
                    break;

                case 2:
                    grid = GetMediumGrid();
                    cages = GetMediumCages();
                    break;

                case 3:
                    grid = GetHardGrid();
                    cages = GetHardCages();
                    break;

                default:
                    Console.WriteLine("Invalid difficulty level.");
                    return;
            }

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

        static int[,] GetEasyGrid()
        {
            return new int[9, 9]
            {
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
        }

        static List<Cage> GetEasyCages()
        {
            return new List<Cage>
            {
                new Cage(15, new List<(int, int)> { (0, 0), (0, 1), (1, 0) }),
                new Cage(10, new List<(int, int)> { (0, 2), (1, 1), (1, 2) }),
                new Cage(20, new List<(int, int)> { (2, 0), (2, 1), (2, 2) }),
                new Cage(7, new List<(int, int)> { (3, 0), (3, 1) }),
                new Cage(16, new List<(int, int)> { (4, 4), (5, 4), (6, 4) }),
                new Cage(18, new List<(int, int)> { (8, 7), (8, 8), (7, 8) })
            };
        }

        static int[,] GetMediumGrid()
        {
            return new int[9, 9]
            {
                { 0, 3, 0, 0, 0, 0, 0, 9, 0 },
                { 0, 0, 0, 9, 0, 0, 0, 0, 4 },
                { 0, 0, 7, 0, 5, 0, 0, 8, 0 },
                { 0, 6, 0, 0, 0, 0, 0, 0, 3 },
                { 9, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 5, 0, 0, 7, 0, 0 },
                { 0, 9, 0, 7, 3, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 7, 2 },
                { 7, 0, 0, 0, 0, 0, 9, 0, 0 }
            };
        }

        static List<Cage> GetMediumCages()
        {
            return new List<Cage>
            {
                new Cage(20, new List<(int, int)> { (0, 1), (1, 0), (1, 1), (1, 2) }),
                new Cage(15, new List<(int, int)> { (2, 3), (2, 4), (3, 1) }),
                new Cage(10, new List<(int, int)> { (4, 0), (5, 0), (5, 1) }),
                new Cage(24, new List<(int, int)> { (6, 1), (6, 2), (7, 4), (7, 3) }),
                new Cage(16, new List<(int, int)> { (8, 0), (7, 0), (6, 0) }),
                new Cage(18, new List<(int, int)> { (5, 3), (6, 4) })
            };
        }

        static int[,] GetHardGrid()
        {
            return new int[9, 9]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
        }

        static List<Cage> GetHardCages()
        {
            return new List<Cage>
            {
                new Cage(15, new List<(int, int)> { (0, 0), (0, 1), (1, 0) }),
                new Cage(10, new List<(int, int)> { (0, 2), (1, 1), (1, 2) }),
                new Cage(20, new List<(int, int)> { (2, 0), (2, 1), (2, 2) }),
                new Cage(7, new List<(int, int)> { (3, 0), (3, 1) }),
                new Cage(16, new List<(int, int)> { (4, 4), (5, 4), (6, 4) }),
                new Cage(18, new List<(int, int)> { (8, 7), (8, 8), (7, 8) }),
            };
        }
    }
}
