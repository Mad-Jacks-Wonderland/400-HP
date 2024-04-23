using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{

    public Transform weaponSocketPos; // get weapon position

     Weapon focusedWeapon, equippedWeapon; // these will be used to equip and fire the gun

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(focusedWeapon != null && Input.GetKeyDown(KeyCode.E)) // picks up the weapon is in range
        {

            for (var i = weaponSocketPos.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(weaponSocketPos.transform.GetChild(i).gameObject); //destroy the weapon
            }


            //Destroy(equippedWeapon);
            EquipWeapon(focusedWeapon); // equips the weapon if you are close enough to it, and press e
            uiManager.UpdateWeaponStats(focusedWeapon.damage, focusedWeapon.reload, focusedWeapon.rateOfFire, focusedWeapon.accuracy, focusedWeapon.shootingRange, focusedWeapon.accuracyOffset, focusedWeapon.ammoCount); // add weapon stats on the UI
            uiManager.UpdateGenerationData(focusedWeapon.hits, focusedWeapon.misses, focusedWeapon.shotsFired, focusedWeapon.averageDistance, focusedWeapon.shotAccuracy, focusedWeapon.reloads); // add Data like reloads and hits on the UI to keep track of to see how the gneration behaves in response to diferrent data
        }

        if(equippedWeapon != null && Input.GetMouseButton(0)) // shots when left mouse button is pressed and updates the UI
        {

            equippedWeapon.Fire(); // triggers the fire script from the currently held
            uiManager.UpdateAmmo(focusedWeapon.currentAmmoCount, focusedWeapon.ammoCount);
            uiManager.UpdateGenerationData(focusedWeapon.hits, focusedWeapon.misses, focusedWeapon.shotsFired, focusedWeapon.averageDistance, focusedWeapon.shotAccuracy, focusedWeapon.reloads);
        }

        if (equippedWeapon != null && Input.GetKeyDown(KeyCode.R)) // reloads if r is pressed and updates the UI
        {

            equippedWeapon.StartReload(); // calls the reload function on the current weapon
            uiManager.UpdateAmmo(focusedWeapon.currentAmmoCount, focusedWeapon.ammoCount);
            uiManager.UpdateGenerationData(focusedWeapon.hits, focusedWeapon.misses, focusedWeapon.shotsFired, focusedWeapon.averageDistance, focusedWeapon.shotAccuracy, focusedWeapon.reloads);
        }

        if(equippedWeapon.isReloading == false)
            uiManager.UpdateAmmo(focusedWeapon.currentAmmoCount, focusedWeapon.ammoCount);
    }

    private void EquipWeapon(Weapon weaponToEquip) // equips the weapon if in proximity to it
    {
        equippedWeapon = weaponToEquip;
        equippedWeapon.transform.parent = weaponSocketPos;
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;    
    }

    private void OnTriggerEnter(Collider other) // sets the focused weapon to the nearest weapon which collides with the player's collider abd hes the "weapon" tag affixed to it
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {

            focusedWeapon = other.GetComponent<Weapon>();

        }
    }

    public Weapon GetCurrentWeapon() // getter to interact with other scripts
    {


        return equippedWeapon;
    }

}
