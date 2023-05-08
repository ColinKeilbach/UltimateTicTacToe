using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiniMaxPlayer : Player
{
    private IScorer scorer = new RowScoring();
    private ThreadedMiniMax tmm = new();
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        if (true)
        {
            tmm.Evaluate(board, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), 5, returnMove.Invoke);
        }
        else
            MiniMax.Evaluate(board, scorer, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), 3, (move, score) =>
            {
                returnMove.Invoke(move);
            });
    }
}
