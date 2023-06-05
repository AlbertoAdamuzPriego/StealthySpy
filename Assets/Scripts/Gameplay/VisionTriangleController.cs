using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Net.NetworkInformation;
using System;
using UnityEngine.UIElements;

[RequireComponent(typeof(PolygonCollider2D))]

//Dibuja y controla un área de visión en forma de triángulo
public class VisionTriangleController : MonoBehaviour
{
    [SerializeField] private float viewDistance = 5f; //Distancia que alcanza de visión
    [SerializeField] private float viewAngle = 90f; //Ángulo que puede detectar
    [SerializeField] private float initialAngle = 90f; //Ángulo de posición inicial 
    [SerializeField] private LayerMask obstacleLayerMask; //Capa que detecta el suelo
    [SerializeField] private int segments = 30; // Número de segmentos para el mesh
    [SerializeField] private Transform originPoint; //Punto de origen para dibujar
    [SerializeField] private Material fovMaterial; //Material
    [SerializeField] private Material fovMaterialDetection; //Material al detectar
    private float  timeDetection=0; //Tiempo transcurrido detectando al jugador
    public float startAngle; //Ángulo inicial
    private Mesh visionMesh; //Mesh que representa el área
    private MeshFilter visionMeshFilter; 
    private MeshRenderer visionMeshRenderer;
    private PolygonCollider2D polygonCollider; //Collider del Mesh
    public bool isPlayerDetected; //Informa si el jugador ha sido detectado
    public bool isFinish; //Informa si la partida finaliza
    void Start()
    {
        visionMesh = new Mesh();
        visionMeshFilter = GetComponent<MeshFilter>();
        visionMeshRenderer = GetComponent<MeshRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        isPlayerDetected = false;
        isFinish = false;
    }

    //Se activa cuando un objeto entra al collider
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Si el collider detectado es el jugador
        if (collider.CompareTag("Player"))
        {
            //Si el jugador es visible (no está escondido)
            if (collider.gameObject.GetComponent<Player>().visible)
            {

                isPlayerDetected = true;

                visionMeshRenderer.material = fovMaterialDetection;

                timeDetection = 0;
            }
        
        }
    }
    
    //Se activa cuando un objeto se mantiene dentro del collider
    private void  OnTriggerStay2D(Collider2D collider)
    {
        //Si el collider detectado es el jugador
        if (collider.CompareTag("Player"))
        {
            //Si el jugador es visible (no está escondido)
            if (collider.gameObject.GetComponent<Player>().visible)
            {
                //Actualizamos el contador
                timeDetection += Time.deltaTime;

                //Si ha pasado suficiente tiempo detectado o el jugador está muy cerca del origen del área se considera que ha sido detectado y pierde la partida 
                if (timeDetection > 0.1f || Vector2.Distance(collider.transform.position, originPoint.transform.position)<0.1f)
                {
                    //Se congela al enemigo
                    collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    //Se desactiva al jugador
                    collider.GetComponent<Player>().enabled = false;
                    collider.GetComponent<Animator>().SetBool("finish", true);

                    //Se llama al evento GameOver
                    GameplayManager.instance.GameOver();

                }
            }


        }

    }

    //Se activa cuando un objeto sale del collider
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Si es el jugador
        if (collider.CompareTag("Player"))
        {
            //Se regresa al estado no detectado
            isPlayerDetected = false;

            visionMeshRenderer.material = fovMaterial;

            timeDetection = 0;
        }
    }

    //Asigna un ángulo inicial
    public void setInitialAngle(float angle)
    {
        initialAngle = angle;
    }

    //Dibuja el mesh con una cierta rotación
    public void DrawMesh(float rotation=0)
    {
        transform.position = originPoint.position;
        
        startAngle = initialAngle - rotation;
        
        // Crear un nuevo mesh para el área de visión
        visionMesh.Clear();
        Vector3[] vertices = new Vector3[segments + 1 + 1]; //Vértices del mesh
        Vector2[] points = new Vector2[vertices.Length]; //Puntos del collider
        int[] triangles = new int[segments * 3]; //Triángulos del mesh

        // Calcular los vértices del mesh y detectar objetos
        Vector3 origin = transform.position;
        vertices[0] = Vector3.zero;
        points[0] = Vector2.zero;
        float angleIncrement = viewAngle / segments; //Ángulo de diferencia entre cada segmento
        float currentAngle = -viewAngle / 2f - startAngle; //Ángulo inicial


        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= segments; i++)
        {
            Vector3 vertex;
            Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up; //Calcula la dirección según el ángulo

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, obstacleLayerMask); //Detecta si hay colisión 
            if (hit.collider != null)
            {
                //Si se detecta colisión el vértice se dibuja en la colisión
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                //Si no se detecta colisión el vértice se dibuja en la distancia máxima
                vertex = transform.InverseTransformPoint(origin + direction * viewDistance);
            }


            //Se añade el nuevo vértice
            vertices[vertexIndex] = vertex;
            points[vertexIndex] = vertex;

            // Se asignan los vértices a cada triángulo 
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            currentAngle -= angleIncrement;

        }

        // Asignar los vértices y triángulos al mesh
        visionMesh.vertices = vertices;
        visionMesh.triangles = triangles;

        //Asignar los puntos al collider
        polygonCollider.points = points;

        // Recalcular las normales y límites del mesh
        visionMesh.RecalculateNormals();
        visionMesh.RecalculateBounds();

        // Asignar el mesh actualizado al MeshFilter
        visionMeshFilter.mesh = visionMesh;
    }
}
