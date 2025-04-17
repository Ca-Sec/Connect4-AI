using System.Collections.Generic;
using UnityEngine;

public class MiniMaxAI : MonoBehaviour
{
    public Board board;
    public int maxDepth = 5;

    // Gets current board state and begins algorithm for move
    public void MakeAIMove()
    {
        int bestCol = GetBestMove(board.GetBoardState(), maxDepth);
        board.PlacePiece(bestCol, 2);
    }

    // Looks for the best current move
    int GetBestMove(int[,] boardState, int depth)
    {
        int bestScore = int.MinValue;
        int bestCol = 0;

        for (int col = 0; col < 7; col++)
        {
            int row = GetNextOpenRow(boardState, col);
            if (row != -1)
            {
                int[,] tempBoard = (int[,])boardState.Clone();
                tempBoard[col, row] = 2;

                int score = MiniMax(tempBoard, depth - 1, false);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestCol = col;
                }
            }
        }

        return bestCol;
    }

    // Essence of a search engine of MiniMax
    int MiniMax(int[,] boardState, int depth, bool isMaximizing)
    {
        int winner = CheckWinner(boardState);
        if (winner == 2) return 1000;
        if (winner == 1) return -1000;
        if (IsFull(boardState) || depth == 0) return EvaluateBoard(boardState);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            for (int col = 0; col < 7; col++)
            {
                int row = GetNextOpenRow(boardState, col);
                if (row != -1)
                {
                    int[,] tempBoard = (int[,])boardState.Clone();
                    tempBoard[col, row] = 2;

                    int eval = MiniMax(tempBoard, depth - 1, false);
                    maxEval = Mathf.Max(maxEval, eval);
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int col = 0; col < 7; col++)
            {
                int row = GetNextOpenRow(boardState, col);
                if (row != -1)
                {
                    int[,] tempBoard = (int[,])boardState.Clone();
                    tempBoard[col, row] = 1;

                    int eval = MiniMax(tempBoard, depth - 1, true);
                    minEval = Mathf.Min(minEval, eval);
                }
            }
            return minEval;
        }
    }

    // Basic Heuristic
    int EvaluateBoard(int[,] boardState)
    {
        int score = 0;

        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (boardState[x, y] == 2)
                {
                    score += 1;
                }
            }
        }

        return score;
    }

    // Helps generate valid moves
    int GetNextOpenRow(int[,] boardState, int col)
    {
        for (int y = 0; y < 6; y++)
        {
            if (boardState[col, y] == 0)
                return y;
        }
        return -1;
    }

    // Checks to see if the col is full or not
    bool IsFull(int[,] boardState)
    {
        for (int x = 0; x < 7; x++)
        {
            if (GetNextOpenRow(boardState, x) != -1)
                return false;
        }
        return true;
    }

    // Checks to see if this moves leads to a win for the designated player
    int CheckWinner(int[,] boardState)
    {
        int width = boardState.GetLength(0);
        int height = boardState.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int player = boardState[x, y];
                if (player == 0) continue;

                // Horizontal
                if (x + 3 < width && player == boardState[x + 1, y] && player == boardState[x + 2, y] && player == boardState[x + 3, y])
                    return player;
                // Vertical
                if (y + 3 < height && player == boardState[x, y + 1] && player == boardState[x, y + 2] && player == boardState[x, y + 3])
                    return player;
                // Diagonal down-right
                if (x + 3 < width && y + 3 < height && player == boardState[x + 1, y + 1] && player == boardState[x + 2, y + 2] && player == boardState[x + 3, y + 3])
                    return player;
                // Diagonal up-right
                if (x + 3 < width && y - 3 >= 0 && player == boardState[x + 1, y - 1] && player == boardState[x + 2, y - 2] && player == boardState[x + 3, y - 3])
                    return player;
            }
        }

        return 0;
    }
}
