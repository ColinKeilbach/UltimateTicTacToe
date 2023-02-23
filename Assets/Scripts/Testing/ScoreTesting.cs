using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTesting : MonoBehaviour
{

    private void Start()
    {
        Debug.Log(TestOne());
        Debug.Log(TestTwo());
    }

    private float TestOne()
    {
        Board b = new();

        b.Move(0, 0, 0, 0); // X
        b.Move(0, 0, 1, 2); // O

        b.Move(1, 2, 0, 0); // X
        b.Move(0, 0, 1, 1); // O

        b.Move(1, 1, 0, 0); // X
        b.Move(0, 0, 1, 0); // O

        b.Move(1, 0, 1, 0); // X
        b.Move(1, 0, 0, 0); // O

        b.Move(2, 0, 1, 0); // X
        b.Move(1, 0, 2, 1); // O

        b.Move(2, 1, 1, 0); // X
        b.Move(1, 0, 2, 2); // O

        b.Move(2, 2, 1, 0); // X
        b.Move(1, 0, 0, 1); // O

        return new CountScoring().Score(b);
    }

    private float TestTwo()
    {
        Board b = new();

        b.Move(0, 0, 0, 0); // X
        b.Move(0, 0, 1, 2); // O

        b.Move(1, 2, 0, 0); // X
        b.Move(0, 0, 1, 1); // O

        b.Move(1, 1, 0, 0); // X
        b.Move(0, 0, 1, 0); // O

        b.Move(1, 0, 1, 0); // X
        b.Move(1, 0, 0, 0); // O

        b.Move(2, 0, 1, 0); // X
        b.Move(1, 0, 2, 1); // O

        b.Move(2, 1, 1, 0); // X
        b.Move(1, 0, 2, 2); // O

        b.Move(2, 2, 1, 0); // X
        b.Move(1, 0, 2, 0); // O

        return new CountScoring().Score(b);
    }
}
