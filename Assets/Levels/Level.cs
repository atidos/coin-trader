using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public Coin Coins[];
    public Notification notifs[];
    public float targetBalance = 0f;
    Sprite targetImage;
}
