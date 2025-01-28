using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JellyObstacle : MonoBehaviour,IPlayerInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractPlayer(Player player)
    {
        player.gameObject.SetActive(false);
        transform.DOShakePosition(0.2f, 2f);
    }
}
