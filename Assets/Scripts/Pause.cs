using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool gamePause;
    public GameObject menu;
    private RoomMove[] rm;
    

    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectsOfType<RoomMove>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool canPause = true;
            foreach(RoomMove r in rm)
            {
                if (r.direction != "")
                    canPause = false;
            }

            if(canPause)
                PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePause = true;
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        gamePause = false;
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
