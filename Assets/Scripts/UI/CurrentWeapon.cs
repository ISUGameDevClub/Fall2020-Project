using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    private UI_Inventory uii;
    public int SwitchWeapon;
    public void Update()
    {
        uii = GetComponent<UI_Inventory>();
        SwitchWeaponKeyPress();
        SwitchWeaponController();
    }
    private void SwitchWeaponKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && uii.isFull[0])
        {
            SwitchWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && uii.isFull[1])
        {

            SwitchWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && uii.isFull[2])
        {

            SwitchWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && uii.isFull[3])
        {

            SwitchWeapon = 3;
        }
    }

    private void SwitchWeaponController()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && SwitchWeapon < 3)
        {
            if (SwitchWeapon < 3 && uii.isFull[SwitchWeapon + 1])
                SwitchWeapon += 1;
            else if (SwitchWeapon < 2 && uii.isFull[SwitchWeapon + 2])
                SwitchWeapon += 2;
            else if (SwitchWeapon < 1 && uii.isFull[SwitchWeapon + 3])
                SwitchWeapon += 3;

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && SwitchWeapon > 0)
        {
            if (SwitchWeapon > 0 && uii.isFull[SwitchWeapon - 1])
                SwitchWeapon -= 1;
            else if (SwitchWeapon > 1 && uii.isFull[SwitchWeapon - 2])
                SwitchWeapon -= 2;
            else if (SwitchWeapon > 2 && uii.isFull[SwitchWeapon - 3])
                SwitchWeapon -= 3;

        }
    }
}
