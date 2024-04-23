using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText; // ammo display
    [SerializeField]
    private Text weaponStats; // statistics of currently held weapon
    [SerializeField]
    private Text weaponTypeChoice; // choice of weapon to generate
    [SerializeField]
    private Text weaponGenData; // weapon statistics like hits and misses
    [SerializeField]
    private Text weaponPartData; // weapon's scores for the adjusted weapon generation
    public void UpdateAmmo(int count, int maxAmmo)
    {
        ammoText.text = "Ammo: " + count.ToString() + " / " + maxAmmo.ToString();
    }

    public void UpdateWeaponStats(float damage, float reloadSpeed, float rateOfFire, float accuracy, float range, float recoil, float magSize )
    {

        weaponStats.text = "Current weapon Stats: " + "\n" +
            "Damage: " + damage.ToString() + "\n" +
            "Reload speed: " + reloadSpeed.ToString() + "\n" + 
            "Rate Of Fire: " + rateOfFire.ToString() + "\n" + 
            "Accuracy: " + accuracy.ToString() + "\n" + 
            "Range: " + range.ToString() + "\n" + 
            "Recoil: " + recoil.ToString() + "\n" + 
            "Magazine Size: " + magSize.ToString();


    }

    public void UpdateGenerationData(float hits, float misses, float shotsFired, float averageDistance, float shotAccuracy, float reloads)
    {
        weaponGenData.text = "Current weapon generation data: " + "\n" +
            "Hits: " + hits.ToString() + "\n" +
            "Misses: " + misses.ToString() + "\n" +
            "Shots Fired: " + shotsFired.ToString() + "\n" +
            "Average Shot Distance: " + averageDistance.ToString() + "\n" +
            "Accuracy: " + shotAccuracy.ToString() + "\n" +
            "Reloads: " + reloads.ToString();
            
    }

    public void UpdateWeaponChoice(string partName)
    {

        weaponTypeChoice.text = "Currently selected weapon type: " + partName;

    }

    public void UpdatePartScore(float scopeScore, float barrelScore, float stockScore, float magScore)
    {
        weaponPartData.text = "Current weapon part generation data: " + "\n" +
             "Scope score: " + scopeScore.ToString() + "\n" +
             "Barrel score: " + barrelScore.ToString() + "\n" +
             "Stock score: " + stockScore.ToString() + "\n" +
             "Magazine Score: " + magScore.ToString();
             
    }
}
