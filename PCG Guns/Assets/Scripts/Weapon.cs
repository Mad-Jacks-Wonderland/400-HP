using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float damage = 10, reload = 3, rateOfFire = 0.5f, accuracy = 1.0f, shootingRange = 2000, accuracyOffset = 0.00f; // basic weapon stats, to be overriden later
    public int ammoCount = 15;
    public int currentAmmoCount;
    public float nextFire;
    public bool isReloading = false;
    //bullet 
    public GameObject bullet;
    public GameObject camHolder;

    //bullet force
    public float shootForce, upwardForce;

    public Transform attackPoint;
    public Camera fpsCam;
    public LineRenderer lRender;

    public float hits;
    public float misses;
    public float shotsFired;
    public float averageDistance;
    private float totalDistance;
    public float shotAccuracy;
    public float reloads;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        Reload(); // make sure the gun is reloaded at the start of its functionality

        fpsCam = FindObjectOfType<Camera>(); // find the camera
        if (fpsCam)
            Debug.Log("CanvasRenderer object found: " + fpsCam.name);
        else
            Debug.Log("No CanvasRenderer object could be found");
        lRender = FindObjectOfType<LineRenderer>();

        camHolder = GameObject.Find("Camera Holder");

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); // find the UI manager
    }


   

    public void Fire() // shoots with delay depending on the fire rate attribute on the weapon base and the attachments
    {
        if (currentAmmoCount > 0 && Time.time >= nextFire)
        {

            Shoot();
            nextFire = Time.time + rateOfFire;

        }

       // uiManager.UpdateGenerationData(hits,misses,shotsFired,averageDistance, shotAccuracy, reloads);
    }

    void Shoot() // private shoot function, called inside the fire function which can be called from outside
    {

        currentAmmoCount--; // decrease the ammo
        shotsFired++;

        Vector3 recoilOffset = UnityEngine.Random.insideUnitSphere * ((1 - accuracy) * accuracyOffset); // set the offset caused by guns base inaccuracy and attachments

        Debug.Log("firing raycast");
        RaycastHit hit;
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward  + recoilOffset ); // currently shoots raycasts, still thinking if i want it to shoot physical projectiles or hitscan

        Debug.DrawRay(ray.origin, ray.direction * shootingRange, Color.green, 5); // draws ray

        if(Physics.Raycast(ray, out hit, shootingRange))
        {

            lRender.SetPosition(0, fpsCam.transform.position);
            lRender.SetPosition(1, fpsCam.transform.forward * shootingRange); // render line in order to see the trajectory of a last shot
            
            //Debug.Log(hit.transform.name); // displays the name of the hit object
            Debug.Log(hit.transform.tag); // displays the name of the hit object

            if (hit.transform.tag == "enemy") // if the bullet hits the enemy, add a hit and calculate the distance total and average shot distance to use in adjusted weapon generation
            {

                hits++;

                totalDistance = totalDistance + (Vector3.Distance(fpsCam.transform.position, hit.transform.position));

                averageDistance = totalDistance / (/*hits / */shotsFired);

            }
            else
            {

                Debug.Log("missed"); // misses are also used and kept track of
                misses++;
            }

        }
        else    
        {

            Debug.Log("missed"); // misses are also used and kept track of
            misses++;
        }

        shotAccuracy = hits / shotsFired; // calculate accuracy
     




    }

    void Reload() // reloads the gun and sets it back to not reloading
    {
        reloads++;
        currentAmmoCount = ammoCount;
        isReloading = false;
        //uiManager.UpdateGenerationData(hits, misses, shotsFired, averageDistance, shotAccuracy, reloads);
    }

    public void StartReload() // if the gun isnt reloading, reload after the time specified by reload time attribute
    {

        if(!isReloading)
        {

            isReloading = true;
            Invoke("Reload", reload);

        }

    }

    void SetWeaponStats(Dictionary<WeaponPart.PartStatType, float> weaponStats) // sets the stats by adding the modifiers from the weapon parts
    {

        damage += weaponStats[WeaponPart.PartStatType.DAMAGE];
        reload += weaponStats[WeaponPart.PartStatType.RELOAD_SPEED];
        accuracy += weaponStats[WeaponPart.PartStatType.ACCURACY];
        ammoCount += (int)weaponStats[WeaponPart.PartStatType.AMMO_CAPACITY];
        rateOfFire += weaponStats[WeaponPart.PartStatType.FIRE_RATE];
    }


    public void Initialize(Dictionary<WeaponPart.PartStatType, float> weaponStats) // initialization of the weapon stats calculation
    {

        SetWeaponStats(weaponStats);

    }

}
