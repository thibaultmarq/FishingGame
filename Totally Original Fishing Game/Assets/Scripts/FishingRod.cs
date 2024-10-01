using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FishingRod : MonoBehaviour
{
    private static FishingRod instance = null;
    public static FishingRod Instance => instance;

    [SerializeField] Bait bait;
    Vector2 throwAngle;
    float throwDistance = 0.0f;
    int cursorDirection = 1;
    bool isAimingDone = false;

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

    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.ANGULARSELECTION)
        {
            throwAngle = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
            UiManager.Instance.SetAngleArrow(transform.position, throwAngle);
            throwDistance = 0.0f;
        }
        else if (GameManager.Instance.GameState == GameState.DISTANCESELECTION)
        {
            throwDistance += cursorDirection * Time.deltaTime;
            if (throwDistance <= 0.0f || throwDistance >= 1.0f)
            {
                cursorDirection *= -1;
            }
            UiManager.Instance.SetDistanceSlider(throwDistance);
        } else if (GameManager.Instance.GameState == GameState.FISHING && !(isAimingDone))
        {
            Debug.Log(throwAngle);
            Debug.Log(throwDistance);
            Vector2 newPosition = new Vector2(throwDistance *10* throwAngle.x, throwDistance*10 * throwAngle.y);
            UiManager.Instance.StopAngleArrow();
            bait.gameObject.SetActive(true);
            bait.MoveFromTo(transform.position, newPosition);
            isAimingDone =true;
        }

    }

    public void Aim()
    {
        isAimingDone = false;

    }
}
