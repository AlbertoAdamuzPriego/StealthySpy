using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject levelMenuCanvas;
    
    void Start()
    {
        //Suscribimos la funcion activateMainMenu al evento OnMainMenu
        GameManager.instance.OnMainMenu += activateMainMenu;

        //Suscribimos la funcion activateSettingsMenu al evento OnSettingsMenu
        GameManager.instance.OnSettingsMenu += activateSettingsMenu;

        //Suscribimos la funcion activateLevelMenu al evento OnLevelMenu
        GameManager.instance.OnLevelMenu += activateLevelMenu;
    }

    private void activateMainMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(2, 2, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(2, 2, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(2, 2, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(2, 2, 1), 0.3f);

        settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        levelMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        levelMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);



    }

    private void activateSettingsMenu()
    {
        settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(5, 5, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(5, 5, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        
    }

    private void activateLevelMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        settingsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        settingsMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


        levelMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        levelMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(2, 2, 1), 0.3f);
        levelMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

    }
}
