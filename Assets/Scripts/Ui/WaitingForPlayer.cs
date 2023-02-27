using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaitingForPlayer : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private ProfileScript player;
    [SerializeField]
    private TextMeshProUGUI gameId;
    [SerializeField]
    private GameHandler gameHandler;
    [SerializeField]
    private ProfileScript profileX;
    [SerializeField]
    private ProfileScript profileO;
    [SerializeField]
    private Connection connection;

    public UnityEvent onGameReady;

    public void OnMessage(Command command)
    {
        if(command.command == "created")
        {
            // successfully created game
            gameObject.SetActive(true);
            gameId.text = command.param;
            player.SetText(usernameField.text);
        }
        else if (command.command == "connected")
        {
            profileX.SetText(usernameField.text);
            profileO.SetText(command.param);

            LocalPlayer lp = gameHandler.AddComponent<LocalPlayer>();
            lp.SetConnection(connection);
            gameHandler.SetX(lp);

            NetworkPlayer np = gameHandler.AddComponent<NetworkPlayer>();
            np.SetConnection(connection);
            gameHandler.SetO(np);

            onGameReady?.Invoke();
        }
    }
}
