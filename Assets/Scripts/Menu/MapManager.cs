using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Numerics;

//Gestiona la informaci�n de los mapas 
public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Map> maps = new List<Map>(); 
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private MapButton mapButton;
    [SerializeField] private GameObject scrollbar;

    void Start()
    {
        GameManager.instance.OnLevelMenu += CreateButton;

        foreach (Map map in maps) 
        {
            map.LoadScore();
       }
    }

    //Crea los botones de la selecci�n de mapas
    private void CreateButton()
    {
        
        foreach(Map map in maps)
        {
            
            MapButton button;
            button = Instantiate(mapButton, buttonContainer.transform);
            button.MapName = map.mapName;
            button.SceneName = map.sceneName;
            button.SetBestTime(map.GetBestTime());
            button.SetAveragteTime(map.GetAverageTime());
        }

        //Se desuscribre para evitar duplicados
        GameManager.instance.OnLevelMenu -= CreateButton;
     
    }

    public int GetNumberOfMaps()
    {
        return maps.Count;
    }

    //Recalcula las puntuaciones del �ltimo mapa jugado
    public void RecalculateScores()
    {
        //Se recoge la informaci�n del �tlimo mapa guardada
        string mapName = PlayerPrefs.GetString("lastMap");
        float newScore = PlayerPrefs.GetFloat("lastScore",-1);
        int difficulty = PlayerPrefs.GetInt("difficulty");

        //Si la nueva puntuaci�n no es v�lida no se actualiza
        if (newScore == -1)
            return;

        foreach(Map map in maps)
        {
            //Se busca el mapa
            if (mapName == map.mapName) 
            {
                //Se actualiza solo la lista de la dificultad espec�fica
                float[] mapScore = map.GetScoreList(difficulty);
                
                //Se inserta de forma ordenada la nueva puntuaci�n
                for (int i=0;i<mapScore.Length;i++)
                {
                    //Se comprueba que la nueva puntuaci�n es menor o que ya no hay registradas (para evitar actualizar si la nueva puntuaci�n es mayor que las ya registradas)
                    if(newScore < mapScore[i] || mapScore[i]==0)
                    {
                        
                        if (i>0)
                        {
                            //Se compara que sea diferente a la anterior para evitar duplicados
                            if (newScore != mapScore[i - 1])
                            {
       
                                map.SetScoreList(InsertNewElementInVector(mapScore, i, newScore),difficulty);
                                map.SaveScore();
                                
                             
                                return;
                                
                            }

                            //Si es igual no se hace nada
                            else
                            {
                                
                                return;
                            }
                                
                           
                        }

                        //Si es la primera
                        else
                        {
                          
                            if (newScore != mapScore[0])
                            {
                                
                         
                                map.SetScoreList(InsertNewElementInVector(mapScore, i, newScore),difficulty);
                                map.SaveScore();
                                
                                return;
                            }

                            //Si se obtiene la misma puntuaci�n no se hace nada
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

    //Inserta un nuevo elemento en la lista
    private float[] InsertNewElementInVector(float[] vector, int posInsert, float value)
    {
        //Deplaza todos los elementos una posici�n m�s desde el final hasta la posici�n de inserci�n
        for(int i=vector.Length-1;i>posInsert;i--)
        {
            vector[i] = vector[i-1];
        }

        //Se inserta el nuevo elemento
        vector[posInsert] = value;


        return vector;
    }

    public List<Map> GetMaps()
    {
        return maps;
    }
}
