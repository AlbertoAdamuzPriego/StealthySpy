using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
public class GameplayManager : MonoBehaviour
{
    public event Action OnGameplay;
    public event Action OnPause; 
    public event Action OnFinished; 

    [SerializeField] private int dificulty;
    [SerializeField] private float time;

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

    public void Finish()
    {
        OnFinished?.Invoke();
        Debug.Log("FINISH");
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


}
