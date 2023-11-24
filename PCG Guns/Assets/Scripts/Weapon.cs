using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float damage = 10, reload = 3, rateOfFire = 0.5f, accuracy = 0.8f, shootingRange = 2000, accuracyOffset = 0.6f;
    public int ammoCount = 15;
    public int currentAmmoCount;
    public float nextFire;
    bool isReloading = false;
   
    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (ammoCount > 0 && Time.time > nextFire)
        {

            Shoot();
            nextFire = Time.time + rateOfFire;

        }
       

    }

    void Shoot()
    {

        currentAmmoCount--;

        Vector3 recoilOffset = UnityEngine.Random.insideUnitSphere * ((1 - accuracy) * accuracyOffset);

        Debug.Log("firing raycast");
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward + recoilOffset);

        Debug.DrawRay(ray.origin, ray.direction * shootingRange, Color.green, 5);

        if(Physics.Raycast(ray, out hit, shootingRange))
        {

            Debug.Log(hit.transform.name);

        }
    }

    void Reload()
    {

        currentAmmoCount = ammoCount;
        isReloading = false;
    }

    public void StartReload()
    {

        if(!isReloading)
        {

            isReloading = true;
            Invoke("Reload", reload);

        }

    }

    void SetWeaponStats(Dictionary<WeaponPart.PartStatType, float> weaponStats)
    {

        damage = weaponStats[WeaponPart.PartStatType.DAMAGE];
        reload = weaponStats[WeaponPart.PartStatType.RELOAD_SPEED];
        accuracy = weaponStats[WeaponPart.PartStatType.ACCURACY];
        ammoCount = (int)weaponStats[WeaponPart.PartStatType.AMMO_CAPACITY];
        rateOfFire = weaponStats[WeaponPart.PartStatType.FIRE_RATE];
    }


    public void Initialize(Dictionary<WeaponPart.PartStatType, float> weaponStats)
    {

        SetWeaponStats(weaponStats);

    }

}
