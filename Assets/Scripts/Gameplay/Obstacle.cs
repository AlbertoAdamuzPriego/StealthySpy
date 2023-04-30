using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int dificulty;
    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.instance.OnGameplay += EnableComponent;
        GameplayManager.instance.OnPause += DisableComponent;
    }


    public int Dificulty()
    {
        return dificulty;
    }

    private void SetComponentActive(bool enable)
    {
        if (GetComponent<EnemyController>() != null)
        {
            GetComponent<EnemyController>().enabled = enable;
        }

        else if (GetComponentInChildren<SecurityCamera>() != null)
        {
            GetComponentInChildren<SecurityCamera>().enabled = enable;
        }
    }

    private void EnableComponent()
    {
        SetComponentActive(true);
    }

    private void DisableComponent()
    {
        SetComponentActive(false);
    }
}
