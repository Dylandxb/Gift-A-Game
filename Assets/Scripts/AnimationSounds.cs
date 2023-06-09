using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    AudioSource animSoundPlay;
    Animator anim;
    void Start()
    {
        animSoundPlay = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void Step()
    {
        if(anim.GetFloat("Speed") >0.1 && anim.GetFloat("Speed") <= 0.5)                        //Prevents overlapping of footstep sound when transitioning from Walk to Sprint
        {
            animSoundPlay.PlayOneShot(animSoundPlay.clip, volumeScale: 0.5f) ;
        }
        
    }

    public void StepRun()
    {
        if(anim.GetFloat("Speed") > 0.5 && anim.GetFloat("Speed") <= 1)
        {
            animSoundPlay.PlayOneShot(animSoundPlay.clip, volumeScale: 1f);                     //Increase footstep sound when sprinting
        }
    }
}
