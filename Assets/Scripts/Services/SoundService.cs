using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

public class SoundService : BaseService<SoundService>
{
    private List<SoundData> soundDatas;
    [SerializeField] private AudioSource audioSourcePrefab;

    private Dictionary<string, float> intervalTimes =new Dictionary<string, float>();
    
    public override void Initialize()
    {
        base.Initialize();
        soundDatas = new List<SoundData>(Resources.LoadAll<SoundData>("SoundDatas"));
        foreach (SoundData soundData in soundDatas)
        {
            intervalTimes.Add(soundData.Key,0);
        }
    }

    public void PlaySound(string key)
    {
        if (!SaveData.IsSoundOn) return;
        
        if(Time.time<intervalTimes[key]) return;
        
        SoundData soundData = soundDatas.Find(o => o.Key == key);
        AudioSource audioSource = LeanPool.Spawn(audioSourcePrefab);
        
        audioSource.volume = soundData.Volume;
        audioSource.pitch = soundData.Pitch;
        audioSource.loop = soundData.IsLoop;
        audioSource.clip = soundData.AudioClip;
        audioSource.Play();

        intervalTimes[key] = Time.time + soundData.Interval;

        if (!soundData.IsLoop)
        {
            LeanPool.Despawn(audioSource,soundData.AudioClip.length);
        }

    }
}
