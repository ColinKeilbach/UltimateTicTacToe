using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveDataUIElement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;
    private SaveLoadSystem loader;

    private string dataPath;

    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<SaveLoadSystem>();
    }

    public void SetDataPath(string text)
    {
        dataPath = text;
        string uiText = text.Replace(".save", "");
        textMesh.text = uiText;
    }

    public void Load() => loader.Load(dataPath);

    public void Delete()
    {
        File.Delete(Application.persistentDataPath + "/" + dataPath);

        loader.UpdateLoadList();

        // Destroys UI object
        Destroy(gameObject);
    }
}
