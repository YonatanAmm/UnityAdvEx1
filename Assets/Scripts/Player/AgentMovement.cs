using System;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    public event UnityAction OnAgentReachDestinationActionEvent; //Informs that the player has reached the final destination
    
    private static readonly int _speed = Animator.StringToHash("Speed");

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;
    
    private float _originalSpeed;
    private bool reached = true;
    
    public NavMeshAgent Agent { get { return agent; } }
    private bool finishedAnimation;

    void Start()
    {
        _originalSpeed = agent.speed;
        SetAreasCosts();
        finishedAnimation = true;
    }
    void Update()
    {
        Clicked();
        HasArrived();

        if (animator)
        {
            animator.SetFloat(_speed, agent.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other) //When the player reaches final, invoke this event
    {
        if (other.CompareTag("Final"))
        {
            OnAgentReachDestinationActionEvent?.Invoke();
        }
        else if (other.CompareTag("Obstacle"))  //When a ball hits the character
        {
            GetHit();
        }
    }

    public void Clicked()
    {
        if(Input.GetMouseButtonDown(0) && finishedAnimation)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit rayHit,Mathf.Infinity, layerMask))
            {
                agent.SetDestination(rayHit.point);
                //animator.SetBool("playRunning", true);
                //animator.SetBool("playIdle", false);
                reached = false;
                agent.isStopped = false;
            }
        }
    }

    public void HasArrived()
    {
        if (agent != null && !reached && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            reached = true;
            agent.isStopped = true;
            //animator.SetBool("playRunning", false);
            //animator.SetBool("playIdle", true);
        }
    }

    public void SetAreasCosts()
    {
        if (agent != null && agent.agentTypeID == -334000983) // ID 2 = Elf but for some reason it goes into this number when checked.
        {
            agent.SetAreaCost(6,1);
        }
        else if (agent != null && agent.agentTypeID == 0) // ID 0 = Human
        {
            agent.SetAreaCost(6,6);
        }
    }

    public void ResetSpeed() //To return to the previouse state of speed after leaving a special surface
    {
        agent.speed = _originalSpeed;
    }

    public void GetHit()   //Generic method for when the character gets hit
    {
        agent.isStopped = true;
        animator.SetTrigger("Falling");
    }

    public void ControlMovement(bool state)
    {
        finishedAnimation = state;
    }

}
