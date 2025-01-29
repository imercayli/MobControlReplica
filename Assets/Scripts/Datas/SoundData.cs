using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundData", menuName = "ScriptableObjects/SoundData")]
public class SoundData : ScriptableObject
{
   [SerializeField] private string key;
   [SerializeField] private AudioClip audioClip;
   [SerializeField] private float interval;
   [SerializeField] private bool isLoop;
   [SerializeField] private bool isPlayOnAwake;
   [Range(0, 1)] [SerializeField] private float volume = 1;
   [Range(0, 3)] [SerializeField] private float pitch = 1;

   public string Key => key;
   public AudioClip AudioClip => audioClip;
   public float Interval => interval;
   public bool IsLoop => isLoop;
   public bool IsPlayOnAwake => isPlayOnAwake;
   public float Volume => volume;
   public float Pitch => pitch;
}
