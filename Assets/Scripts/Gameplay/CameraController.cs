using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target; //Objeto que es seguido por la cámara
    [SerializeField] float minHeight, maxHeight;
    [SerializeField] float minX, maxX;
    private Transform farBackground, middleBackground, nearBackground;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        lastPos = new Vector2(transform.position.x, transform.position.y);

        farBackground = GameObject.FindGameObjectWithTag("FarBackground").GetComponent<Transform>();
        middleBackground = GameObject.FindGameObjectWithTag("MiddleBackground").GetComponent<Transform>();
        nearBackground = GameObject.FindGameObjectWithTag("NearBackground").GetComponent<Transform>();
    }

    void LateUpdate()
    {

        //Actualizamos la posición de la cámara a la posición del CameraPoint
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
        nearBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.25f;

        lastPos = new Vector2(transform.position.x, transform.position.y);
    }
}