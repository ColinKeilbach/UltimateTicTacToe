using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command
{
    public CommandMove(string move) : base("move", move) { }
}
