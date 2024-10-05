using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider DistanceSlider;
    [SerializeField] LineRenderer LineRenderer;

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

    public void SetAngleArrow(Vector2 playerPos, Vector2 angle)
    {
        if (LineRenderer)
        {
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, playerPos);
            if (angle != Vector2.zero) 
                LineRenderer.SetPosition(1, playerPos + angle/ Mathf.Sqrt(angle.x * angle.x + angle.y * angle.y) * 20);
        }
    }

    public void StopAngleArrow()
    {
        if (LineRenderer)
        {
            LineRenderer.positionCount = 0;
        }
    }
}
