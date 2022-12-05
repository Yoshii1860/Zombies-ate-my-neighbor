using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float openTime = 2;
    GameObject gameObjectClicked;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    Transform weapons;

    void Start()
    {
        weapons = transform.GetChild(0).GetChild(0);
        Debug.Log(weapons);
    }

   void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 100.0f))
            {
                gameObjectClicked = rayHit.collider.gameObject;
                float distanceToTarget = Vector3.Distance(gameObjectClicked.transform.position, transform.position);
                if(gameObjectClicked.GetComponent<BoxContent>().isOpen == false && distanceToTarget <= 2)
                {
                    gameObjectClicked = rayHit.collider.gameObject;
                    OpenBox();
                }
                else if(gameObjectClicked.GetComponent<BoxContent>().isOpen == true && distanceToTarget <= 2)
                {
                    NewWeaponPickup();
                }
                else
                {
                    return;
                }
            }
        } 
    }

    void OpenBox()
    {
        gameObjectClicked.GetComponent<Animator>().SetTrigger("open");
        gameObjectClicked.GetComponent<BoxContent>().isOpen = true;
    }

    void NewWeaponPickup()
    {
        GameObject child = gameObjectClicked.GetComponent<BoxContent>().weapon;
        child.GetComponent<Weapon>().pickedUp = true;
        foreach (Transform oldChild in weapons)
        {
            oldChild.gameObject.SetActive(false);
        }
        child.SetActive(true);
        Destroy(gameObjectClicked.transform.GetChild(2).gameObject);
    }
}
