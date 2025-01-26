using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanonMovement : MonoBehaviour
{
    [SerializeField] private Transform canonObject;
    [SerializeField] private float xMovementSpeed;
    [SerializeField] private float xSideLimit;

    [SerializeField] private float playerSpawnRate;

    private float playerSpawnTimer;

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
  public  float animationDuration = 0.15f;  // Time to reach 100
  public float overlapDelay = 0.05f;       // Overlap delay between blendshapes
    // Start is called before the first frame update
    void Start()
    {
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
       // AnimateBlendShapesWithOverlap();
    }

    private void OnTouchMoved(TouchInput touch)
    {
        var movementDelta =  touch.DeltaScreenPosition.x * xMovementSpeed * Vector3.right *Time.deltaTime;

        var finalPosition = canonObject.localPosition + movementDelta;

        finalPosition.x = Mathf.Clamp(finalPosition.x, -xSideLimit, xSideLimit);

        canonObject.localPosition = finalPosition;

        if (Time.time > playerSpawnTimer)
        {
            AnimateBlendShapesWithOverlap();
            playerSpawnTimer = Time.time + playerSpawnRate;
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            AnimateBlendShapesWithOverlap();
    }

    private void SpawnPlayer()
    {
        //AnimateBlendShapesWithOverlap();
        CharacterMovement characterMovement = CurrencyFlowIconPool.Instance.GetPlayer();
        characterMovement.transform.position = playerSpawnPoint.position;
        characterMovement.transform.rotation = playerSpawnPoint.rotation;
    }
    
    void AnimateBlendShapesWithOverlap()
    {
        Sequence sequence = DOTween.Sequence();
        int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            int index = i;  // Capture index to avoid closure issues

            // Animate the blendshape to 100
            sequence.Append(DOTween.To(
                () => skinnedMeshRenderer.GetBlendShapeWeight(index),
                weight => skinnedMeshRenderer.SetBlendShapeWeight(index, weight),
                100f,
                animationDuration
            ).SetEase(Ease.Linear));

            // Start next blendshape animation with a slight overlap
            sequence.AppendInterval(overlapDelay);

            // Animate the blendshape back to 0
            sequence.Append(DOTween.To(
                () => skinnedMeshRenderer.GetBlendShapeWeight(index),
                weight => skinnedMeshRenderer.SetBlendShapeWeight(index, weight),
                0f,
                animationDuration
            ).SetEase(Ease.Linear));
        }

        sequence.AppendCallback(SpawnPlayer);
        //  sequence.SetLoops(-1, LoopType.Restart); // Infinite looping
    }
}
