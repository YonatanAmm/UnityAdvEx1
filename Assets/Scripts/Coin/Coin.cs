using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent;

    [SerializeField] public CoinsInfo info; //The value of the coin is here, Second scriptable object to link the values of the coins
    [SerializeField] private GameObject coinGFX;

    private void Start()
    {
        transform.localScale = new Vector3(1, info.sizeMultiplier, info.sizeMultiplier); //Make them larger through the multiplier in the coinInfo scriptable object
    }
    private void OnTriggerEnter(Collider other) //Invokes the inspector event and does a particle effect
    {
        if (other.CompareTag("Player"))
        {
            SetGFX(false);
            OnTriggerEnterEvent?.Invoke();
        }
    }

    public void SetGFX(bool state) //Particle effect handling
    {
        coinGFX.SetActive(state);
    }
}
