using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public Vector2 timeRange;
    public Vector2Int priceRange;
    public Vector2 curveTimeLenght;

    private void Start()
    {
        LoadLevel(1);
        readyCoins = new List<Coin>(level.coins);

        StartCoroutine(CoinLoop(0));
    }

    private void Update()
    {

    }

    IEnumerator CoinLoop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //create coinpanel

        float nextTime = Random.Range(timeRange.x, (float)timeRange.y);
        int peakPrice = Random.Range(priceRange.x, priceRange.y);
        float peakTime = Random.Range(curveTimeLenght.x, (float)curveTimeLenght.y);

        CreateCoinPanel(readyCoins[Random.Range(0, readyCoins.Count)], peakPrice, peakTime);

        StartCoroutine(CoinLoop(nextTime));
    }

    IEnumerator NotificationLoop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //create coinpanel
        StartCoroutine(NotificationLoop(0));
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

    void CreateCoinPanel(Coin coin, int peakPrice, float curveTime)
    {
        GameObject newCoinPanelObj = Instantiate(coinPanelPrefab, coinSpace.transform);

        newCoinPanelObj.GetComponent<RectTransform>().localPosition += new Vector3(0, topOffset + coinPanels.Count * offset); //localposition is the position relative to parent

        newCoinPanelObj.transform.localScale = Vector3.zero;
        newCoinPanelObj.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutElastic, 0.1f, 0.5f);

        CoinPanel newCoinPanel = newCoinPanelObj.GetComponent<CoinPanel>();
        newCoinPanel.Init(coin, peakPrice, curveTime);
        coinPanels.Add(newCoinPanel);

        coinSpace.sizeDelta = new Vector3(coinSpace.sizeDelta.x, -(coinPanels.Count-1) * offset - bottomOffset - topOffset);
    }

    void GenerateNotification(Notification notifs)
    {
        
    }

    public void RemoveCoinPanel(CoinPanel coinPanel)
    {
        int index = coinPanels.IndexOf(coinPanel);
        coinPanels.Remove(coinPanel);
        Destroy(coinPanel.gameObject);

        for(int i = index; i < coinPanels.Count; i++)
        {
            coinPanels[i].GetComponent<RectTransform>().DOLocalMoveY(coinPanels[i].GetComponent<RectTransform>().localPosition.y - offset, 0.3f).SetEase(Ease.InOutQuad);
        }
    }
}
