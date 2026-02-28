using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new Input Reader",menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject,InputActions.IPlayerActions
{
    private InputActions _inputActions;
    
    public event Action<Vector2> MoveEvent;
    public event Action<bool> FireEvent;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new InputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        
        if(context.canceled)
            MoveEvent?.Invoke(Vector2.zero);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
            FireEvent?.Invoke(true);
        
        if(context.canceled)
            FireEvent?.Invoke(false);
    }
}
