using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : WeaponPart
{

    public Transform gunBarrelSocket;
    public Transform gunMagazineSocket;
    public Transform gunStockSocket;
    public Transform gunScopeSocket;

    // Start is called before the first frame update

    List<WeaponPart> gunParts = new List<WeaponPart>();
    Dictionary<PartStatType, float> weaponStats = new Dictionary<PartStatType, float>();

    public void Initialize(WeaponPart barrel, WeaponPart mag ,WeaponPart stock ,WeaponPart scope)
    {
        gunParts.Add(barrel);
        gunParts.Add(mag);
        gunParts.Add(stock);
        gunParts.Add(scope);

        CalculateStats();
    }

    private void CalculateStats()
    {
        // go through all gun parts and their statistics

        foreach (WeaponPart part in gunParts)
        {
            foreach(KeyValuePair<PartStatType, float> stat in part.stats)
            {
                //Debug.Log(stat.Key);
                //Debug.Log(stat.Value);

                weaponStats.Add(stat.Key, stat.Value);
            }
        }
    }
}
