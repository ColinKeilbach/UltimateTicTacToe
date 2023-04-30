using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxPlayer : Player
{
    private IScorer scorer = new RowScoring();

    public void SetScorer(IScorer scorer) => this.scorer = scorer;

    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        MiniMax.Evaluate(board, scorer, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), then: (move, score) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
}
