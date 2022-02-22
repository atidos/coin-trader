using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    RectTransform rt;
    public float objective;
    public float money;


    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = objective / money;

        //rt.right = Mathf.Lerp(rt.rect.width, 0, ratio);
    }
}
