using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider DistanceSlider;

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

    public void SetDistanceSlider(float currentDistanceValue)
    {
        DistanceSlider.value = currentDistanceValue;
    }

    public void SetAngleArrow(Vector2 angle)
    {

    }
}
