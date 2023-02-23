using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetImage(Sprite sprite) => image.sprite = sprite;
    public Sprite GetSprite() => image.sprite;
    public void SetText(string text) => textMesh.text = text;
    public string GetText() => textMesh.text;
}
