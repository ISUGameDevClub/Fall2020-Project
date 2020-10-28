﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float musicVolume;
    public float musicFadeSpeed;
    public AudioSource mainTheme;
    public AudioSource shopTheme;
    public AudioSource bossTheme;
    public string currentSong;

    private bool bossThemeCanPlay;
    // Start is called before the first frame update
    void Start()
    {
        mainTheme.volume = 0;
        shopTheme.volume = 0;
        bossTheme.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSong=="Main" && mainTheme.volume < musicVolume) 
        {
            mainTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong=="Main")
        {
            mainTheme.volume = musicVolume; 
        }
        else if (mainTheme.volume > 0)
        {
            mainTheme.volume -= Time.deltaTime * musicFadeSpeed;
        }

        if (currentSong == "Shop" && shopTheme.volume < musicVolume)
        {
            shopTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong == "Shop")
        {
            shopTheme.volume = musicVolume;
        }
        else if (shopTheme.volume > 0)
        {
            shopTheme.volume -= Time.deltaTime * musicFadeSpeed;

        }

        if (currentSong == "Boss" && bossTheme.volume < musicVolume)
        {
            if(!bossThemeCanPlay)
            {
                bossThemeCanPlay = true;
                bossTheme.Play();
            }
            bossTheme.volume += Time.deltaTime * musicFadeSpeed;
        }
        else if (currentSong == "Boss")
        {
            bossTheme.volume = musicVolume;
        }
        else if (bossTheme.volume > 0)
        {
            bossTheme.volume -= Time.deltaTime * musicFadeSpeed;
        }
    }
}