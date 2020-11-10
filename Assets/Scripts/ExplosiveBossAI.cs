using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ExplosiveBossAI : MonoBehaviour
{
    public float spinSpeed;
    public float timeBetweenVollyShots;
    private GameObject player;
    public GameObject myAttack;
    public GameObject myFastAttack;
    private bool attackReady;
    private bool spinAttack;
    private bool spinNext;

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
        if (!spinAttack)
        {
            Rotate();
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * spinSpeed);
        }

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
        if (!spinNext)
        {
            StartCoroutine(VolleyAttack());
            spinNext = true;
        }
        else
        {
            StartCoroutine(SpinAttack());
            spinNext = false;
        }
    }

    private IEnumerator VolleyAttack()
    {
        attackReady = false;
        GameObject atk = null;
        for (int x = 0; x < 5; x++)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            float randAngle = Random.Range(-25f, 25f);
            float rangeMultiplier = Random.Range(-.5f, .5f);
            atk = Instantiate(myFastAttack, transform.position, transform.rotation).gameObject;
            atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
            atk.transform.eulerAngles = new Vector3(0, 0, atk.transform.eulerAngles.z + randAngle);
            atk.GetComponent<ExplisiveProjectile>().projectileSpeed = Mathf.Clamp((playerDistance - 1) + rangeMultiplier, 2, 8) * 1.5f;
            if (atk.GetComponent<PlayerAttack>().attackSound != null)
                AudioSource.PlayClipAtPoint(atk.GetComponent<PlayerAttack>().attackSound.clip, transform.position);
            yield return new WaitForSeconds(timeBetweenVollyShots);
        }
        yield return new WaitForSeconds(myFastAttack.GetComponent<PlayerAttack>().attackSpeed);
        attackReady = true;
    }

    private IEnumerator SpinAttack()
    {
        attackReady = false;
        spinAttack = true;
        GameObject atk = null;
        for (int x = 0; x < 12; x++)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            float randAngle = Random.Range(-25f, 25f);
            float rangeMultiplier = Random.Range(-.5f, .5f);
            atk = Instantiate(myAttack, transform.position, transform.rotation).gameObject;
            atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
            atk.transform.eulerAngles = new Vector3(0, 0, atk.transform.eulerAngles.z + randAngle);
            atk.GetComponent<ExplisiveProjectile>().projectileSpeed = Mathf.Clamp((playerDistance - 1) + rangeMultiplier, 2, 8);
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
        foreach (PlayerInRoom piy in FindObjectsOfType<PlayerInRoom>())
        {
            if (piy.roomType == "Boss")
                piy.roomType = "Main";
        }
    }
}
