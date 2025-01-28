using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CharacterSpawnSmokeParticle : MonoBehaviour
{
    private void OnEnable()
    {
      //  LeanPool.Despawn(this,2f);

        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}
