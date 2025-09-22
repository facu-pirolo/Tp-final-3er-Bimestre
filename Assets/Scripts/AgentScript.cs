using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform targetTR;
    [SerializeField] Animator anim;
    [SerializeField] float velocity;
    [SerializeField] Transform [] puntosDePatru;
    public bool Patrulaje = true;
    int puntoActual = -1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (puntosDePatru.Length > 0)
        {
            agent.destination = puntosDePatru[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
      if(!agent.pathPending && agent.remainingDistance < 0.3f && puntosDePatru.Length > 0)
        { 
            puntoActual = (puntoActual + 1) % puntosDePatru.Length;
        agent.destination = puntosDePatru[puntoActual].position;
    }

    
        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed",velocity);
    }
}
