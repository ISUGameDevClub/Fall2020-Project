using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotBossAI : MonoBehaviour
{
    private GameObject player;

    public float speed;
    private bool charging;
    private bool stunned;
    public float timeUntilCharge;
    private float chargeTimer;
    public float timeStunned;
    private float stunTimer;
    public float backwardsSpeed;
    private GameObject stairs;

    private void Awake()
    {
        stairs = FindObjectOfType<LevelTransition>().gameObject;
        stairs.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        chargeTimer = timeUntilCharge;
        stunTimer = timeStunned;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stunned && !charging && chargeTimer > 0)
        {
            Rotate();
            chargeTimer -= Time.deltaTime;

            if (chargeTimer <= .5f && chargeTimer > .3f)
                GetComponent<Animator>().SetTrigger("Prepare");
        }
        else if (!charging && !stunned)
        {
            charging = true;
        }
        else if (charging)
        {
            Move();
        }
        else if (stunned && stunTimer > 0)
        {
            MoveAwayFromWall();
            stunTimer -= Time.deltaTime;

        }
        else
        {
            chargeTimer = timeUntilCharge;
            stunTimer = timeStunned;
            stunned = false;
        }
        
    }
    private void Rotate()
    {
        Vector3 direction = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 movement = transform.right * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(movement.x, movement.y, 0);
    }

    private void MoveAwayFromWall()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 movement = -transform.right * (backwardsSpeed) * Time.deltaTime;
        transform.position = transform.position + new Vector3(movement.x, movement.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (charging && !stunned && collision.gameObject.tag == "Wall")
        {
            stunned = true;
            charging = false;
        }
    }

    private void OnDestroy()
    {
        foreach(PlayerInRoom piy in FindObjectsOfType<PlayerInRoom>())
        {
            if (piy.roomType == "Boss")
                piy.roomType = "Main";
        }
<<<<<<< HEAD

        if(FindObjectOfType<Timer>() != false) // MOVE TO FINAL BOSS
        {
            FindObjectOfType<Timer>().StopTimer();
        }
=======
        stairs.SetActive(true);
>>>>>>> d8f934a9bedafd7e6df3a29a5952d9ad8ba196ed
    }
}
