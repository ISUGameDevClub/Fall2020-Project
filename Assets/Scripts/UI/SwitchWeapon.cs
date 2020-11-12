using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;

public class SwitchWeapon : MonoBehaviour
{
    private WeaponInventory wi;
    private CurrentWeapon cw;
    private PlayerMovement pm;
    public GameObject WeaponBox;
    public string WeaponType;
    public int CurrentWeapon;
<<<<<<< HEAD
    private Text text;
    public int myWeaponNumber;
    public Sprite CharacterWithWeapon;
    public int myWeaponTypeNumber;
=======
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
    
    public void Start()
    {
        wi = FindObjectOfType<WeaponInventory>();
        cw = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrentWeapon>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CurrentWeapon = cw.SwitchWeapon;
        
        AddWeapon(CurrentWeapon);
    }

    private void AddWeapon(int slot)
    {
        if(wi.weapons[slot] == WeaponType)
        {
            pm.curAttack = WeaponBox;
        }  
    }
    


}
