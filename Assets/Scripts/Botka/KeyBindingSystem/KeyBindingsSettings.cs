using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/*
 * @Author JAke Botka
 */
public class KeyBindingsSettings
{

    
    public enum ControlBindings
    {
        ForwardMove,BackwardMove,LeftMove,RightMove,Shoot,Reload,Interact
    }

    public KeyBindingsSettings()
    {

    }

    [SerializeField] private KeyCode _ForwardMoveBinding;
    [SerializeField] private KeyCode _BackwardMoveBinding;
    [SerializeField] private KeyCode _LeftMoveBinding;
    [SerializeField] private KeyCode _RightMoveBinding;
    [SerializeField] private KeyCode _ShootBinding;
    [SerializeField] private KeyCode _ReloadBinding;
    [SerializeField] private KeyCode _InteractBinding;


    public KeyCode getFowardMoveBinding()
    {
        return _ForwardMoveBinding;
    }

    public KeyCode getBackwardMoveBinding()
    {
        return _BackwardMoveBinding;
    }

    public KeyCode getLeftMoveBinding()
    {
        return _LeftMoveBinding;
    }

    public KeyCode getRightMoveBinding()
    {
        return _RightMoveBinding;
    }
    public KeyCode getShootBinding()
    {
        return _ShootBinding;
    }
    public KeyCode getReloadBinding()
    {
        return _ReloadBinding;
    }
    public KeyCode getInteractBinding()
    {
        return _InteractBinding;
    }

    public void setBindings(ControlBindings key, KeyCode binding)
    {
        switch (key)
        {
            case ControlBindings.ForwardMove:
                _ForwardMoveBinding = binding;
                break;
            case ControlBindings.BackwardMove:
                _BackwardMoveBinding = binding;
                break;
            case ControlBindings.LeftMove:
                _LeftMoveBinding = binding;
                break;
            case ControlBindings.RightMove:
                _RightMoveBinding = binding;
                break;
            case ControlBindings.Shoot:
                _ShootBinding = binding;
                break;
            case ControlBindings.Reload:
                _ReloadBinding = binding;
                break;
            case ControlBindings.Interact:
                _InteractBinding = binding;
                break;
            default:
                break;
        }
    }

    public KeyCode getBinding(ControlBindings key)
    {
        switch (key)
        {
            case ControlBindings.ForwardMove:
               return _ForwardMoveBinding;
                
            case ControlBindings.BackwardMove:
                return _BackwardMoveBinding;
              
            case ControlBindings.LeftMove:
                return _LeftMoveBinding;
                
            case ControlBindings.RightMove:
                return _RightMoveBinding;
                
            case ControlBindings.Shoot:
                return _ShootBinding;
                
            case ControlBindings.Reload:
                return _ReloadBinding;
                
            case ControlBindings.Interact:
                return _InteractBinding;
                
            default:
                return KeyCode.None;
        }
    }
}
