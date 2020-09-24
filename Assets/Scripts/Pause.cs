using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool gamePause;
    public GameObject menu;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause Game");
            PauseGame();
            
        }
       
    }
    public void PauseGame()
    {
        gamePause = true;
        Time.timeScale = 0;
        Debug.Log("Menu Showing");
        menu.SetActive(true);

    }

    public void ResumeGame()
    {
        gamePause = false;
        Time.timeScale = 1;
        Debug.Log("Menu Hidden");
        menu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
