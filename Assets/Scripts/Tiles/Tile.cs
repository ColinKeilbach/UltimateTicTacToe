using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    public enum Value
    {
        Blank = 0,
        X,
        O
    }

    public static Value Inverse(Value v) => v switch
    {
        Value.O => Value.X,
        Value.X => Value.O,
        _ => Value.Blank
    };

    public static string ValueToString(Value v) => v switch
    {
        Value.O => "O",
        Value.X => "X",
        _ => ""
    };

    public static int ValueToInt(Value v) => v switch
    {
        Value.O => -1,
        Value.X => 1,
        _ => 0
    };

    public static Value IntToValue(int v) => v switch
    {
        -1 => Value.O,
        1 => Value.X,
        _ => Value.Blank
    };

    private Value _value = Value.Blank;

    protected Value value
    {
        get { return _value; }
        private set
        {
            _value = value;
            // set text
            textMesh.text = ValueToString(value);
            textMesh.gameObject.SetActive(value != Value.Blank);
        }
    }

    public Value GetValue() => value;
    public void SetValue(Value value) => this.value = value;
}
