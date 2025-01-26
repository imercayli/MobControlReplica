using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubePortal : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float teleportTime = 2;
    
    public void Interact(Player player)
    {
        TeleportPlayer(player);
    }
    
    private void TeleportPlayer(Player player)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            player.gameObject.SetActive(false);
            yield return new WaitForSeconds(teleportTime);
            player.gameObject.SetActive(true);
            player.transform.position = exitPoint.position;
            player.transform.rotation = exitPoint.rotation;
        }
    }
}
