using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPickup : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    Rigidbody rb;
    PlayerSounds ps;

    void Start() 
    {
        rb = transform.parent.parent.GetComponent<Rigidbody>();
        ps = transform.parent.parent.GetComponent<PlayerSounds>();
    }

    void Update()
    {
        if(rb.isKinematic == false)
        {
            rb.isKinematic = true;
            ps.StopSounds();
            if(transform.parent.GetChild(0).gameObject.activeSelf)
            transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            ps.StartSounds();
            rb.isKinematic = false;
            transform.parent.GetChild(0).gameObject.SetActive(true);
            trigger.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
