using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public string[] weapon;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddWeapon(string newWeapon){
      for(int x = 0; x <weapon.Length; x++){
        if(weapon[x] == ""){
          weapon[x] = newWeapon;
          x = weapon.Length;
        }
      }
    }

}
