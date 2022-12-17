using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    private void Awake() 
    {
        Debug.Log("Resets all PlayerPrefs");
        PlayerPrefs.DeleteAll();
    }
}
