using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : WeaponPart
{

    public Transform gunBarrelSocket; // assigns the spots in which the parts will be attached to
    public Transform gunMagazineSocket;
    public Transform gunStockSocket;
    public Transform gunScopeSocket;

    public Weapon weapon;

    int rawRarity = 0;
    // Start is called before the first frame update

    List<WeaponPart> gunParts = new List<WeaponPart>(); // create a list of weapon parts to use in gun generation
    Dictionary<PartStatType, float> weaponStats = new Dictionary<PartStatType, float>(); // new dictionart for stat types and values

    public void Initialize(WeaponPart barrel, WeaponPart mag ,WeaponPart stock ,WeaponPart scope) // adds weapon parts to a list, and then initializes them using a dictionary of weapon stats
    {
        gunParts.Add(barrel);
        gunParts.Add(mag);
        gunParts.Add(stock);
        gunParts.Add(scope);

        CalculateStats(); // this part calculates the statistics based on part's assigned statistic modifier
        DetermineRarity();
        weapon.Initialize(weaponStats);
    }

    private void CalculateStats()
    {
        // go through all gun parts and their statistics

        foreach (WeaponPart part in gunParts)
        {

            rawRarity += (int)part.rarity;

            foreach(KeyValuePair<PartStatType, float> stat in part.stats)
            {
                //Debug.Log(stat.Key);
                //Debug.Log(stat.Value);

                weaponStats.Add(stat.Key, stat.Value); // adds modifier to list which will be sent further to generate the weapon stats
            }
        }
    }

    private void DetermineRarity() // determines rarity based on the rarity of all the parts
    {

        int averageRarity = rawRarity / gunParts.Count;
        averageRarity = Math.Clamp(averageRarity, 0, 4);
        rarity = (RarityLevel)averageRarity;

        Debug.Log(rarity);
    }
}
