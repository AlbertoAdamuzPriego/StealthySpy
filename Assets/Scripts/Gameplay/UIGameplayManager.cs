using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class UIGameplayManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject finishCanvas;

    private void Start()
    {
        GameplayManager.instance.OnGameplay += ActivateGameplayCanvas;
        GameplayManager.instance.OnPause += ActivatePauseCanvas;
        GameplayManager.instance.OnGameOver += ActivateGameOverCanvas;
        GameplayManager.instance.OnCompleted += ActivateFinishCanvas;
    }

    private void ActivateGameplayCanvas()
    {
        gameplayCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    private void ActivatePauseCanvas()
    {
        gameplayCanvas.SetActive(false);
        pauseCanvas.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("Score"+GameplayManager.instance.GetDifficulty() + PlayerPrefs.GetString("lastMap")+ "0");
   
        if(bestTime > 0) 
        {
            pauseCanvas.transform.GetChild(5).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss") + "\n";
        }

        pauseCanvas.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(FindAnyObjectByType<Timeout>().GetTime()).ToString(@"mm\:ss") + "\n";
    }

    private void ActivateGameOverCanvas()
    {
        gameplayCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        
        if (FindAnyObjectByType<Timeout>().TimeHasExpired()) 
        {
            gameOverCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = "SE HA AGOTADO EL TIEMPO";
        }

        else
        {
            gameOverCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = "HAS SIDO DETECTADO";
        }

        
        gameOverCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 1f);

        StartCoroutine(Pausar());
        gameOverCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 2f);
        gameOverCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 2f);
    }

    private void ActivateFinishCanvas()
    {
        Debug.Log("CANVAS");
        gameplayCanvas.SetActive(false);
        finishCanvas.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("Score0" + PlayerPrefs.GetString("lastMap") + "0");


        if (bestTime > 0)
        {
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss") + "\n";
        }

        float newTime = FindAnyObjectByType<Timeout>().GetTime();

        finishCanvas.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(newTime).ToString(@"mm\:ss") + "\n";

        if(bestTime == 0 || bestTime>newTime)
        {
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = TimeSpan.FromSeconds(newTime).ToString(@"mm\:ss") + "\n";
            finishCanvas.transform.GetChild(4).transform.GetChild(0).GetComponentInChildren<TMP_Text>().color = Color.yellow;
 
        }
    }


    IEnumerator Pausar()
    {
        yield return new WaitForSecondsRealtime(3f);
    }

    public void ActivatePanelSafePoint(bool enable)
    {
        gameplayCanvas.transform.GetChild(0).gameObject.SetActive(enable);
    }
}