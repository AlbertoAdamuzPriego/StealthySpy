using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//Temporizador del mapa
public class Timeout:MonoBehaviour
{
    [SerializeField] private float time=0;
    private bool enable = false;
    [SerializeField] private TMP_Text text; //Texto donde se imprime el temporizador
    private string timeText; //Temporizador en formato de texto
    private float maxTime; //Tiempo máximo para finalizar el mapa
    private void Start()
    {
        GameplayManager.instance.OnGameplay += ReanudeCount;
        GameplayManager.instance.OnPause += PauseCount;
        GameplayManager.instance.OnFinished += PauseCount;
        GameplayManager.instance.OnCompleted += PauseCount;
    }

    //Inicializa y activa el temporizador
    public void StartCount(float MaxTime)
    {
        time = MaxTime;
        maxTime = MaxTime;
        enable = true;
    }


    private void Update()
    {
        //Actualiza el tiempo si está activo
        if(enable) 
        {
            time -= Time.deltaTime;

            //Si el tiempo llega a cero se da por finalizada la partida
            if(time <= 0 )
            {
                enable = false;
                GameplayManager.instance.GameOver();
            }
        }

        //Imprime el temporizador en la pantalla

        timeText = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

        text.text = timeText;


    }

    //Pausa el temporizador
    public void PauseCount() 
    { 
        enable = false;
    }

    //Reanuda el temporizador
    public void ReanudeCount()
    {
        enable = true;
    }

    //Devuelve el tiempo empleado
    public float GetTime()
    {
        return maxTime-time;
    }

    //Comprueba si el tiempo ha expirado
    public bool TimeHasExpired()
    {
        if (time <= 0)
            return true;

        return false;
    }

}
