using System.Collections;
using System.Collections.Generic;
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

    
    void Start()
    {
        indicatorImage.color = Color.green;
    }
    
    public void Init(Text pText , Text cText , Image cImage)
    {
        priceText = pText;
        coinText = cText;
        coinIcon = cImage;
    }

    void Update()
    {

        if (false)
        {
            indicatorImage.color = Color.red;
        }
    }
}
