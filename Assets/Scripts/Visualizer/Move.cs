using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private Board currentBoardState;
    private GameObject prefab;
    [SerializeField]
    private TextMeshProUGUI textMesh;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Transform content;

    private Vector4Int move;

    private void Awake()
    {
        prefab = Resources.Load("Move") as GameObject;
    }

    public void SetValues(Board currentBoardState, Vector4Int lastMove)
    {
        this.currentBoardState = currentBoardState;
        currentBoardState.Move(lastMove);

        string side = !currentBoardState.GetXToMove() ? "X" : "O"; // flipped because this should mark the last move
        float score = new CountScoring().Score(currentBoardState);

        textMesh.text = side + "\n" + lastMove + "\n" + score;

        // set up the color of the button
        Color c;
        if (score < 0)
            c = Color.Lerp(Color.red, Color.white, score / -1000);
        else
            c = Color.Lerp(Color.white, Color.green, score / 1000);

        image.color = c;
        move = lastMove;
    }

    public Vector4Int GetMove() => move;

    public Move[] GetChildMoves()
    {
        if (content.childCount == 0)
            CalculateMove();

        return content.GetComponentsInChildren<Move>();
    }

    public void CalculateMove()
    {
        currentBoardState ??= new();

        if (content.childCount == 0)
            StartCoroutine(MakeMoves());
    }

    private IEnumerator MakeMoves()
    {
        float start = Time.realtimeSinceStartup;
        List<Vector4Int> moves;

        moves = currentBoardState.GetPossibleMoves();

        foreach (var move in moves)
        {
            GameObject o = Instantiate(prefab, content);
            Move m = o.GetComponent<Move>();
            m.SetValues(currentBoardState.Clone() as Board, move);

            if (Time.realtimeSinceStartup - start >= 1000 / Application.targetFrameRate)
            {
                yield return null;
                start = Time.realtimeSinceStartup;
            }
        }
    }
}
