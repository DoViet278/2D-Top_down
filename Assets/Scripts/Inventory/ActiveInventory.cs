using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlot = 0;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();  
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToogleActiveSlot((int)ctx.ReadValue<float>()-1);

        ToogleHighlight(0);
    }

    private void OnEnable()
    {
        playerControls.Inventory.Enable();
    }

    
    private void ToogleActiveSlot(int num)
    {
        ToogleHighlight(num);
    }

    private void ToogleHighlight(int num)
    {
        activeSlot = num;   

        foreach(Transform invetorySlot in this.transform)
        {
            invetorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(num).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();   
    }

    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);    
        }
        Transform childTransform = transform.GetChild(activeSlot);
        InventorySlot slot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfos weaponInfo = slot.GetWeaponInfos();    

        if(weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;    

        //tao object active weapon
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());   
    }

}
