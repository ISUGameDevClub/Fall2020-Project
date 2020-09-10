using UnityEngine;
using System.Collections;
/**
 * @Author Jake Botka
 */
public class Checkpoint : MonoBehaviour
{
    
   // private Level _Level; // current  level that this instance is assigned to
   
   [SerializeField] private DifficultyScriptableObject[] _Difficulty;
    private Collider _Collider;
    private bool _Activated;

    public bool ActivateCheckpoint;

    [Header("Debug - DO NOT SET")]
   [SerializeField] private bool isActivated;
    [SerializeField] private string triggerInfo;

   void Awake()
    {
        gameObject.name = "Checkpoint - " + gameObject.transform.GetSiblingIndex();
    }
    /**
     * 
     * 
     */
    void Start()
    {
       // _Difficulty = GameManager.instance.GetGameDifficulty();
        _Collider = gameObject.GetComponent<Collider>();

        Debug.Log("Game difficulty recorded : " + _Difficulty.ToString());
      

        if (_Collider != null)
        {
            if (!(_Collider.isTrigger))
            {
                _Collider.isTrigger = true;
                Debug.LogWarning("Trigger collider did not have istrigger set to true");
            }
        }
        else
        {
            Debug.LogError("There is no collider on this gameobject. Please add one");
        }

    }

   /***
    * 
    * 
    */
    void Update()
    {
        this.dataValidation();

        this.updateDebugVariables();
    }
    /**
     * 
     * 
     */
    private void dataValidation()
    {
        if (gameObject.transform.position.y < 0)
        {

        }

        if (ActivateCheckpoint == true && _Activated == false)
        {
            _Activated = true;
            //GameManager.instance.ActivateCheckpoint(this);
        }

        if (_Collider != null)
        {
            if (_Collider.isTrigger == false)
            {
                _Collider.isTrigger = true;
            }
        }
        else
        {

        }
        

    }

    public bool hasDifficulty(DifficultyScriptableObject difficulty)
    {
        if (_Difficulty != null)
        {
            foreach(DifficultyScriptableObject dif in _Difficulty)
            {
                if (dif.Equals(difficulty))
                {
                    return true;
                }
            }

        }
        return false;
    }
    /**
     * 
     * 
     */
    private void updateDebugVariables()
    {
        isActivated = _Activated;
    }
/**
 * 
 * 
 */
    private void OnTriggerEnter(Collider other)
    {
        if (!(_Activated))
        {
            if (other.gameObject.tag == "Player")
            {
                _Activated = true;
              //  GameManager.instance.ActivateCheckpoint(this);
               // GameManager.instance.SetCheckpoint(this);
            }
        }

        triggerInfo = other.gameObject.name;
    }

    /**
     * 
     * 
     */
    private void OnTriggerExit(Collider other)
    {
        triggerInfo = "";
    }
}
