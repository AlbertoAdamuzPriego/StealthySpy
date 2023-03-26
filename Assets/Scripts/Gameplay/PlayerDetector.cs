using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float fov;
    [SerializeField] private int rayCount;
    private float angle;
    private float angleIncrease;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        angle = 0f;
        angleIncrease = fov / rayCount;


        Vector3 origin = Vector3.zero;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] ui = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycast2D = Physics2D.Raycast((Vector2) origin, (Vector2) GetVectorFromAngle(angle), viewDistance, layerMask);
            
            if (raycast2D.collider == null)
            {
                Debug.Log("NULL");
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycast2D.point;
            }

           
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease; //Unity va en sentido contrario
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = ui;
        mesh.triangles = triangles;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}