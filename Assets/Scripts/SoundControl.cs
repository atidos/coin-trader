using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{


    public void SetAudio(bool isOff)
    {
        AudioListener.pause = isOff;
        string onOff=isOff?"off.": "on.";
        Debug.Log("Audio is "+ onOff);
    }



}
