using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScorer
{
    /// <summary>
    /// Returns a score based on how good each side is doing
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public long Score(Board board);
}
