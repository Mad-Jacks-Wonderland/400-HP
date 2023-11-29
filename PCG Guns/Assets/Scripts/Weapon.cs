using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float damage = 10, reload = 3, rateOfFire = 0.5f, accuracy = 0.8f, shootingRange = 2000, accuracyOffset = 0.6f; // basic weapon stats, to be overriden later
    public int ammoCount = 15;
    public int currentAmmoCount;
    public float nextFire;
    bool isReloading = false;
   
    // Start is called before the first frame update
    void Start()
    {
        Reload(); // make sure the gun is reloaded at the start of its functionality
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() // shoots with delay depending on the fire rate attribute on the weapon base and the attachments
    {
        if (ammoCount > 0 && Time.time > nextFire)
        {

            Shoot();
            nextFire = Time.time + rateOfFire;

        }
       

    }

    void Shoot() // private shoot function, called inside the fire function which can be called from outside
    {

        currentAmmoCount--; // decrease the ammo

        Vector3 recoilOffset = UnityEngine.Random.insideUnitSphere * ((1 - accuracy) * accuracyOffset); // set the offcet caused by guns base innacuracy and attachments

        Debug.Log("firing raycast");
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward + recoilOffset); // currently shoots raycasts, still thinking if i want it to shoot physical projectiles or hitscan

        Debug.DrawRay(ray.origin, ray.direction * shootingRange, Color.green, 5); // draws ray

        if(Physics.Raycast(ray, out hit, shootingRange))
        {

            Debug.Log(hit.transform.name); // displays the name of the hit object

        }
    }

    void Reload() // reloads the gun and sets it back to not reloading
    {

        currentAmmoCount = ammoCount;
        isReloading = false;
    }

    public void StartReload() // if the gun isnt reloading, reload after the time specified by reload time attribute
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
