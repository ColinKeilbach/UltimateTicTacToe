using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField]
    public enum Style
    {
        Random,
        MiniMax,
        Hybrid
    }

    [SerializeField]
    private Style style;
    [SerializeField]
    private Tile.Value side = Tile.Value.O;

    private GameHandler gh;
    [SerializeField]
    private GameObject clickPrevention;

    private bool calculating = false;
    public bool Calculating
    {
        get { return calculating; }
        set
        {
            calculating = value;
            clickPrevention.SetActive(value);
        }
    }

    public void SetStyle(Style style) => this.style = style;

    private void Awake()
    {
        gh = FindObjectOfType<GameHandler>();
    }

    private void FixedUpdate()
    {
        Board board = gh.GetBoard();
        // not calculating and is my turn
        if (!Calculating && board.GetXToMove() == (Tile.Value.X == side))
        {
            List<Vector4Int> possibleMoves = board.GetPossibleMoves();
            if (possibleMoves.Count > 0)
            {
                Calculating = true;
                switch (style)
                {
                    case Style.Random:
                        RandomMove(possibleMoves);
                        break;

                    case Style.MiniMax:
                        MiniMaxMove(board);
                        break;

                    case Style.Hybrid:
                        HybridMove(board);
                        break;
                }
            }
        }

        // prevent the user from moving on the AI's turn
        if (Calculating)
            clickPrevention.SetActive(true);
    }

    /// <summary>
    /// Move randomly based on possible moves
    /// </summary>
    /// <param name="board"></param>
    private void RandomMove(List<Vector4Int> possibleMoves)
    {
        Vector4Int target = possibleMoves[Random.Range(0, possibleMoves.Count)];
        gh.Move(target);

        Calculating = false;
    }

    /// <summary>
    /// Move based on MiniMax algorythm
    /// </summary>
    /// <param name="board"></param>
    private void MiniMaxMove(Board board)
    {
        MiniMax.Evaluate(board, new RowScoring(), Tile.ValueToInt(side), then: (move, score) =>
        {
            gh.Move(move);
            Calculating = false;
        });
    }

    /// <summary>
    /// Move based on MiniMax algorythm when it is not the first move
    /// </summary>
    /// <param name="board"></param>
    private void HybridMove(Board board)
    {
        if (board.GetMovesMade() == 0)
        {
            RandomMove(board.GetPossibleMoves());
        }
        else
        {
            MiniMaxMove(board);
        }
    }
}
