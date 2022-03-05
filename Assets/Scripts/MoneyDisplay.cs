using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    Text moneyText;

    private void Start()
    {
        moneyText = GetComponent<Text>();
    }

    void Update()
    {
        int money = GameManager.Instance.Money;
        moneyText.text = money.ToString() + "$";
    }
}
