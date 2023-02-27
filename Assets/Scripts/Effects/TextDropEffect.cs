using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDropEffect : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float defaultSize = -1;

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        if (defaultSize == -1)
            defaultSize = textMesh.fontSize;

        float startSize = defaultSize * 1.2f;

        StartCoroutine(DropEffect(startSize, defaultSize, 0.15f));
    }

    private void OnDisable()
    {
        textMesh.fontSize = defaultSize;
    }

    private IEnumerator DropEffect(float start, float end, float waitTime)
    {
        float elapsedTime = 0.0001f; // cannot start at 0 because it is the devisor
        while (elapsedTime * elapsedTime / waitTime <= 1)
        {
            textMesh.fontSize = Mathf.Lerp(start, end, (elapsedTime * elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textMesh.fontSize = end;
        yield return null;
    }
}
