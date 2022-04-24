using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class muzikAyar : MonoBehaviour
{
    private AudioSource AudioSource;

    public Slider volumeSlider;

    public GameObject objectMusic;
    private float musicVolume = 0f;
    
   
    private void Start()
    {

        objectMusic = GameObject.FindWithTag("music");
        AudioSource = objectMusic.GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = musicVolume;
        volumeSlider.value= musicVolume;
    }

   
    void Update()
    {
        AudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
    }
}
