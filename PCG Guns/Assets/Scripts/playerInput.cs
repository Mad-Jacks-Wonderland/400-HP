using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{

    public Transform weaponSocketPos; // get weapon position

    Weapon focusedWeapon, equippedWeapon; // these will be used to equip and fire the gun
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(focusedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {

            EquipWeapon(focusedWeapon); // equips the weapon if you are close enough to it, and press e

        }

        if(equippedWeapon != null && Input.GetMouseButton(0))
        {

            equippedWeapon.Fire(); // triggers the fire script from the currently held

        }

        if (equippedWeapon != null && Input.GetKeyDown(KeyCode.R))
        {

            equippedWeapon.StartReload(); // calls the reload function on the current weapon

        }
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

}
