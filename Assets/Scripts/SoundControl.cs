using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.interactable = true;
        if (PlayerPrefs.HasKey("sound"))
        {
            toggle.isOn = (PlayerPrefs.GetInt("sound") != 1);
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void SetAudio(bool isOff)
    {
        AudioListener.pause = isOff;
        string onOff = isOff? "off." : "on.";
        Debug.Log("Audio is "+ onOff);

        PlayerPrefs.SetInt("sound", (isOff ? 0 : 1));
    }



}
