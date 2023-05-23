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
    public event Action OnCompleted;

    [SerializeField] string map;

    [SerializeField] private int difficulty;
    [SerializeField] private float timeEasy;
    [SerializeField] private float timeNormal;
    [SerializeField] private float timeDifficult;
    

    Timeout timer;
    private int finishMode;
    private string finishModePrefs="finish";

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

    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        timer= FindAnyObjectByType<Timeout>();
        InitializeObstacles();

        switch(difficulty)
        {
            case 0: timer.StartCount(timeEasy); break;
            case 1: timer.StartCount(timeNormal); break;
            case 2: timer.StartCount(timeDifficult); break;

        }

        map = SceneManager.GetActiveScene().name;
        OnFinished += SaveData;
        Gameplay();

    }

    public void Gameplay()
    {
 
       OnGameplay?.Invoke();
  
    }

    public void Pause()
    {
        OnPause?.Invoke();
       
    }

    public void Finish(int mode)
    {
        finishMode = mode;
        OnFinished?.Invoke();
    

        SceneManager.LoadScene("MainScene");
    }

    public void Completed()
    {
        
        OnCompleted?.Invoke();


    }
    public void GameOver()
    {

        OnGameOver?.Invoke();
  
    }


    private void InitializeObstacles()
    {
        if (difficulty < 2)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.GetComponent<Obstacle>().Dificulty() > difficulty)
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

        if(finishMode == 0) 
        {
            PlayerPrefs.SetFloat("lastScore", timer.GetTime());
        }
    }

    public int GetFinishMode()
    {
        return finishMode;
    }

    public string GetMapName()
    {
        return map;
    }

}
