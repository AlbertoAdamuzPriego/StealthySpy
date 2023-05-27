using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using JetBrains.Annotations;

//Es la clase principal que se encarga de gestionar la aplicaci�n
public class GameManager : MonoBehaviour
{
    /*
    * Estos eventos llaman a una serie de funciones suscritas a ellos cuando son invocados 
    * permitiendo la comunicaci�n y el control instant�neo con otros scripts
    */
    public event Action OnMainMenu; //Estado en men� principal
    public event Action OnSettingsMenu; //Estado en men� de ajustes 
    public event Action OnLevelMenu; //Estado en men� de niveles
    public event Action OnCreditsMenu; //Estado en men� de cr�ditos
    public event Action OnScoreMenu; //Estado en men� de puntuaciones
    public event Action ChangeDifficulty; //Evento al cambiar la dificultad

    public static GameManager instance; //Instancia del script (Patr�n Singleton)

    public int finishMode; //Variable que sirve para identificar c�mo ha finalizado una partida

    [SerializeField] private TMP_Text difficultyLabel; // Texto del selector de dificultad

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
        LoadData();
    }

    //Activa el men� principal
    public void MainMenu()
    {
        //Ejecuta las funciones suscritas al evento OnMainMenu
        OnMainMenu?.Invoke();

    }

    //Activa el men� de ajustes
    public void SettingsMenu()
    {
        //Ejecuta las funciones suscritas al evento OnSettingsMenu
        OnSettingsMenu?.Invoke();
      
    }

    //Activa el men� de niveles
    public void LevelMenu()
    {
        //Ejecuta las funciones suscritas al evento OnLevelMenu
       
        OnLevelMenu?.Invoke();

        //PlayerPrefs.SetInt("finish", -1);

    }

    //Activa el men� de cr�ditos
    public void CreditsMenu()
    {
        //Ejecuta las funciones suscritas al evento OnLevelMenu
        OnCreditsMenu?.Invoke();

    }

    //Activa el men� de puntuaciones
    public void ScoreMenu()
    {
        //Ejecuta las funciones suscritas al evento OnScoreMenu
        OnScoreMenu?.Invoke();
        
    }

    //Cierra el juego
    public void CloseGame()
    {
        PlayerPrefs.DeleteKey("finish");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    //Activa el evento de cambio de dificultad
    public void Difficulty()
    {
        ChangeDifficulty?.Invoke();
    }

    //Carga los datos necesarios para la aplicaci�n
    private void LoadData()
    {
        //Carga la dificultad
        int difficulty = PlayerPrefs.GetInt("difficulty");
        switch (difficulty)
        {
            case 0: difficultyLabel.SetText("F�cil"); break;

            case 1: difficultyLabel.SetText("Normal"); break;

            case 2: difficultyLabel.SetText("Dificil"); break;
        }

        //Carga el modo de finalizaci�n para la partida (-1 = no hay partida)
        finishMode = PlayerPrefs.GetInt("finish",-1);
        
        //EL jugador super� la partida
        if(finishMode==0 )
        {
            //Se actualiza primero las puntuaciones para evitar bugs
            FindAnyObjectByType<MapManager>().RecalculateScores();
            LevelMenu();
        }

        //El jugador perdi� la partida o la abandon�
        else
        {

            MainMenu();
        }

        //Se actualiza a -1 de nuevo
        PlayerPrefs.SetInt("finish", -1);
    }

}
