using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioSource pickup;
    [SerializeField] GameObject canvas;
    [SerializeField] Transform canvasParent;
    [SerializeField] GameObject canvasTwo;
    [SerializeField] GameObject canvasThree;

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            pickup.Play();
            if(canvasParent.childCount == 0)
            {
                NewCanvasInstantiate(canvas);
            }
            else if (canvasParent.childCount == 1)
            {
                NewCanvasInstantiate(canvasTwo);
            }
            else if (canvasParent.childCount == 2)
            {
                NewCanvasInstantiate(canvasThree);
            }
            Destroy(gameObject);
        }
    }

    void NewCanvasInstantiate(GameObject canvas)
    {
        GameObject newCanvas = Instantiate(canvas) as GameObject;
        newCanvas.transform.SetParent(canvasParent);
        newCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ammoAmount.ToString();
        newCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ammoType.ToString();
        Destroy(newCanvas, 2f);
    }
}
