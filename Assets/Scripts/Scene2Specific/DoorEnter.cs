using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorEnter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI needKey;
    public bool key = false;
    bool lookAtDoor = false;

    void Update() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Door")
        {
            Debug.Log(rayHit.transform.gameObject);
        }
    }
}
