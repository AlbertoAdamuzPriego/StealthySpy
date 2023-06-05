using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

//Es la clase principal que dirige la aplicación durante una partida
public class GameplayManager : MonoBehaviour
{
    /*
    * Estos eventos llaman a una serie de funciones suscritas a ellos cuando son invocados 
    * permitiendo la comunicación y el control instantáneo con otros scripts
    */
    public event Action OnGameplay; //Estado en partida
    public event Action OnPause; //Estado en pausa
    public event Action OnFinished; //Estado finalizando partida
    public event Action OnGameOver; //Estado partida fallida
    public event Action OnCompleted; //Estado mapa completado

    [SerializeField] string map; //Nombre del mapa
    [SerializeField] private int difficulty; //Dificultad actual
    
    //Tiempo máximo para completar el mapa en cada dificultad
    [SerializeField] private float timeEasy; 
    [SerializeField] private float timeNormal;
    [SerializeField] private float timeDifficult;
    

    Timeout timer; //Temporizador
    private int finishMode; //Modo de finalización
    private string finishModePrefs="finish"; //Clave para leer la dificultad

    public static GameplayManager instance; //Instancia del script (Patron Singleton)
    private void Awake()
    {
      
        if (instance != null && instance != this)
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
        //Se lee la dificultad seleccionada y se inicializa el temporizador al tiempo correspondiente
        difficulty = PlayerPrefs.GetInt("difficulty");
        timer= FindAnyObjectByType<Timeout>();
        InitializeObstacles();

        switch(difficulty)
        {
            case 0: timer.StartCount(timeEasy); break;
            case 1: timer.StartCount(timeNormal); break;
            case 2: timer.StartCount(timeDifficult); break;

        }

        //Se recoge el nombre del mapa
        map = SceneManager.GetActiveScene().name;
        OnFinished += SaveData;
        Gameplay();

    }

    //Activa las funciones necesarias para jugar la partida
    public void Gameplay()
    {
 
       OnGameplay?.Invoke();
  
    }

    //Activa el menú de pausa
    public void Pause()
    {
        OnPause?.Invoke();
       
    }

    //Activa las funciones necesarias para finalizar la partida
    //Mode = 0 -> Partida completada con éxito -- Mode!=0 -> Partida fallida
    public void Finish(int mode)
    {

        finishMode = mode;
        OnFinished?.Invoke();
    
        //Vuelve al menú principal
        SceneManager.LoadScene("MainScene");
    }

    //Activa las funciones necesarias cuando una partida se completa
    public void Completed()
    {
        
        OnCompleted?.Invoke();


    }

    //Activa las funciones necesarias cuando el jugador falla la partida
    public void GameOver()
    {

        OnGameOver?.Invoke();
  
    }

    //Inicializa los obstaculos que varían según la dificultad seleccionada
    private void InitializeObstacles()
    {
        //Si la dificultad==2 por defecto aparecen todos
        if (difficulty < 2)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

            //Para cada obstáculo comprueba si la dificultad actual es igual o superior a su nivel. Si no lo desactiva
            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.GetComponent<Obstacle>().Dificulty() > difficulty)
                {
                    obstacle.SetActive(false);
                }
            }
        }
       
    }

    //Reinicia el mapa
    public void Restart()
    {
        SceneManager.LoadScene(map);
        
    }

    //Guarda cómo ha finalizado la partida y la puntuación obtenida, si es necesario.
    private void SaveData()
    {
        PlayerPrefs.SetInt(finishModePrefs, finishMode);

        if(finishMode == 0) 
        {
            PlayerPrefs.SetFloat("lastScore", timer.GetTime());
        }
    }

    //Devuelve el modo de finalización
    public int GetFinishMode()
    {
        return finishMode;
    }

    //Devuelve el nombre del mapa
    public string GetMapName()
    {
        return map;
    }

    //Devuelve la dificultad actual
    public int GetDifficulty()
    {
        return difficulty;
    }

}
