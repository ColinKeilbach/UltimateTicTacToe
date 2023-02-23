using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountScoring : IScorer
{
    public long Score(Board board)
    {
        long score = 0;

        if (board.GetValue() != 0)
            return board.GetValue() * 100000; // who won

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                if (board.GetValue(x, y) != 0)
                    score += board.GetValue(x, y) * 100;
                else
                {
                    for (int z = 0; z < 3; z++)
                        for (int w = 0; w < 3; w++)
                            score += board.GetValue(x, y, z, w);
                }
            }

        return score;
    }
}
