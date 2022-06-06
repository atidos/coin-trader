using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Trader/Level")]
public class Level : ScriptableObject
{
    public List<Coin> coins;
    public List<Notification> notifs;
    public int startingBalance = 0;
    public int targetBalance = 0;
    public Sprite targetImage;
}
