using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public Pause pause;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.gamePause == true)
        {
            
            menu.SetActive(true);
        }
        if (!pause.gamePause)
        {
            menu.SetActive(false);
        }
       
    }
}
