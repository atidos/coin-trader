using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    RectTransform rtBar;
    public RectTransform rtFill;
    public float objective;
    public float money;

    float targetValue=0;
    float currentValue;
    float vel;
    public float smoothTime = 0.2f;

    private void Start()
    {
        rtBar = GetComponent<RectTransform>();
    }


    void Update()
    {
        float ratio = money / objective;


        targetValue = -Mathf.Lerp(rtBar.rect.width, 0, ratio);
        currentValue = Mathf.SmoothDamp(rtFill.offsetMax.x, targetValue, ref vel, smoothTime);

        rtFill.offsetMax = new Vector2(currentValue, rtFill.offsetMax.y);


            
    }
}
