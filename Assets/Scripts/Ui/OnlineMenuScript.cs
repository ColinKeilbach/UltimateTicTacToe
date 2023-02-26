using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineMenuScript : MonoBehaviour
{
    [SerializeField]
    private Connection connection;
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField gameIDField;

    public void OnCreate()
    {
        connection.SendWebSocketMessage(new CommandConnect(usernameField.text));
        connection.SendWebSocketMessage(new CommandCreate());
    }
    public void OnJoin()
    {
        connection.SendWebSocketMessage(new CommandConnect(usernameField.text));
        connection.SendWebSocketMessage(new CommandJoin(gameIDField.text));
    }
}
