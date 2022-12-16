using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] GameObject savePoint;
    SaveGame saveGame;
    Vector3 playerPosition;

    void Start()
    {
        saveGame = FindObjectOfType<SaveGame>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("SavePoint");
            GameObject newSavePoint = Instantiate(savePoint, savePoint.transform);
            playerPosition = other.gameObject.transform.position;
            Debug.Log("SavePoint: " + playerPosition);
            newSavePoint.GetComponent<SaveGame>().SetPlayerPosition(playerPosition);
            newSavePoint.GetComponent<SaveGame>().enabled = true;
            this.transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
