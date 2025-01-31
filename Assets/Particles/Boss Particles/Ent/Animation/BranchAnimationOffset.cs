using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BranchAnimationOffset : MonoBehaviour
{
    public float minTime, maxTime, minValue, maxValue,x,y,z;
    // Start is called before the first frame update
    void Start()
    { 
        transform.localScale = Vector3.zero;
        var RandomScaleTime = UnityEngine.Random.Range(minTime, maxTime);
        var RandomScaleValue = UnityEngine.Random.Range(minValue, maxValue);
        float waituntilclose = 1.5f;

        StartCoroutine(TrigAnimRandom());
        IEnumerator TrigAnimRandom()
        {
            GetComponent<Animator>().speed = 1f / RandomScaleTime;
            transform.DOScale(Vector3.one * RandomScaleValue, RandomScaleTime);
            transform.eulerAngles = new Vector3(UnityEngine.Random.Range(-x, x), UnityEngine.Random.Range(-y, y), UnityEngine.Random.Range(-z, z));

            yield return new WaitForSeconds(RandomScaleTime - 0.15f);

            GetComponent<Animator>().SetTrigger("Open");

            yield return new WaitForSeconds(waituntilclose);

            GetComponent<Animator>().speed = 1f;
            GetComponent<Animator>().SetTrigger("Close");
            transform.DOScale(Vector3.zero, 1f);

        } 
    } 
}
