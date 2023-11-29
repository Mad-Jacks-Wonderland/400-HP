using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
   // public List<WeaponStatInfo> baseStats;

    public enum PartStatType // the type of modifier for any stat
    {
        DAMAGE,
        ACCURACY,
        AMMO_CAPACITY,
        RELOAD_SPEED,
        FIRE_RATE
    }

    public enum RarityLevel // varying levels of rarity
    {
       MAKESHIFT = 0,
       STANDARD_ISSUE = 1,
       RARE = 2,
       EXPERIMENTAL = 3,
       UNIQUE = 4
    }

    [System.Serializable]
    public class WeaponStatInfo // this class will hold a type of stat, and a range of stat modifier which will be assigned to a attachment
    {

        public PartStatType stat;

        public float minStatValue;
        public float maxStatValue;

    }

    [SerializeField]
    public List<WeaponStatInfo> baseStats;
    public Dictionary<PartStatType, float> stats = new Dictionary<PartStatType, float>();

    public RarityLevel rarity;
    

    private void Awake()
    {
        foreach (WeaponStatInfo statInfo in baseStats) // loops through the list of stat modifiers and randomly gives the part a modifier value between assigned minimum and maximum
        {

            float pickedValue = Random.Range(statInfo.minStatValue, statInfo.maxStatValue);
            Debug.Log(pickedValue);
            stats.Add(statInfo.stat, pickedValue);
        }
    }
}
