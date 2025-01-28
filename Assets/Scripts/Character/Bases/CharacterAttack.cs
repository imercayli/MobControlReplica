using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    protected CharacterBase characterBase;
    [SerializeField] private float damageAmount;

    public float DamageAmount => damageAmount;
    
    public void Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
    }
}
