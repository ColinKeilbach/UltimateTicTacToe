using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GameHandler))]
public class ClickHandler : MonoBehaviour
{
    private EventSystem eventSystem;
    private GraphicRaycaster raycaster;
    private GameHandler gameHandler;

    public UnityEvent<Vector4Int> onClick;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        raycaster = FindObjectOfType<GraphicRaycaster>();
        gameHandler = FindObjectOfType<GameHandler>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            //Clicked
            GameObject target = OnClick();

            if (target != null)
            {
                onClick?.Invoke(gameHandler.TileToCoord(target.GetComponent<Tile>()));
            }
        }
    }

    private GameObject OnClick()
    {
        PointerEventData pointerData = new(eventSystem);

        pointerData.position = transform.position;

        List<RaycastResult> results = new();

        raycaster.Raycast(pointerData, results);

        if (results.Count > 0) return results[0].gameObject;

        return null;
    }
}
