using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ThreadedMiniMax : MonoBehaviour
{
    private readonly IScorer scorer = new RowScoring();
    private readonly System.Random random = new();
    private int evals = 0;
    public delegate void Next(Vector4Int move);

    public void Evaluate(Board board, int side, int depth, Next next)
    {
#if PLATFORM_WEBGL // WebGL cannot use threads
        float start = Time.realtimeSinceStartup;

        evals = 0;
        Vector4Int result = Evaluate(board, depth, float.NegativeInfinity, float.PositiveInfinity, side == 1, out float score);

        next.Invoke(result);
        float time = Time.realtimeSinceStartup - start;
        Debug.Log("Operations: " + evals + " Time: " + time + " seconds");
#else
        float start = Time.realtimeSinceStartup;
        BackgroundWorker worker = new();
        worker.DoWork += (s, e) =>
        {
            evals = 0;
            e.Result = Evaluate(board, depth, float.NegativeInfinity, float.PositiveInfinity, side == 1, out float score);
        };
        worker.RunWorkerCompleted += (s, e) =>
        {
            next.Invoke((Vector4Int)e.Result);
            float time = Time.realtimeSinceStartup - start;
            Debug.Log("Operations: " + evals + " Time: " + time + " seconds");
        };
        worker.RunWorkerAsync();
#endif
    }

    private Vector4Int Evaluate(Board position, int depth, float alpha, float beta, bool maximizingPlayer, out float score)
    {
        evals++;

        float eval;
        if (depth <= 0 || position.IsGameOver())
        {
            score = scorer.Score(position);
            return null;
        }

        // current best next moves
        List<Vector4Int> bestMoves = new();

        // Get list of possible moves
        List<Vector4Int> moves = position.GetPossibleMoves();

        if (maximizingPlayer)
        {
            // Maximizing
            float maxEval = float.NegativeInfinity;

            foreach (Vector4Int move in moves)
            {
                // Make move
                Board newPosition = position.Clone() as Board;
                newPosition.Move(move);

                // Calculate next move
                Evaluate(newPosition, depth - 1, alpha, beta, false, out eval);

                // Check how well the next move did
                if (eval > maxEval)
                {
                    // this move is better than the last
                    maxEval = eval;
                    bestMoves.Clear();
                    bestMoves.Add(move);
                }
                else if (eval == maxEval)
                    bestMoves.Add(move); // this move is equally good

                // Update alpha
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }

            // return the evaluated score
            score = maxEval;
        }
        else
        {
            // Minimizing
            float minEval = float.PositiveInfinity;

            foreach (Vector4Int move in moves)
            {
                // Make move
                Board newPosition = position.Clone() as Board;
                newPosition.Move(move);

                // Calculate next move
                Evaluate(newPosition, depth - 1, alpha, beta, true, out eval);

                // Check how well the next move did
                if (eval < minEval)
                {
                    // this move is better than the last
                    minEval = eval;
                    bestMoves.Clear();
                    bestMoves.Add(move);
                }
                else if (eval == minEval)
                    bestMoves.Add(move); // this move is equally good

                // Update beta
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }

            // return the evaluated score
            score = minEval;
        }

        // select move from list
        Vector4Int[] movesArr = bestMoves.ToArray();
        Vector4Int returnMove = null;
        if (bestMoves.Count > 0)
        {
            int moveIndex = random.Next(0, movesArr.Length);
            returnMove = movesArr[moveIndex];
        }

        // return
        return returnMove;
    }
}
