using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private float musicVolume = 1;
    private float sfxVolume = 1;

    [SerializeField] Image musicButton;
    [SerializeField] Image sfxButton;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] Sprite volumeIcon;
    [SerializeField] Sprite muteIcon;


    // Start is called before the first frame update
    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("music", 1);
        sfxVolume = PlayerPrefs.GetFloat("sfx", 1);

        musicSlider.value= musicVolume;
        sfxSlider.value= sfxVolume;

        musicSlider.onValueChanged.AddListener((value)=>SetMusicVolume(value));
        sfxSlider.onValueChanged.AddListener((value) => SetSFXVolume(value));

        if(FindAnyObjectByType<GameplayManager>()!=null)
            GameplayManager.instance.OnGameplay += UpdateVolume;


        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();

        

    }

    public void SetMusicVolume(float value)
    {
        if(value<=0)
        {
            musicButton.sprite = muteIcon;
        }

        else
        {
            musicButton.sprite = volumeIcon;
        }

        musicSlider.value = value;
        musicVolume = value;
        PlayerPrefs.SetFloat("music", value);
        UpdateVolume();
    }


    public void SetSFXVolume(float value)
    {
        if (value <= 0)
        {
            sfxButton.sprite = muteIcon;
        }

        else
        {
            sfxButton.sprite = volumeIcon;
        }

        sfxSlider.value = value;
        sfxVolume= value;
        PlayerPrefs.SetFloat("sfx", value);
        UpdateVolume();
    }

    public void MuteMusic()
    {
        if(musicVolume>0)
        {
            SetMusicVolume(0);
        }

        else
        {
            SetMusicVolume(0.1f);
        }

        UpdateVolume();
    }

    public void MuteSFX()
    {
        if (sfxVolume > 0)
        {
            SetSFXVolume(0);
        }

        else
        {
            SetSFXVolume(0.1f);
        }

        UpdateVolume();
    }

    private void UpdateVolume()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Itera a través de los objetos encontrados
        foreach (AudioSource audioSource in audioSources)
        {
           if(audioSource.gameObject.tag=="music")
            {
                audioSource.volume = musicVolume;
            }

           else
            {
                audioSource.volume = sfxVolume;
            }
        }
    }

}
