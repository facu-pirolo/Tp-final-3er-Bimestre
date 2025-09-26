using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RayCats : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Vector3 rayOrigin;
    [SerializeField] Transform sightOrigin;
    [SerializeField] float rayDistance = 10f;
    [SerializeField] Transform[] puntosDePatru;
    [SerializeField] RayCats rayVision;
    [SerializeField] bool Patrulaje = true;
    int puntoActual = 0;
    [SerializeField] Transform Jugador;
    [SerializeField] Animator anim;
    [SerializeField] float velocity;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f && puntosDePatru.Length > 0)
        {
            puntoActual = (puntoActual + 1) % puntosDePatru.Length;
            agent.destination = puntosDePatru[puntoActual].position;
        }

        if (!Patrulaje)
        {
            agent.destination = Jugador.position;
        }
        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed", velocity);

        Ray ray = new Ray(sightOrigin.position, sightOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == "Jugador")
            {
                Jugador = hit.collider.transform;
                Debug.Log("Detectó a: " + hit.collider.gameObject.name);
                Patrulaje = false;
            }
        }
    }


    void OnDrawGizmos()
    {

        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(sightOrigin.position, sightOrigin.position + sightOrigin.forward * rayDistance);
    }
}

