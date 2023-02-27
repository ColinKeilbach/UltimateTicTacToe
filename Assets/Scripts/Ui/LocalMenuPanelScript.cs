using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LocalMenuPanelScript : MonoBehaviour, ISingleton
{
    [SerializeField]
    private GameHandler gameHandler;
    [SerializeField]
    private ProfileScript one;
    [SerializeField]
    private ProfileScript two;
    [SerializeField]
    private ProfileManager pm;

    public void OnGameStart()
    {
        // get data from profile selection
        pm.GetProfileOne().SetImage(one.GetSprite());
        pm.GetProfileTwo().SetImage(two.GetSprite());
        pm.GetProfileOne().SetText(one.GetText());
        pm.GetProfileTwo().SetText(two.GetText());

        Player p;

        // X Player
        switch (one.GetText())
        {
            case "Human":
                p = gameHandler.AddComponent<HumanPlayer>();
                gameHandler.SetX(p);
                break;
            case "Easy Bot":
                p = gameHandler.AddComponent<RandomPlayer>();
                gameHandler.SetX(p);
                break;
            case "Hard Bot":
                p = gameHandler.AddComponent<MiniMaxPlayer>();
                gameHandler.SetX(p);
                break;
        }

        // O Player
        switch (two.GetText())
        {
            case "Human":
                p = gameHandler.AddComponent<HumanPlayer>();
                gameHandler.SetO(p);
                break;
            case "Easy Bot":
                p = gameHandler.AddComponent<RandomPlayer>();
                gameHandler.SetO(p);
                break;
            case "Hard Bot":
                p = gameHandler.AddComponent<MiniMaxPlayer>();
                gameHandler.SetO(p);
                break;
        }
    }
}
