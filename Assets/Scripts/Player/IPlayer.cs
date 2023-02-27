using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public delegate void ReturnMove(Vector4Int move);

    public bool Thinking { get; protected set; }

    public abstract void RequestMove(Board board, ReturnMove returnMove);
}
