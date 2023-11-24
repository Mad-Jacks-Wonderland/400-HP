using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{

    public Transform weaponSocketPos;

    Weapon focusedWeapon, equippedWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(focusedWeapon != null && Input.GetKeyDown(KeyCode.E))
        {

            EquipWeapon(focusedWeapon);

        }

        if(equippedWeapon != null && Input.GetMouseButton(0))
        {

            equippedWeapon.Fire();

        }

        if (equippedWeapon != null && Input.GetKeyDown(KeyCode.R))
        {

            equippedWeapon.StartReload();

        }
    }

    private void EquipWeapon(Weapon weaponToEquip)
    {
        equippedWeapon = weaponToEquip;
        equippedWeapon.transform.parent = weaponSocketPos;
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {

            focusedWeapon = other.GetComponent<Weapon>();

        }
    }

}
