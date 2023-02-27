using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AIPlayer : Player
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

    public void SetStyle(Style style) => this.style = style;

    private void Awake()
    {
        gh = FindObjectOfType<GameHandler>();
    }

    /// <summary>
    /// Move randomly based on possible moves
    /// </summary>
    /// <param name="board"></param>
    private void RandomMove(List<Vector4Int> possibleMoves, ReturnMove returnMove)
    {
        Vector4Int target = possibleMoves[Random.Range(0, possibleMoves.Count)];
        returnMove.Invoke(target);
        Thinking = false;
    }

    /// <summary>
    /// Move based on MiniMax algorythm
    /// </summary>
    /// <param name="board"></param>
    private void MiniMaxMove(Board board, ReturnMove returnMove)
    {
        MiniMax.Evaluate(board, new RowScoring(), Tile.ValueToInt(side), then: (move, score) =>
        {
            returnMove.Invoke(move);
            Thinking = false;
        });
    }

    /// <summary>
    /// Move based on MiniMax algorythm when it is not the first move
    /// </summary>
    /// <param name="board"></param>
    private void HybridMove(Board board, ReturnMove returnMove)
    {
        if (board.GetMovesMade() == 0)
        {
            RandomMove(board.GetPossibleMoves(), returnMove);
        }
        else
        {
            MiniMaxMove(board, returnMove);
        }
    }

    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        List<Vector4Int> possibleMoves = board.GetPossibleMoves();
        if (possibleMoves.Count > 0)
        {
            Thinking = true;
            switch (style)
            {
                case Style.Random:
                    RandomMove(possibleMoves, returnMove);
                    break;

                case Style.MiniMax:
                    MiniMaxMove(board, returnMove);
                    break;

                case Style.Hybrid:
                    HybridMove(board, returnMove);
                    break;
            }
        }
    }
}
