using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveScene()
    {
        
        SceneManager.LoadScene("Dungeon");
    }
    public void Quit()
    {
        Application.Quit();
      
    }
}
