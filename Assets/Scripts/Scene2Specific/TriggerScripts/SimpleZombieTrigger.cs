using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleZombieTrigger : MonoBehaviour
{
    [SerializeField] EnemyAI zombie1;
    [SerializeField] EnemyAI zombie2;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            zombie1.chaseRange = 50f;
            zombie2.chaseRange = 50f;
        }
    }
}
