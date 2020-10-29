using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    public string nextScene = "Title";
    public bool fadeOut, fadeIn;
    public float fadeSpeed;
    public float a;
    public Image trans;
    public GameObject act;
    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut == true)
        {
            Color objectColor = trans.GetComponent<Image>().color;
            float fadeAmount = objectColor.a - (fadeSpeed - Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            trans.GetComponent<Image>().color = objectColor;
            if(objectColor.a <= 0)
            {
                fadeOut = false;
            }
            
        }

        if (fadeIn == true)
        {
            Color objectColor = trans.GetComponent<Image>().color;
            float fadeAmount = objectColor.a + (fadeSpeed - Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            trans.GetComponent<Image>().color = objectColor;
            if (objectColor.a >= 1)
            {
                fadeIn = false;
            }

        }
    }

    public void FadeOut()
    {
        act.SetActive(true);
        fadeOut = true;
    }

    public void FadeIn()
    {
        act.SetActive(true);
        fadeIn = true;
        StartCoroutine(Wait());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Wait()
    {
        Barrel[] allbarrels = FindObjectsOfType<Barrel>();
        foreach (Barrel bar in allbarrels)
        {
            bar.itemToSpawn = null;
        }
        if(FindObjectOfType<PlayerHealth>() != null)
            FindObjectOfType<PlayerHealth>().hearts[0] = null;

        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(nextScene);
    }


    public void FadeToTitle()
    {
        act.SetActive(true);
        fadeIn = true;
        StartCoroutine(WaitForTitle());
    }

    IEnumerator WaitForTitle()
    {
        Barrel[] allbarrels = FindObjectsOfType<Barrel>();
        foreach (Barrel bar in allbarrels)
        {
            bar.itemToSpawn = null;
        }
        if (FindObjectOfType<PlayerHealth>() != null)
            FindObjectOfType<PlayerHealth>().hearts[0] = null;

        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("Title");
    }

    public void FadeToDeath()
    {
        act.SetActive(true);
        fadeIn = true;
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        Barrel[] allbarrels = FindObjectsOfType<Barrel>();
        foreach (Barrel bar in allbarrels)
        {
            bar.itemToSpawn = null;
        }
        if (FindObjectOfType<PlayerHealth>() != null)
            FindObjectOfType<PlayerHealth>().hearts[0] = null;

        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("Death Screen");
    }
}
