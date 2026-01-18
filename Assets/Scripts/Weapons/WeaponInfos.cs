using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons")]
public class WeaponInfos : ScriptableObject
{
    public GameObject weaponPrefab;
    public float countdownTime;
    public int weaponDamage;
    public int weaponRange;
}
