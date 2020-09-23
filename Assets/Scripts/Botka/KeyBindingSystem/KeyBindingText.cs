using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyBindingText : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField]private KeyBindingsSettings.ControlBindings _BindingText;
    [SerializeField] private bool _NonBoundable;
    private int _CorresponingIndex;
    private Text _UIText;
  

    public void OnPointerClick(PointerEventData eventData)
    {
       KeyBindingUIManager manager =  GameObject.FindGameObjectWithTag(KeyBindingUIManager.TAG).GetComponentInChildren<KeyBindingUIManager>();
        Debug.Log("Binding clicked");
        if (manager != null)
        {
            if (!(_NonBoundable))
            {
                manager.StartKeyBindingEvent(this);
            }
            else
            {
                Debug.Log("Control is not boundable");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _UIText = GetComponentInChildren<Text>();
        if (_UIText != null)
        {
            _UIText.text = "KeyBindingHere";
        }

       _UIText.text =  KeyBindingsManager.instance.getKeyBindings().getBinding(_BindingText).ToString();
        
    }

    public void setText(string text)
    {
        if (_UIText != null)
        {
            _UIText.text = text;
        }
    }

    public KeyBindingsSettings.ControlBindings getBindingType()
    {
        return _BindingText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered : " + gameObject.name);
    }
}
