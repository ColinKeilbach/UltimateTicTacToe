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

        Player p = null;

        // X Player
        switch (one.GetText())
        {
            case "Human":
                p = gameHandler.AddComponent<HumanPlayer>();
                break;
            case "Random Bot":
                p = gameHandler.AddComponent<RandomPlayer>();
                break;
            case "Minimax Bot":
                p = gameHandler.AddComponent<MiniMaxPlayer>();
                break;
            case "Aggressive Bot":
                p = gameHandler.AddComponent<AggressivePlayer>();
                break;
        }
        gameHandler.SetX(p);

        // O Player
        switch (two.GetText())
        {
            case "Human":
                p = gameHandler.AddComponent<HumanPlayer>();
                break;
            case "Random Bot":
                p = gameHandler.AddComponent<RandomPlayer>();
                break;
            case "Minimax Bot":
                p = gameHandler.AddComponent<MiniMaxPlayer>();
                break;
            case "Aggressive Bot":
                p = gameHandler.AddComponent<AggressivePlayer>();
                break;
        }
        gameHandler.SetO(p);
    }
}
