using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    public void SetText(string text) => textMesh.text = text;
}
