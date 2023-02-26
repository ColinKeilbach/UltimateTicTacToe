using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandJoin : Command
{
    public CommandJoin(string gameID) : base("join", gameID) { }
}
