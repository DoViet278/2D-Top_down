using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfos weaponInfos;

    public WeaponInfos GetWeaponInfos()
    {
        return weaponInfos;
    }   
}
