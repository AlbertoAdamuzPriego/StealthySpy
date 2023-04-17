using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Net.NetworkInformation;
using System;
using UnityEngine.UIElements;

[RequireComponent(typeof(PolygonCollider2D))]
public class VisionTriangleController : MonoBehaviour
{
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float initialAngle = 90f;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private int segments = 30; // número de segmentos para el mesh
    [SerializeField] private Transform originPoint;
    public float startAngle;
    private Mesh visionMesh;
    private MeshFilter visionMeshFilter;
    private PolygonCollider2D polygonCollider;
    public bool isPlayerDetected;
    void Start()
    {
        visionMesh = new Mesh();
        visionMeshFilter = GetComponent<MeshFilter>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        isPlayerDetected = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        if(collider.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }

    public bool DrawMesh(float rotation)
    {
        transform.position = originPoint.position;
        Debug.Log(rotation);
        startAngle = initialAngle - rotation;
        // Crear un nuevo mesh para el área de visión
        visionMesh.Clear();
        Vector3[] vertices = new Vector3[segments + 1 + 1];
        Vector2[] points = new Vector2[vertices.Length];
        int[] triangles = new int[segments * 3];

        // Calcular los vértices del mesh y detectar objetos
        Vector3 origin = transform.position;
        vertices[0] = Vector3.zero;
        points[0] = Vector2.zero;
        float angleIncrement = viewAngle / segments;
        float currentAngle = -viewAngle / 2f - startAngle;
        ;


        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= segments; i++)
        {
            Vector3 vertex;
            Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, obstacleLayerMask);
            if (hit.collider != null)
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = transform.InverseTransformPoint(origin + direction * viewDistance);
            }



            vertices[vertexIndex] = vertex;
            points[vertexIndex] = vertex;

            // Calcular los triángulos del mesh
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


        polygonCollider.points = points;

        // Recalcular las normales y límites del mesh
        visionMesh.RecalculateNormals();
        visionMesh.RecalculateBounds();

        // Asignar el mesh actualizado al MeshFilter
        visionMeshFilter.mesh = visionMesh;

        return isPlayerDetected;
    }
}
