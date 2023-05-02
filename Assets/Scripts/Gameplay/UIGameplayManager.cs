using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;

    private void Start()
    {
        GameplayManager.instance.OnGameplay += ActivateGameplayCanvas;
        GameplayManager.instance.OnPause += ActivatePauseCanvas;
        GameplayManager.instance.OnGameOver += ActivateGameOverCanvas;
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

    private void ActivateGameOverCanvas()
    {
        gameplayCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 1f);

        StartCoroutine(Pausar());
        gameOverCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 2f);
        gameOverCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 2f);
    }

    IEnumerator Pausar()
    {
        yield return new WaitForSecondsRealtime(3f);
    }
}