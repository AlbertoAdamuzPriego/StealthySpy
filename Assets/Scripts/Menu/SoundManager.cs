using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Se encarga de controlar el volumen de la música y los efectos de sonido, así como reproducir la música y ciertos efectos.
public class SoundManager : MonoBehaviour
{
    //Volumen [0-1]
    private float musicVolume = 1;
    private float sfxVolume = 1;

    //Botones de silenciar
    [SerializeField] Image musicButton;
    [SerializeField] Image sfxButton;

    //Barras de deslizamiento
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    //Iconos de silenciar
    [SerializeField] Sprite volumeIcon;
    [SerializeField] Sprite muteIcon;

    private AudioSource audioSource; //Componente encargado del sonido
    [SerializeField] AudioClip alarmClip; //Efecto de alarma

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos los valores predeterminados
        musicVolume = PlayerPrefs.GetFloat("music", 1);
        sfxVolume = PlayerPrefs.GetFloat("sfx", 1);

        musicSlider.value= musicVolume;
        sfxSlider.value= sfxVolume;

        musicSlider.onValueChanged.AddListener((value)=>SetMusicVolume(value));
        sfxSlider.onValueChanged.AddListener((value) => SetSFXVolume(value));

        //En caso de estar en un mapa suscribimos las funciones a los eventos
        if(FindAnyObjectByType<GameplayManager>()!=null)
        {
            GameplayManager.instance.OnGameplay += UpdateVolume;
            GameplayManager.instance.OnGameOver += Alarm;
        }
        
        //Activamos la música
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();

        UpdateVolume();
        

    }

    //Establece el volumen de la musica
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

    //Establece el volumen de los efectos de sonido
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

    //Silencia la música
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

    //Silencia los efectos de sonido
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

    //Actualiza todos los componentes que reproducen sonidos
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

    //Reproduce una alarma
    private void Alarm()
    {
        audioSource.clip=alarmClip;
        audioSource.loop = false;
        audioSource.volume = sfxVolume;
        audioSource.Play();
    }

}
