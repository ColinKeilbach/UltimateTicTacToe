using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command
{
    public CommandMove(Vector4Int move) : base("move", JsonUtility.ToJson(move)) { }
}
