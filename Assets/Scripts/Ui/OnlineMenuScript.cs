using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnlineMenuScript : MonoBehaviour
{
    [SerializeField]
    private Connection connection;
    [SerializeField]
    private GameHandler gameHandler;
    [SerializeField]
    private ProfileScript profileX;
    [SerializeField]
    private ProfileScript profileO;
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField gameIDField;
    [SerializeField]
    private TextMeshProUGUI errorTextMesh;
    [SerializeField]
    private WaitingForPlayer wfp;

    public UnityEvent onGameReady;

    private void OnEnable()
    {
        gameIDField.text = "";
    }

    public void OnCreate()
    {
        if (usernameField.text == "")
        {
            StartCoroutine(FlashRed(usernameField, 1));
            return;
        }

        connection.SendWebSocketMessage(new CommandConnect(usernameField.text));
        connection.SendWebSocketMessage(new CommandCreate());
        connection.onMessage.RemoveListener(wfp.OnMessage);
        connection.onMessage.AddListener(wfp.OnMessage);
    }
    public void OnJoin()
    {
        // make sure fields are filled out
        bool shouldReturn = false;
        if (usernameField.text == "")
        {
            StartCoroutine(FlashRed(usernameField, 1));
            shouldReturn = true;
        }
        if (gameIDField.text == "")
        {
            StartCoroutine(FlashRed(gameIDField, 1));
            shouldReturn = true;
        }
        if (shouldReturn) return;

        // connect
        connection.SendWebSocketMessage(new CommandConnect(usernameField.text));
        connection.SendWebSocketMessage(new CommandJoin(gameIDField.text));
        connection.onMessage.RemoveListener(HandleJoinResponse);
        connection.onMessage.AddListener(HandleJoinResponse);
    }

    private void HandleJoinResponse(Command command)
    {
        connection.onMessage.RemoveListener(HandleJoinResponse);

        if (command.command == "error")
        {
            errorTextMesh.text = command.param;
        }
        else if (command.command == "connected")
        {
            profileX.SetText(command.param);
            profileO.SetText(usernameField.text);

            NetworkPlayer np = gameHandler.AddComponent<NetworkPlayer>();
            np.SetConnection(connection);
            gameHandler.SetX(np);

            LocalPlayer lp = gameHandler.AddComponent<LocalPlayer>();
            lp.SetConnection(connection);
            gameHandler.SetO(lp);

            onGameReady?.Invoke();
        }
    }

    private IEnumerator FlashRed(TMP_InputField field, float timeToMove)
    {
        TextMeshProUGUI placeholder = field.GetComponentInChildren<TextMeshProUGUI>();
        Color start = placeholder.color;
        placeholder.color = Color.red;
        float t = 0;
        while (t < 1)
        {
            placeholder.color = Color.Lerp(Color.red, start, t);
            t = t + Time.deltaTime / timeToMove;
            yield return null;
        }
        placeholder.color = start;
    }
}
