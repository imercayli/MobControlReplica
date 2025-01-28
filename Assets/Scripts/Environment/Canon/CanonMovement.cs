using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanonMovement : MonoBehaviour
{
    private InputService inputService; 
    private bool isMovementActive;
    [SerializeField] private Transform canonObject;
    [SerializeField] private float xMovementSpeed;
    [SerializeField] private float xSideLimit;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelRotationSpeed = 100f;
  
    // Start is called before the first frame update
    void Start()
    {
        inputService = ServiceSystem.GetService<InputService>();
        inputService.OnTouchDeltaPositionChanged += Move;
        SetMovementActivation(true);
    }

    private void Move(Vector2 touchDelta)
    {
        if(!isMovementActive) return;
        
        var movementDelta =  touchDelta.x * xMovementSpeed * Vector3.right *Time.deltaTime;

        var finalPosition = canonObject.localPosition + movementDelta;

        finalPosition.x = Mathf.Clamp(finalPosition.x, -xSideLimit, xSideLimit);

        canonObject.localPosition = finalPosition;

        RotateWheels(movementDelta.x);
    }

    private void RotateWheels(float movementAmount)
    {
        float rotationAngle = movementAmount * wheelRotationSpeed;
        foreach (var wheel in wheels)
        {
            wheel.Rotate(Vector3.right, rotationAngle);
        }
    }
    
    public void SetMovementActivation(bool isActive)
    {
        isMovementActive = isActive;
    }
}
