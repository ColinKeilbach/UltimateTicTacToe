using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Player
{
    private Connection connection;

    private event ReturnMove onMove;

    public void SetConnection(Connection connection)
    {
        this.connection = connection;

        connection.onMessage.RemoveListener(HandleIncoming);
        connection.onMessage.AddListener(HandleIncoming);
    }

    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        onMove -= returnMove;
        onMove += returnMove;
    }

    private void HandleIncoming(Command command)
    {
        if(command.command == "move")
        {
            Vector4Int move = JsonUtility.FromJson<Vector4Int>(command.param);
            Thinking = false;
            onMove.Invoke(move);
        }
    }
}
