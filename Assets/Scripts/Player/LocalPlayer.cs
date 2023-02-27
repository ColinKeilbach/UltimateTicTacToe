using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player
{
    private Connection connection;

    private event ReturnMove onMove;
    private void Awake()
    {
        FindObjectOfType<ClickHandler>().onClick.RemoveListener(OnClick);
        FindObjectOfType<ClickHandler>().onClick.AddListener(OnClick);
    }

    public void SetConnection(Connection connection)
    {
        this.connection = connection;
    }

    public override void RequestMove(Board board, ReturnMove returnMove)
    {
        Thinking = true;
        onMove -= returnMove;
        onMove += returnMove;
    }

    private void OnClick(Vector4Int move)
    {
        if (Thinking)
        {
            Thinking = false;
            connection.SendWebSocketMessage(new CommandMove(move));
            onMove?.Invoke(move);
        }
    }
}
