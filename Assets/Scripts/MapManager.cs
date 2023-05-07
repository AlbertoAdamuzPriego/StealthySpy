using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Numerics;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Map> maps = new List<Map>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private MapButton mapButton;
    [SerializeField] private GameObject scrollbar;

    void Start()
    {
        //GameManager.instance.OnMainMenu += CreateButton;
        //GameManager.instance.OnLevelMenu += RecalculateScores;
        GameManager.instance.OnLevelMenu += CreateButton;

        foreach (Map map in maps) 
        {
            map.LoadScore();
       }
    }

    private void CreateButton()
    {
        
        foreach(Map map in maps)
        {
            
            MapButton button;
            button = Instantiate(mapButton, buttonContainer.transform);
            //map.LoadScore();
            button.MapName = map.mapName;
            button.SceneName = map.sceneName;
            button.SetBestTime(map.GetBestTime());
            button.SetAveragteTime(map.GetAverageTime());
        }

        GameManager.instance.OnLevelMenu -= CreateButton;
     
    }

    public int GetNumberOfMaps()
    {
        return maps.Count;
    }

    public void RecalculateScores()
    {
       
        string mapName = PlayerPrefs.GetString("lastMap");
        float newScore = PlayerPrefs.GetFloat("lastScore",-1);
        int difficulty = PlayerPrefs.GetInt("difficulty");

        Debug.Log(newScore);

        if (newScore == -1)
            return;

        foreach(Map map in maps)
        {
        
            if (mapName == map.mapName) 
            {

                float[] mapScore = map.GetScoreList(difficulty);
                
                for (int i=0;i<mapScore.Length;i++)
                {
                    Debug.Log(i);
                    if(newScore < mapScore[i] || mapScore[i]==0)
                    {
                  
                        if (i>0)
                        {
                          
                            if (newScore != mapScore[i - 1])
                            {
       
                                map.SetScoreList(InsertNewElementInVector(mapScore, i, newScore),difficulty);
                                map.SaveScore();
                                
                             
                                return;
                                
                            }

                            else
                            {
                                
                                return;
                            }
                                
                           
                        }

                        else
                        {
                          
                            if (newScore != mapScore[0])
                            {
                                
                         
                                map.SetScoreList(InsertNewElementInVector(mapScore, i, newScore),difficulty);
                                map.SaveScore();
                                
                                return;
                            }

                            else
                            {
                                
                                return;
                            }
                                
                        }
                       
                    }
                }
            
            }
        }
    }

    private float[] InsertNewElementInVector(float[] vector, int posInsert, float value)
    {

        for(int i=vector.Length-1;i>posInsert;i--)
        {
            vector[i] = vector[i-1];
        }

        vector[posInsert] = value;


        return vector;
    }

    public List<Map> GetMaps()
    {
        return maps;
    }
}
