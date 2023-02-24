using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHistoryView : MonoBehaviour
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject moveViewPrefab;

    public void AddMove(Vector4Int move)
    {
        GameObject go = Instantiate(moveViewPrefab, content);
        go.GetComponent<MoveView>().SetText(move.ToString());
    }
}
