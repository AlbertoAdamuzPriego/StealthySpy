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
    public event Action OnScoreMenu;


    public static GameManager instance; //Instancia del script (Patron Singleton)

    public int finishMode;

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
        LoadData();


    }

    //Activa el menú principal
    public void MainMenu()
    {
        //Comprueba que existe algo suscrito al evento OnMainMenu
        OnMainMenu?.Invoke();
        PlayerPrefs.SetInt("finish", -1);

    }

    //Activa el menú de ajustes
    public void SettingsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnSettingsMenu
        OnSettingsMenu?.Invoke();
      
    }

    //Activa el menú de niveles
    public void LevelMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
       
        OnLevelMenu?.Invoke();
        PlayerPrefs.SetInt("finish", -1);

    }

    //Activa el menú de créditos
    public void CreditsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
        OnCreditsMenu?.Invoke();

    }

    public void ScoreMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
        OnScoreMenu?.Invoke();
        
    }

    //Cierra el juego
    public void CloseGame()
    {
        PlayerPrefs.SetInt("finish",-1);
        Application.Quit();
    }

    private void LoadData()
    {
        finishMode = PlayerPrefs.GetInt("finish");
        
        if(finishMode==0 )
        {
            
        }

        else if(finishMode==1 ) 
        {
            
        }

        else if(finishMode==2 )
        {
            LevelMenu(); 
        }

        else
        {
            MainMenu();
        }
    }

    private void Update()
    {


    }
}
