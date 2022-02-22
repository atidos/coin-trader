using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Level level;

    float timeToNextCoin;

    private void Start()
    {
        LoadLevel(1);
    }

    private void Update()
    {

    }

    void LoadLevel(int lvindex) 
    {
        level = Resources.Load<Level>("Levels/" + lvindex);
        print(level.coins[0].coinName);
    }

    bool ChangeBalance(float amount)
    {
        level.targetBalance += amount;
        return true;
    }

    void CreateCoinPanel(Coin coin)
    {

    }

    void GenerateNotification(Notification notifs)
    {
        
    }

    
}
