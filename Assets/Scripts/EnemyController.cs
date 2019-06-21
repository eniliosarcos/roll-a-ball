using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MethodDestinationPlayer();
        MethodRollingTheBallEnemyIA();
    }

    void MethodDestinationPlayer()
    {
        agent.SetDestination(player.position);
    }

    void MethodRollingTheBallEnemyIA()
    {
        Vector3 v3 = rb.velocity;
        v3.x = agent.velocity.x;
        v3.z = agent.velocity.z;

        rb.velocity = v3;

        transform.position = gameObject.transform.parent.position;
    }
}
