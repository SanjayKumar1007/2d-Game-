using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiomanager : MonoBehaviour
{
    public Sound[] musicSound;
    public SFX[] sfxSound;
    public SFX2[] sfxSound2;
    public AudioSource musicSource, sfxSource, sfxSource2;
    public static Audiomanager instance;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        volumecontrol();
    }

    public void playmusic(string name)
    {
        Sound s = Array.Find(musicSound, s =>  s.name == name);
       
        if(s == null)
        {
            print("not founded");
        }
        else
        {
          
            musicSource.clip = s.audioclip;
            
            musicSource.Play();
        }


    }
    public void sfxmusic(string name)
    {
        SFX sfx = Array.Find(sfxSound, sfx => sfx.sfxName == name);

        if (sfx == null)
        {
            print("not founded");
        }
        else
        {

            sfxSource.clip = sfx.sfxClip;

            sfxSource.Play();
        }


    }

    public void sfxmusic2(string name)
    {
        SFX2 sfx2 = Array.Find(sfxSound2, sfx2 => sfx2.sfxName == name);

        if (sfx2 == null)
        {
            print("not founded");
        }
        else
        {

            sfxSource2.clip = sfx2.sfxClip;

            sfxSource2.Play();
        }


    }


    public void volumecontrol()
    {
        musicSource.volume = musicSlider.value;
        sfxSource.volume  = sfxSlider.value;
        sfxSource2.volume = sfxSlider.value;

    }
}
