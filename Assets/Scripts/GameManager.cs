using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    todo:
    -CoinLoop and Notification loop
    -dynamic coinSpace adjustment
    -bi coin çıkarınca sonrakilerin yukarı gelmesi
    -buy/sell mekaniği (coinpanel ile beraber)
    -objective bar kontrolü
    -oyun kazanma
*/

public class GameManager : MonoBehaviour
{
    Level level;

    float timeToNextCoin;

    public GameObject coinPanelPrefab;

    List<Coin> readyCoins;
    public RectTransform coinSpace;

    public float topOffset = -275;
    public float offset = -195;
    public float bottomOffset = -275;

    public List<CoinPanel> coinPanels = new List<CoinPanel>();

    private void Start()
    {
        LoadLevel(1);
        readyCoins = new List<Coin>(level.coins);
        CreateCoinPanel(readyCoins[0]);
        CreateCoinPanel(readyCoins[1]);
        CreateCoinPanel(readyCoins[2]);
        CreateCoinPanel(readyCoins[3]);
        CreateCoinPanel(readyCoins[4]);
    }

    private void Update()
    {

    }

    IEnumerator CoinLoop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //create coinpanel
        StartCoroutine(CoinLoop(0));
    }

    IEnumerator NotificationLoop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //create coinpanel
        StartCoroutine(CoinLoop(0));
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
        GameObject newCoinPanelObj = Instantiate(coinPanelPrefab, coinSpace.transform);

        newCoinPanelObj.GetComponent<RectTransform>().localPosition += new Vector3(0, topOffset + coinPanels.Count * offset); //localposition is the position relative to

        CoinPanel newCoinPanel = newCoinPanelObj.GetComponent<CoinPanel>();
        newCoinPanel.Init(coin, 1000, 10);
        coinPanels.Add(newCoinPanel);
    }

    void GenerateNotification(Notification notifs)
    {
        
    }

    
}
