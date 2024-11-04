using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider DistanceSlider;
    [SerializeField] LineRenderer LineRenderer;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private int curScore= 0;

    private static UiManager instance = null;
    public static UiManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int score)
    {
        curScore += score;
        textMeshProUGUI.text = "Score: " + curScore.ToString();
    }

    public void SetDistanceSlider(float currentDistanceValue)
    {
        DistanceSlider.value = currentDistanceValue;
    }

    public void SetAngleArrow(Vector2 playerPos, Vector2 baitPos)
    {
        if (LineRenderer)
        {
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, playerPos);
            if (baitPos != Vector2.zero) 
                LineRenderer.SetPosition(1, playerPos + baitPos);
        }
    }

    public void StopAngleArrow()
    {
        if (LineRenderer)
        {
            LineRenderer.positionCount = 0;
        }
    }

    public void ShowSlider()
    {
        DistanceSlider.gameObject.SetActive(true);
    }    
    public void HideSlider()
    {
        DistanceSlider.gameObject.SetActive(false);
    }
}
