using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

//Botones de la selección de mapas
public class MapButton : MonoBehaviour
{
    private string mapName;
    private string sceneName; //Escena donde se encuentra el escenario del mapa
    private float[] bestTime = new float[3]; //Mejor tiempo de cada dificultad
    private float[] averageTime = new float[3]; //Tiempo medio de cada dificultad
    public string MapName
    {
        set { mapName = value; }
    }

    public string SceneName
    {
        set { sceneName = value; }
    }

    public void SetBestTime(float[] times)
    {
        bestTime = times;
    }

    public void SetAveragteTime(float[] times)
    {
        averageTime= times;
    }

    private void Start()
    {
        transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = mapName;

        var button = GetComponent<Button>();
        button.onClick.AddListener(OpenMap);

        UpdateDifficulty();

        GameManager.instance.ChangeDifficulty += UpdateDifficulty;
        GameManager.instance.OnLevelMenu += UpdateDifficulty;
    }
 
    public void OpenMap()
    {
        //Se guarda el último mapa para actualizar las puntuaciones
        PlayerPrefs.SetString("lastMap", mapName);
        SceneManager.LoadScene(sceneName);
    }

    //Actualiza la dificultad que se muestra en el botón
    private void UpdateDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty",0);

        switch(difficulty)
        {
            case 0:
                if (bestTime[0] != 0)
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[0]).ToString(@"mm\:ss");
                    transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[0]).ToString(@"mm\:ss");
                }

                else
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "--:--";
                    transform.GetChild(3).GetComponent<TMP_Text>().text = "--:--";
                }
                break;
            case 1:
                if (bestTime[1] != 0)
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[1]).ToString(@"mm\:ss");
                    transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[1]).ToString(@"mm\:ss");
                }

                else
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "--:--";
                    transform.GetChild(3).GetComponent<TMP_Text>().text = "--:--";
                }
                
                break;
            case 2:
                if (bestTime[2] != 0)
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[2]).ToString(@"mm\:ss");
                    transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[2]).ToString(@"mm\:ss");
                }

                else
                {
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "--:--";
                    transform.GetChild(3).GetComponent<TMP_Text>().text = "--:--";
                }
                
                break;
        }
      
    }
}
