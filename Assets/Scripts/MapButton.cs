using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private string mapName;
    private string sceneName;

    public string MapName
    {
        set { mapName = value; }
    }

    public string SceneName
    {
        set { sceneName = value; }
    }

    private void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = mapName;

        var button = GetComponent<Button>();
        button.onClick.AddListener(OpenMap);
    }

    public void OpenMap()
    {
        SceneManager.LoadScene(sceneName);
    }
}
