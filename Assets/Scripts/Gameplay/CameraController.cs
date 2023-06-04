using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlador de la cámara
public class CameraController : MonoBehaviour
{
    private Transform target; //Objeto que es seguido por la cámara
    [SerializeField] float minHeight, maxHeight; //Altura mínima y máxima
    [SerializeField] float minX, maxX; //Posiciones mínima y máxima en el eje X
    private Transform farBackground, middleBackground, nearBackground; //Fondo
    private Vector2 lastPos; //Última posición

    void Start()
    {
        //Se asigna al jugador para que la cámara lo siga
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

        //Calculamos el desplazamiento del fondo
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //Movemos el fondo
        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
        nearBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.25f;

        //Reiniciamos
        lastPos = new Vector2(transform.position.x, transform.position.y);
    }
}