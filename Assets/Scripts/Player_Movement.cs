using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float playerSpeed;
    public Pause pause;
    public GameObject curAttack;
    private bool atkReady;
    // Start is called before the first frame update
    void Start()
    {
        atkReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!pause.gamePause)
        {
            Move();
            Rotate();
            if(Input.GetMouseButtonDown(0) && atkReady)
            {
                Attack();
            }
        }
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 movement = Vector2.ClampMagnitude(playerInput, 1) * playerSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(movement.x, movement.y, 0);
    }

    private void Rotate()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Attack()
    {
        GameObject ATK = Instantiate(curAttack, transform.position, transform.rotation).gameObject;
        ATK.transform.Translate(Vector3.right);
    }
    
    private IEnumerator PlayerAttacked()
    {
        atkReady = false;
        yield return new WaitForSeconds(curAttack.GetComponent<PlayerAttack>().attackSpeed);
        atkReady = true;

    }
}
