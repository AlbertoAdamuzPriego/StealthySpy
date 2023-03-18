using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov : MonoBehaviour
{
    Vector3 origin;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.left, 20,layerMask);
        
        if(hit)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position - (Vector3.right * 20));
    }
}
