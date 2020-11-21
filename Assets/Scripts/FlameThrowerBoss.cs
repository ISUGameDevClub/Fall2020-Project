using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerBoss : MonoBehaviour
{

    public float speed;
    public CyberChipDrop ccd;
    public float spinSpeed;
    public float timeBetweenVollyShots;
    private GameObject player;
    public GameObject myAttack;
    public GameObject myFastAttack;
    private bool attackReady;
    private bool spinAttack;
    private int spinNext;
    private GameObject stairs;
    private bool doingVolley;

    private void Awake()
    {
        stairs = FindObjectOfType<LevelTransition>().gameObject;
        stairs.SetActive(false);
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
        if (player != null)
        {
            if (!spinAttack && !doingVolley)
            {
                MoveTo();
                Rotate();
            }
            else if(!spinAttack && doingVolley)
            {
                Rotate();
            }
            else if (spinAttack)
            {
                transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * spinSpeed);
            }

            if (attackReady)
            {
                Attack();
            }
        }
    }

    private void MoveTo()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerDirection = (transform.position - player.transform.position);
        playerDirection = playerDirection.normalized;
        Vector2 movement = playerDirection * speed * Time.deltaTime * -1;
        transform.position = transform.position + new Vector3(movement.x, movement.y, 0);
    }

    private void Rotate()
    {
        Vector3 direction = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Attack()
    {
        if (spinNext > 0)
        {
            StartCoroutine(VolleyAttack());
            spinNext--;
        }
        else
        {
            StartCoroutine(SpinAttack());
            spinNext = 3;
        }
    }

    private IEnumerator VolleyAttack()
    {
        doingVolley = true;
        attackReady = false;
        GameObject atk = null;
        float zChange = -30;
        for (int x = 0; x < 5; x++)
        {
            atk = Instantiate(myFastAttack, transform.position, transform.rotation).gameObject;
            atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
            atk.transform.eulerAngles = new Vector3(0, 0, atk.transform.eulerAngles.z + zChange);
            zChange += 15;
            if (atk.GetComponent<PlayerAttack>().attackSound != null)
                AudioSource.PlayClipAtPoint(atk.GetComponent<PlayerAttack>().attackSound.clip, transform.position);
            yield return new WaitForSeconds(timeBetweenVollyShots);
        }
        doingVolley = false;
        yield return new WaitForSeconds(myFastAttack.GetComponent<PlayerAttack>().attackSpeed);
        attackReady = true;
    }

    private IEnumerator SpinAttack()
    {
        attackReady = false;
        spinAttack = true;
        GameObject atk = null;
        float rangeMultiplier = 2;
        for (int x = 0; x < 10; x++)
        {
            atk = Instantiate(myAttack, transform.position, transform.rotation).gameObject;
            atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
            atk.GetComponent<Projectile>().projectileSpeed = Mathf.Clamp(3 + rangeMultiplier, 2, 8);
            atk.GetComponent<Projectile>().speedLossOverTime = Mathf.Clamp(3 + rangeMultiplier, 2, 8);
            rangeMultiplier *= -1;
            if (atk.GetComponent<PlayerAttack>().attackSound != null)
                AudioSource.PlayClipAtPoint(atk.GetComponent<PlayerAttack>().attackSound.clip, transform.position);
            yield return new WaitForSeconds(timeBetweenVollyShots);
        }
        spinAttack = false;
        yield return new WaitForSeconds(myAttack.GetComponent<PlayerAttack>().attackSpeed);
        attackReady = true;
    }

    private void OnDestroy()
    {
        stairs.SetActive(true);

        foreach (PlayerInRoom piy in FindObjectsOfType<PlayerInRoom>())
        {
            if (piy.roomType == "Boss")
                piy.roomType = "Victory";
        }

        if (FindObjectOfType<Timer>() != false)
        {
            FindObjectOfType<Timer>().StopTimer();
        }

        if (ccd != null && !GameQuiting.gameEnding)
            ccd.DropChips();
    }
}
