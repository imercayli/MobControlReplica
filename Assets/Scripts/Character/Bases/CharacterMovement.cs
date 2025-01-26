using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isMovementActive;

    private void Start()
    {
        isMovementActive = true;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isMovementActive) return;

        Vector3 newPos = transform.position + transform.forward;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        GetComponent<CharacterAnimator>().SetBool(AnimationKey.IsWalking,true);
    }

    public void SetMovementActivation(bool isActive)
    {
        isMovementActive = isActive;
    }
}