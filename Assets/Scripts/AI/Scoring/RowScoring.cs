using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RowScoring : IScorer
{
    private enum Axis
    {
        None = -1,
        // Diagonal
        TLBR,
        TRBL,
        // Horizontal
        Top,
        Middle,
        Bottom,
        // Vertical
        Left,
        Center,
        Right
    }

    const long scoreLarge3InARow = 1000000000000000;
    const long scoreLarge2InARow = 1000000000000;
    const long scoreLarge1InARow = 1000000000;
    const long scoreSmall3InARow = 1000000;
    const long scoreSmall2InARow = 1000;
    const long scoreSmall1InARow = 1;

    public long Score(Board board)
    {
        long score = 0;

        // three in a row on main board
        if (board.IsGameOver())
            return board.GetValue() * scoreLarge3InARow;

        // two in a row on main board
        score += scoreLarge2InARow * GetTwoInARow(board, 1);
        score += -scoreLarge2InARow * GetTwoInARow(board, -1);

        // one in a row on main board
        score += scoreLarge1InARow * GetOneInARow(board, 1);
        score += -scoreLarge1InARow * GetOneInARow(board, -1);

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                //three in a row on small board
                if (GetThreeInARow(board, 1, x, y))
                    score += scoreSmall3InARow;
                if (GetThreeInARow(board, -1, x, y))
                    score += -scoreSmall3InARow;

                // two in a row on small board
                score += scoreSmall2InARow * GetTwoInARow(board, 1, x, y);
                score += -scoreSmall2InARow * GetTwoInARow(board, -1, x, y);

                // one in a row on small board
                score += scoreSmall1InARow * GetOneInARow(board, 1, x, y);
                score += -scoreSmall1InARow * GetOneInARow(board, -1, x, y);
            }

        return score;
    }

    #region Scoring

    private int GetNumberInLine(Board b, int side, Axis axis, int x, int y)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            switch (axis)
            {
                case Axis.TLBR:
                    if (b.GetValue(x, y, i, i) == side)
                        count++;
                    break;
                case Axis.TRBL:
                    if (b.GetValue(x, y, i, 2 - i) == side)
                        count++;
                    break;
                case Axis.Top:
                    if (b.GetValue(x, y, i, 0) == side)
                        count++;
                    break;
                case Axis.Middle:
                    if (b.GetValue(x, y, i, 1) == side)
                        count++;
                    break;
                case Axis.Bottom:
                    if (b.GetValue(x, y, i, 2) == side)
                        count++;
                    break;
                case Axis.Left:
                    if (b.GetValue(x, y, 0, i) == side)
                        count++;
                    break;
                case Axis.Center:
                    if (b.GetValue(x, y, 1, i) == side)
                        count++;
                    break;
                case Axis.Right:
                    if (b.GetValue(x, y, 2, i) == side)
                        count++;
                    break;
            }
        }

        return count;
    }
    private int GetNumberInLine(Board b, int side, Axis axis)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            switch (axis)
            {
                case Axis.TLBR:
                    if (b.GetValue(i, i) == side)
                        count++;
                    break;
                case Axis.TRBL:
                    if (b.GetValue(i, 2 - i) == side)
                        count++;
                    break;
                case Axis.Top:
                    if (b.GetValue(i, 0) == side)
                        count++;
                    break;
                case Axis.Middle:
                    if (b.GetValue(i, 1) == side)
                        count++;
                    break;
                case Axis.Bottom:
                    if (b.GetValue(i, 2) == side)
                        count++;
                    break;
                case Axis.Left:
                    if (b.GetValue(0, i) == side)
                        count++;
                    break;
                case Axis.Center:
                    if (b.GetValue(1, i) == side)
                        count++;
                    break;
                case Axis.Right:
                    if (b.GetValue(2, i) == side)
                        count++;
                    break;
            }
        }

        return count;
    }

    public bool GetThreeInARow(Board b, int side, int x, int y)
    {
        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i, x, y) == 3)
                return true;
        }

        return false;
    }
    public bool GetThreeInARow(Board b, int side)
    {
        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i) == 3)
                return true;
        }

        return false;
    }

    private int GetTwoInARow(Board b, int side, int x, int y)
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i, x, y) == 2)
                if (GetNumberInLine(b, -side, (Axis)i, x, y) == 0)
                    count++;
        }

        return count;
    }
    private int GetTwoInARow(Board b, int side)
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i) == 2)
                if (GetNumberInLine(b, -side, (Axis)i) == 0)
                    count++;
        }

        return count;
    }

    private int GetOneInARow(Board b, int side, int x, int y)
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i, x, y) == 1)
                if (GetNumberInLine(b, -side, (Axis)i, x, y) == 0)
                    count++;
        }

        return count;
    }
    private int GetOneInARow(Board b, int side)
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            if (GetNumberInLine(b, side, (Axis)i) == 1)
                if (GetNumberInLine(b, -side, (Axis)i) == 0)
                    count++;
        }

        return count;
    }

    private int GetMaxInARow(Board b, int side, int x, int y)
    {
        int score = 0;

        if (GetOneInARow(b, side, x, y) > 0)
            score = 1;

        if (GetTwoInARow(b, side, x, y) > 0)
            score = 2;

        if (GetThreeInARow(b, side, x, y))
            score = 3;

        return score;
    }
    private int GetMaxInARow(Board b, int side)
    {
        int score = 0;

        if (GetOneInARow(b, side) > 0)
            score = 1;

        if (GetTwoInARow(b, side) > 0)
            score = 2;

        if (GetThreeInARow(b, side))
            score = 3;

        return score;
    }
    #endregion
}
