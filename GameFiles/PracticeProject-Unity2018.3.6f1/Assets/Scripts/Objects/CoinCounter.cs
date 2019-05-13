using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    private static int currentCoins;

    public TextMeshProUGUI CoinText;

    public void AddCoins(int amount)
    {
        currentCoins = currentCoins+  amount;
        SetCoins();
    }

    public void SetCoins()
    {
        CoinText.SetText("Coins: "+currentCoins.ToString());
    }

    public int getCoinAmount()
    {
        return currentCoins;
    }
}
