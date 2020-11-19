using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberChipDrop : MonoBehaviour
{
    public GameObject[] chips;

    public GameObject pos1;
    public GameObject pos2;

    private void Update()
    {
        if (pos1 == null && pos2 != null)
            Destroy(pos2);
        else if (pos1 != null && pos2 == null)
            Destroy(pos1);
    }

    public void DropChips()
    {
        int chip1 = 0;
        for(int x = 0; x < 100; x++)
        {
            int rand = Random.Range(0, chips.Length);

            if (chips[rand].GetComponent<CyberChip>().speedBoost && !PlayerMovement.speedBoost)
            {
                pos1 = Instantiate(chips[rand], pos1.transform.position, pos1.transform.rotation);
                chip1 = rand;
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().attackSpeedBoost && !PlayerMovement.attackSpeedBoost)
            {
                pos1 = Instantiate(chips[rand], pos1.transform.position, pos1.transform.rotation);
                chip1 = rand;
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().healthBoost && PlayerMovement.maxHealth != 6)
            {
                pos1 = Instantiate(chips[rand], pos1.transform.position, pos1.transform.rotation);
                chip1 = rand;
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().batteryBoost && BatteryInventory.batteries < 5)
            {
                pos1 = Instantiate(chips[rand], pos1.transform.position, pos1.transform.rotation);
                chip1 = rand;
                break;
            }
        }

        for (int x = 0; x < 100; x++)
        {
            int rand = Random.Range(0, chips.Length);

            if (chips[rand].GetComponent<CyberChip>().speedBoost && !PlayerMovement.speedBoost && rand != chip1)
            {
                pos2 = Instantiate(chips[rand], pos2.transform.position, pos2.transform.rotation);
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().attackSpeedBoost && !PlayerMovement.attackSpeedBoost && rand != chip1)
            {
                pos2 =  Instantiate(chips[rand], pos2.transform.position, pos2.transform.rotation);
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().healthBoost && PlayerMovement.maxHealth != 6 && rand != chip1)
            {
                pos2 =  Instantiate(chips[rand], pos2.transform.position, pos2.transform.rotation);
                break;
            }

            if (chips[rand].GetComponent<CyberChip>().batteryBoost && BatteryInventory.batteries < 5 && rand != chip1)
            {
                pos2 = Instantiate(chips[rand], pos2.transform.position, pos2.transform.rotation);
                break;
            }
        }
    }
}
