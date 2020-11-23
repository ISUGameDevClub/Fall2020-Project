using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZDoor : MonoBehaviour
{
    public int cost;
    public Spawner[] activateSpawners;
    public SpriteRenderer mySprite;
    public Collider2D myCol;
    public GameObject myText;
    public Text mt;

    private void Start()
    {
        mt.text = cost.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && BatteryInventory.batteries >= cost)
        {
            BatteryInventory.batteries -= cost;
            mySprite.enabled = false;
            myCol.enabled = false;
            myText.SetActive(false);

            for(int x = 0; x < activateSpawners.Length; x++)
            {
                activateSpawners[x].active = true;
            }
        }
    }
}
