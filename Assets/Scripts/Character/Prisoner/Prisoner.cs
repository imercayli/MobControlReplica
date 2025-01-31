using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<CharacterAnimator>().SetTrigger(AnimationKey.AskHelp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
