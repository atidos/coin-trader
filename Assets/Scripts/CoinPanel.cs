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
    public Image frameImage;
    public AnimationCurve curve;

    public Sprite indicatorUp;
    public Sprite indicatorDown;
    public Sprite buyButtonImage;
    public Sprite buyButtonDisabledImage;
    public Sprite sellButtonImage;
    public Sprite goldFrame;
    public Sprite diamondFrame;
    public Color upColor;
    public Color downColor;

    public AudioClip[] buySounds;
    public AudioClip[] sellSounds;
    public AudioClip[] notSounds;

    public Coin coin;

    float previous = 0; //price in previous frame
    float current = 0; //price in current frame
    float price = 0; //price after calculating with priceConstant
    float priceConstant = 1000; // value to multiply with current price
    float timeConstant = 50; // value to divide current position on curve, helps determining the lifetime of the coin/curve

    float initTime;

    bool purchased = false;


    void Start()
    {
        initTime = Time.time;
    }
    
    public void Init(Coin coin, float pCons, float tCons, int tier)
    {
        coinText.text = coin.coinName;
        coinIcon.sprite = coin.icon;
        priceConstant = pCons;
        timeConstant = tCons;
        this.coin = coin;

        if (tier == 0)
        {
            frameImage.enabled = false;
        }
        else if (tier == 1)
        {
            frameImage.sprite = goldFrame;
        }
        else if(tier == 2)
        {
            frameImage.sprite = diamondFrame;
        }
    }

    void Update()
    {
        current = curve.Evaluate((Time.time-initTime)/timeConstant); //calculates the current price using the curve and timeConstant

        if((Time.time - initTime) / timeConstant > 1)
        {
            GameManager.Instance.RemoveCoinPanel(this);
        }

        price = current * priceConstant; 
        priceText.text = Utils.AbbrevationUtility.AbbreviateNumber(price) + "$"; //this and the above line shape the price tag

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
                GameManager.Instance.Money -= price;
                purchased = true;
                button.image.sprite = sellButtonImage;

                if(buySounds.Length > 0)
                    GameManager.Instance.audioSource.PlayOneShot(buySounds[Random.Range(0, buySounds.Length - 1)], 0.2f);
            }
            else
            {
                transform.DOShakePosition(0.3f, 10);

                if (notSounds.Length > 0)
                    GameManager.Instance.audioSource.PlayOneShot(notSounds[Random.Range(0, notSounds.Length - 1)], 0.2f);
            }
        }
        else
        {
            GameManager.Instance.Money += price;
            GameManager.Instance.RemoveCoinPanel(this);

            if (sellSounds.Length > 0)
                GameManager.Instance.audioSource.PlayOneShot(sellSounds[Random.Range(0, sellSounds.Length - 1)], 0.2f);
        }
    }
}