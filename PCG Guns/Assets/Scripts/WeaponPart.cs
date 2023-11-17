using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
   // public List<WeaponStatInfo> baseStats;

    public enum PartStatType
    {
        DAMAGE,
        ACCURACY,
        AMMO_CAPACITY,
        RELOAD_SPEED,
        FIRE_RATE
    }

    [System.Serializable]
    public class WeaponStatInfo
    {

        public PartStatType stat;

        public float minStatValue;
        public float maxStatValue;

    }

    [SerializeField]
    public List<WeaponStatInfo> baseStats;
    public Dictionary<PartStatType, float> stats = new Dictionary<PartStatType, float>();

    private void Awake()
    {
        foreach (WeaponStatInfo statInfo in baseStats) 
        {

            float pickedValue = Random.Range(statInfo.minStatValue, statInfo.maxStatValue);
            Debug.Log(pickedValue);
            stats.Add(statInfo.stat, pickedValue);
        }
    }
}
