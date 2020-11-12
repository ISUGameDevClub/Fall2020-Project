using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : MonoBehaviour
{
    public float timeToShootAfterSeeingThePlayer;
    private GameObject player;

    public float speed;
    public float stun;
    public float sightDistance;

    public GameObject myAttack;
    private bool attackReady;
    private float hasSeenForTime;

    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = Vector3.zero;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        attackReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stun <= 0)
        {
            if (LookForPlayer())
            {
                playerPosition = player.transform.position;
                if ((player.transform.position - transform.position).magnitude < sightDistance)
                {
                    Move();
                }
                else
                {
                    MoveAway();
                }
                Rotate();
                if (attackReady && hasSeenForTime <= 0)
                {
                    Attack();
                }
                else
                    hasSeenForTime -= Time.deltaTime;
            }

            else
            {
                hasSeenForTime = timeToShootAfterSeeingThePlayer;
                if (playerPosition != Vector3.zero)
                    MoveToLast();
            }
        }
        else
        {
            stun -= Time.deltaTime;
        }
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerDirection = (transform.position - player.transform.position);
        playerDirection = playerDirection.normalized;
        Vector2 movement = playerDirection * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(movement.x,movement.y, 0);
    }

    private void MoveAway()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerDirection = (transform.position - player.transform.position);
        playerDirection = playerDirection.normalized;
        Vector2 movement = playerDirection * speed * Time.deltaTime * -1;
        transform.position = transform.position + new Vector3(movement.x,movement.y, 0);
    }

    private void MoveToLast()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerDirection = (transform.position - playerPosition);
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

    private bool LookForPlayer(){
        if (player != null)
        {
            int layer_mask = LayerMask.GetMask("Default");
            Vector3 direction = (player.transform.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100, layer_mask);
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }
}
