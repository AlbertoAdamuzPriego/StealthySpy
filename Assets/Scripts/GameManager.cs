using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu; //Estado en men� principal
    public event Action OnSettingsMenu; //Estado en men� de ajustes 
    public event Action OnLevelMenu; //Estado en men� de niveles
    public event Action OnCreditsMenu; //Estado en men� de cr�ditos


    public static GameManager instance; //Instancia del script (Patron Singleton)

    //Controla que solo exista una instancia de GameManager
    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //La aplicaci�n comienza en el menu principal
        MainMenu();
    }

    //Activa el men� principal
    public void MainMenu()
    {
        //Comprueba que existe algo suscrito al evento OnMainMenu
        OnMainMenu?.Invoke();
        Debug.Log("MainMenu activated");
    }

    //Activa el men� de ajustes
    public void SettingsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnSettingsMenu
        OnSettingsMenu?.Invoke();
        Debug.Log("SenttingMenu activated");
    }

    //Activa el men� de niveles
    public void LevelMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
        Debug.Log("LevelMenu activated");
        OnLevelMenu?.Invoke();
        Debug.Log("LevelMenu activated");
    }

    //Activa el men� de cr�ditos
    public void CreditsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
        OnCreditsMenu?.Invoke();
        Debug.Log("CreditsMenu activated");
    }

    //Cierra el juego
    public void CloseGame()
    {
     
        Application.Quit();
    }
}
