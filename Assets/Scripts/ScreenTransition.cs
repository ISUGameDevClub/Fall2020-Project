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

        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Wait(SceneManager.GetActiveScene().name));
        }
    }

    public void FadeOut()
    {
        act.SetActive(true);
        fadeOut = true;
    }

    public void FadeIn()
    {
        StartCoroutine(Wait(nextScene));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FadeToTitle()
    {
        StartCoroutine(Wait("Title"));
    }

    public void FadeToDeath()
    {
        StartCoroutine(Wait("Death Screen"));
    }

    public void FadeToCredits()
    {
        StartCoroutine(Wait("Credits"));
    }

    IEnumerator Wait(string ns)
    {
        act.SetActive(true);
        fadeIn = true;
        Barrel[] allbarrels = FindObjectsOfType<Barrel>();
        foreach (Barrel bar in allbarrels)
        {
            bar.itemToSpawn = null;
        }
        ItemDrops[] allItems = FindObjectsOfType<ItemDrops>();
        foreach (ItemDrops itemDrop in allItems)
        {
            itemDrop.possibleCommonDrops = new GameObject[0];
            itemDrop.possibleRareDrops = new GameObject[0];
            itemDrop.possibleLegendaryDrops = new GameObject[0];
            itemDrop.alwaysDropped = null;
        }
        if (FindObjectOfType<PlayerHealth>() != null)
            FindObjectOfType<PlayerHealth>().hearts[0] = null;

        yield return new WaitForSecondsRealtime(1);
        if(FindObjectOfType<UI_Inventory>() != null)
            FindObjectOfType<UI_Inventory>().UpdateStaticWeapons();
        SceneManager.LoadScene(ns);
    }
}
