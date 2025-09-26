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
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
      velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed",velocity);
    }
}
