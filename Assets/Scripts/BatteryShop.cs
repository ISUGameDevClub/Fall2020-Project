using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryShop : MonoBehaviour
{
    public bool weapon;
    public int batteryCost;
    public BatteryInventory be;
    public AudioSource rejectSound;
    bool canPlaySound;
    public bool zMode;

    void Start()
    {
        be = FindObjectOfType<BatteryInventory>();
        canPlaySound = true;
    }

    public bool CanPickup()
    {
        bool invFull = true;

        bool[] inv = FindObjectOfType<UI_Inventory>().isFull;
        foreach(bool slot in inv)
        {
            if (!slot)
                invFull = false;
        }

        if (BatteryInventory.batteries >= batteryCost && (!invFull || !weapon))
        {
            BatteryInventory.batteries -= batteryCost;
            return true;
        }
        if (canPlaySound)
            StartCoroutine(playSound());
        return false;
    }

    private IEnumerator playSound()
    {
        canPlaySound = false;
        AudioSource.PlayClipAtPoint(rejectSound.clip, transform.position);
        yield return new WaitForSeconds(.5f);
        canPlaySound = true;
    }
}
