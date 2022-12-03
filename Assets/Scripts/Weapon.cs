using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlashOne;
    [SerializeField] ParticleSystem muzzleFlashTwo;
    [SerializeField] GameObject muzzleFlashThree;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] bool fullAuto = false;
    [SerializeField] public bool pickedUp = false;

    bool canShoot = true;

    void OnEnable() 
    {
        canShoot = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) >= 1)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType); 
        }
        else
        {
            Debug.Log("NO AMMO");
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

        if (Input.GetMouseButton(0) && fullAuto == true && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    void PlayMuzzleFlash()
    {
        muzzleFlashOne.Play();
        muzzleFlashTwo.Play();
        muzzleFlashThree.SetActive(true);
        Invoke("TurnOffLight", 0.2f);
    }

    void TurnOffLight()
    {
        muzzleFlashThree.SetActive(false);
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
