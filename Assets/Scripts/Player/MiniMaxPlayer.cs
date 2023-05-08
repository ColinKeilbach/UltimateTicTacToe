using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiniMaxPlayer : Player
{
    private readonly ThreadedMiniMax tmm = new();
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        tmm.Evaluate(board, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), 5, (move) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
}
