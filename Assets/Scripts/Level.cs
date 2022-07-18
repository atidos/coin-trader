using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Trader/Level")]
public class Level : ScriptableObject
{
    public List<Coin> coins;
    public List<Notification> notifs;
    public float startingBalance
    {
        get
        {
            //Some other code
            return targetBalance * startFraction;
        }
    }

    public float startFraction = 0.5f;
    public float targetBalance = 1;
    public Sprite targetImage;
}
