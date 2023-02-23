using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScorer
{
    /// <summary>
    /// Returns a score between -10000 and 10000 based on how good each side is doing
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public long Score(Board board);
}
