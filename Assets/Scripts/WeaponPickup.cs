using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Canvas ammoCanvas;
    [SerializeField] Canvas actionCanvas;
    [SerializeField] Canvas reticleToHide;
    [SerializeField] AudioSource boxOpenAudio;
    [SerializeField] TextMeshProUGUI presseImg;
    [SerializeField] TextMeshProUGUI alreadyGotImg;
    [SerializeField] TextMeshProUGUI needKey;
    [SerializeField] GameObject paper;
    [SerializeField] AudioClip lockedDoor;
    [SerializeField] AudioClip unlockDoor;
    [SerializeField] GameObject newKey;

    float openTime = 2;
    GameObject gameObjectClicked;
    GameObject gameObjectFocus;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    Transform weapons;
    PaperPickup paperPickup;
    AudioSource audioSource;
    public bool key = false;
    bool doorOpen = false;

    void Start()
    {
        weapons = transform.GetChild(0).GetChild(0);
        actionCanvas.enabled = false;
        int scene = SceneManager.GetActiveScene().buildIndex;
        if(scene == 0)
        {
            ammoCanvas.enabled = false;
        }
        paperPickup = FindObjectOfType<PaperPickup>();
        audioSource = GetComponent<AudioSource>();
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
            if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Interact")
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
                else return;
            }
            else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Action")
            {
                paper.SetActive(true);
            }
            else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Door")
            {
                if(key && !doorOpen)
                {
                    rayHit.transform.GetComponent<Animator>().SetBool("open", true);
                    audioSource.PlayOneShot(unlockDoor, 0.7f);
                    doorOpen = true;
                }
                else
                {
                    audioSource.PlayOneShot(lockedDoor, 0.7f);
                }
            }
            else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Key")
            {
                newKey.SetActive(true);
                key = true;
                rayHit.transform.gameObject.SetActive(false);
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
        if(!child.GetComponent<Weapon>().pickedUp)
        {
            foreach (Transform oldChild in weapons)
            {
                oldChild.gameObject.SetActive(false);
            }
            child.SetActive(true);
            child.GetComponent<AudioSource>().Play();
            child.GetComponent<Weapon>().pickedUp = true;
            if(!ammoCanvas.enabled)
            {
                ammoCanvas.enabled = true;
            }
            Destroy(gameObjectClicked.transform.GetChild(2).gameObject);
        }
    }

   void FindActionToDisplay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Interact")
        {
            actionCanvas.enabled = true;
            reticleToHide.enabled = false;
            if(rayHit.transform.gameObject.GetComponent<BoxContent>().weapon.GetComponent<Weapon>().pickedUp 
            && rayHit.transform.gameObject.GetComponent<BoxContent>().isOpen)
            {
                presseImg.enabled = false;
                alreadyGotImg.enabled = true;
                needKey.enabled = false;
            }
            else
            {
                presseImg.enabled = true;
                alreadyGotImg.enabled = false;
                needKey.enabled = false;
            }
        }
        else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Action")
        {
            actionCanvas.enabled = true;
            reticleToHide.enabled = false;
            presseImg.enabled = true;
            alreadyGotImg.enabled = false;
            needKey.enabled = false;
        }
        else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Door")
        {
            actionCanvas.enabled = true;
            reticleToHide.enabled = false;
            if(key)
            {
                presseImg.enabled = true;
                needKey.enabled = false;
            }
            else
            {
                presseImg.enabled = false;
                needKey.enabled = true;
            }
            alreadyGotImg.enabled = false;
        }
        else if(Physics.Raycast(ray, out rayHit, 2.1f) && rayHit.transform.gameObject.tag == "Key")
        {
            actionCanvas.enabled = true;
            reticleToHide.enabled = false;
            presseImg.enabled = true;
            alreadyGotImg.enabled = false;
            needKey.enabled = false;
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
                reticleToHide.enabled = true;
                actionCanvas.enabled = false;
            }
        }
    }
}
