using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static private bool timerEnabled;
    public Text timerText;
    static public float seconds;
    static public int minutes;
    private bool timerStoped;

    // Start is called before the first frame update
    void Start()
    {
        timerText.gameObject.SetActive(false);
        if(timerEnabled)
            timerText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStoped)
        {
            timerText.color = Color.black;
            seconds += Time.deltaTime;
            if (seconds > 60)
            {
                minutes++;
                seconds -= 60;
            }

            if (timerText != null)
            {
                if (Mathf.RoundToInt(seconds) > 9)
                    timerText.text = minutes.ToString("F0") + ":" + Mathf.RoundToInt(seconds).ToString("F0");
                else
                    timerText.text = minutes.ToString("F0") + ":" + "0" + Mathf.RoundToInt(seconds).ToString("F0");
            }
        }
        else
            timerText.color = Color.green;
    }
    
    public void ShowTimer()
    {
        if (!timerEnabled)
        {
            timerEnabled = true;
            timerText.gameObject.SetActive(true);
        }
        else
        {
            timerEnabled = false;
            timerText.gameObject.SetActive(false);
        }
    }

    public void StopTimer()
    {
        timerStoped = true;
    }
}
