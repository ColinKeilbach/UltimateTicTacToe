using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform left;
    [SerializeField]
    private RectTransform middle;
    [SerializeField]
    private RectTransform right;

    private void Start()
    {
        StartCoroutine(RotateAboutCenter(left, middle, 180f));
    }

    private IEnumerator RotateAboutCenter(Transform a, Transform b, float degrees)
    {
        Vector3 center = Vector3.Lerp(a.position, b.position, 0.5f);

        for(float i = 0; i < 1; i += Time.deltaTime)
        {
            a.RotateAround(center, Vector3.forward, degrees * Time.deltaTime);
            b.RotateAround(center, Vector3.forward, degrees * Time.deltaTime);
            yield return null;
        }
    }
}
