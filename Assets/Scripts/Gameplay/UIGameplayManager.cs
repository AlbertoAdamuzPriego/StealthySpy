using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

//Controla la interfaz dentro de una partida
public class UIGameplayManager : MonoBehaviour
{
    //Canvas de cada men�
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas; //Se activa cuando el usuario pierde la partida
    [SerializeField] GameObject finishCanvas; //Se activa cuando el usuario gana la partida

    private void Start()
    {
        GameplayManager.instance.OnGameplay += ActivateGameplayCanvas;
        GameplayManager.instance.OnPause += ActivatePauseCanvas;
        GameplayManager.instance.OnGameOver += ActivateGameOverCanvas;
        GameplayManager.instance.OnCompleted += ActivateFinishCanvas;
    }

    //Activa la interfaz de juego
    private void ActivateGameplayCanvas()
    {
        gameplayCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
    
    //Activa el men� de pausa
    private void ActivatePauseCanvas()
    {
        gameplayCanvas.SetActive(false);
        pauseCanvas.SetActive(true);

        //Se lee el mejor tiempo registrado del mapa en la dificultad actual
        float bestTime = PlayerPrefs.GetFloat("Score"+GameplayManager.instance.GetDifficulty() + PlayerPrefs.GetString("lastMap")+ "0");
        
        //Si es v�lido
        if(bestTime > 0) 
        {
            //Se muestra el texto
            pauseCanvas.transform.GetChild(5).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss") + "\n";
        }

        //Se muestra la puntuaci�n actual
        pauseCanvas.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(FindAnyObjectByType<Timeout>().GetTime()).ToString(@"mm\:ss") + "\n";
    }

    //Activa el men� de partida perdida
    private void ActivateGameOverCanvas()
    {
        gameplayCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        //Comprueba por qu� ha finalizado la partida y se imprime el mensaje correspondiente
        if (FindAnyObjectByType<Timeout>().TimeHasExpired()) 
        {
            gameOverCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = "SE HA AGOTADO EL TIEMPO";
        }

        else
        {
            gameOverCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = "HAS SIDO DETECTADO";
        }

        
        gameOverCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 1f);

        //Se da algo de delay para que aparezcan los botones
        StartCoroutine(Pausar());
        gameOverCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 2f);
        gameOverCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 2f);
    }

    //Activa el men� de partida completada
    private void ActivateFinishCanvas()
    {
      
        gameplayCanvas.SetActive(false);
        finishCanvas.SetActive(true);

        //Se lee el mejor tiempo registrado del mapa en la dificultad actual
        float bestTime = PlayerPrefs.GetFloat("Score0" + PlayerPrefs.GetString("lastMap") + "0");

        //Si es v�lido se imprime
        if (bestTime > 0)
        {
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss") + "\n";
        }

        //Se lee la puntuaci�n de la partida y se imprime
        float newTime = FindAnyObjectByType<Timeout>().GetTime();

        finishCanvas.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(newTime).ToString(@"mm\:ss") + "\n";

        //Si la mejor puntuaci�n no es valida o es peor que la nueva, la nueva puntuaci�n sustituye a la antigua y se resalta en amarillo
        if(bestTime == 0 || bestTime>newTime)
        {
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(newTime).ToString(@"mm\:ss") + "\n";
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().color = Color.yellow;
 
        }
    }

    //Funci�n que espera 3 segundos
    IEnumerator Pausar()
    {
        yield return new WaitForSecondsRealtime(3f);
    }

    //Activa el panel que oscurece la pantalla para dar el efecto de estar escondido 
    public void ActivatePanelSafePoint(bool enable)
    {
        gameplayCanvas.transform.GetChild(0).gameObject.SetActive(enable);
    }
}