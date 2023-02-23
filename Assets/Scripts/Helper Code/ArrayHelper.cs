using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayHelper
{
    #region Populate Array

    private delegate T[] FillArray<T>(T[] array, T value);

    public static void Populate<T>(T[] array, T value)
    {
        int dimension = array.Rank; // gets the number of dimenions the array has


    }

    #endregion
}
