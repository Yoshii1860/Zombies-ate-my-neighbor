using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOfDollTrigger : MonoBehaviour
{
    [SerializeField] EnemyAI zombie;

    void Start() 
    {
        zombie.enabled = false;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            zombie.enabled = true;
        }
    }
}
