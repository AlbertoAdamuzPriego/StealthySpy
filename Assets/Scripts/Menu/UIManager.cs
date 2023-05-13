using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject levelMenuCanvas;
    [SerializeField] private GameObject scoreMenuCanvas;

    void Start()
    {
        //Suscribimos la funcion activateMainMenu al evento OnMainMenu
        GameManager.instance.OnMainMenu += activateMainMenu;

        //Suscribimos la funcion activateSettingsMenu al evento OnSettingsMenu
        GameManager.instance.OnSettingsMenu += activateSettingsMenu;

        //Suscribimos la funcion activateLevelMenu al evento OnLevelMenu
        GameManager.instance.OnLevelMenu += activateLevelMenu;

        GameManager.instance.OnScoreMenu += activateScoreMenu;
    }

    private void activateMainMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.3f);

        settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        levelMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 1), 0.3f);
        levelMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 1), 0.3f);

        scoreMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0,0), 0f);
        scoreMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0f);



    }

    private void activateSettingsMenu()
    {
        settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(5, 5, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(5, 5, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(10, 7, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(2, 2, 1), 0.3f);
        
    }
    

    private void activateLevelMenu()
    {
       if(GameManager.instance.finishMode == -1)
        {
            mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

            settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


            levelMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            levelMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
            levelMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            levelMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(5, 7, 1), 0.3f);
            levelMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(-1, 1, 1), 0f);
            levelMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(1, 1, 1), 0f);

        }
        
        else
        {
            levelMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0f);
            levelMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
            levelMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0f);
            levelMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(5, 7, 1), 0f);
            levelMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1, 1, 1), 0f);
            levelMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(1, 1, 1), 0f);


            mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0f);
            mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0f);

           // levelMenuCanvas.GetComponentInChildren<Scrollbar>().value = 0f;
           // levelMenuCanvas.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.GetComponent<RectTransform>().DOMoveX(320f, 0.01f);
        }
     
    }

    private void activateScoreMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        scoreMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0f);
        scoreMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0f);
        scoreMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0f);
        scoreMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0.9f, 0.9f, 1), 0f);
        scoreMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1f, 1f, 1), 0f);
        scoreMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(-1f, 1f, 0), 0f);
        scoreMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(1f, 1f, 0), 0f);

    }

}
