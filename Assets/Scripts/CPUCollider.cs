using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUCollider : MonoBehaviour
{
    bool insideCollider = false;

    public bool InsideCollider()
    {
        return insideCollider;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            insideCollider = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            insideCollider = false;
        }
    }
}
