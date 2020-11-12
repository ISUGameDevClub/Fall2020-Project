using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public bool alwaysDropsAnItem;
    public GameObject[] possibleCommonDrops;
    public GameObject[] possibleRareDrops;
    public GameObject[] possibleLegendaryDrops;
    public GameObject alwaysDropped;
    private void OnDestroy()
    {
        int dropNumber = 0;
        int maxNumber = (possibleCommonDrops.Length * 3 + possibleRareDrops.Length * 2 + possibleLegendaryDrops.Length * 1) * 2;
        if (!alwaysDropsAnItem)
        {
            dropNumber = Random.Range(0, maxNumber + 1);
        }
        else
        {
            dropNumber = Random.Range((maxNumber / 2) + 1, maxNumber + 1);
        }
        if (dropNumber > maxNumber / 2)
        {
            for (int i = maxNumber / 2; i < (maxNumber / 2) + possibleCommonDrops.Length * 3; i++)
            {
                if (i + 1 == dropNumber)
                {
                    if (possibleCommonDrops[(i - (maxNumber / 2)) / 3] != null)
                    {
                        Instantiate(possibleCommonDrops[(i - (maxNumber / 2)) / 3], transform.position, new Quaternion(0, 0, 0, 0));

                    }
                }
            }

            for (int i = (maxNumber / 2) + possibleCommonDrops.Length * 3; i < (maxNumber / 2) + possibleCommonDrops.Length * 3 + possibleRareDrops.Length * 2; i++)
            {
                if (i + 1 == dropNumber)
                {
                    if (possibleRareDrops[(i - ((maxNumber / 2) + possibleCommonDrops.Length * 3)) / 2] != null)
                    {
                        Instantiate(possibleRareDrops[(i - ((maxNumber / 2) + possibleCommonDrops.Length * 3)) / 2], transform.position, new Quaternion(0, 0, 0, 0));
                    }
                }
            }
            for (int i = (maxNumber / 2) + possibleCommonDrops.Length * 3 + possibleRareDrops.Length * 2; i < (maxNumber / 2) + possibleCommonDrops.Length * 3 + possibleRareDrops.Length * 2 + possibleLegendaryDrops.Length; i++)
            {
                if (i + 1 == dropNumber)
                {
                    if (possibleRareDrops[(i - ((maxNumber / 2) + possibleCommonDrops.Length * 3)) / 2] != null)
                    {
                        Instantiate(possibleLegendaryDrops[(i - ((maxNumber / 2) + possibleCommonDrops.Length * 3 + possibleRareDrops.Length * 2))], transform.position, new Quaternion(0, 0, 0, 0));
                    }
                }
            }
        }
        Debug.Log(dropNumber);
        if (alwaysDropped != null)
        {
            Instantiate(alwaysDropped, transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}