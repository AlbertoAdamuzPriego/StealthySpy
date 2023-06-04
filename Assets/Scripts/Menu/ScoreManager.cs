using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Numerics;

//Se encarga de crear los botones para el menú de puntuaciones
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonContainer; //Contenedor del scrollbar
    [SerializeField] private ScoreButton scoreButton; //Clase ScoreButton
    

    void Start()
    {
        //Suscribe la función al evento OnScoreMenu
        GameManager.instance.OnScoreMenu += CreateButton;
    }

    //Crea los botones del menú de puntuaciones
    private void CreateButton()
    {
        List<Map> maps = FindAnyObjectByType<MapManager>().GetMaps();
     
        //Por cada mapa instancia un botón 
        foreach(Map map in maps)
        {
            
            ScoreButton button;
            button = Instantiate(scoreButton, buttonContainer.transform);
            button.Map = map;
         
        }

        //Es necesario desuscribirlo para no duplicar
        GameManager.instance.OnScoreMenu -= CreateButton;
     
    }
}
