using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;
using UnityEngine.Events;

public class Connection : MonoBehaviour
{
    WebSocket websocket;
    [Header("Server Variables")]
    [SerializeField]
    private string link;

    [Header("Listeners")]
    public UnityEvent onOpen;
    public UnityEvent onClose;
    public UnityEvent<Command> onMessage;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket(link);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open");
            onOpen?.Invoke();
        };

        websocket.OnError += (e) =>
        {
            Debug.LogError("Error: " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed with code: " + e);
            onClose?.Invoke();
        };

        websocket.OnMessage += (bytes) =>
        {
            // getting the message as a string
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message);
            Command command = JsonUtility.FromJson<CommandRecieved>(message);
            onMessage?.Invoke(command);
        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await websocket.Connect();

    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    public async void SendWebSocketMessage(Command command)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending plain text
            await websocket.SendText(JsonUtility.ToJson(command)); // sending blank string sends an empty json object
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}
