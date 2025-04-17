using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public List<Cell> columns = new List<Cell>();
    public List<Image> buttons = new List<Image>();
    public TMP_Text winText;
    public MiniMaxAI ai;

    public int[,] boardState = new int [7,6];

    private int winner = 0;

    private bool humanTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        ButtonColor(Color.red);

        for (int i = 0; i < boardState.GetLength(0); i++)
        {
            for (int j = 0; j < boardState.GetLength(1); j++)
            {
                boardState[i, j] = 0;
            }
        }
    }

    // Sets button color
    private void ButtonColor(Color color)
    {
        foreach(Image i in buttons)
        {
            i.color = color;
        }
    }    

    // Updates visual of the board
    private void UpdateBoard(int col, int row, int player)
    {
        columns[col].cells[row].GetComponent<CellValue>().value = player;

        for (int i = 0; i < columns.Count; i++)
        {
            foreach (GameObject c in columns[i].cells)
            {
                switch (c.GetComponent<CellValue>().value)
                {
                    case 1:
                        c.GetComponent<SpriteRenderer>().color = Color.red;
                        break;
                    case 2:
                        c.GetComponent<SpriteRenderer>().color = Color.yellow;
                        break;
                    default:
                        c.GetComponent<SpriteRenderer>().color = Color.white;
                        break;
                }
            }
        }

        CheckWin();

        switch (player)
        {
            case 1:
                humanTurn = false;
                ButtonColor(Color.yellow);
                if (winner == 0)
                    StartCoroutine(DelayedAIMove());
                break;
            case 2:
                humanTurn = true;
                ButtonColor(Color.red);
                break;
            default:
                humanTurn = true;
                ButtonColor(Color.red);
                break;
        }
    }

    // Checks to see if any player has currently won with the current state of the board
    private void CheckWin()
    {
        int width = boardState.GetLength(0);  // 7
        int height = boardState.GetLength(1); // 6
        bool isBoardFull = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int player = boardState[x, y];

                // Check Draw
                if (player == 0)
                {
                    isBoardFull = false;
                    continue;
                }

                // Check Horizontal
                if (x + 3 < width &&
                    player == boardState[x + 1, y] && player == boardState[x + 2, y] && player == boardState[x + 3, y])
                {
                    EndGame(player);
                    return;
                }

                // Check Vertical
                if (y + 3 < height && player == boardState[x, y + 1] && player == boardState[x, y + 2] && player == boardState[x, y + 3])
                {
                    EndGame(player);
                    return;
                }

                // Check Diagonal Down-Right
                if (x + 3 < width && y + 3 < height && player == boardState[x + 1, y + 1] && player == boardState[x + 2, y + 2] && player == boardState[x + 3, y + 3])
                {
                    EndGame(player);
                    return;
                }

                // Check Diagonal Up-Right
                if (x + 3 < width && y - 3 >= 0 && player == boardState[x + 1, y - 1] && player == boardState[x + 2, y - 2] && player == boardState[x + 3, y - 3])
                {
                    EndGame(player);
                    return;
                }
            }
        }

        if (isBoardFull)
        {
            EndGame(3);
        }
    }

    // Ends the game based if the player won, computer won, or if it was a draw
    private void EndGame(int winningPlayer)
    {
        winner = winningPlayer;
        humanTurn = false;

        switch(winner)
        {
            case 1:
                winText.text = "PLAYER\nWINS!";
                break;
            case 2:
                winText.text = "COMPUTER\nWINS!";
                break;
            case 3:
                winText.text = "DRAW!";
                break;
        }

        foreach (Image i in buttons)
        {
            i.raycastTarget = false;
        }
        ButtonColor(Color.gray);
    }

    // Places a piece in the specified col for the designated player
    public void PlacePiece(int col, int player)
    {
        for (int i = 0; i < columns[col].cells.Count; i++)
        {
            if (columns[col].cells[i].GetComponent<CellValue>().value == 0)
            {
                boardState[col, i] = player;
                UpdateBoard(col, i, player);
                break;
            }
        }
    }

    // Makes the AI make a move
    IEnumerator DelayedAIMove()
    {
        yield return new WaitForSeconds(0.5f);
        ai.MakeAIMove();
    }

    // Gives the current board state when called
    public int[,] GetBoardState()
    {
        return boardState;
    }

    public void ColOne()
    {
        if (humanTurn)
            PlacePiece(0, 1);
    }

    public void ColTwo()
    {
        if (humanTurn)
            PlacePiece(1, 1);
    }

    public void ColThree()
    {
        if (humanTurn)
            PlacePiece(2, 1);
    }

    public void ColFour()
    {
        if (humanTurn)
            PlacePiece(3, 1);
    }

    public void ColFive()
    {
        if (humanTurn)
            PlacePiece(4, 1);
    }

    public void ColSix()
    {
        if (humanTurn)
            PlacePiece(5, 1);
    }

    public void ColSeven()
    {
        if (humanTurn)
            PlacePiece(6, 1);
    }

    public void ResetBoard()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

[System.Serializable]
public class Cell
{
    public List<GameObject> cells = new List<GameObject>();
}