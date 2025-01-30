using UnityEngine;

public class OnHitBehaivor : StateMachineBehaviour
{
    AgentMovement AgentMovement;
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);

        if (!AgentMovement)
        {
            AgentMovement = animator.GetComponent<AgentMovement>();
        }

        AgentMovement.ControlMovement(false);
    }


    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
        AgentMovement.ControlMovement(true);
    }
}
