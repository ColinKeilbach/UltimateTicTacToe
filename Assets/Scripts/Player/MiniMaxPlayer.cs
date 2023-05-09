using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MiniMaxPlayer : Player
{
# if PLATFORM_WEBGL
    private readonly IScorer scorer = new RowScoring();
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        MiniMax.Evaluate(board, scorer, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), 3, (move, score) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
#else
    private readonly ThreadedMiniMax tmm = new();
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        tmm.Evaluate(board, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), 5, (move) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
#endif
}
