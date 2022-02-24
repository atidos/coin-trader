using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    public Button button;
    public Image indicatorImage;
    public Text priceText;
    public Text coinText;
    public Image coinIcon;
    public AnimationCurve curve;

    float previous = 0; //price in previous frame
    float current = 0; //price in current frame
    float price = 0; //price after calculating with priceConstant
    int priceConstant = 1000; // value to multiply with current price
    int timeConstant = 10; // value to divide current position on curve, helps determining the lifetime of the coin/curve

    void Start()
    {
        indicatorImage.color = Color.green; 
    }
    
    public void Init( Text cText , Image cImage, AnimationCurve aCurve,int pCons, int tCons)
    {
        //TODO: find a way to get partial inputs in Init function and take ungiven values as defaults above.
        coinText = cText;
        coinIcon = cImage;
        curve = aCurve;
        priceConstant = pCons;
        timeConstant = tCons;
    }
    
    void Update()
    {
        current = curve.Evaluate(Time.time/timeConstant); //calculates the current price using the curve and timeConstant
        price = current * priceConstant; 
        priceText.text = Mathf.Floor(price).ToString(); //this and the above line shape the price tag

        if (current<previous) //checks if the price started dropping or not
        {
            indicatorImage.color = Color.red; //if price is dropping change the color to red
        }

        previous = current;
    }
}