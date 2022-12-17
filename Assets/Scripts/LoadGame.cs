using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
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
    [SerializeField] MovieTrigger movieTrigger;
    [SerializeField] DollTrigger dollTrigger;
    [SerializeField] GameObject barrickadeOne;
    [SerializeField] GameObject barrickadeThree;
    [SerializeField] GameObject savePointZero;
    [SerializeField] GameObject savePointOne;
    [SerializeField] GameObject savePointTwo;
    [SerializeField] GameObject savePointThree;
    [SerializeField] Transform player;

    Ammo ammo;
    PlayerHealth playerHealth;

    int pistolAmmo;
    int shotgunAmmo;
    int rifleAmmo;
    int sniperAmmo;
    string loadPistol;
    string loadShotgun;
    string loadRifle;
    string loadSniper;

    float health;

    Vector3 playerPosition;
    float ppX;
    float ppY;
    float ppZ;

    SaveLightChange saveLight;
    FogRenderer fogRenderer;
    DentistTrigger dentist;

    void Awake() 
    {
        ammo = FindObjectOfType<Ammo>();
        playerHealth = player.GetComponent<PlayerHealth>();
        saveLight = FindObjectOfType<SaveLightChange>();
        fogRenderer = triggers.transform.GetChild(2).GetComponent<FogRenderer>();
        dentist = FindObjectOfType<DentistTrigger>();
    }

    void OnEnable() 
    {
        SetPlayerPosition();
    }

    void Start()
    {
        Debug.Log("Load");
        LoadBullets();
        LoadWeapons();
        LoadHealth();
        LoadCanvas();
        LoadPickups();
        LoadEnemies();
        LoadTriggers();
        this.enabled = false;
    }

    void LoadBullets()
    {
        pistolAmmo = PlayerPrefs.GetInt("pistolAmmo");
        shotgunAmmo = PlayerPrefs.GetInt("shotgunAmmo");
        rifleAmmo = PlayerPrefs.GetInt("rifleAmmo");
        sniperAmmo = PlayerPrefs.GetInt("sniperAmmo");
        SetBullets();
    }

    void SetBullets()
    {
        ammo.SetAmmoOnLoad(pistol, pistolAmmo);
        ammo.SetAmmoOnLoad(shotgun, shotgunAmmo);
        ammo.SetAmmoOnLoad(rifle, rifleAmmo);
        ammo.SetAmmoOnLoad(sniper, sniperAmmo);
    }

    void LoadWeapons()
    {
        loadPistol = PlayerPrefs.GetString("pistol");
        loadShotgun = PlayerPrefs.GetString("shotgun");
        loadRifle = PlayerPrefs.GetString("rifle");
        loadSniper = PlayerPrefs.GetString("sniper");
        SetWeapons();
    }

    void SetWeapons()
    {
        if(loadPistol == "true")
        {
            pistolPickedUp.pickedUp = true;
        }
        else
        {
            pistolPickedUp.pickedUp = false;
        }

        if(loadShotgun == "true")
        {
            shotgunPickedUp.pickedUp = true;
        }
        else
        {
            shotgunPickedUp.pickedUp = false;
        }

        if(loadRifle == "true")
        {
            riflePickedUp.pickedUp = true;
        }
        else
        {
            riflePickedUp.pickedUp = false;
        }

        if(loadSniper == "true")
        {
            sniperPickedUp.pickedUp = true;
        }
        else
        {
            sniperPickedUp.pickedUp = false;
        }
    }

    void LoadHealth()
    {
        health = PlayerPrefs.GetFloat("health");
        playerHealth.LoadHealth(health);
    }

    void SetPlayerPosition()
    {
        ppX = PlayerPrefs.GetFloat("ppX");
        ppY = PlayerPrefs.GetFloat("ppY");
        ppZ = PlayerPrefs.GetFloat("ppZ");
        playerPosition = new Vector3(ppX, ppY, ppZ);
        LoadPosition();
    }

    void LoadPosition()
    {
        player.position = playerPosition;
    }

    void LoadCanvas()
    {
        if(PlayerPrefs.GetString("reticle") == "true")
        {
            reticle.enabled = true;
        }
        if(PlayerPrefs.GetString("ammoDisplay") == "true")
        {
            ammoDisplay.enabled = true;
        }
        if(PlayerPrefs.GetString("flashlight") == "true")
        {
            flashlight.SetActive(true);
        }
    }

    void LoadPickups()
    {   
        int i = 0;
        foreach(GameObject pickup in pickups.pickupObjects)
        {
            string nameString = pickup.name + i.ToString();
            if(PlayerPrefs.GetString(nameString) == "true")
            {
                pickups.pickupStrings[i] = "true";
                pickup.SetActive(false);
            }
            else
            {
                pickups.pickupStrings[i] = nameString; 
            }
            i++;
        }
    }

    void LoadEnemies()
    {   
        int i = 0;
        foreach(EnemyHealth enemy in enemies.enemyArray)
        {
            string nameString = enemy.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(nameString) == "true")
            {
                enemies.enemyStrings[i] = "true";
                enemy.transform.gameObject.SetActive(false);
            }
            else
            {
                enemies.enemyStrings[i] = nameString; 
            }
            i++;
        }
    }

    void LoadTriggers()
    {   
        int i = 0;
        foreach(TriggeredScript trigger in triggers.triggerArray)
        {
            string nameString = trigger.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(nameString) == "true")
            {
                triggers.triggerStrings[i] = "true";
                trigger.transform.gameObject.SetActive(false);
                if(i == 1) //LoadLightChange
                {
                    saveLight.enabled = true;
                    fogRenderer.isInside = true;
                    Destroy(savePointZero);
                }

                if(i == 2) //DollTrigger
                {
                    dollTrigger.dollTrigger = true;
                    dollTrigger.publicTriggerEvent();
                    Destroy(savePointOne);
                }

                if(i == 3) //MovieTrigger
                {
                    movieTrigger.movieTrigger = true;
                    Destroy(savePointTwo);
                }

                if(i == 4)
                {
                    barrickadeOne.SetActive(false);
                    barrickadeThree.SetActive(true);
                    Destroy(savePointThree);
                }

                if(i == 6)
                {
                    dentist.DentistTriggered();
                }
            }
            else
            {
                triggers.triggerStrings[i] = nameString; 
            }
            i++;
        }
    }

    public void LoadPlayerPosition()
    {
        player.position = playerPosition;
    }
}
