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

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            pickup.Play();

            GameObject newCanvas = Instantiate(canvas) as GameObject;
            newCanvas.transform.SetParent (canvasParent);
            newCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ammoAmount.ToString();
            newCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ammoType.ToString();
            
            float i = 0;
            float d = 3;
            float t = 0.005f;
            while(i < d)
            {
                i = Time.deltaTime;
                Debug.Log("i " + i);
                newCanvas.transform.GetChild(0).GetComponent<RectTransform>().Translate(0,t,0);
                t = t + 0.005f;
                Debug.Log("t " + t);
            }

            //Destroy(gameObject);
        }
    }
}
