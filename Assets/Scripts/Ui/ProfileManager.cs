using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private ProfileScript profileOne;
    [SerializeField]
    private ProfileScript profileTwo;

    public ProfileScript GetProfileOne() => profileOne;
    public ProfileScript GetProfileTwo() => profileTwo;
}
