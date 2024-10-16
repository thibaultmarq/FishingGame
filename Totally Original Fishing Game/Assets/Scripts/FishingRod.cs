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
    bool isAimingRight = false;
    bool isAimingLeft = false;
    float velocity = Mathf.PI/1000;
    float radian = Mathf.PI/2;

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
            isAimingDone = false;
            if (Gamepad.current != null)
            {
                Vector2 gamepadValue = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
                if (gamepadValue.x != 0 && gamepadValue.y != 0)
                    throwAngle = gamepadValue;
            }

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
            UiManager.Instance.ShowSlider();
            UiManager.Instance.SetDistanceSlider(throwDistance);
        } else if (GameManager.Instance.GameState == GameState.PAUSE && !(isAimingDone))
        {
            UiManager.Instance.HideSlider();
            Debug.Log(throwAngle);
            Debug.Log(throwDistance);
            UiManager.Instance.StopAngleArrow();
            bait.gameObject.SetActive(true);
            bait.MoveFromTo(transform.position, throwDistance, throwAngle);
            isAimingDone =true;
        }
        else
        {
            UiManager.Instance.StopAngleArrow();
            UiManager.Instance.HideSlider();
            //isAimingDone = true;
        }
        //Mathf.PI 

        if (isAimingLeft && !isAimingRight && radian < Mathf.PI * 3/2 - velocity )
        {
            radian += velocity;
            throwAngle +=  Mathf.Sign(radian- Mathf.PI/2 + velocity) * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        else if (isAimingRight && !isAimingLeft && radian > -Mathf.PI/ 2 + velocity)
        {
            radian -= velocity;
            throwAngle -= Mathf.Sign(radian - Mathf.PI / 2 - velocity) * new Vector2(Mathf.Cos(radian ),Mathf.Sin(radian));
        }
    }

    public void Aim()
    {
        isAimingDone = false;
        throwAngle = new Vector2(0, 1);

    }

    public void SelectAngle(int value)
    {
        if (value == -1)
            isAimingRight = !isAimingRight;
        else if (value == 1)
            isAimingLeft = !isAimingLeft;
    }



}
