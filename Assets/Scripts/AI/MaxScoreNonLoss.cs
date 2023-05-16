using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MaxScoreNonLoss : MonoBehaviour
{
    private readonly IScorer scorer = new RowScoring();
    private readonly System.Random random = new();
    public delegate void Next(Vector4Int move);

    public void Evaluate(Board board, int side, Next next)
    {
        float start = Time.realtimeSinceStartup;

        Vector4Int result = Evaluate(board, side == 1, out float score);

        next.Invoke(result);
        float time = Time.realtimeSinceStartup - start;
        Debug.Log("Time: " + time + " seconds");
    }

    private Vector4Int Evaluate(Board position, bool maximizingPlayer, out float score)
    {
        float eval;
        bool gameover = false;
        if (position.IsGameOver())
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
            score = float.NegativeInfinity;

            foreach (Vector4Int move in moves)
            {
                Board b = position.Clone();
                b.Move(move);

                eval = scorer.Score(b);

                if (eval >= score)
                {
                    // check if next move loses
                    List<Vector4Int> moves2 = b.GetPossibleMoves();

                    foreach (Vector4Int m in moves2)
                    {
                        Board b2 = b.Clone();

                        b2.Move(m);

                        if (b2.IsGameOver())
                        {
                            gameover = true;
                            break;
                        }
                    }

                    if (!gameover)
                    {
                        if (eval == score)
                        {
                            bestMoves.Add(move);
                        }
                        else if (eval > score)
                        {
                            score = eval;
                            bestMoves.Clear();
                            bestMoves.Add(move);
                        }
                    }
                    else
                    {
                        gameover = false;

                        // there are no moves right now so you have to add something (rare chance to lose when there is a better move)
                        if (bestMoves.Count == 0)
                            bestMoves.Add(move);
                    }
                }
            }
        }
        else
        {
            score = float.PositiveInfinity;

            foreach (Vector4Int move in moves)
            {
                Board b = position.Clone();
                b.Move(move);

                eval = scorer.Score(b);

                if (eval <= score)
                {
                    // check if next move loses
                    List<Vector4Int> moves2 = b.GetPossibleMoves();

                    foreach (Vector4Int m in moves2)
                    {
                        Board b2 = b.Clone();

                        b2.Move(m);

                        if (b2.IsGameOver())
                        {
                            gameover = true;
                            break;
                        }
                    }

                    if (!gameover)
                    {
                        if (eval == score)
                        {
                            bestMoves.Add(move);
                        }
                        else if (eval < score)
                        {
                            score = eval;
                            bestMoves.Clear();
                            bestMoves.Add(move);
                        }
                    }
                    else
                    {
                        gameover = false;

                        // there are no moves right now so you have to add something (rare chance to lose when there is a better move)
                        if (bestMoves.Count == 0)
                            bestMoves.Add(move);
                    }
                }
            }
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
