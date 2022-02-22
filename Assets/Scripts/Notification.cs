using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Trader/Notification")]
public class Notification : ScriptableObject
{
    public string header;
    public string body;
    public Sprite icon;
}
