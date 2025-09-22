using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCats : MonoBehaviour
{
    [SerializeField] Vector3 rayOrigin;
    [SerializeField] Transform sightOrigin;
    [SerializeField] float rayDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(sightOrigin.position, sightOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Impactó con: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag=="Jugador")
            {

            }
        }
    }

    void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(sightOrigin.position, sightOrigin.position + sightOrigin.forward * rayDistance); ;
    }
}
