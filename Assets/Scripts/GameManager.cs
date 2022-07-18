using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

/*
    todo:
    -Notification loop
    -oyun kazanma
    -BAZAN, offsetler karışıyor çok kötü ooluyor. DOTween dolayısıyla... (fixed)
*/

public class GameManager : MonoBehaviour
{
    public int maxLevel = 30; 
    public Level level;

    float timeToNextCoin;

    public GameObject coinPanelPrefab;
    public GameObject tutorialCoinPanelPrefab;

    List<Coin> readyCoins;
    public RectTransform coinSpace;

    public float topOffset = -275;
    public float offset = -195;
    public float bottomOffset = -275;

    public Text startPanelText;
    public GameObject winPanel;
    public Image objectiveImage;

    public List<CoinPanel> coinPanels = new List<CoinPanel>();

    public Vector2 timeRange;
    public Vector2 priceRange;
    public Vector2 curveTimeLenght;

    private float money = 1000;

    public AudioSource audioSource;

    IEnumerator coinCoroutine;

    public AnimationCurve curve;

    public float Money
    {
        get 
        {
            return money;
        }
        set
        {
            money = value;
            if (money >= level.targetBalance)
            {
                EndLevel();
            }
        }
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("level")) PlayerPrefs.SetInt("level", 0);
        startPanelText.text = PlayerPrefs.GetInt("level").ToString();
        LoadLevel(PlayerPrefs.GetInt("level"));
    }

    private void Update()
    {

    }

    IEnumerator CoinLoop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //create coinpanel

        float nextTime = Random.Range(timeRange.x, (float)timeRange.y);

        int[] dice = { 0,0,0,0,0,1,1,1,1,2,2,2 };
        int tier = dice[Random.Range(0, dice.Length)];
        float peakPrice = CalculateCoinPrice(tier);

        float peakTime = Random.Range(curveTimeLenght.x, (float)curveTimeLenght.y);

        while(readyCoins.Count == 0)
        {
            yield return null;
        }


       

        CreateCoinPanel(readyCoins[Random.Range(0, readyCoins.Count)], peakPrice, peakTime, tier);


        coinCoroutine = CoinLoop(nextTime);
        StartCoroutine(coinCoroutine);
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

        readyCoins = new List<Coin>(level.coins);
        priceRange = new Vector2(level.startingBalance, level.targetBalance/5);
        Money = level.startingBalance;

        objectiveImage.sprite = level.targetImage;

        Debug.Log("Level " + lvindex + " loaded.");
    }

    public void StartLevel()
    {
        coinCoroutine = CoinLoop(0);
        StartCoroutine(coinCoroutine);
    }

    public void EndLevel()
    {
        winPanel.SetActive(true);
        coinSpace.DOMoveX(-1500, 0.5f).SetEase(Ease.InQuad);
        int levelIndex = PlayerPrefs.GetInt("level");
        
        if(levelIndex < maxLevel) PlayerPrefs.SetInt("level", levelIndex + 1);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CreateCoinPanel(Coin coin, float peakPrice, float curveTime, int tier)
    {
        GameObject newCoinPanelObj = (PlayerPrefs.GetInt("level") != 0) ?  
            Instantiate(coinPanelPrefab, coinSpace.transform):
            Instantiate(tutorialCoinPanelPrefab, coinSpace.transform);

        newCoinPanelObj.GetComponent<RectTransform>().localPosition += new Vector3(0, topOffset + coinPanels.Count * offset); //localposition is the position relative to parent

        newCoinPanelObj.transform.localScale = Vector3.zero;
        newCoinPanelObj.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutElastic, 0.1f, 0.5f);

        CoinPanel newCoinPanel = newCoinPanelObj.GetComponent<CoinPanel>();

        newCoinPanel.Init(coin, peakPrice, curveTime, tier);
        coinPanels.Add(newCoinPanel);

        coinSpace.sizeDelta = new Vector3(coinSpace.sizeDelta.x, -(coinPanels.Count-1) * offset - bottomOffset - topOffset);

        readyCoins.Remove(coin);
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
            if(coinPanels[i])
            {
                coinPanels[i].GetComponent<RectTransform>().DOLocalMoveY(topOffset + i * offset, 0.3f).SetEase(Ease.InOutQuad);
            }
        }

        readyCoins.Add(coinPanel.coin);
    }

    float CalculateCoinPrice(int tier)
    {
        float min = level.startingBalance;
        float max = level.targetBalance * 0.2f;

        if (tier == 0)
        {
            return Random.Range(min, LerpExp(min, max, 0.2f));
        }
        else if (tier == 1)
        {
            return Random.Range(LerpExp(min, max, 0.5f), LerpExp(min, max, 0.7f)) / 0.34f;
        }
        else if (tier == 2)
        {
            return Random.Range(LerpExp(min, max, 0.8f), max) / 0.34f;
        }
        return min;
    }

    float LerpExp(float a, float b, float t)
    {
        return a + curve.Evaluate(t) * (b - a);
    }
}
