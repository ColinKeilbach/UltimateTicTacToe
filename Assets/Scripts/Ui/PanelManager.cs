using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private Dictionary<string, Transform> panels = new();

    private Transform current;

    // Start is called before the first frame update
    void Awake()
    {
        // get list of children
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
            panels.Add(t.gameObject.name, t);
            current = t;
        }

        current.gameObject.SetActive(true);
    }

    public void ChangePanel(string next)
    {
        try
        {
            Transform nextPanel = panels[next];

            nextPanel.gameObject.SetActive(true);
            current.gameObject.SetActive(false);
            current = nextPanel;
        }
        catch
        {
            Debug.LogError("There is no panel attached to \"" + gameObject.name + "\" called \"" + next + "\".");
        }
    }
}
