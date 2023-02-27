using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HumanPlayer : Player
{
    private event ReturnMove onMove;

    private void Awake()
    {
        FindObjectOfType<ClickHandler>().onClick.RemoveListener(OnClick);
        FindObjectOfType<ClickHandler>().onClick.AddListener(OnClick);
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
            onMove?.Invoke(move);
        }
    }
}
