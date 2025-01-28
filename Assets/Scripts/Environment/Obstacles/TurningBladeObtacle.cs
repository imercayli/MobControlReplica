using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningBladeObtacle : ObstacleBase
{
    [SerializeField] private float rotateSpeed;
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up,rotateSpeed*Time.deltaTime);
    }
}
