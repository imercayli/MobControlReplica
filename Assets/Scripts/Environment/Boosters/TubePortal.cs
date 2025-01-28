using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class TubePortal : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform enterPointMesh;
    [SerializeField] private float teleportTime = 2;
    
    public void InteractPlayer(Player player)
    {
        TeleportPlayer(player);
    }
    
    private void TeleportPlayer(Player player)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            //TODO
            float duration = 0.5f;
            player.CharacterMovement.SetMovementActivation(false);
            player.transform.DOMove(enterPointMesh.position, duration);
            player.transform.DOScale(Vector3.one*0.5f,duration).OnComplete((() =>
            {
                player.gameObject.SetActive(false);
            }));
            yield return new WaitForSeconds(teleportTime);
            player.gameObject.SetActive(true);
            player.transform.position = exitPoint.position;
            player.transform.rotation = exitPoint.rotation;
            player.transform.localScale =Vector3.one;
            player.CharacterMovement.SetMovementActivation(true);
            
            player.GetComponent<NavMeshAgent>().SetDestination(EnvironmentManager.Instance.EnemyFortress.transform.position);
        }
    }
}
