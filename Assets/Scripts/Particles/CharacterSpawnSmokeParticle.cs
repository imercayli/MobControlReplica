using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CharacterSpawnSmokeParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            yield return new WaitForSeconds(1f);
            LeanPool.Despawn(this);
        }
    }
}
