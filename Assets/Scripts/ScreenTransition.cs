using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public bool fadeOut, fadeIn;
    public float fadeSpeed;
    public float a;
    public Image trans;
    public GameObject act;
    public Title title;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (fadeIn == true)
        {
            Color objectColor = trans.GetComponent<Image>().color;
            float fadeAmount = objectColor.a + (fadeSpeed - Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            trans.GetComponent<Image>().color = objectColor;
            if (objectColor.a >= 255)
            {
                fadeIn = false;
            }
            StartCoroutine(Wait());
            
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        title.MoveScene();
    }

}
