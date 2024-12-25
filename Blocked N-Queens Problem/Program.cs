using System;
using System.Collections.Generic;

class BlockedNQueensAStar
{
    static void Main()
    {
        // Get user input for N (board size)
        Console.Write("Enter the board size (N): ");
        int N;
        while (!int.TryParse(Console.ReadLine(), out N) || N < 1)
        {
            Console.Write("Invalid input. Please enter a positive integer for N: ");
        }

        // Get user input for blocked cells
        Console.Write("Enter the number of blocked cells: ");
        int blockedCount;
        while (!int.TryParse(Console.ReadLine(), out blockedCount) || blockedCount < 0)
        {
            Console.Write("Invalid input. Please enter a non-negative integer for blocked cells count: ");
        }

        int[,] blockedOnes = new int[blockedCount, 2];

        for (int i = 0; i < blockedCount; i++)
        {
            Console.WriteLine($"Enter the coordinates of blocked cell {i + 1} (row, col): ");
            int row, col;
            while (true)
            {
                Console.Write("Row: ");
                if (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row >= N)
                {
                    Console.WriteLine("Invalid row. It must be between 0 and N-1.");
                    continue;
                }

                Console.Write("Column: ");
                if (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col >= N)
                {
                    Console.WriteLine("Invalid column. It must be between 0 and N-1.");
                    continue;
                }

                // Check for duplicate blocked cells
                bool duplicateFound = false;
                for (int j = 0; j < i; j++)
                {
                    if (blockedOnes[j, 0] == row && blockedOnes[j, 1] == col)
                    {
                        duplicateFound = true;
                        Console.WriteLine("This blocked cell already exists. Please enter a different one.");
                        break;
                    }
                }

                if (!duplicateFound)
                {
                    blockedOnes[i, 0] = row;
                    blockedOnes[i, 1] = col;
                    break;
                }
            }
        }

        // Validate inputs
        if (!ValidateInputs(N, blockedOnes))
        {
            Console.WriteLine("Invalid inputs.");
            return;
        }

        SolveBlockedNQueensAStar(N, blockedOnes);
    }

    static bool ValidateInputs(int N, int[,] blockedCells)
    {
        if (N < 1)
        {
            Console.WriteLine("Board size (N) must be greater than or equal to 1.");
            return false;
        }

        // Validate blocked cells
        if (blockedCells == null || blockedCells.GetLength(0) == 0)
        {
            Console.WriteLine("Blocked cells cannot be null or empty.");
            return false;
        }

        var blockedSet = new HashSet<string>();

        for (int i = 0; i < blockedCells.GetLength(0); i++)
        {
            int row = blockedCells[i, 0];
            int col = blockedCells[i, 1];

            if (row < 0 || row >= N || col < 0 || col >= N)
            {
                Console.WriteLine($"Blocked cell ({row}, {col}) is out of bounds.");
                return false;
            }

            string key = $"{row},{col}";
            if (blockedSet.Contains(key))
            {
                Console.WriteLine($"Duplicate blocked cell: ({row}, {col}).");
                return false;
            }

            blockedSet.Add(key);
        }

        return true;
    }

    static void SolveBlockedNQueensAStar(int N, int[,] blockedCells)
    {
        var initialState = new State(CreateBoard(N, blockedCells), 0, 0);
        var solution = AStarSearch(initialState, N);

        if (solution != null)
        {
            Console.WriteLine("Solution found!");
            PrintBoard(solution.Board);
        }
        else
        {
            Console.WriteLine("No solution found.");
        }
    }

    static State AStarSearch(State initialState, int N)
    {
        var openSet = new PriorityQueue<State>();
        var closedSet = new HashSet<string>();

        openSet.Enqueue(initialState, initialState.Cost);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current.QueensPlaced == N)
                return current;

            string stateKey = GetStateKey(current.Board);
            if (closedSet.Contains(stateKey))
                continue;

            closedSet.Add(stateKey);

            foreach (var nextState in GenerateNextStates(current, N))
            {
                if (!closedSet.Contains(GetStateKey(nextState.Board)))
                {
                    openSet.Enqueue(nextState, nextState.Cost);
                }
            }
        }

        return null;
    }

    static IEnumerable<State> GenerateNextStates(State currentState, int N)
    {
        var nextStates = new List<State>();
        int[,] currentBoard = currentState.Board;
        int row = currentState.QueensPlaced;

        for (int col = 0; col < N; col++)
        {
            if (CanPlaceQueen(currentBoard, row, col))
            {
                var newBoard = (int[,])currentBoard.Clone();
                newBoard[row, col] = 1;
                int heuristic = CalculateHeuristic(newBoard, N);
                nextStates.Add(new State(newBoard, currentState.QueensPlaced + 1, currentState.G + 1 + heuristic));
            }
        }

        return nextStates;
    }

    static int[,] CreateBoard(int N, int[,] blockedCells)
    {
        var board = new int[N, N];

        for (int i = 0; i < blockedCells.GetLength(0); i++)
        {
            int row = blockedCells[i, 0];
            int col = blockedCells[i, 1];
            board[row, col] = -1;
        }

        return board;
    }

    static int CalculateHeuristic(int[,] board, int N)
    {
        int conflicts = 0;

        for (int row = 0; row < N; row++)
        {
            for (int col = 0; col < N; col++)
            {
                if (board[row, col] == 1)
                {
                    conflicts += CountConflicts(board, row, col, N);
                }
            }
        }

        return conflicts;
    }

    static int CountConflicts(int[,] board, int row, int col, int N)
    {
        int conflicts = 0;

        for (int i = 0; i < N; i++)
        {
            if (i != row && board[i, col] == 1) conflicts++;
        }

        for (int i = -N; i < N; i++)
        {
            if (i != 0 && IsValid(row + i, col + i, N) && board[row + i, col + i] == 1) conflicts++;
            if (i != 0 && IsValid(row + i, col - i, N) && board[row + i, col - i] == 1) conflicts++;
        }

        return conflicts;
    }

    static bool IsValid(int row, int col, int N)
    {
        return row >= 0 && col >= 0 && row < N && col < N;
    }

    static bool CanPlaceQueen(int[,] board, int row, int col)
    {
        if (board[row, col] == -1) return false;

        for (int i = 0; i < row; i++)
        {
            if (board[i, col] == 1) return false;
        }

        for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (board[i, j] == 1) return false;
        }

        for (int i = row - 1, j = col + 1; i >= 0 && j < board.GetLength(1); i--, j++)
        {
            if (board[i, j] == 1) return false;
        }

        return true;
    }

    static string GetStateKey(int[,] board)
    {
        int N = board.GetLength(0);
        var key = new char[N * N];
        int index = 0;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                key[index++] = board[i, j] == -1 ? 'X' : board[i, j] == 1 ? 'Q' : '.';
            }
        }

        return new string(key);
    }

    static void PrintBoard(int[,] board)
    {
        int N = board.GetLength(0);

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (board[i, j] == -1)
                    Console.Write("X ");
                else if (board[i, j] == 1)
                    Console.Write("Q ");
                else
                    Console.Write(". ");
            }
            Console.WriteLine();
        }
    }
}

class State
{
    public int[,] Board { get; }
    public int QueensPlaced { get; }
    public int G { get; }
    public int Cost { get; }

    public State(int[,] board, int queensPlaced, int cost)
    {
        Board = board;
        QueensPlaced = queensPlaced;
        G = queensPlaced;
        Cost = cost;
    }
}

class PriorityQueue<T>
{
    private readonly SortedDictionary<int, Queue<T>> _elements = new();

    public int Count { get; private set; }

    public void Enqueue(T item, int priority)
    {
        if (!_elements.ContainsKey(priority))
        {
            _elements[priority] = new Queue<T>();
        }

        _elements[priority].Enqueue(item);
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0) throw new InvalidOperationException("The queue is empty.");

        var firstPair = _elements.First();
        var item = firstPair.Value.Dequeue();

        if (firstPair.Value.Count == 0)
        {
            _elements.Remove(firstPair.Key);
        }

        Count--;
        return item;
    }
}
