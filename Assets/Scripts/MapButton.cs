using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class MapButton : MonoBehaviour
{
    private string mapName;
    private string sceneName;
    private float[] bestTime = new float[3];
    private float[] averageTime = new float[3];
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

        GameManager.instance.ChangeDifficulty += UpdateDifficulty;
        GameManager.instance.OnLevelMenu += UpdateDifficulty;
    }

    public void OpenMap()
    {
        PlayerPrefs.SetString("lastMap", mapName);
        SceneManager.LoadScene(sceneName);
    }

    private void UpdateDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty");

        switch(difficulty)
        {
            case 0: transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[0]).ToString(@"mm\:ss");
                transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[0]).ToString(@"mm\:ss");
                break;
            case 1:
                transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[1]).ToString(@"mm\:ss");
                transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[1]).ToString(@"mm\:ss");
                break;
            case 2:
                transform.GetChild(2).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(bestTime[2]).ToString(@"mm\:ss");
                transform.GetChild(3).GetComponent<TMP_Text>().text = TimeSpan.FromSeconds(averageTime[2]).ToString(@"mm\:ss");
                break;
        }
      
    }
}
