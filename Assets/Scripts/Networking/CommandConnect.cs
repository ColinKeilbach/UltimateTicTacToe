using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandConnect : Command
{
    public CommandConnect(string username) : base("connect", username) { }
}
