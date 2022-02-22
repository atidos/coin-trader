using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Level level;
    void LoadLevel(int lvindex) {
    
    }
    bool ChangeBalance(float amount)
    {
        level.targetBalance += amount;
        return true;
    }
    void CreateCoinPanel(Coin coins[])
    {

    }
    void GenerateNotifications(Notification notifs[])
    {

    }
}
