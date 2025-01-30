using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinsGenerator", menuName = "Scriptable Objects/Coins/CoinsGenerator")]
public class CoinsGenerator : ScriptableObject
{
    [SerializeField] List<Coin> coins = new List<Coin>();

    public Coin GenerateCoin() //generates a random coin based on the probability that was given by the player in a range from 1-100
    {
        float chance = 0;
        Coin selected = coins[0];
        foreach (Coin coin in coins)
        {
            float chanceNew = Random.Range(coin.info.probability, 100);
            if (chance < chanceNew)
            {
                selected = coin;
                chance = chanceNew;
            } 
        }
        return selected;
    }

    public Coin GenerateCoin(CoinsInfo.CoinSize size) //generate coin by enum definition
    {
        foreach (Coin coin in coins)
        {
            if(coin.info.coinSize == size)
                return coin;
        }
        Debug.Log("Not found");
        return null;
    }
}
