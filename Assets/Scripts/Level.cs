using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Trader/Level")]
public class Level : ScriptableObject
{
    public List<Coin> coins;
    public List<Notification> notifs;
    public float targetBalance = 0f;
    Sprite targetImage;
}
