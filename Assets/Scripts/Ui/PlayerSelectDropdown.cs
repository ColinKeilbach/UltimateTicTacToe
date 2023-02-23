using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSelectDropdown : MonoBehaviour
{
    [SerializeField]
    private ProfileScript profile;
    [SerializeField]
    private List<Sprite> sprites;
    private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        dropdown.SetValueWithoutNotify(0);
        OnValueChanged(0);
    }

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnValueChanged(int value)
    {
        profile.SetText(dropdown.options[value].text);
        profile.SetImage(sprites[value]);
    }
}
