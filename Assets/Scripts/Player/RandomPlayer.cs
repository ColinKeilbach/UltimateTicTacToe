using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayer : Player
{
    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        StartCoroutine(MakeMove(board, returnMove));
    }

    private IEnumerator MakeMove(Board board, ReturnMove returnMove)
    {
        yield return null; // waits for the next frame to move (prevents the game from getting stuck in RandomPlayer vs RandomPlayer matches)
        List<Vector4Int> possibleMoves = board.GetPossibleMoves();
        Vector4Int target = possibleMoves[Random.Range(0, possibleMoves.Count)];
        Thinking = false;
        returnMove.Invoke(target);
    }
}
