using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private GameHandler gameHandler;

    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject saveDataPrefab;

    private void Awake()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        if (gameHandler == null)
            Debug.LogError("No Game Handler Found.");

        UpdateLoadList();
    }

    public void UpdateLoadList()
    {
        string[] info = Directory.GetFiles(Application.persistentDataPath + "/", "*.save");

        foreach (string file in info)
        {
            string fileName = file.Replace(Application.persistentDataPath + "/", "");
            GameObject go = Instantiate(saveDataPrefab, content.transform);
            go.GetComponent<SaveDataUIElement>().SetDataPath(fileName);
        }
    }

    public void Save()
    {
        Board b = gameHandler.GetBoard();
        string fileName = Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".save";

        BinaryFormatter bf = new();
        FileStream fs = new(fileName, FileMode.Create);

        bf.Serialize(fs, b);

        fs.Close();

        UpdateLoadList();
    }

    public void Load(string file)
    {
        string path = Application.persistentDataPath + "/" + file;

        BinaryFormatter bf = new();
        FileStream fs = new(path, FileMode.Open);

        Board board = bf.Deserialize(fs) as Board;

        gameHandler.SetBoard(board);

        fs.Close();
    }
}
