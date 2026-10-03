using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*
 Este script permite mover el cubo haciendo click
 sobre el suelo utilizando un NavMeshAgent.
 */

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public Camera camara;

    public LayerMask sueloLayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.angularSpeed = 700f;

        agent.acceleration = 500f;

        agent.stoppingDistance = 0.1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = camara.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 500f, sueloLayer))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 2f, NavMesh.AllAreas))
                {

                    agent.SetDestination(navHit.position);
                }
                else
                {
                    Debug.Log("Destino inalcanzable");
                }
            }
            else
            {
                Debug.Log("No he golpeado el suelo");
            }
        }
    }
}
