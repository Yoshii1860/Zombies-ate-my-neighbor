using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] AmmoType pistol;
    [SerializeField] AmmoType shotgun;
    [SerializeField] AmmoType rifle;
    [SerializeField] AmmoType sniper;
    [SerializeField] Weapon pistolPickedUp;
    [SerializeField] Weapon shotgunPickedUp;
    [SerializeField] Weapon riflePickedUp;
    [SerializeField] Weapon sniperPickedUp;
    [SerializeField] Canvas reticle;
    [SerializeField] Canvas ammoDisplay;
    [SerializeField] GameObject flashlight;
    [SerializeField] SavePickups pickups;
    [SerializeField] SaveEnemies enemies;
    [SerializeField] SaveTriggers triggers;
    [SerializeField] Transform player;

    Ammo ammo;
    PlayerHealth playerHealth;

    int pistolAmmo = 0;
    int shotgunAmmo = 0;
    int rifleAmmo = 0;
    int sniperAmmo = 0;

    int iSE = 0;
    int iSP = 0;
    int iST = 0;
    
    float health;

    Vector3 playerPosition;

    void Awake() 
    {
        ammo = FindObjectOfType<Ammo>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Start()
    {
        Debug.Log("Save");
        GetBullets();
        GetWeapons();
        SaveHealth();
        SavePlayerPosition();
        SaveCanvas();
        SavePickups();
        SaveEnemies();
        SaveTriggers();
        PlayerPrefs.Save();
        this.enabled = false;
    }

    void GetBullets()
    {
        pistolAmmo = ammo.GetCurrentAmmo(pistol);
        shotgunAmmo = ammo.GetCurrentAmmo(shotgun);
        rifleAmmo = ammo.GetCurrentAmmo(rifle);
        sniperAmmo = ammo.GetCurrentAmmo(sniper);
        SaveBullets();
    }

    void SaveBullets()
    {
        PlayerPrefs.SetInt("pistolAmmo", pistolAmmo);
        PlayerPrefs.SetInt("shotgunAmmo", shotgunAmmo);
        PlayerPrefs.SetInt("rifleAmmo", rifleAmmo);
        PlayerPrefs.SetInt("sniperAmmo", sniperAmmo);
    }

    void GetWeapons()
    {
        if(pistolPickedUp.pickedUp)
        {
            PlayerPrefs.SetString("pistol", "true");
        }
        else
        {
            PlayerPrefs.SetString("pistol", "false");
        }

        if(shotgunPickedUp.pickedUp)
        {
            PlayerPrefs.SetString("shotgun", "true");
        }
        else
        {
            PlayerPrefs.SetString("shotgun", "false");
        }

        if(riflePickedUp.pickedUp)
        {
            PlayerPrefs.SetString("rifle", "true");
        }
        else
        {
            PlayerPrefs.SetString("rifle", "false");
        }

        if(sniperPickedUp.pickedUp)
        {
            PlayerPrefs.SetString("sniper", "true");
        }
        else
        {
            PlayerPrefs.SetString("sniper", "false");
        }
    }

    void SaveHealth()
    {
        health = playerHealth.GetHealth();
        PlayerPrefs.SetFloat("health", health);
    }

    void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("ppX", playerPosition.x);
        PlayerPrefs.SetFloat("ppY", playerPosition.y);
        PlayerPrefs.SetFloat("ppZ", playerPosition.z);
    }

    void SaveCanvas()
    {
        if(reticle.enabled)
        {
            PlayerPrefs.SetString("reticle", "true");
        }
        else
        {
            PlayerPrefs.SetString("reticle", "false");
        }

        if(ammoDisplay.enabled)
        {
            PlayerPrefs.SetString("ammoDisplay", "true");
        }
        else
        {
            PlayerPrefs.SetString("ammoDisplay", "false");
        }
        
        if(flashlight.activeSelf)
        {
            PlayerPrefs.SetString("flashlight", "true");
        }
        else
        {
            PlayerPrefs.SetString("flashlight", "false");
        }
    }

    void SavePickups()
    {
        int i = 0;
        foreach(GameObject pickup in pickups.pickupObjects)
        {   
            string nameString = pickup.name + i.ToString();      
            string name = pickups.pickupStrings[i];
            PlayerPrefs.SetString(nameString, name);
            i++;
        }
    }

    void SaveEnemies()
    {
        int i = 0;
        foreach(EnemyHealth enemy in enemies.enemyArray)
        {   
            string nameString = enemy.transform.gameObject.name + i.ToString();      
            string name = enemies.enemyStrings[i];
            PlayerPrefs.SetString(nameString, name);
            i++;
        }
    }

    void SaveTriggers()
    {
        foreach(TriggeredScript trigger in triggers.triggerArray)
        {   
            string nameString = trigger.transform.gameObject.name + iST.ToString();      
            string name = triggers.triggerStrings[iST];
            PlayerPrefs.SetString(nameString, name);
            iST++;
            if(iST == triggers.triggerArray.Length)
            {
                iST = 0;
            }
        }
    }

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }
}
