using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingForPlayer : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private ProfileScript player;
    [SerializeField]
    private TextMeshProUGUI gameId;
    public void OnMessage(Command command)
    {
        if(command.command == "created")
        {
            // successfully created game
            gameObject.SetActive(true);
            gameId.text = command.param;
            player.SetText(usernameField.text);
        }
        else if(command.command == "connected")
        {
            // handle player joining
        }
    }
}
