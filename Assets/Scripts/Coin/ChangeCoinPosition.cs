using UnityEngine;

public class ChangeCoinPosition : MonoBehaviour
{
    [SerializeField] private float pushDistance = 1.0f; // Distance to push the coin

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            PushCoinAway(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            PushCoinAway(other);
        }
    }

    private void PushCoinAway(Collider coin)
    {
        // Store the original Y level of the coin
        float originalY = coin.transform.position.y;

        // Calculate the direction away from the obstacle (this object)
        Vector3 pushDirection = (coin.transform.position - transform.position).normalized;

        // Move the coin in the push direction by the specified distance
        coin.transform.position += pushDirection * pushDistance;

        // Reapply the original Y level
        coin.transform.position = new Vector3(coin.transform.position.x, originalY, coin.transform.position.z);
    }
}
