using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    public Sprite buyButtonImage;
    public Sprite buyButtonDisabledImage;
    public Sprite sellButtonImage;
    public Color upColor;
    public Color downColor;

    float previous = 0; //price in previous frame
    float current = 0; //price in current frame
    float price = 0; //price after calculating with priceConstant
    int priceConstant = 1000; // value to multiply with current price
    float timeConstant = 50; // value to divide current position on curve, helps determining the lifetime of the coin/curve

    float initTime;

    bool purchased = false;


    void Start()
    {
        initTime = Time.time;
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

        if((Time.time - initTime) / timeConstant > 1)
        {
            GameManager.Instance.RemoveCoinPanel(this);
        }

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

        if(!purchased)
        {
            if(GameManager.Instance.Money < price)
            {
                if(button.image.sprite != buyButtonDisabledImage)
                    button.image.sprite = buyButtonDisabledImage;
            }
            else 
            {
                if (button.image.sprite != buyButtonImage)
                    button.image.sprite = buyButtonImage;
            }
        }

        previous = current;
    }

    public void BuySell()
    {
        if(!purchased) //not purchased yet
        {
            if(GameManager.Instance.Money >= price)
            {
                GameManager.Instance.Money -= (int)price;
                purchased = true;
                button.image.sprite = sellButtonImage;
            }
            else
            {
                button.GetComponent<RectTransform>().DOShakePosition(0.5f, 25f);
            }
        }
        else
        {
            GameManager.Instance.Money += (int)price;
            GameManager.Instance.RemoveCoinPanel(this);
        }
    }
}