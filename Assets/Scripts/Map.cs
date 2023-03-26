using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Map : ScriptableObject
{
    public string mapName="";
    public enum Difficulty {easy,medium,hard};

    public string sceneName;
    private Vector3 bestTime;
}
