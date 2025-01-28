using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : BaseService<InputService>
{
    private InputSource inputSource;

    public bool IsPressed { get; private set; }
    
    public event Action<bool> OnTouchStateChanged;
    public event Action<Vector2> OnTouchDeltaPositionChanged;

   

    public override void Initialize()
    {
        base.Initialize();
        inputSource = new InputSource();

        inputSource.Enable();

        inputSource.Player.TouchContact.started += OnStartTouch;
        inputSource.Player.TouchContact.canceled += OnEndTouch;
        inputSource.Player.TouchDeltaPosition.performed += OnTouchDeltaPositionPerformed;
    }

    public override void DeInitialize()
    {
        base.DeInitialize();
        inputSource.Player.TouchContact.started -= OnStartTouch;
        inputSource.Player.TouchContact.canceled -= OnEndTouch;
        inputSource.Player.TouchDeltaPosition.performed -= OnTouchDeltaPositionPerformed;

        inputSource.Disable();
    }

    private void OnTouchDeltaPositionPerformed(InputAction.CallbackContext context)
    {
         OnTouchDeltaPositionChanged?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnStartTouch(InputAction.CallbackContext context)
    {
        IsPressed = true;
        OnTouchStateChanged?.Invoke(IsPressed);
    }

    private void OnEndTouch(InputAction.CallbackContext context)
    {
        IsPressed = false;
        OnTouchStateChanged?.Invoke(IsPressed);
    }
}