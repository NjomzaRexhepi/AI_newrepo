using System;
using System.Collections.Generic;

namespace ChessGame;
public class ChessGame
{
    private const int BoardSize = 8;
    private char[,] board;
    private Stack<(string move, char capturedPiece)> moveHistory;
    private List<string> recentMoves;

    public ChessGame()
    {
        board = new char[BoardSize, BoardSize]
        {
            { 'r', '.', 'b', 'q', 'k', 'b', '.', 'r' },
            { 'p', 'p', 'p', '.', '.', 'p', 'p', 'p' },
            { '.', '.', 'n', '.', 'p', '.', '.', '.' },
            { '.', '.', '.', 'P', 'P', '.', '.', '.' },
            { '.', '.', '.', 'p', 'N', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { 'P', 'P', 'P', 'P', '.', 'P', 'P', 'P' },
            { 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R' }
        };
        moveHistory = new Stack<(string move, char capturedPiece)>();
        recentMoves = new List<string>();
    }

    public void PrintBoard()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public int EvaluateBoard()
    {
        int score = 0;
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                char piece = board[row, col];
                score += GetPieceValue(piece);
            }
        }
        return score;
    }

    private int GetPieceValue(char piece)
    {
        switch (piece)
        {
            case 'P': return 1;
            case 'N': return 3;
            case 'B': return 3;
            case 'R': return 5;
            case 'Q': return 9;
            case 'K': return 1000;
            case 'p': return -1;
            case 'n': return -3;
            case 'b': return -3;
            case 'r': return -5;
            case 'q': return -9;
            case 'k': return -1000;
            default: return 0;
        }
    }

    public List<string> GetPossibleMoves(char player)
    {
        List<string> moves = new List<string>();
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                char piece = board[row, col];
                if (char.IsUpper(piece) && player == 'P' || char.IsLower(piece) && player == 'p')
                {
                    moves.AddRange(GenerateMovesForPiece(row, col, piece));
                }
            }
        }
        return moves;
    }

    private List<string> GenerateMovesForPiece(int row, int col, char piece)
    {
        switch (char.ToLower(piece))
        {
            case 'p': return GeneratePawnMoves(row, col, piece);
            case 'r': return GenerateRookMoves(row, col, piece);
            case 'n': return GenerateKnightMoves(row, col, piece);
            case 'b': return GenerateBishopMoves(row, col, piece);
            case 'q': return GenerateQueenMoves(row, col, piece);
            default: return new List<string>();
        }
    }

    private List<string> GeneratePawnMoves(int row, int col, char player)
    {
        List<string> moves = new List<string>();
        int direction = player == 'P' ? -1 : 1;

        if (IsValidMove(row + direction, col) && board[row + direction, col] == '.')
        {
            moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(col + 'a')}{8 - (row + direction)}");

            if ((player == 'P' && row == 6 || player == 'p' && row == 1) && IsValidMove(row + 2 * direction, col) && board[row + 2 * direction, col] == '.')
            {
                moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(col + 'a')}{8 - (row + 2 * direction)}");
            }
        }

        foreach (int dCol in new[] { -1, 1 })
        {
            if (IsValidMove(row + direction, col + dCol) && IsOpponentPiece(board[row + direction, col + dCol], player))
            {
                moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(col + dCol + 'a')}{8 - (row + direction)}");
            }
        }

        return moves;
    }

    public List<string> GenerateRookMoves(int row, int col, char player)
    {
        return GenerateLinearMoves(row, col, player, new[] { (1, 0), (0, 1), (-1, 0), (0, -1) });
    }

    public List<string> GenerateBishopMoves(int row, int col, char player)
    {
        return GenerateLinearMoves(row, col, player, new[] { (1, 1), (1, -1), (-1, 1), (-1, -1) });
    }

    public List<string> GenerateQueenMoves(int row, int col, char player)
    {
        return GenerateLinearMoves(row, col, player, new[] { (1, 0), (0, 1), (-1, 0), (0, -1), (1, 1), (1, -1), (-1, 1), (-1, -1) });
    }

    public List<string> GenerateKnightMoves(int row, int col, char player)
    {
        List<string> moves = new List<string>();
        int[,] directions = new int[,]
        {
            { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
            { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newRow = row + directions[i, 0];
            int newCol = col + directions[i, 1];
            if (IsValidMove(newRow, newCol) && !IsSamePlayerPiece(board[newRow, newCol], player))
            {
                moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(newCol + 'a')}{8 - newRow}");
            }
        }

        return moves;
    }

    public List<string> GenerateKingMoves(int row, int col, char player)
    {
        List<string> moves = new List<string>();
        int[,] directions = new int[,]
        {
            { 1, 0 }, { 1, 1 }, { 1, -1 }, { 0, 1 },
            { 0, -1 }, { -1, 0 }, { -1, 1 }, { -1, -1 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newRow = row + directions[i, 0];
            int newCol = col + directions[i, 1];
            if (IsValidMove(newRow, newCol) && !IsSamePlayerPiece(board[newRow, newCol], player))
            {
                moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(newCol + 'a')}{8 - newRow}");
            }
        }

        return moves;
    }

    private List<string> GenerateLinearMoves(int row, int col, char player, (int, int)[] directions)
    {
        List<string> moves = new List<string>();

        foreach (var (dr, dc) in directions)
        {
            int newRow = row + dr;
            int newCol = col + dc;

            while (IsValidMove(newRow, newCol))
            {
                if (board[newRow, newCol] == '.')
                {
                    moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(newCol + 'a')}{8 - newRow}");
                }
                else
                {
                    if (IsOpponentPiece(board[newRow, newCol], player))
                    {
                        moves.Add($"{(char)(col + 'a')}{8 - row}{(char)(newCol + 'a')}{8 - newRow}");
                    }
                    break;
                }
                newRow += dr;
                newCol += dc;
            }
        }

        return moves;
    }

    private bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
    }

    private bool IsOpponentPiece(char piece, char player)
    {
        return char.IsUpper(piece) && char.IsLower(player) || char.IsLower(piece) && char.IsUpper(player);
    }

    private bool IsSamePlayerPiece(char piece, char player)
    {
        return char.IsUpper(piece) && char.IsUpper(player) || char.IsLower(piece) && char.IsLower(player);
    }

    private bool IsKingInDanger(char player)
    {
        char king = player == 'P' ? 'K' : 'k';
        int kingRow = -1, kingCol = -1;

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (board[row, col] == king)
                {
                    kingRow = row;
                    kingCol = col;
                    break;
                }
            }
            if (kingRow != -1) break;
        }

        char opponent = player == 'P' ? 'p' : 'P';
        List<string> opponentMoves = GetPossibleMoves(opponent);
        foreach (string move in opponentMoves)
        {
            int endRow = 8 - (move[3] - '0');
            int endCol = move[2] - 'a';
            if (endRow == kingRow && endCol == kingCol)
            {
                Console.WriteLine($"King in danger: {move}");
                return true;
            }
        }

        return false;
    }

    public int Minimax(int depth, bool isMaximizingPlayer, int alpha, int beta)
    {
        if (depth == 0)
        {
            return EvaluateBoard();
        }

        List<string> possibleMoves = GetPossibleMoves(isMaximizingPlayer ? 'P' : 'p');

        if (possibleMoves.Count == 0)
        {
            return isMaximizingPlayer ? int.MinValue : int.MaxValue;
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (string move in possibleMoves)
            {
                MakeMove(move);
                int eval = Minimax(depth - 1, false, alpha, beta);
                UndoMove(move);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                {
                    break;
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (string move in possibleMoves)
            {
                MakeMove(move);
                int eval = Minimax(depth - 1, true, alpha, beta);
                UndoMove(move);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                {
                    break;
                }
            }
            return minEval;
        }
    }


    public string FindBestMove(int depth, char player)
    {
        List<string> possibleMoves = GetPossibleMoves(player);
        string bestMove = null;
        int bestValue = player == 'P' ? int.MinValue : int.MaxValue;
        int alpha = int.MinValue;
        int beta = int.MaxValue;

        foreach (string move in possibleMoves)
        {
            if (IsMoveRepeated(move))
            {
                continue;
            }

            MakeMove(move);
            int moveValue = Minimax(depth - 1, player == 'P' ? false : true, alpha, beta);
            UndoMove(move);

            if (player == 'P' && moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
            else if (player == 'p' && moveValue < bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }

        if (IsKingInDanger(player))
        {
            List<string> kingMoves = GenerateKingMovesForDanger(player);
            foreach (string move in kingMoves)
            {
                if (IsMoveRepeated(move))
                {
                    continue;
                }

                MakeMove(move);
                if (!IsKingInDanger(player))
                {
                    bestMove = move;
                    UndoMove(move);
                    break;
                }
                UndoMove(move);
            }
        }

        return bestMove;
    }


    private List<string> GenerateKingMovesForDanger(char player)
    {
        List<string> moves = new List<string>();
        char king = player == 'P' ? 'K' : 'k';
        int kingRow = -1, kingCol = -1;

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (board[row, col] == king)
                {
                    kingRow = row;
                    kingCol = col;
                    break;
                }
            }
            if (kingRow != -1) break;
        }

        int[,] directions = new int[,]
        {
        { 1, 0 }, { 1, 1 }, { 1, -1 }, { 0, 1 },
        { 0, -1 }, { -1, 0 }, { -1, 1 }, { -1, -1 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newRow = kingRow + directions[i, 0];
            int newCol = kingCol + directions[i, 1];
            if (IsValidMove(newRow, newCol) && !IsSamePlayerPiece(board[newRow, newCol], player))
            {
                moves.Add($"{(char)(kingCol + 'a')}{8 - kingRow}{(char)(newCol + 'a')}{8 - newRow}");
            }
        }

        return moves;
    }

    private bool IsMoveRepeated(string move)
    {
        int repeatCount = 0;
        foreach (string recentMove in recentMoves)
        {
            if (recentMove == move)
            {
                repeatCount++;
                if (repeatCount >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void MakeMove(string move)
    {
        if (move.Length != 4)
        {
            throw new ArgumentException("Invalid move format");
        }

        int startCol = move[0] - 'a';
        int startRow = 8 - (move[1] - '0');
        int endCol = move[2] - 'a';
        int endRow = 8 - (move[3] - '0');

        if (!IsValidMove(startRow, startCol) || !IsValidMove(endRow, endCol))
        {
            throw new ArgumentException("Invalid move coordinates");
        }

        char capturedPiece = board[endRow, endCol];
        moveHistory.Push((move, capturedPiece));

        board[endRow, endCol] = board[startRow, startCol];
        board[startRow, startCol] = '.';

        recentMoves.Add(move);
        if (recentMoves.Count > 20)
        {
            recentMoves.RemoveAt(0);
        }
    }

    private void UndoMove(string move)
    {
        if (move.Length != 4)
        {
            throw new ArgumentException("Invalid move format");
        }

        int startCol = move[0] - 'a';
        int startRow = 8 - (move[1] - '0');
        int endCol = move[2] - 'a';
        int endRow = 8 - (move[3] - '0');

        if (!IsValidMove(startRow, startCol) || !IsValidMove(endRow, endCol))
        {
            throw new ArgumentException("Invalid move coordinates");
        }

        var (lastMove, capturedPiece) = moveHistory.Pop();


        board[startRow, startCol] = board[endRow, endCol];
        board[endRow, endCol] = capturedPiece;

        recentMoves.RemoveAt(recentMoves.Count - 1);
    }

    private bool IsGameOver()
    {
        bool whiteKingExists = false;
        bool blackKingExists = false;

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (board[row, col] == 'K')
                {
                    whiteKingExists = true;
                }
                if (board[row, col] == 'k')
                {
                    blackKingExists = true;
                }
            }
        }

        return !whiteKingExists || !blackKingExists || GetPossibleMoves('P').Count == 0 || GetPossibleMoves('p').Count == 0;
    }

    public static void Main()
    {
        ChessGame game = new ChessGame();
        game.PrintBoard();

        char currentPlayer = 'P';
        int moveCount = 0;

        while (!game.IsGameOver() && moveCount < 200)
        {
            string bestMove = game.FindBestMove(3, currentPlayer);
            if (bestMove == null)
            {
                Console.WriteLine($"{(currentPlayer == 'P' ? "White" : "Black")} has no valid moves.");
                break;
            }

            game.MakeMove(bestMove);
            Console.WriteLine($"{(currentPlayer == 'P' ? "White" : "Black")} moves: {bestMove}");
            game.PrintBoard();

            currentPlayer = currentPlayer == 'P' ? 'p' : 'P';
            moveCount++;
        }

        Console.WriteLine("Game over.");
    }
}
