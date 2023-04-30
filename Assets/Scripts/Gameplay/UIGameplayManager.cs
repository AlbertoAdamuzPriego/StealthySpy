using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject pauseCanvas;


    private void Start()
    {
        GameplayManager.instance.OnGameplay += ActivateGameplayCanvas;
        GameplayManager.instance.OnPause += ActivatePauseCanvas;
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
    }
}
