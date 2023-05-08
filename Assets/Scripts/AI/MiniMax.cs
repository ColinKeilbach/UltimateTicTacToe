using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMax : MonoBehaviour
{
    private static MiniMax instance;
    private static int operations = 0;
    private static IScorer scorer;

    public delegate void Then(Vector4Int move, float score);

    public static void Evaluate(Board board, IScorer scorer, int side, int depth = 3, Then then = null)
    {
        if (instance == null)
        {
            Transform t = FindObjectOfType<Transform>();
            instance = t.AddComponent<MiniMax>();
        }

        float startTime = Time.realtimeSinceStartup;

        MiniMax.scorer = scorer;
        instance.StartCoroutine(Minimax(board, depth, float.NegativeInfinity, float.PositiveInfinity, side == 1, (move, score) =>
        {
            then?.Invoke(move, score);
            Debug.Log("Depth: " + depth + " Score: " + score + " Move: " + move + "\nOperations: " + operations + " Time: " + (Time.realtimeSinceStartup - startTime) + " seconds");
            operations = 0;
        }));
    }

    // Minimax based on Sebastian Lague

    private static float returnScore = 0;

    private static IEnumerator Minimax(Board position, int depth, float alpha, float beta, bool maximizingPlayer, Then then = null)
    {
        if (depth <= 0 || position.IsGameOver())
        {
            returnScore = scorer.Score(position);
            then?.Invoke(null, returnScore);
            yield break;
        }

        // current best next moves
        HashSet<Vector4Int> bestMoves = new();

        // Get list of possible moves
        List<Vector4Int> moves = position.GetPossibleMoves();

        if (maximizingPlayer)
        {
            // Maximizing
            float maxEval = float.NegativeInfinity;

            foreach (Vector4Int move in moves)
            {
                operations++;

                // Make move
                Board newPosition = position.Clone() as Board;
                newPosition.Move(move);

                // Calculate next move
                yield return Minimax(newPosition, depth - 1, alpha, beta, false);
                float eval = returnScore;

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
            returnScore = maxEval;
        }
        else
        {
            // Minimizing
            float minEval = float.PositiveInfinity;

            foreach (Vector4Int move in moves)
            {
                operations++;

                // Make move
                Board newPosition = position.Clone() as Board;
                newPosition.Move(move);

                // Calculate next move
                yield return Minimax(newPosition, depth - 1, alpha, beta, true);
                float eval = returnScore;

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
            returnScore = minEval;
        }

        // select move from list
        Vector4Int[] movesArr = bestMoves.ToArray();
        Vector4Int returnMove = null;
        if (bestMoves.Count > 0)
        {
            int moveIndex = Random.Range(0, movesArr.Length);
            returnMove = movesArr[moveIndex];
        }

        // run then
        then?.Invoke(returnMove, returnScore);
    }
}
