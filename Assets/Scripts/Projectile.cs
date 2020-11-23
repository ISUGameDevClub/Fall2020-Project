using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float speedLossOverTime;
    private float speedLoss;
    public bool verticalSprite;
    public GameObject verticalSpriteGameObject;
    public bool passOverDestructible;
    public bool stopOnWall;

    // Start is called before the first frame update
    void Start()
    {
        if(verticalSprite && verticalSpriteGameObject != null)
        {
            verticalSpriteGameObject.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        speedLoss += Time.deltaTime * speedLossOverTime;
        transform.Translate(Vector3.right * Mathf.Max((projectileSpeed - speedLoss), 0)* Time.deltaTime);

        if (verticalSprite && verticalSpriteGameObject != null)
        {
            verticalSpriteGameObject.transform.position = transform.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Barrel" || (!passOverDestructible && collision.gameObject.tag == "Destructible" && !(collision.gameObject.GetComponent<Health>().Zmode && GetComponent<HurtBox>().playerAttack)))
        {
            if (!stopOnWall || (stopOnWall && collision.gameObject.tag != "Wall"))
                Destroy(gameObject);
            else
            {
                projectileSpeed = 0;
                speedLossOverTime = 0;
                speedLoss = 0;
            }
        }
        else if (collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Turret")
        {
            AudioSource.PlayClipAtPoint(collision.GetComponent<AudioSource>().clip, transform.position);

            if(collision.gameObject.GetComponentInParent<PlayerMovement>())
            {
                FindObjectOfType<UI_Inventory>().durs[collision.gameObject.GetComponentInParent<PlayerMovement>().GetComponent<CurrentWeapon>().SwitchWeapon] -= 10;

                if (FindObjectOfType<UI_Inventory>().durs[collision.gameObject.GetComponentInParent<PlayerMovement>().GetComponent<CurrentWeapon>().SwitchWeapon] <= 0)
                {
                    FindObjectOfType<UI_Inventory>().isFull[collision.gameObject.GetComponentInParent<PlayerMovement>().GetComponent<CurrentWeapon>().SwitchWeapon] = false;
                    FindObjectOfType<WeaponInventory>().weapons[collision.gameObject.GetComponentInParent<PlayerMovement>().GetComponent<CurrentWeapon>().SwitchWeapon] = "";
                    Destroy(FindObjectOfType<UI_Inventory>().slots[collision.gameObject.GetComponentInParent<PlayerMovement>().GetComponent<CurrentWeapon>().SwitchWeapon].GetComponentInChildren<SwitchWeapon>().gameObject);
                    FindObjectOfType<CurrentWeapon>().SwitchWeapon = 0;
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (verticalSprite && verticalSpriteGameObject != null)
        {
            Destroy(verticalSpriteGameObject);
        }
    }
}
