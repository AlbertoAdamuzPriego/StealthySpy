using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Map> maps = new List<Map>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private MapButton mapButton;

    void Start()
    {
        GameManager.instance.OnLevelMenu += CreateButton;
    }

    private void CreateButton()
    {
        foreach(Map map in maps)
        {
            
            MapButton button;
            button = Instantiate(mapButton, buttonContainer.transform);
            button.MapName = map.mapName;
            button.SceneName = map.sceneName;
            
        }

        GameManager.instance.OnLevelMenu -= CreateButton;
    }

    public int GetNumberOfMaps()
    {
        return maps.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
