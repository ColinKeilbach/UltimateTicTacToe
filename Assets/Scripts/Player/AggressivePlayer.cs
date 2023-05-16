using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressivePlayer : Player
{
    private readonly MaxScoreNonLoss msnl = new();
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        StartCoroutine(MakeMove(board, returnMove));
    }

    private IEnumerator MakeMove(Board board, ReturnMove returnMove)
    {
        yield return null;
        msnl.Evaluate(board, Tile.ValueToInt(board.GetXToMove() ? Tile.Value.X : Tile.Value.O), (move) =>
        {
            Thinking = false;
            returnMove.Invoke(move);
        });
    }
}
