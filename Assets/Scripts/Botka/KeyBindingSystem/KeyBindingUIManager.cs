using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingUIManager : MonoBehaviour
{
    public const string TAG = "KeyBindingUI";
    [SerializeField] [Range(2f,50f)]private float _RequestTimeOut;
    [SerializeField] private Text _InstructionalText;

    private Coroutine _RequestTimeoutCoroutine;
    private KeyCode lastKeyPressed;
    private bool _runEvent;

    private KeyBindingText _eventObject;
    // Start is called before the first frame update
    void Start()
    {
        // _InstructionalText.enabled = false;
        _InstructionalText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnGUI()
    {
        if (_runEvent)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                lastKeyPressed = e.keyCode;
                _eventObject.setText(e.keyCode.ToString());
                KeyBindingsManager.instance.getKeyBindings().setBindings(_eventObject.getBindingType(), e.keyCode);
                this.StopKeyBindingEvent();
                Debug.Log("Detected key code: " + e.keyCode);
            }
        }
    }


    public void StartKeyBindingEvent(KeyBindingText text)
    {
        Debug.Log("Started");
        _eventObject = text;
        _InstructionalText.gameObject.SetActive(true);
        _runEvent = true;
        if (_RequestTimeoutCoroutine == null)
        {
            _RequestTimeoutCoroutine = StartCoroutine(RequestTimeout());
        }
       
    }

    public void StopKeyBindingEvent()
    {
        Debug.Log("Event Stopped");
        _eventObject = null;
        _runEvent = false;
        _InstructionalText.gameObject.SetActive(false);
        if (_RequestTimeoutCoroutine != null)
        {
            StopCoroutine(_RequestTimeoutCoroutine);
        }
    }

    private IEnumerator RequestTimeout()
    {
        yield return new WaitForSeconds(_RequestTimeOut);
    }

    
}
