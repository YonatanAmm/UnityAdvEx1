using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public event UnityAction<InformationSend> OnCoinCollected;
    public UnityEvent<SurfaceType> OnEventSurfaceEnterEvent;
    public UnityEvent OnEventSurfaceExitEvent;

    [SerializeField] private Collider myCollider;
    [SerializeField] private ParticleSystem particleEffect;

    private int _coinsCounter;

    private void OnTriggerEnter(Collider other) //Trigger check on if the player entered a coin or a surface to handle invocations
    {
        if (other.CompareTag("Coin"))
        {
            Coin coinColided = other.GetComponent<Coin>(); //Coin trigger
            if (coinColided != null)
            {
                _coinsCounter++;
                InformationSend send = new InformationSend(coinColided,_coinsCounter);
                OnCoinCollected?.Invoke(send);
            }
        }
        else if (other.CompareTag("Surfaces")) //Surface trigger
        {
            SurfaceType surfaceType = other.GetComponent<SurfaceType>();
            SetEffectColor(surfaceType.ColorEffect);
            OnEventSurfaceEnterEvent.Invoke(surfaceType);
        }
    }

    private void OnTriggerExit(Collider other) //Informs that the player has left the surface
    {
        if (other.CompareTag("Surfaces"))
        {
            OnEventSurfaceExitEvent.Invoke();
        }
    }

    private void SetEffectColor(Color color) //Determine which color to put in the particle effect based on the surface type
    {
        var mainModule = particleEffect.main; // Access the Main Module
        mainModule.startColor = color; // Set the start color    
    }

    public struct InformationSend //Sending information to the events through a struct (Bonus)
    {
        public Coin Coin;
        public int CollectedAmount;

        public InformationSend(Coin coin, int counter)
        {
            this.Coin = coin;
            this.CollectedAmount = counter;
        }
    };
}

