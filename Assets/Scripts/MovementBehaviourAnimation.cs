using UnityEngine;
using UnityEngine.Animations;

public class MovementBehaviourAnimation : StateMachineBehaviour
{
    private AgentMovement playerRef;
    
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator,stateMachinePathHash);
        
        if (!playerRef)
        {
            playerRef = animator.GetComponent<AgentMovement>();
        }

        playerRef.ControlMovement(false);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash, controller);

        playerRef.ControlMovement(true);
    }
}
