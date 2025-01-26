using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float scaleAmount, time;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(scaleAmount, time).SetLoops(-1, LoopType.Yoyo);
    }
}
