using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public CoinCounter coinCounter;

    //When a Collider with the "Is Trigger" attribute is entered it will call the function below. The Script will just check for Collider's on the GameObject it's attached to not global.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coinCounter.AddCoins(1);
            Destroy(gameObject);
        }
    }

}
