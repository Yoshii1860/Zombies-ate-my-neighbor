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

    Ammo ammo;
    int pistolAmmo = 0;
    int shotgunAmmo = 0;
    int rifleAmmo = 0;
    int sniperAmmo = 0;

    void Awake() 
    {
        this.enabled = false;
        ammo = FindObjectOfType<Ammo>();
    }

    void Start()
    {
        GetBullets();
        GetWeapons();
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
}
