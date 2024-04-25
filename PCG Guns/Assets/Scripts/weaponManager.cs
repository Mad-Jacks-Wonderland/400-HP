using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponManager : MonoBehaviour
{

    public List<WeightedPart> gunBody; // create lists of weapon parts to generate weapons with
    public List<WeightedPart> gunScope;
    public List<WeightedPart> gunMag;
    public List<WeightedPart> gunStock;
    public List<WeightedPart> gunBarrel;

    //public List<GameObject> gunBody; // old part list
    //public List<GameObject> gunScope;
    //public List<GameObject> gunMag;
    //public List<GameObject> gunStock;
    //public List<GameObject> gunBarrel;

    public int CurrentBody; // keep track of the currently used weapon index
    private int bodyChoice = 0; // this is used for custom spawning of a selected weapon
    Weapon currentWeapon; // current weapon player is using
   public GameObject Player; // reference to the player

    private UIManager uiManager; // reference to the UI so the data can be displayed

    float barrelSelection;
    float scopeSelection;
    float stockSelection;
    float magSelection;

    enum GunPart
    {
       BODY,
       BARREL,
       SCOPE,
       STOCK,
       MAG
    }

    // Start is called before the first frame update
    void Start() // get components and find UI manager, and update the weapon body name
    {
        currentWeapon = Player.GetComponent<playerInput>().GetCurrentWeapon();

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        uiManager.UpdateWeaponChoice(gunBody[bodyChoice].partName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // currently generates new weapon upon pressing spacebar
        {
            GenerateRandomGun();
            ResetWeights();
        }

        if (Input.GetKeyDown(KeyCode.C)) // currently generates new chosen weapon upon pressing C
        {
            GenerateSelectedGun(bodyChoice);
            ResetWeights();
        }

        if (Input.GetKeyDown(KeyCode.V)) // currently generates new adapted weapon using button V
        {
            GenerateAdjustedGun();
        }

       

        if (Input.GetKeyDown(KeyCode.Z)) // Z and X switches between the weapon bodies to generate and update UI to let the player know what weapon he wants to generate
        {
            bodyChoice--;

            if (bodyChoice < 0)
                bodyChoice = gunBody.Count - 1;

            uiManager.UpdateWeaponChoice(gunBody[bodyChoice].partName);
        }

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            bodyChoice++;

            if (bodyChoice >= gunBody.Count)
                bodyChoice = 0;

            uiManager.UpdateWeaponChoice(gunBody[bodyChoice].partName);
        }

        currentWeapon = Player.GetComponent<playerInput>().GetCurrentWeapon(); // calculates the score for procedural weighted random selection of the parts later

        if(currentWeapon != null)
        {

            barrelSelection = 20 * (currentWeapon.misses / currentWeapon.damage / currentWeapon.accuracy);
            scopeSelection = ((currentWeapon.averageDistance + currentWeapon.hits) - (currentWeapon.misses * 2)) / currentWeapon.accuracy;
            stockSelection = 10 * (currentWeapon.reloads * 2) / 1 + currentWeapon.accuracy;
            magSelection = 0.4f * (((10 * currentWeapon.reloads) + (currentWeapon.shotsFired * 0.25f)) / currentWeapon.rateOfFire);

            uiManager.UpdatePartScore(scopeSelection, barrelSelection, stockSelection, magSelection); // score is being updated and displayed on the UI

        }
       
    }

    private void GenerateRandomGun() // script loops through all the parts and spawns a random part for each attachment slot
    {
        // get a random body from a list and instanciate it
        
        GameObject RandomBody = GetRandomBody(gunBody);
        GameObject insBody = Instantiate(RandomBody, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBod = insBody.GetComponent<WeaponBody>();


        WeaponPart barrel = SpawnWeaponPart(gunBarrel, weaponBod.gunBarrelSocket); // the part is being randomly picked for all the slots of the weapon
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

    private void GenerateSelectedGun(int bodynum) // script loops through all the parts and spawns a random part for each attachment slot
    {
        // get a random body from a list and instanciate it

        GameObject Body = gunBody[bodynum].gunPart;
        GameObject insBody = Instantiate(Body, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBod = insBody.GetComponent<WeaponBody>();

        CurrentBody = bodynum; // keep the track of the index of a weapon body

        WeaponPart barrel = SpawnWeaponPart(gunBarrel, weaponBod.gunBarrelSocket); // fetch all the parts of the weapon
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

    private void GenerateAdjustedGun() // adjusted weapon script, it will spawn the weapon according to the scores the player achieved during the game
    {
        // get a selected body

        GameObject Body = gunBody[CurrentBody].gunPart;
        GameObject insBody = Instantiate(Body, Vector3.zero, Quaternion.identity);
        WeaponBody weaponBod = insBody.GetComponent<WeaponBody>();


        WeaponPart barrel = SpawnAdjustedWeaponPart(gunBarrel, weaponBod.gunBarrelSocket, GunPart.BARREL); // fetch the appropriate part using weighted random chance
        //GameObject RandomBarrel = GetRandomPart(gunBarrel);
        //Instantiate(RandomBarrel, weaponBod.gunBarrelSocket.position, Quaternion.identity);

        WeaponPart stock = SpawnAdjustedWeaponPart(gunStock, weaponBod.gunStockSocket, GunPart.STOCK);
        //GameObject RandomStock = GetRandomPart(gunStock);
        //Instantiate(RandomStock, weaponBod.gunStockSocket.position, Quaternion.identity);

        WeaponPart mag = SpawnAdjustedWeaponPart(gunMag, weaponBod.gunMagazineSocket, GunPart.MAG);
        //GameObject RandomMag = GetRandomPart(gunMag);
        //Instantiate(RandomMag, weaponBod.gunMagazineSocket.position, Quaternion.identity);

        WeaponPart scope = SpawnAdjustedWeaponPart(gunScope, weaponBod.gunScopeSocket, GunPart.SCOPE);
        //GameObject RandomScope = GetRandomPart(gunScope);
        //Instantiate(RandomScope, weaponBod.gunScopeSocket.position, Quaternion.identity);

        weaponBod.Initialize(barrel, mag, stock, scope); // weapon is then put together inside WeaponBody script
    }

    WeaponPart SpawnWeaponPart(List<WeightedPart> parts, Transform socket) // spawns a single weapon part
    {

        GameObject randomPart = GetRandomPart(parts); // get random part
        GameObject insPart = Instantiate(randomPart, socket.transform.position, socket.transform.rotation); // create part in the game engine
        insPart.transform.parent = socket; // put the part into its associated spot on the weapon body

        return insPart.GetComponent<WeaponPart>(); //  send back the instanciated weapon part
    }

    WeaponPart SpawnAdjustedWeaponPart(List<WeightedPart> parts, Transform socket, GunPart type)
    {
        

        currentWeapon = Player.GetComponent<playerInput>().GetCurrentWeapon();
        
        int i = 0;

        switch (type) // determine the type of part you want to generate
        {
            case GunPart.BARREL:
               
                
                
                    switch(barrelSelection) // add weighting to the part and the ones next to it to add some randomness to the weapon generation
                    {
                    case <= 20:
                        parts[0].weight += 20;
                        parts[1].weight += 10;
                        break;

                    case > 20 and < 40:

                        parts[0].weight += 10;
                        parts[1].weight += 20;
                        parts[2].weight += 10;

                        break;

                    case > 40 and < 60:

                        parts[1].weight += 10;
                        parts[2].weight += 20;
                        parts[3].weight += 10;

                        break;

                    case > 60 and < 80:

                        parts[2].weight += 10;
                        parts[3].weight += 20;
                        parts[4].weight += 10;

                        break;

                    case >= 80:

                        parts[3].weight += 10;
                        parts[4].weight += 20;

                        break;
                    }
                    
                break;
            case GunPart.SCOPE:
                

                switch (scopeSelection)
                {
                    case <= 20:
                        parts[0].weight += 20;
                        parts[1].weight += 10;
                        break;

                    case > 20 and < 40:

                        parts[0].weight += 10;
                        parts[1].weight += 20;
                        parts[2].weight += 10;

                        break;

                    case > 40 and < 60:

                        parts[1].weight += 10;
                        parts[2].weight += 20;
                        parts[3].weight += 10;

                        break;

                    case > 60 and < 80:

                        parts[2].weight += 10;
                        parts[3].weight += 20;
                        parts[4].weight += 10;

                        break;

                    case >= 80:

                        parts[3].weight += 10;
                        parts[4].weight += 20;

                        break;
                }

                break;
            case GunPart.STOCK:
               

                switch (stockSelection)
                {
                    case <= 20:
                        parts[0].weight += 20;
                        parts[1].weight += 10;
                        break;

                    case > 20 and < 40:

                        parts[0].weight += 10;
                        parts[1].weight += 20;
                        parts[2].weight += 10;

                        break;

                    case > 40 and < 60:

                        parts[1].weight += 10;
                        parts[2].weight += 20;
                        parts[3].weight += 10;

                        break;

                    case > 60 and < 80:

                        parts[2].weight += 10;
                        parts[3].weight += 20;
                        parts[4].weight += 10;

                        break;

                    case >= 80:

                        parts[3].weight += 10;
                        parts[4].weight += 20;

                        break;
                }

                break;
            case GunPart.MAG:
               

                switch (magSelection)
                {
                    case <= 20:
                        parts[0].weight += 20;
                        parts[1].weight += 10;
                        break;

                    case > 20 and < 40:

                        parts[0].weight += 10;
                        parts[1].weight += 20;
                        parts[2].weight += 10;

                        break;

                    case > 40 and < 60:

                        parts[1].weight += 10;
                        parts[2].weight += 20;
                        parts[3].weight += 10;

                        break;

                    case > 60 and < 80:

                        parts[2].weight += 10;
                        parts[3].weight += 20;
                        parts[4].weight += 10;

                        break;

                    case >= 80:

                        parts[3].weight += 10;
                        parts[4].weight += 20;

                        break;
                }

                break;



        }


      

        GameObject selectedPart = GetRandomWeighted(parts); // get random weighted part
        GameObject insPart = Instantiate(selectedPart, socket.transform.position, socket.transform.rotation); // create part in the game engine
        insPart.transform.parent = socket; // put the part into its associated spot on the weapon body

        return insPart.GetComponent<WeaponPart>(); //  send back the instanciated weapon part


    }

    private GameObject GetRandomPart(List<WeightedPart> partList) // gets random part from the lists generated at the beginning, all parts are added manually, but it makes the whole system flexible with easy process of adding new parts
    {
        int randomNum = UnityEngine.Random.Range(0, partList.Count); // random part is picked

        return partList[randomNum].gunPart; // and then sent back to add to weapon
    }

    

   private GameObject GetRandomBody(List<WeightedPart> partList) // fetches a random weaponbody specifically, in order to keep track of a generated weapon index
    {

        int randomNum = UnityEngine.Random.Range(0, partList.Count); // random part is picked

        CurrentBody = randomNum; // save the number for adjusted weapon generation

        return partList[randomNum].gunPart; // and then sent back to add to weapon
    }

   

 

    GameObject GetRandomWeighted(List<WeightedPart> weightedValueList) // gets random weighted value using a script below
    {
        GameObject output = null;

        //Getting a random weight value
        int totalWeight = 0;
        foreach (var entry in weightedValueList)
        {
            totalWeight += entry.weight;
        }
        var rndWeightValue = UnityEngine.Random.Range(1, totalWeight + 1);

        //Checking where random weight value falls
        var processedWeight = 0;
        foreach (var entry in weightedValueList)
        {
            processedWeight += entry.weight;
            if (rndWeightValue <= processedWeight)
            {
                output = entry.gunPart;
                break;
            }
        }

        return output;
    }

    public void ResetWeights() // resets the weights after new weapon is generated
    {

        gunBarrel[0].weight = 5;
        gunBarrel[1].weight = 10;
        gunBarrel[2].weight = 20;
        gunBarrel[3].weight = 10;
        gunBarrel[4].weight = 5;

        gunMag[0].weight = 5;
        gunMag[1].weight = 10;
        gunMag[2].weight = 20;
        gunMag[3].weight = 10;
        gunMag[4].weight = 5;

        gunStock[0].weight = 5;
        gunStock[1].weight = 10;
        gunStock[2].weight = 20;
        gunStock[3].weight = 10;
        gunStock[4].weight = 5;

        gunScope[0].weight = 5;
        gunScope[1].weight = 10;
        gunScope[2].weight = 20;
        gunScope[3].weight = 10;
        gunScope[4].weight = 5;

    }
}
