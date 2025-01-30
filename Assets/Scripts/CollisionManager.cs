using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private AgentMovement playerRef;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("yesss");
            playerRef.GetHit();
        }
    }
}
