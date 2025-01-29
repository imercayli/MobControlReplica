using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonLevelController : MonoBehaviour
{
    [SerializeField] private List<CanonMovement> levels;
    private int levelIndex;
    [SerializeField] private AnimationCurve canonObjMoveAnimationCurve;
    [SerializeField] private float yMovementOffset;
    
    public CanonMovement CurrentCanon { get; private set;}

    private void Start()
    {
        SetLevel();
        canonObjMoveAnimationCurve.keys[1].value = yMovementOffset;
    }

    private void SetLevel()
    {
        levels.ForEach(o=>o.gameObject.SetActive(false));
        CurrentCanon =levels[levelIndex];
        CurrentCanon.gameObject.SetActive(true);
    }

    public void UpgradeCanon(GameObject canonUpgradeObject)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            canonUpgradeObject.transform.SetParent(null);
            float animTime = 1f;
            float timer =0;
            Vector3 startPos = canonUpgradeObject.transform.position;
            while (timer<animTime)
            {
                float t = timer / animTime;
                canonUpgradeObject.transform.position = Vector3.Lerp(startPos,
                    CurrentCanon.CanonObject.transform.position + Vector3.up * canonObjMoveAnimationCurve.Evaluate(t),t );
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(canonUpgradeObject);
            levelIndex++;
            SetLevel();
        }
    }
}
