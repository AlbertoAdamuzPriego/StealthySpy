using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    public event Action OnGameplay;
    public event Action OnPause; 
    public event Action OnFinished;
    public event Action OnGameOver;

    [SerializeField] string map;

    [SerializeField] private int dificulty;
    [SerializeField] private float time;

    private int finishMode;
    private string finishModePrefs="finish";

    public static GameplayManager instance; //Instancia del script (Patron Singleton)

    public int Dificulty { set { dificulty=value; } }

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

    // Start is called before the first frame update
    void Start()
    {
        Timeout timer= FindAnyObjectByType<Timeout>();
        InitializeObstacles();
        timer.StartCount(time);
        map = SceneManager.GetActiveScene().name;

        OnFinished += SaveData;
        Gameplay();

    }

    public void Gameplay()
    {
 
       OnGameplay?.Invoke();
       Debug.Log("PLAY");
    }

    public void Pause()
    {
        OnPause?.Invoke();
        Debug.Log("PAUSED");
    }

    public void Finish(int mode)
    {
        finishMode = mode;
        OnFinished?.Invoke();
        Debug.Log("FINISH");

        SceneManager.LoadScene("MainScene");
    }
    public void GameOver()
    {

        OnGameOver?.Invoke();
  
    }



    private void InitializeObstacles()
    {
        if (dificulty < 2)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.GetComponent<Obstacle>().Dificulty() > dificulty)
                {
                    obstacle.SetActive(false);
                }
            }
        }
       
    }

    public void Restart()
    {
        SceneManager.LoadScene(map);
        
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(finishModePrefs, finishMode);
    }

    public int GetFinishMode()
    {
        return finishMode;
    }

}