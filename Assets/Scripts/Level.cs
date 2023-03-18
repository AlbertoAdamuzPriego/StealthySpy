using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] private string levelName="";
    [SerializeField] private enum Difficulty {easy,medium,hard};

    [SerializeField] private string sceneName;
    private Vector3 bestTime;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
