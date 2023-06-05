using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

//Es la clase principal que dirige la aplicaci�n durante una partida
public class GameplayManager : MonoBehaviour
{
    /*
    * Estos eventos llaman a una serie de funciones suscritas a ellos cuando son invocados 
    * permitiendo la comunicaci�n y el control instant�neo con otros scripts
    */
    public event Action OnGameplay; //Estado en partida
    public event Action OnPause; //Estado en pausa
    public event Action OnFinished; //Estado finalizando partida
    public event Action OnGameOver; //Estado partida fallida
    public event Action OnCompleted; //Estado mapa completado

    [SerializeField] string map; //Nombre del mapa
    [SerializeField] private int difficulty; //Dificultad actual
    
    //Tiempo m�ximo para completar el mapa en cada dificultad
    [SerializeField] private float timeEasy; 
    [SerializeField] private float timeNormal;
    [SerializeField] private float timeDifficult;
    

    Timeout timer; //Temporizador
    private int finishMode; //Modo de finalizaci�n
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

    //Activa el men� de pausa
    public void Pause()
    {
        OnPause?.Invoke();
       
    }

    //Activa las funciones necesarias para finalizar la partida
    //Mode = 0 -> Partida completada con �xito -- Mode!=0 -> Partida fallida
    public void Finish(int mode)
    {

        finishMode = mode;
        OnFinished?.Invoke();
    
        //Vuelve al men� principal
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

    //Inicializa los obstaculos que var�an seg�n la dificultad seleccionada
    private void InitializeObstacles()
    {
        //Si la dificultad==2 por defecto aparecen todos
        if (difficulty < 2)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

            //Para cada obst�culo comprueba si la dificultad actual es igual o superior a su nivel. Si no lo desactiva
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

    //Guarda c�mo ha finalizado la partida y la puntuaci�n obtenida, si es necesario.
    private void SaveData()
    {
        PlayerPrefs.SetInt(finishModePrefs, finishMode);

        if(finishMode == 0) 
        {
            PlayerPrefs.SetFloat("lastScore", timer.GetTime());
        }
    }

    //Devuelve el modo de finalizaci�n
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
