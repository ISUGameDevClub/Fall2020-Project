using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool speedBoost = false;
    public static bool attackSpeedBoost = false;
    public static int maxHealth = 5;

    public GameObject curAttack;
    public float speed;
    private bool attackReady;
    public float stun;
    public GameObject shieldObject;

    public AudioSource footstep1;
    public AudioSource footstep2;
    public AudioSource weaponBreak;

    private float timeUntilNextStep;
    private bool step1Next;

    private void Start()
    {
        attackReady = true;
        shieldObject.SetActive(false);
        GetComponent<Health>().maxHealth = maxHealth;
        GetComponent<Health>().curHealth = maxHealth;
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        if (stun <= 0 && !FindObjectOfType<Pause>().gamePause)
        {
            Rotate();

            if (Input.GetMouseButton(0) && attackReady)
            {
                Attack();
            }
        }
        else if (!FindObjectOfType<Pause>().gamePause)
        {
            stun -= Time.deltaTime;
        }

        if (curAttack == null)
            shieldObject.SetActive(true);
        else
            shieldObject.SetActive(false);

        if (Input.GetKey(KeyCode.Q) && GetComponent<CurrentWeapon>().SwitchWeapon != 0)
        {
            PickUp pu = Instantiate(GetComponent<UI_Inventory>().slots[GetComponent<CurrentWeapon>().SwitchWeapon].GetComponentInChildren<SwitchWeapon>().myDrop, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<PickUp>();
            pu.dur = FindObjectOfType<UI_Inventory>().durs[GetComponent<CurrentWeapon>().SwitchWeapon];
            pu.justDropped = 1;
            FindObjectOfType<UI_Inventory>().durs[GetComponent<CurrentWeapon>().SwitchWeapon] -= 100;

            FindObjectOfType<UI_Inventory>().isFull[GetComponent<CurrentWeapon>().SwitchWeapon] = false;
            FindObjectOfType<WeaponInventory>().weapons[GetComponent<CurrentWeapon>().SwitchWeapon] = "";
            Destroy(FindObjectOfType<UI_Inventory>().slots[GetComponent<CurrentWeapon>().SwitchWeapon].GetComponentInChildren<SwitchWeapon>().gameObject);
            FindObjectOfType<CurrentWeapon>().SwitchWeapon = 0;

            AudioSource.PlayClipAtPoint(weaponBreak.clip, transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (stun <= 0 && !FindObjectOfType<Pause>().gamePause)
        {
            Move();
        }
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 movement = new Vector2(0, 0);

        if (!speedBoost)
            movement = Vector2.ClampMagnitude(playerInput, 1) * speed * Time.fixedDeltaTime;
        else
            movement = Vector2.ClampMagnitude(playerInput, 1) * speed * 1.25f * Time.fixedDeltaTime;

        GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position + new Vector2(movement.x, movement.y);

        if (playerInput.magnitude > .5f)
        {
            if (timeUntilNextStep > 0)
                timeUntilNextStep -= Time.deltaTime;
            else
            {
                if (step1Next)
                {
                    AudioSource.PlayClipAtPoint(footstep1.clip, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(footstep2.clip, transform.position);
                }
                timeUntilNextStep = .25f;
                step1Next = !step1Next;
            }
        }
        else
            timeUntilNextStep = .2f;
    }

    private void Rotate()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Attack()
    {
        if (curAttack != null)
        {
            StartCoroutine(PlayerAttacked());
            GameObject atk = Instantiate(curAttack, transform.position, transform.rotation).gameObject;
            FindObjectOfType<UI_Inventory>().durs[GetComponent<CurrentWeapon>().SwitchWeapon] -= atk.GetComponent<PlayerAttack>().breakSpeed;
            atk.transform.Translate(Vector3.right * atk.GetComponent<PlayerAttack>().spawnDistancefromPlayer);
                
            if (FindObjectOfType<UI_Inventory>().durs[GetComponent<CurrentWeapon>().SwitchWeapon] <= 0)
            {
                FindObjectOfType<UI_Inventory>().isFull[GetComponent<CurrentWeapon>().SwitchWeapon] = false;
                FindObjectOfType<WeaponInventory>().weapons[GetComponent<CurrentWeapon>().SwitchWeapon] = "";
                Destroy(FindObjectOfType<UI_Inventory>().slots[GetComponent<CurrentWeapon>().SwitchWeapon].GetComponentInChildren<SwitchWeapon>().gameObject);
                FindObjectOfType<CurrentWeapon>().SwitchWeapon = 0;

                AudioSource.PlayClipAtPoint(weaponBreak.clip, transform.position);
            }
            else if (atk.GetComponent<PlayerAttack>().attackSound != null)
                AudioSource.PlayClipAtPoint(atk.GetComponent<PlayerAttack>().attackSound.clip, transform.position);
        }
    }

    private IEnumerator PlayerAttacked()
    {
        attackReady = false;
        if(!attackSpeedBoost)
            yield return new WaitForSeconds(curAttack.GetComponent<PlayerAttack>().attackSpeed);
        else
            yield return new WaitForSeconds(curAttack.GetComponent<PlayerAttack>().attackSpeed * .8f);
        attackReady = true;
    }
    
    public void StunPlayer(float time)
    {
        if (stun <= time)
        {
            stun = time;
        }
    }
}
