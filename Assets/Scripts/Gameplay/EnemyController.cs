using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speedMovement;

    [SerializeField] private Vector2[] pointsMovement;

    [SerializeField] private float minDistance;

    [SerializeField] private float waitTime;

    private float timer;

    int currentTarget=0;

    private SpriteRenderer sprite;

    [SerializeField] VisionTriangleController fov;

    private bool playerIsDetected;
    // Start is called before the first frame update
    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        Turn();

        fov=GetComponentInChildren<VisionTriangleController>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsDetected)
        {
            if (MoveToTarget())
            {
                transform.position = Vector2.MoveTowards(transform.position, pointsMovement[currentTarget], speedMovement * Time.deltaTime);
            }

            else
            {
                UpdateTimer();

                if (timer >= waitTime)
                {
                    currentTarget = (currentTarget + 1) % pointsMovement.Length;
                    Turn();
                    timer = 0;
                }
            }

        }

        else
        {

        }


        fov.DrawMesh();
    }

    private bool MoveToTarget()
    {
        if(Vector2.Distance(pointsMovement[currentTarget], transform.position)>0.1)
        {
            return true;
        }

        return false;
    }
    private void Turn()
    {
        if (transform.position.x < pointsMovement[currentTarget].x)
        {
            fov.setInitialAngle(40);
            transform.GetChild(0).GetComponent<Transform>().position = transform.position + new Vector3(0.3f, -0.28f,0);
            sprite.flipX = false;
        }

        else
        {
            fov.setInitialAngle(220);
            transform.GetChild(0).GetComponent<Transform>().position = transform.position + new Vector3(-0.3f, -0.28f, 0);
            sprite.flipX = true;
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}
