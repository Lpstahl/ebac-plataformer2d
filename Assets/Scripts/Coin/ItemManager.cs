using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
   
{
    public SOInt coins;
    public SOInt coinsRed;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        coinsRed.value = 0;
        
        UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateUI();
    }

    public void AddCoinsRed(int amount = 1)
    {
        coinsRed.value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIInGameManager.UpdateTextCoins(coins.value.ToString(), coinsRed.value.ToString());
    }
}
