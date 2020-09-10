using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingsManager : MonoBehaviour
{
    public static KeyBindingsManager instance;

    private void Awake()
    {
        instance = this;
        _KeyBindingSettings = new KeyBindingsSettings();
        ResetToDefaults();
       
    }

     private KeyBindingsSettings _KeyBindingSettings;
  
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("Reset To Default Bindings")]
    public void ResetToDefaults()
    {
        if (_KeyBindingSettings != null)
        {
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.ForwardMove, KeyCode.W);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.BackwardMove, KeyCode.S);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.LeftMove, KeyCode.A);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.RightMove, KeyCode.D);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.Shoot, KeyCode.Mouse0);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.Reload, KeyCode.R);
            _KeyBindingSettings.setBindings(KeyBindingsSettings.ControlBindings.Interact, KeyCode.E);
        }

    }

    public void LoadKeyBindings(KeyBindingsSettings settings)
    {
        _KeyBindingSettings = settings;
    }


    public KeyBindingsSettings getKeyBindings()
    {
        return _KeyBindingSettings;
    }
}
