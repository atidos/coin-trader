using UnityEngine;
using UnityEngine.UI;
//TODO: add button fnc & curve manuplation parameters
public class CoinPanel : MonoBehaviour
{
    public Button button;
    public Image indicatorImage;
    public Text priceText;
    public Text coinText;
    public Image coinIcon;
    public AnimationCurve curve;

    public Sprite indicatorUp;
    public Sprite indicatorDown;
    public Color upColor;
    public Color downColor;

    float previous = 0; //price in previous frame
    float current = 0; //price in current frame
    float price = 0; //price after calculating with priceConstant
    int priceConstant = 1000; // value to multiply with current price
    float timeConstant = 50; // value to divide current position on curve, helps determining the lifetime of the coin/curve

    float initTime;

    public GameManager gameManager;

    void Start()
    {
        initTime = Time.time;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    
    public void Init(Coin coin, int pCons, float tCons)
    {
        coinText.text = coin.coinName;
        coinIcon.sprite = coin.icon;
        priceConstant = pCons;
        timeConstant = tCons;
    }

    void Update()
    {
        current = curve.Evaluate((Time.time-initTime)/timeConstant); //calculates the current price using the curve and timeConstant
        price = current * priceConstant; 
        priceText.text = Mathf.Floor(price).ToString() + "$"; //this and the above line shape the price tag

        if (current < previous && indicatorImage.sprite != indicatorDown) //checks if the price started dropping or not
        {
            indicatorImage.sprite = indicatorDown; //if price is dropping change the color to red
            priceText.color = downColor;
        }
        else if(current > previous && indicatorImage.sprite != indicatorUp) //checks if the price started dropping or not
        {
            indicatorImage.sprite = indicatorUp; //if price is dropping change the color to red
            priceText.color = upColor;
        }

        previous = current;
    }

    public void Buy()
    {
        gameManager.RemoveCoinPanel(this);
    }
}