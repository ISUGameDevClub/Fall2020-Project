using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public GameObject[] itemButtons;

    public bool[] isFull;
    public GameObject[] slots;
    public int[] durs;

    private WeaponInventory wi;

    public static int[] lastDurs = new int[4];
    public static string[] lastWeaponTypes = new string[4];
    public static int[] lastItemButtonNums = new int[4];

    //0: Knife
    //1: Axe
    //2: Pistol
    //3: Machine Gun
    //4: Shield
    //5:
    //6:
    //7:
    //8: Flamethrower


    private void Start()
    {
        wi = FindObjectOfType<WeaponInventory>();
        GetWeapon(100, "Knife", 0);

        for(int x = 0; x < slots.Length; x++)
        {
            if (lastItemButtonNums[x] != 0)
            {
                GetWeapon(lastDurs[x], lastWeaponTypes[x], lastItemButtonNums[x]);
            }
        }

    }

    public void UpdateStaticWeapons()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            if (slots[x].GetComponentInChildren<SwitchWeapon>() != null)
            {
                lastDurs[x] = durs[x];
                lastItemButtonNums[x] = slots[x].GetComponentInChildren<SwitchWeapon>().myWeaponTypeNumber;
                lastWeaponTypes[x] = slots[x].GetComponentInChildren<SwitchWeapon>().WeaponType;
            }
            else
            {
                lastItemButtonNums[x] = 0;
            }
        }
    }

    public bool GetWeapon(int durability, string weaponType, int itemButtonNum)
    {
        wi.AddWeapon(weaponType);
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;
                durs[i] = durability;
                Instantiate(itemButtons[itemButtonNum], slots[i].transform, false).GetComponent<SwitchWeapon>().myWeaponNumber = i;
                return true;
            }
        }
        return false;
    }
}
