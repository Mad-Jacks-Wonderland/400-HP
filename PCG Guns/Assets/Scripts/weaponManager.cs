using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponManager : MonoBehaviour
{

    public List<GameObject> gunBody; // create lists of weapon parts to generate weapons with
    public List<GameObject> gunScope;
    public List<GameObject> gunMag;
    public List<GameObject> gunStock;
    public List<GameObject> gunBarrel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // currently generates new weapon upon pressing spacebar
            GenerateGun();
    }

    private void GenerateGun() // script loops through all the parts and spawns a random part for each attachment slot
    {
        // get a random body from a list
        
        GameObject RandomBody = GetRandomPart(gunBody);
        GameObject insBody = Instantiate(RandomBody, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBod = insBody.GetComponent<WeaponBody>();


        WeaponPart barrel = SpawnWeaponPart(gunBarrel, weaponBod.gunBarrelSocket);
        //GameObject RandomBarrel = GetRandomPart(gunBarrel);
        //Instantiate(RandomBarrel, weaponBod.gunBarrelSocket.position, Quaternion.identity);

        WeaponPart stock = SpawnWeaponPart(gunStock, weaponBod.gunStockSocket);
        //GameObject RandomStock = GetRandomPart(gunStock);
        //Instantiate(RandomStock, weaponBod.gunStockSocket.position, Quaternion.identity);

        WeaponPart mag = SpawnWeaponPart(gunMag, weaponBod.gunMagazineSocket);
        //GameObject RandomMag = GetRandomPart(gunMag);
        //Instantiate(RandomMag, weaponBod.gunMagazineSocket.position, Quaternion.identity);

        WeaponPart scope = SpawnWeaponPart(gunScope, weaponBod.gunScopeSocket);
        //GameObject RandomScope = GetRandomPart(gunScope);
        //Instantiate(RandomScope, weaponBod.gunScopeSocket.position, Quaternion.identity);

        weaponBod.Initialize(barrel, mag, stock, scope); // weapon is then put together inside WeaponBody script
    }

    WeaponPart SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {

        GameObject randomPart = GetRandomPart(parts); // get random part
        GameObject insPart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation); // create part in the game engine
        insPart.transform.parent = socket; // put the part into its associated spot on the weapon body

        return insPart.GetComponent<WeaponPart>(); //  send back the instanciated weapon part
    }

    private GameObject GetRandomPart(List<GameObject> partList) // gets random part from the lists generated at the beginning, all parts are added manually, but it makes the whole system flexible with easy process of adding new parts
    {
        int randomNum = UnityEngine.Random.Range(0, partList.Count); // random part is picked

        return partList[randomNum]; // and then sent back to add to weapon
    }
}
