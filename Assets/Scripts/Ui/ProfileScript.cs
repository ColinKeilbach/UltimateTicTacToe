using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI textMesh;
    [SerializeField]
    private GameObject thinkingAnimation;

    public void SetImage(Sprite sprite) => image.sprite = sprite;
    public Sprite GetSprite() => image.sprite;
    public void SetText(string text) => textMesh.text = text;
    public string GetText() => textMesh.text;
    public void SetThinking(bool active) => thinkingAnimation.SetActive(active);
}
