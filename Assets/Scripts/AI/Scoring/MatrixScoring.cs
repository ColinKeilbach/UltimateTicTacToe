using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MatrixScoring : IScorer
{
    private const int X_WIN = 100000;
    private const int O_WIN = -100000;
    private readonly int[,,] scores = {
        { // X
        {  // X
                X_WIN, // X
                10, // Blank
                0 // O
            },
         {  // Blank
                10, // X
                1, // Blank
                0 // O
            },
         {  // O
                0, // X
                0, // Blank
                0 // O
            },
        },
        { // Blank
        {  // X
                10, // X
                1, // Blank
                0 // O
            },
         {  // Blank
                1, // X
                0, // Blank
                -1 // O
            },
         {  // O
                0, // X
                -1, // Blank
                -10 // O
            },
        },
        { // O
        {  // X
                0, // X
                0, // Blank
                0 // O
            },
         {  // Blank
                0, // X
                -1, // Blank
                -10 // O
            },
         {  // O
                0, // X
                -10, // Blank
                O_WIN // O
            },
        },
    };

    public long Score(Board board)
    {
        if (board.GetBoardValue() != 0)
            return board.GetBoardValue() * X_WIN * X_WIN;

        int[] cels = new int[9];
        long score = 0;
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                int i = x + y * 3;
                cels[i] = board.GetValue(x, y);
                score += cels[i] == 0 ? ScoreSmall(x,y,board) : cels[i] * X_WIN;
                cels[i]++;
            }

        score +=
            scores[cels[0], cels[1], cels[2]] +
            scores[cels[3], cels[4], cels[5]] +
            scores[cels[6], cels[7], cels[8]] +
            scores[cels[0], cels[3], cels[6]] +
            scores[cels[1], cels[4], cels[7]] +
            scores[cels[2], cels[5], cels[8]] +
            scores[cels[0], cels[4], cels[8]] +
            scores[cels[6], cels[4], cels[2]];

        return score;
    }

    public int ScoreSmall(int x, int y, Board board)
    {
        // add 1 so that -1 = 0
        int tl = board.GetValue(x, y, 0, 0) + 1;
        int tm = board.GetValue(x, y, 1, 0) + 1;
        int tr = board.GetValue(x, y, 2, 0) + 1;
        int ml = board.GetValue(x, y, 0, 1) + 1;
        int mm = board.GetValue(x, y, 1, 1) + 1;
        int mr = board.GetValue(x, y, 2, 1) + 1;
        int bl = board.GetValue(x, y, 0, 2) + 1;
        int bm = board.GetValue(x, y, 1, 2) + 1;
        int br = board.GetValue(x, y, 2, 2) + 1;

        int score =
            scores[tl, tm, tr] +
            scores[ml, mm, mr] +
            scores[bl, bm, br] +
            scores[tl, ml, bl] +
            scores[tm, mm, bm] +
            scores[tr, mr, br] +
            scores[tl, mm, br] +
            scores[bl, mm, tr];

        score = Mathf.Clamp(score, O_WIN, X_WIN);

        return score;
    }
}
