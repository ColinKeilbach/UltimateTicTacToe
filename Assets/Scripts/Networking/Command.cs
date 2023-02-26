using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Command
{
    public Command(string command, string param)
    {
        this.command = command;
        this.param = param;
    }

    public string command;
    public string param;
}
