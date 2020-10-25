using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    public int SwitchWeapon;
    public void Update()
    {
        SwitchWeaponKeyPress();
        SwitchWeaponController();
    }
    private void SwitchWeaponKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            SwitchWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            SwitchWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            SwitchWeapon = 3;
        }
    }
    private void SwitchWeaponController()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && SwitchWeapon < 3)
        {
            SwitchWeapon += 1;

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && SwitchWeapon > 0)
        {
            SwitchWeapon -= 1;

        }
    }
}
