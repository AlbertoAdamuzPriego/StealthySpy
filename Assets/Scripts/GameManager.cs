using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu; //Estado en menú principal
    public event Action OnSettingsMenu; //Estado en menú de ajustes 
    public event Action OnLevelMenu; //Estado en menú de niveles
    public event Action OnCreditsMenu; //Estado en menú de créditos


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
        //La aplicación comienza en el menu principal
        MainMenu();
    }

    //Activa el menú principal
    public void MainMenu()
    {
        //Comprueba que existe algo suscrito al evento OnMainMenu
        OnMainMenu?.Invoke();
        Debug.Log("MainMenu activated");
    }

    //Activa el menú de ajustes
    public void SettingsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnSettingsMenu
        OnSettingsMenu?.Invoke();
        Debug.Log("SenttingMenu activated");
    }

    //Activa el menú de niveles
    public void LevelMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
        Debug.Log("LevelMenu activated");
        OnLevelMenu?.Invoke();
        Debug.Log("LevelMenu activated");
    }

    //Activa el menú de créditos
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
