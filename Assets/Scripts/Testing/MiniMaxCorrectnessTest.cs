using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxCorrectnessTest : MonoBehaviour
{
    [SerializeField]
    private int depth;
    [SerializeField]
    private float numberOfTests;

    // Start is called before the first frame update
    void Start()
    {
        IScorer scorer = new CountScoring();
        Board board = TestOne();

        List<Vector4Int> goodMoves = GetGoodMovesMin(board);

        for (int i = 0; i < numberOfTests; i++)
        {
            MiniMax.Evaluate(board, scorer, -1, depth, (v, s) =>
            {
                Board b = board.Clone() as Board;
                bool isGood = goodMoves.Contains(v);
                b.Move(v);
                Debug.Log(v + (isGood ? " is a good move!" : " is not a good move!") + " : " + scorer.Score(board));

                    MiniMax.Evaluate(b, scorer, 1, depth - 1, (counter, counterScore) => {
                        List<Vector4Int> counters = GetGoodMovesMax(b);
                        Debug.Log("Worst counter move: " + GetGoodMovesMin(b)[0]);
                        b.Move(counter);
                        bool isGood = counters.Contains(counter);
                        Debug.Log(counter + (isGood ? " is a good counter move!" : " is not a good counter move!") + " : " + scorer.Score(b));

                        MiniMax.Evaluate(b, scorer, -1, depth - 2, (counter, counterScore) => {
                            List<Vector4Int> counters = GetGoodMovesMin(b);
                            b.Move(counter);
                            bool isGood = counters.Contains(counter);
                            Debug.Log(counter + (isGood ? " is a good counter move!" : " is not a good counter move!") + " : " + scorer.Score(b));
                        });
                    });
            });
        }
    }

    private Board TestOne()
    {
        Board board = new();

        board.Move(1, 0, 0, 0); // X
        board.Move(0, 0, 1, 1); // O

        board.Move(1, 1, 0, 0); // X
        board.Move(0, 0, 2, 1); // O

        board.Move(2, 1, 0, 0); // X
        board.Move(0, 0, 0, 1); // O

        board.Move(0, 1, 1, 0); // X
        board.Move(1, 0, 0, 2); // O

        board.Move(0, 2, 1, 0); // X
        board.Move(1, 0, 2, 2); // O

        board.Move(2, 2, 0, 0); // X

        return board;
    }

    private Board TestTwo()
    {
        Board board = new();

        board.Move(1, 0, 0, 0); // X
        board.Move(0, 0, 1, 1); // O

        board.Move(1, 1, 0, 0); // X
        board.Move(0, 0, 2, 1); // O

        board.Move(2, 1, 0, 0); // X
        board.Move(0, 0, 0, 1); // O

        board.Move(0, 1, 1, 0); // X
        board.Move(1, 0, 0, 2); // O

        board.Move(0, 2, 1, 0); // X
        board.Move(1, 0, 2, 2); // O

        board.Move(2, 2, 0, 1); // X
        board.Move(0, 1, 0, 2); // O

        board.Move(0, 2, 1, 2); // X
        board.Move(1, 2, 0, 2); // O

        board.Move(0, 2, 1, 1); // X
        board.Move(1, 1, 0, 1); // O

        board.Move(0, 1, 1, 0); // X

        return board;
    }

    private List<Vector4Int> GetGoodMovesMin(Board position)
    {
        List<Vector4Int> goodMoves = new();
        float bestScore = float.PositiveInfinity;

        List<Vector4Int> possibleMoves = position.GetPossibleMoves();

        foreach (Vector4Int move in possibleMoves)
        {
            Board b = position.Clone() as Board;
            b.Move(move);
            float score = new CountScoring().Score(b);

            if (score < bestScore)
            {
                goodMoves.Clear();
                goodMoves.Add(move);
                bestScore = score;
            }
            else if (score == bestScore)
            {
                goodMoves.Add(move);
            }
        }

        return goodMoves;
    }

    private List<Vector4Int> GetGoodMovesMax(Board position)
    {
        List<Vector4Int> goodMoves = new();
        float bestScore = float.NegativeInfinity;

        List<Vector4Int> possibleMoves = position.GetPossibleMoves();

        foreach (Vector4Int move in possibleMoves)
        {
            Board b = position.Clone() as Board;
            b.Move(move);
            float score = new CountScoring().Score(b);

            if (score > bestScore)
            {
                goodMoves.Clear();
                goodMoves.Add(move);
                bestScore = score;
            }
            else if (score == bestScore)
            {
                goodMoves.Add(move);
            }
        }

        return goodMoves;
    }
}
