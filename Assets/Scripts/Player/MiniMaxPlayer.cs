using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiniMaxPlayer : Player
{
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        MiniMax.Evaluate(board, new RowScoring(), Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), then: (move, score) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
}
