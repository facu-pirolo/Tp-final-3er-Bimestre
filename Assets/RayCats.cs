using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    float tiempoVision = 0f;
    [SerializeField] float tiempoOlvido = 2f;



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
        else
        {
            if (!Patrulaje)
            {
                agent.destination = Jugador.position;
            }

            
            tiempoVision += Time.deltaTime;
            if (tiempoVision >= tiempoOlvido)
            {
                Patrulaje = true;
                Jugador = null;
                tiempoVision = 0f;
            }
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
                tiempoVision = 0f;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            SceneManager.LoadScene("Mensaje");
        }
    }



    void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(sightOrigin.position, sightOrigin.position + sightOrigin.forward * rayDistance);
    }
}

