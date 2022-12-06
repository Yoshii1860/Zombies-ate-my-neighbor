using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerEnter : MonoBehaviour
{
    [SerializeField] GameObject[] scavangerAll;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject scavanger in scavangerAll)
            {
                if(!scavanger.activeSelf)
                {
                    scavanger.SetActive(true);
                }
                scavanger.GetComponent<EnemyAI>().chaseRange = 500f;
            }
        }
    }
}
