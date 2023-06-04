using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Canvas de cada menú
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject mapMenuCanvas;
    [SerializeField] private GameObject scoreMenuCanvas;
    [SerializeField] private GameObject creditsMenuCanvas;
    [SerializeField] private GameObject loadingCanvas;

    void Start()
    {
        //Suscribimos cada función a su evento correspondiente
        GameManager.instance.OnMainMenu += activateMainMenu;

        GameManager.instance.OnSettingsMenu += activateSettingsMenu;

        GameManager.instance.OnLevelMenu += activateMapMenu;

        GameManager.instance.OnScoreMenu += activateScoreMenu;

        GameManager.instance.OnCreditsMenu += activateCreditsMenu;
    }

    //Activa el menú principal
    private void activateMainMenu()
    {
        //Escala los objetos a una determinada escala en una cantidad de segundos (0.3)
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.3f);

        //Mueve el objeto en el eje Y
        settingsMenuCanvas.transform.DOMoveY(2000, 0.3f);

        mapMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mapMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mapMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mapMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mapMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 1), 0.3f);
        mapMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 1), 0.3f);

        scoreMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0,0), 0f);
        scoreMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0f);
        scoreMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0f);

        creditsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


    }

    //Activa el menú de configuración
    private void activateSettingsMenu()
    {
        
        settingsMenuCanvas.transform.DOMoveY(510, 0.3f);
       
    }

    //Activa el menú de selector de mapa
    private void activateMapMenu()
    {
            mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


            mapMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            mapMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
            mapMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            mapMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(3, 3, 1), 0.3f);
            mapMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(-1, 1, 1), 0f);
            mapMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(1, 1, 1), 0f);
     
    }

    //Activa el menú de puntuaciones
    private void activateScoreMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        scoreMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        scoreMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        scoreMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        scoreMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0.9f, 0.9f, 1), 0.3f);
        scoreMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
        scoreMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(-1f, 1f, 0), 0.3f);
        scoreMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(1f, 1f, 0), 0.3f);
        scoreMenuCanvas.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.transform.DOMoveX(0, 0f);

    }

    //Activa el menú de créditos
    private void activateCreditsMenu()
    {
      
        creditsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
        creditsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 0), 0.3f);
    }

    //Activa la pantalla de carga
    public void ActivateLoadingCanvas()
    {
        loadingCanvas.SetActive(true);
    }
}
