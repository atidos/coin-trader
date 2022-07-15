using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    RectTransform rtBar;
    public RectTransform rtFill;
    public Text objectiveText;

    float targetValue=0;
    float currentValue;
    float vel;
    public float smoothTime = 0.2f;

    public AnimationCurve curve;

    private void Start()
    {
        rtBar = GetComponent<RectTransform>();
        objectiveText.text = GameManager.Instance.level.targetBalance.ToString() + "$";
    }

    void Update()
    {
        float ratio = (float)GameManager.Instance.Money / GameManager.Instance.level.targetBalance;

        targetValue = -LerpExp(rtBar.rect.width, 0, ratio);
        currentValue = Mathf.SmoothDamp(rtFill.offsetMax.x, targetValue, ref vel, smoothTime);

        rtFill.offsetMax = new Vector2(currentValue, rtFill.offsetMax.y);
    }

    float LerpExp(float a, float b, float t)
    {
        return a + curve.Evaluate(t) * (b - a);
    }
}
