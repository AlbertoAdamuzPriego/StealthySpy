using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlador de la c�mara
public class CameraController : MonoBehaviour
{
    private Transform target; //Objeto que es seguido por la c�mara
    [SerializeField] float minHeight, maxHeight; //Altura m�nima y m�xima
    [SerializeField] float minX, maxX; //Posiciones m�nima y m�xima en el eje X
    private Transform farBackground, middleBackground, nearBackground; //Fondo
    private Vector2 lastPos; //�ltima posici�n

    void Start()
    {
        //Se asigna al jugador para que la c�mara lo siga
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        lastPos = new Vector2(transform.position.x, transform.position.y);

        farBackground = GameObject.FindGameObjectWithTag("FarBackground").GetComponent<Transform>();
        middleBackground = GameObject.FindGameObjectWithTag("MiddleBackground").GetComponent<Transform>();
        nearBackground = GameObject.FindGameObjectWithTag("NearBackground").GetComponent<Transform>();
    }

    void LateUpdate()
    {

        //Actualizamos la posici�n de la c�mara a la posici�n del CameraPoint
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