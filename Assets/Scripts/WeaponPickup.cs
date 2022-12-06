using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Canvas ammoCanvas;
    [SerializeField] AudioSource boxOpenAudio;
    [SerializeField] Canvas actionCanvas;

    float openTime = 2;
    GameObject gameObjectClicked;
    GameObject gameObjectFocus;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    Transform weapons;

    void Start()
    {
        weapons = transform.GetChild(0).GetChild(0);
        actionCanvas.enabled = false;
        ammoCanvas.enabled = false;
    }

   void Update()
    {
        FindActionToDisplay();
        DisableActionDisplay();
        BoxInteractions();     
    }

    void BoxInteractions()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 2.1f))
            {
                gameObjectClicked = rayHit.collider.gameObject;
                float distanceToTarget = Vector3.Distance(gameObjectClicked.transform.position, transform.position);
                if(gameObjectClicked.GetComponent<BoxContent>().isOpen == false)
                {
                    OpenBox();
                }
                else if(gameObjectClicked.GetComponent<BoxContent>().isOpen == true)
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
        boxOpenAudio.Play();
    }

    void NewWeaponPickup()
    {
        GameObject child = gameObjectClicked.GetComponent<BoxContent>().weapon;
        foreach (Transform oldChild in weapons)
        {
            oldChild.gameObject.SetActive(false);
        }
        child.SetActive(true);
        if(!child.GetComponent<Weapon>().pickedUp)
        {
            child.GetComponent<AudioSource>().Play();
            child.GetComponent<Weapon>().pickedUp = true;
        }
        if(!ammoCanvas.enabled)
        {
            ammoCanvas.enabled = true;
        }
        Destroy(gameObjectClicked.transform.GetChild(2).gameObject);
    }

    void FindActionToDisplay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Interact")
        {
            actionCanvas.enabled = true;
        }
    }

    void DisableActionDisplay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, 100.0f))
        {
            gameObjectFocus = rayHit.collider.gameObject;
            float dist = Vector3.Distance(gameObjectFocus.transform.position, transform.position);
            if(dist >= 2.1f)
            {
                actionCanvas.enabled = false;
            }
        }
    }
}
