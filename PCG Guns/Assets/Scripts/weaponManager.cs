using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponManager : MonoBehaviour
{

    public List<GameObject> gunBody;
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
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateGun();
    }

    private void GenerateGun()
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

        weaponBod.Initialize(barrel, mag, stock, scope);
    }

    WeaponPart SpawnWeaponPart(List<GameObject> parts, Transform socket)
    {

        GameObject randomPart = GetRandomPart(parts);
        GameObject insPart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation);
        insPart.transform.parent = socket;

        return insPart.GetComponent<WeaponPart>();
    }

    private GameObject GetRandomPart(List<GameObject> partList)
    {
        int randomNum = UnityEngine.Random.Range(0, partList.Count);

        return partList[randomNum];
    }
}
