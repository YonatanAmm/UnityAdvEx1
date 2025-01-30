using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Obstecals : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject[] wayPoints;
    private int currentPath = 0;
    private bool hasArrived = false;
    void Start()
    {
        CalculateCosts();
        agent.SetDestination(wayPoints[currentPath].transform.position);
    }

    void Update()
    {
        HasReached();
    }

    public void HasReached()
    {
        if (agent != null && !hasArrived && agent.remainingDistance <= 0.1f && !agent.pathPending)
        {
            hasArrived = true;
            NewDestination();
        }
    }

    public void NewDestination()
    {
        if (currentPath == 0)
        {
            currentPath++;
            agent.SetDestination(wayPoints[currentPath].transform.position);
            hasArrived = false;
        }
        else if (currentPath == 1)
        {
            currentPath--;
            agent.SetDestination(wayPoints[currentPath].transform.position);
            hasArrived = false;
        }
    }

    public void CalculateCosts()
    {
        agent.SetAreaCost(3, 1);
        agent.SetAreaCost(0, 1);
    }
}
