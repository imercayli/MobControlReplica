using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startView;
    [SerializeField] private CinemachineVirtualCamera mainView;
    public float switchDelay = 2f;

    private void Start()
    {
        SwitchToMainCamera();
    }

    private void SwitchToMainCamera()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            yield return new WaitForSeconds(switchDelay);
            startView.Priority = 5;
            mainView.Priority = 10;
        }
    }
   
}
