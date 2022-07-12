using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip coinCollectClip;
    public AudioClip damageSoundClip;
    public AudioClip jumpSoundClip;
    [HideInInspector]
    public AudioSource coinCollect, damageSound;
    public static AudioSource jumpSound;
    
    // Start is called before the first frame update
    void Start()
    {
        coinCollect = assignAudioSource(coinCollectClip);
        damageSound = assignAudioSource(damageSoundClip);
        jumpSound = assignAudioSource(jumpSoundClip);
    }


    AudioSource assignAudioSource(AudioClip clip)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.loop = false;
        audio.playOnAwake = false;

        return audio;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
