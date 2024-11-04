using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FishingRod : MonoBehaviour
{
    private static FishingRod instance = null;
    public static FishingRod Instance => instance;

    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private Sprite fishing;
    [SerializeField] private Sprite standing;

    [SerializeField] Bait bait;
    Vector2 throwAngle;
    float throwDistance = 0.0f;
    int cursorDirection = 1;
    bool isAimingDone = false;
    bool isAimingRight = false;
    bool isAimingLeft = false;
    float velocity = Mathf.PI/500;
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
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            SR.sprite = fishing;
            UiManager.Instance.SetAngleArrow(transform.position + new Vector3(0.5f, 1.4f), new Vector2(-0.5f,- 1.4f) + throwDistance * 10 * throwAngle / Mathf.Sqrt(throwAngle.x * throwAngle.x + throwAngle.y * throwAngle.y));


        }
        else
        {
            SR.sprite = standing;
            UiManager.Instance.StopAngleArrow();
        }

            if (GameManager.Instance.GameState == GameState.ANGULARSELECTION)
        {
            isAimingDone = false;
            if (Gamepad.current != null)
            {
                Vector2 gamepadValue = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
                if (gamepadValue.x != 0 && gamepadValue.y != 0)
                    throwAngle = gamepadValue;
            }

            UiManager.Instance.SetAngleArrow(transform.position, throwAngle / Mathf.Sqrt(throwAngle.x * throwAngle.x + throwAngle.y * throwAngle.y) * 20);
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
            UiManager.Instance.StopAngleArrow();
            bait.gameObject.SetActive(true);
            bait.MoveFromTo(transform.position, throwDistance, throwAngle);
            isAimingDone =true;
        }
        else
        {
            //UiManager.Instance.StopAngleArrow();
            UiManager.Instance.HideSlider();
            //isAimingDone = true;
        }
        //Mathf.PI 

        if (isAimingLeft && !isAimingRight &&!isAimingDone && radian < Mathf.PI * 3/2 - velocity )
        {
            radian += velocity;
            throwAngle +=  Mathf.Sign(radian- Mathf.PI/2 + velocity) * new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        else if (isAimingRight && !isAimingLeft && !isAimingDone && radian > -Mathf.PI/ 2 + velocity)
        {
            radian -= velocity;
            throwAngle -= Mathf.Sign(radian - Mathf.PI / 2 - velocity) * new Vector2(Mathf.Cos(radian ),Mathf.Sin(radian));
        }
    }

    public void Aim()
    {
        isAimingDone = false;
        throwAngle = new Vector2(0, 1);
        radian = Mathf.PI / 2;
        isAimingRight = false;
        isAimingLeft = false;

    }

    public void SelectAngle(int value)
    {
        if (value == -1)
        {
            isAimingRight = !isAimingRight;
            isAimingLeft = false;
        }
        else if (value == 1)
        {
            isAimingLeft = !isAimingLeft;
            isAimingRight = false;
        }
    }



}
