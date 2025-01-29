using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanonPlayerSpawner : MonoBehaviour
{
    private InputService inputService;
    private GameService gameService;
    private bool isSpawningActive;
    [SerializeField] private float playerSpawnRate;
    private float playerSpawnTimer;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    public float animationDuration = 0.15f;
    public float overlapDelay = 0.05f;

    void Start()
    {
        inputService = ServiceSystem.GetService<InputService>();
        SetSpawningActivation(true);
        gameService = ServiceSystem.GetService<GameService>();
        gameService.OnGameOver += (isSuccess) => { SetSpawningActivation(false); };
    }

    void Update()
    {
        PlayerSpawnRoutine();
    }

    private void PlayerSpawnRoutine()
    {
        if (!isSpawningActive || !inputService.IsPressed) return;

        if (Time.time < playerSpawnTimer) return;

        AnimateBlendShapesWithOverlap();
        playerSpawnTimer = Time.time + playerSpawnRate;
    }

    void AnimateBlendShapesWithOverlap()
    {
        Sequence sequence = DOTween.Sequence();
        int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            int index = i; 

            sequence.Append(DOTween.To(
                () => skinnedMeshRenderer.GetBlendShapeWeight(index),
                weight => skinnedMeshRenderer.SetBlendShapeWeight(index, weight),
                100f,
                animationDuration
            ).SetEase(Ease.Linear));

            sequence.AppendInterval(overlapDelay);

            sequence.Append(DOTween.To(
                () => skinnedMeshRenderer.GetBlendShapeWeight(index),
                weight => skinnedMeshRenderer.SetBlendShapeWeight(index, weight),
                0f,
                animationDuration
            ).SetEase(Ease.Linear));
        }

        sequence.AppendCallback(SpawnPlayer);
    }

    private void SpawnPlayer()
    {
        Player player = ServiceSystem.GetService<PlayerFactory>()
            .CreateInstance(playerSpawnPoint.position, playerSpawnPoint.rotation);
        player.CharacterMovement.SetTargetForward(EnvironmentManager.Instance.EnemyFortress.transform.position,true);
        ServiceSystem.GetService<CharacterSpawnSmokeParticleFactory>()
            .CreateInstance(playerSpawnPoint.position + Vector3.up * 1, playerSpawnPoint.rotation);
    }

    public void SetSpawningActivation(bool isActive)
    {
        isSpawningActive = isActive;
    }

    private void OnDestroy()
    {
        gameService.OnGameOver -= (isSuccess) => { SetSpawningActivation(false); };
    }
}