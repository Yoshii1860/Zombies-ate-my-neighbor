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
    Ammo ammo;
    int pistolAmmo;
    int shotgunAmmo;
    int rifleAmmo;
    int sniperAmmo;
    string loadPistol;
    string loadShotgun;
    string loadRifle;
    string loadSniper;

    void Awake() 
    {
        this.enabled = false;
        ammo = FindObjectOfType<Ammo>();
    }

    void Start()
    {
        LoadBullets();
        LoadWeapons();
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
}
