using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Trader/Coin")]
public class Coin : ScriptableObject
{
    public string coinName;
    public string description;
    public Sprite icon;
}
