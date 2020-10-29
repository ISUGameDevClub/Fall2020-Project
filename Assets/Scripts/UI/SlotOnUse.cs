using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotOnUse : MonoBehaviour
{
    public float opacity;
    public GameObject[] Slots;
    public CurrentWeapon cw;
    public int CurrentWeapon;
    public void Start()
    {
        cw= GameObject.FindGameObjectWithTag("Player").GetComponent<CurrentWeapon>();
        
    }

    private void Update()
    {
        CurrentWeapon = cw.SwitchWeapon;
        OnUse();
    }

    private void OnUse()
    {
        if (CurrentWeapon == 0)
        {
            Slots[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }else{
            Slots[0].GetComponent<Image>().color = new Color(1, 1, 1, opacity);
        }
                
        if (CurrentWeapon == 1)
        {
            Slots[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            Slots[1].GetComponent<Image>().color = new Color(1, 1, 1, opacity);
        }
         if (CurrentWeapon == 2)
        {
            Slots[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            Slots[2].GetComponent<Image>().color = new Color(1, 1, 1, opacity);
        }
         if (CurrentWeapon == 3)
        {
            Slots[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            Slots[3].GetComponent<Image>().color = new Color(1, 1, 1, opacity);
        }
        
    }
   

}
