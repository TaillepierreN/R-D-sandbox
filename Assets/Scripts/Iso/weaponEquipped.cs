using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponEquipped : MonoBehaviour
{
    [SerializeField] GameObject weaponslot;

    public void equippedWeapon()
    {
        weaponslot.SetActive(true);
    }
}
