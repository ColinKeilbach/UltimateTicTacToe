using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Board : ICloneable
{
    #region Variables

    // 1 = X, 0 = Blank, -1 = O
    private int boardValue;
    private int[,] mainBoard = new int[3, 3];
    private int[,,,] subBoards = new int[3, 3, 3, 3];
    private bool xToMove = true;
    private bool freeMove = true;
    private int[] targetBoard = new int[2];
    private int movesMade = 0;

    private int currentTurn { get => xToMove ? 1 : -1; }

    #endregion

    #region Constructors

    public Board()
    {
        // initializes the boards to 0 (empty)
        boardValue = 0;

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                    for (int w = 0; w < 3; w++)
                        subBoards[x, y, z, w] = 0;

                mainBoard[x, y] = 0;
            }
    }

    #endregion

    #region Getters and Setters

    public int GetBoardValue() => boardValue;
    public int GetValue(int x, int y, int z, int w) => mainBoard[x, y] == 0 ? subBoards[x, y, z, w] : mainBoard[x, y];
    public int GetValue(int x, int y) => mainBoard[x, y];
    public int GetValue() => boardValue;
    public int GetValueFromSubboard(int x, int y, int z, int w) => subBoards[x, y, z, w];
    public bool GetXToMove() => xToMove;
    public bool GetFreeMove() => freeMove;
    public bool IsGameOver() => boardValue != 0;
    public Vector2Int GetTargetBoard() => new(targetBoard[0], targetBoard[1]);
    public int GetMovesMade() => movesMade;

    private void ToggleXToMove() => xToMove = !xToMove;

    private void SetValue(int x, int y, int value)
    {
        mainBoard[x, y] = value;
        CheckWin();
    }
    private void SetValue(int x, int y, int z, int w, int value)
    {
        subBoards[x, y, z, w] = value;
        Update(x, y);
    }

    #endregion

    #region Gameplay

    public void Move(Vector4Int v) => Move(v.x, v.y, v.z, v.w);
    public void Move(int x, int y, int z, int w)
    {
        freeMove = false;
        SetValue(x, y, z, w, currentTurn);
        ToggleXToMove();

        if (GetValue(z, w) != 0 || GetBlanks(z, w) == 0)
            freeMove = true;
        else
        {
            targetBoard[0] = z;
            targetBoard[1] = w;
        }

        movesMade++;
    }

    private void CheckWin()
    {
        RowScoring rs = new();

#if UNITY_EDITOR
        Debug.LogWarning("CheckWin() uses the GetThreeInARow() function from RowScoring.");
#endif

        if (rs.GetThreeInARow(this, -1))
            boardValue = -1;
        else if (rs.GetThreeInARow(this, 1))
            boardValue = 1;
    }
    private void Update(int x, int y)
    {
        RowScoring rs = new();

        if (rs.GetThreeInARow(this, -1, x, y))
            SetValue(x, y, -1);
        else if (rs.GetThreeInARow(this, 1, x, y))
            SetValue(x, y, 1);
    }

    public List<Vector4Int> GetPossibleMoves()
    {
        if (GetFreeMove())
        {
            List<Vector4Int> output = new();

            if (boardValue != 0)
                return output;

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        for (int w = 0; w < 3; w++)
                            if (GetValue(x, y, z, w) == 0)
                                output.Add(new Vector4Int(x, y, z, w));

            return output;
        }
        else
            return GetPossibleMoves(GetTargetBoard());
    }
    private List<Vector4Int> GetPossibleMoves(Vector2Int targetGrid) => GetPossibleMoves(targetGrid.x, targetGrid.y);
    private List<Vector4Int> GetPossibleMoves(int x, int y)
    {
        List<Vector4Int> output = new();

        if (boardValue != 0 || GetValue(x, y) != 0)
            return output;

        for (int z = 0; z < 3; z++)
            for (int w = 0; w < 3; w++)
                if (GetValue(x, y, z, w) == 0)
                    output.Add(new Vector4Int(x, y, z, w));

        return output;
    }

    public int GetBlanks(int x, int y)
    {
        int count = 0;

        for (int z = 0; z < 3; z++)
            for (int w = 0; w < 3; w++)
                if (GetValueFromSubboard(x, y, z, w) == 0)
                    count++;

        return count;
    }

    #endregion

    #region ICloneable Implementation

    /// <summary>
    /// Deep clone of Board
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
        using MemoryStream stream = new();
        BinaryFormatter formatter = new();

        formatter.Serialize(stream, this);
        stream.Position = 0;
        return formatter.Deserialize(stream);
    }

    #endregion
}
