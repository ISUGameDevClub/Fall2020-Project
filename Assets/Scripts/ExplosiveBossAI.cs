using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ExplosiveBossAI : MonoBehaviour
{
    private GameObject player;
    public GameObject myAttack;
    private bool attackReady;

    private GameObject stairs;
    private Projectile missile;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        attackReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if (attackReady)
        {
            Attack();
        }
    }

    private void Rotate()
    {
        Vector3 direction = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Attack()
    {
        StartCoroutine(EnemyAttacked());
        GameObject atk = Instantiate(myAttack, transform.position, transform.rotation).gameObject;
        atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
        if (atk.GetComponent<PlayerAttack>().attackSound != null)
            AudioSource.PlayClipAtPoint(atk.GetComponent<PlayerAttack>().attackSound.clip, transform.position);
    }

    private IEnumerator EnemyAttacked()
    {
        attackReady = false;
        yield return new WaitForSeconds(myAttack.GetComponent<PlayerAttack>().attackSpeed);
        attackReady = true;
    }


    private void OnDestroy()
    {
        foreach (PlayerInRoom piy in FindObjectsOfType<PlayerInRoom>())
        {
            if (piy.roomType == "Boss")
                piy.roomType = "Main";
        }
        stairs.SetActive(true);
    }
}
