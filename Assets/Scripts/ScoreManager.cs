using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Numerics;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ScoreButton scoreButton;
    [SerializeField] private GameObject scrollbar;

    void Start()
    {
       
        GameManager.instance.OnScoreMenu += CreateButton;

   
    }

    private void CreateButton()
    {
        List<Map> maps = FindAnyObjectByType<MapManager>().GetMaps();
        foreach(Map map in maps)
        {
            
            ScoreButton button;
            button = Instantiate(scoreButton, buttonContainer.transform);
            button.Map = map;
         
        }

        GameManager.instance.OnScoreMenu -= CreateButton;
     
    }
}
