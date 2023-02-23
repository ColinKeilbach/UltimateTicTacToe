using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LocalMenuPanelScript : MonoBehaviour, ISingleton
{
    [SerializeField]
    private ProfileScript one;
    [SerializeField]
    private ProfileScript two;
    [SerializeField]
    private ProfileManager pm;

    [SerializeField]
    private GameObject xAi;
    [SerializeField]
    private GameObject oAi;

    public void OnGameStart()
    {
        // get data from profile selection
        pm.GetProfileOne().SetImage(one.GetSprite());
        pm.GetProfileTwo().SetImage(two.GetSprite());
        pm.GetProfileOne().SetText(one.GetText());
        pm.GetProfileTwo().SetText(two.GetText());

        // change AI difficulty
        switch (one.GetText())
        {
            case "Human":
                xAi.SetActive(false);
                break;
            case "Easy Bot":
                xAi.GetComponent<AIPlayer>().SetStyle(AIPlayer.Style.Random);
                xAi.SetActive(true);
                break;
            case "Hard Bot":
                xAi.GetComponent<AIPlayer>().SetStyle(AIPlayer.Style.MiniMax);
                xAi.SetActive(true);
                break;
        }

        // change AI difficulty
        switch (two.GetText())
        {
            case "Human":
                oAi.SetActive(false);
                break;
            case "Easy Bot":
                oAi.GetComponent<AIPlayer>().SetStyle(AIPlayer.Style.Random);
                oAi.SetActive(true);
                break;
            case "Hard Bot":
                oAi.GetComponent<AIPlayer>().SetStyle(AIPlayer.Style.MiniMax);
                oAi.SetActive(true);
                break;
        }
    }
}
