using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool loading;
    private bool canEnter;
    private bool nextScene;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1,1,1,0);
        canEnter = false;
    }

    private void Update()
    {
        if (loading)
        {
            sr.color = new Color(1, 1, 1, sr.color.a + Time.deltaTime * .35f);

            if (sr.color.a >= 1)
            {
                canEnter = true;
                loading = false;
            }
        }
    }

    private void OnEnable()
    {
        loading = true;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canEnter && !nextScene)
        {
            nextScene = true;
            if(GetComponent<BeatenGame>())
            {
                GetComponent<BeatenGame>().BeatGame();
            }
            FindObjectOfType<ScreenTransition>().FadeIn();
        }
        else if (collision.gameObject.tag == "Destructible")
            Destroy(collision.gameObject);
    }
}
