using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu; //Estado en men� principal
    public event Action OnSettingsMenu; //Estado en men� de ajustes 
    public event Action OnLevelMenu; //Estado en men� de niveles
    public event Action OnCreditsMenu; //Estado en men� de cr�ditos
    public event Action OnScoreMenu;
    public event Action ChangeDifficulty;

    public static GameManager instance; //Instancia del script (Patron Singleton)

    public int finishMode;

    [SerializeField] private TMP_Text difficultyLabel;

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
        LoadData();


    }

    //Activa el men� principal
    public void MainMenu()
    {
        //Comprueba que existe algo suscrito al evento OnMainMenu
        OnMainMenu?.Invoke();

    }

    //Activa el men� de ajustes
    public void SettingsMenu()
    {
        //Comprueba que existe algo suscrito al evento OnSettingsMenu
        OnSettingsMenu?.Invoke();
      
    }

    //Activa el men� de niveles
    public void LevelMenu()
    {
        //Comprueba que existe algo suscrito al evento OnLevelMenu
       
        OnLevelMenu?.Invoke();

        //PlayerPrefs.SetInt("finish", -1);

    }

    //Activa el men� de cr�ditos
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
        PlayerPrefs.DeleteKey("finish");
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void Difficulty()
    {
        ChangeDifficulty?.Invoke();
    }

    private void LoadData()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty");
        switch (difficulty)
        {
            case 0: difficultyLabel.SetText("F�cil"); break;

            case 1: difficultyLabel.SetText("Normal"); break;

            case 2: difficultyLabel.SetText("Dificil"); break;
        }

        finishMode = PlayerPrefs.GetInt("finish",-1);
        
        if(finishMode==0 )
        {
            FindAnyObjectByType<MapManager>().RecalculateScores();
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

        PlayerPrefs.SetInt("finish", -1);
    }

}
