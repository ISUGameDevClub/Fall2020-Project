using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public ClosedEnemyDoors ced;
    public GameObject camera;
    public bool horizontal;
    private bool transitioning;
    // direction is going to be empty until someone touches the door
    public string direction;
    // camera starting position for when the door is touched 
    private Vector3 origin;
    private Vector3 endPosition;
    private GameObject player;
    public float XTransitionDistance;
    public float YTransitionDistance;
    public float TransitionSpeed;
    // Start is called before the first frame update
    void Start()
    { //This will search for camera (and there is only one of these). 
        camera = FindObjectOfType<Camera>().gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
        // LateUpdate is used when moving the camera 
    {
        if (direction == "Right" && Vector3.Distance(camera.transform.position,origin) < XTransitionDistance) //GOing to give us the distance between the camera and where the camera started 
        {
            Time.timeScale = 0;
       
            player.transform.position=Vector3.MoveTowards(player.transform.position,endPosition, 5f*Time.unscaledDeltaTime);
            camera.transform.Translate(new Vector3(TransitionSpeed, 0, 0) * Time.unscaledDeltaTime); //Time.deltatime = The amount of seconds since the previous frame. This is moving a unit per second with Time.deltatime instead of per frame. 
        }
        else if (direction == "Right")
        {
            Time.timeScale = 1;
    
            direction = "";
            camera.transform.position = origin + new Vector3(XTransitionDistance, 0, 0);
        }
        else if (direction == "Left" && Vector3.Distance(camera.transform.position, origin) < XTransitionDistance) //GOing to give us the distance between the camera and where the camera started 
        {
            Time.timeScale = 0;
            player.transform.position = Vector3.MoveTowards(player.transform.position, endPosition, 5f * Time.unscaledDeltaTime);
            camera.transform.Translate(new Vector3(-TransitionSpeed, 0, 0) * Time.unscaledDeltaTime); //Time.deltatime = The amount of seconds since the previous frame. This is moving a unit per second with Time.deltatime instead of per frame. 
        }
        else if (direction == "Left")
        {
            Time.timeScale = 1; //Time turns back on when the transition finishes
            direction = "";
            camera.transform.position = origin + new Vector3(-XTransitionDistance, 0, 0);
        }
        else if(direction == "Up" && Vector3.Distance(camera.transform.position, origin) < YTransitionDistance) //GOing to give us the distance between the camera and where the camera started 
        {
            Time.timeScale = 0;
            player.transform.position = Vector3.MoveTowards(player.transform.position, endPosition, 5f * Time.unscaledDeltaTime);
            camera.transform.Translate(new Vector3(0, TransitionSpeed, 0) * Time.unscaledDeltaTime); //Time.deltatime = The amount of seconds since the previous frame. This is moving a unit per second with Time.deltatime instead of per frame. 
        }
        else if (direction == "Up")
        {
            Time.timeScale = 1;
   
            direction = "";
            camera.transform.position = origin + new Vector3(0, YTransitionDistance, 0);
        }
        else if (direction == "Down" && Vector3.Distance(camera.transform.position, origin) < YTransitionDistance) //GOing to give us the distance between the camera and where the camera started 
        {
            Time.timeScale = 0;
            player.transform.position = Vector3.MoveTowards(player.transform.position, endPosition, 5f * Time.unscaledDeltaTime);
            camera.transform.Translate(new Vector3(0,-TransitionSpeed, 0) * Time.unscaledDeltaTime); //Time.deltatime = The amount of seconds since the previous frame. This is moving a unit per second with Time.deltatime instead of per frame. 
        }
        else if (direction == "Down")
        {
            Time.timeScale = 1; //Time turns back on when the transition finishes
        
            direction = "";
            camera.transform.position = origin + new Vector3(0,-YTransitionDistance,0);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && direction == "") //The room will only transition when its not transitioning. 
        { //horizontal is a manually set boolean that is telling the door if its a left or right movement or a vertical movement
            origin = camera.transform.position;
            player = other.gameObject; //Other is the thing that the player is colliding with. It will save the player for later use.
            if (horizontal)
            {
                if (transform.position.x > camera.transform.position.x)
                {
                    direction = "Right";
                    endPosition = player.transform.position + new Vector3(2.5f, 0, 0);
                }
                else if (transform.position.x < camera.transform.position.x)
                {
                    direction = "Left";
                    endPosition = player.transform.position + new Vector3(-2.5f, 0, 0);
                }
            }
            else
            {
                if (transform.position.y > camera.transform.position.y)
                {
                    direction = "Up";
                    endPosition = player.transform.position + new Vector3(0,2.5f, 0);
                }
                else if (transform.position.y < camera.transform.position.y)
                {
                    direction = "Down";
                    endPosition = player.transform.position + new Vector3(0, -2.5f, 0);
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ced.ActivateRoom(other);
    }
}
