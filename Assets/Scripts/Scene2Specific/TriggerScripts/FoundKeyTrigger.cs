using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundKeyTrigger : MonoBehaviour
{
    [SerializeField] GameObject hordeTrigger;

    void Start() 
    {
        this.transform.parent.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.transform.parent.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            hordeTrigger.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
