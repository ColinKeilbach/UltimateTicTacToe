using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScoring : IScorer
{
    public long Score(Board board)
    {
        return ScoreFullBoard(board, 1) - ScoreFullBoard(board, -1);
    }

    private long ScoreFullBoard(Board b, int side)
    {
        if (b.IsGameOver())
            return b.GetValue() * 10000000;

        // get board scores
        int topLeft = (side * b.GetValue(0, 0) + 1) * 1000;
        int topMid = (side * b.GetValue(0, 1) + 1) * 1000;
        int topRight = (side * b.GetValue(0, 2) + 1) * 1000;
        int midLeft = (side * b.GetValue(1, 0) + 1) * 1000;
        int midMid = (side * b.GetValue(1, 1) + 1) * 1000;
        int midRight = (side * b.GetValue(1, 2) + 1) * 1000;
        int botLeft = (side * b.GetValue(2, 0) + 1) * 1000;
        int botMid = (side * b.GetValue(2, 1) + 1) * 1000;
        int botRight = (side * b.GetValue(2, 2) + 1) * 1000;

        // check if it should score ninths
        if (topLeft == 1) topLeft = ScoreNinth(b, 0, 0, side);
        if (topMid == 1) topMid = ScoreNinth(b, 0, 1, side);
        if (topRight == 1) topRight = ScoreNinth(b, 0, 2, side);
        if (midLeft == 1) midLeft = ScoreNinth(b, 1, 0, side);
        if (midMid == 1) midMid = ScoreNinth(b, 1, 1, side);
        if (midRight == 1) midRight = ScoreNinth(b, 1, 2, side);
        if (botLeft == 1) botLeft = ScoreNinth(b, 2, 0, side);
        if (botMid == 1) botMid = ScoreNinth(b, 2, 1, side);
        if (botRight == 1) botRight = ScoreNinth(b, 2, 2, side);

        // put in score sections
        int botRightDiagonal = topLeft * midMid * botRight;
        int botLeftDiagonal = topRight * midMid * botLeft;
        int leftCol = topLeft * midLeft * botLeft;
        int midCol = topMid * midMid * botMid;
        int rightCol = topRight * midRight * botRight;
        int topRow = topLeft * topMid * topRight;
        int midRow = midLeft * midMid * midRight;
        int botRow = botLeft * botMid * botRight;

        return botRightDiagonal + botLeftDiagonal + leftCol + midCol + rightCol + topRow + midRow + botRow;
    }

    private int ScoreNinth(Board b, int x, int y, int side)
    {
        // get board scores
        int topLeft = side * b.GetValue(x, y, 0, 0) + 1;
        int topMid = side * b.GetValue(x, y, 0, 1) + 1;
        int topRight = side * b.GetValue(x, y, 0, 2) + 1;
        int midLeft = side * b.GetValue(x, y, 1, 0) + 1;
        int midMid = side * b.GetValue(x, y, 1, 1) + 1;
        int midRight = side * b.GetValue(x, y, 1, 2) + 1;
        int botLeft = side * b.GetValue(x, y, 2, 0) + 1;
        int botMid = side * b.GetValue(x, y, 2, 1) + 1;
        int botRight = side * b.GetValue(x, y, 2, 2) + 1;

        // put in score sections
        int botRightDiagonal = topLeft * midMid * botRight;
        int botLeftDiagonal = topRight * midMid * botLeft;
        int leftCol = topLeft * midLeft * botLeft;
        int midCol = topMid * midMid * botMid;
        int rightCol = topRight * midRight * botRight;
        int topRow = topLeft * topMid * topRight;
        int midRow = midLeft * midMid * midRight;
        int botRow = botLeft * botMid * botRight;

        return botRightDiagonal + botLeftDiagonal + leftCol + midCol + rightCol + topRow + midRow + botRow;
    }
}
