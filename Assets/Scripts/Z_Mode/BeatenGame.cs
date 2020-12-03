using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatenGame : MonoBehaviour
{
    public bool beatGame;
    public int fastestSeconds;
    public int fastestMinutes;
    public int highestWave;
    public bool checkIfGameBeaten;
    public bool showTime;
    public bool showWave;

    public void BeatGame()
    {
        LoadPlayer();
        beatGame = true;

        if((fastestMinutes == 0 && fastestSeconds == 0) || Timer.minutes <= fastestMinutes)
        {
            if((fastestMinutes == 0 && fastestSeconds == 0) || Timer.minutes < fastestMinutes || Timer.seconds < fastestSeconds)
            {
                fastestMinutes = Timer.minutes;
                fastestSeconds = Mathf.RoundToInt(Timer.seconds);
            }
        }

        SaveSystem.SavePlayerData(this);
    }

    public void ResetGame()
    {
        beatGame = false;
        fastestSeconds = 0;
        fastestMinutes = 0;
        highestWave = 0;

        SaveSystem.SavePlayerData(this);
    }

    public void ZombieDeath()
    {
        LoadPlayer();

        if(FindObjectOfType<ZController>().currentWave > highestWave)
        {
            highestWave = FindObjectOfType<ZController>().currentWave;

            SaveSystem.SavePlayerData(this);
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadData();

        if (data != null)
        {
            beatGame = data.beatenGame;
            fastestSeconds = data.fastestSeconds;
            fastestMinutes = data.fastestMinutes;
            highestWave = data.highestWave;
        }
    }

    private void Start()
    {
        if(checkIfGameBeaten)
        {
            LoadPlayer();
            if (!beatGame)
                Destroy(gameObject);
            else if(showTime)
            {
                if (Mathf.RoundToInt(fastestSeconds) > 9)
                    GetComponent<Text>().text = "Fastest Time: " + fastestMinutes.ToString("F0") + ":" + Mathf.RoundToInt(fastestSeconds).ToString("F0");
                else
                    GetComponent<Text>().text = "Fastest Time: " + fastestMinutes.ToString("F0") + ":" + "0" + Mathf.RoundToInt(fastestSeconds).ToString("F0");
            }
            else if(showWave)
            {
                if(highestWave != 0)
                    GetComponent<Text>().text = "Highest Wave: " + highestWave.ToString("F0");
                else
                    Destroy(gameObject);
            }
        }
    }
}
