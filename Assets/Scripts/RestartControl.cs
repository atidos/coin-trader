using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RestartControl : MonoBehaviour
{
    int level = 0;
    bool levelSelect = false;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, -720), 1f, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ActivateLevelSelect()
    {
        levelSelect = true;
    }
    public void DeactivateLevelSelect()
    {
        levelSelect = false;
    }
    public void SelectLevel()
    {
        if (!levelSelect) return;
        PlayerPrefs.SetInt("level", level);
        level++;
    }
}

