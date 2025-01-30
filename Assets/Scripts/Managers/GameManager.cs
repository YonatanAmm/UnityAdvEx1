using System.Collections.Generic;
using UnityEngine;
using static Character;

public class GameManager : MonoBehaviour
{
    //Coin Spawn Variables
    [Header("References")]
    [SerializeField] UIManager uiManager;
    [SerializeField] Character elfCharacter;
    [SerializeField] CoinsGenerator coinsGenerator;

    [Header("Game Manager Data")]
    [SerializeField] private int amount;
    [SerializeField] private Vector3 rangeMin = new Vector3(-25, 1.5f, -25);
    [SerializeField] private Vector3 rangeMax = new Vector3(25, 1.5f, 25);
    [SerializeField] private float objectRadius = 1f;
    
    private List<Vector3> _spawnedPositions = new List<Vector3>();

    private void Start()
    {
        uiManager.DecreaseSpeed += HandleAgentSpeed;
        elfCharacter.OnCoinCollected += AmountChecker;
        SpawnObjects();
    }

    private void SpawnObjects() //Spawns the specified amount of coins (Amount) on the surface. Coin's type is random based on specified probability, spawning handeling from CoinsGenerator class
    {
        int spawnedCount = 0;
        while (spawnedCount < amount)
        {
            // Generate a random position within the range
            Vector3 randomPosition = new Vector3(
                Random.Range(rangeMin.x, rangeMax.x),
                rangeMax.y,
                Random.Range(rangeMin.z, rangeMax.z)
            );

            // Check if this position overlaps with any already spawned
            if (IsPositionValid(randomPosition))
            {
                Instantiate(coinsGenerator.GenerateCoin(), randomPosition, Quaternion.identity);
                _spawnedPositions.Add(randomPosition);
                spawnedCount++;
            }
        }
        
        //Making sure the surface can contain the amount of coins without overlapping
        if (spawnedCount < amount) 
        {
            Debug.LogWarning($"Could only spawn {spawnedCount}/{amount} objects without overlap.");
        }
    }

    private bool IsPositionValid(Vector3 position) //Before spawning, makes sure we are not spawning a coin on top of a coin
    {
        foreach (Vector3 spawnedPosition in _spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < objectRadius * 2)
            {
                return false; 
            }
        }

        return true; 
    }

    private void AmountChecker(InformationSend sent) //Checking if the coins collected from the player are equal to the coins total amount
    {
        if (amount == sent.CollectedAmount)
        {
            uiManager.SetCollectedText(true);
        }
    }

    private void HandleAgentSpeed(float newSpeed, AgentMovement agentMovement) //Handles the agent speed
    {
        if (newSpeed != 1)
        {
            agentMovement.Agent.speed *= newSpeed;
        }
        else
        {
            agentMovement.ResetSpeed();
        }
    }
}
