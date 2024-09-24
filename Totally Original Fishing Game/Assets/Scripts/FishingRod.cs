using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FishingRod : MonoBehaviour
{
    public void Throw()
    {
        
    }

    public Vector2 DefineThrowAngle()
    {
        Vector2 ThrowAngle = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
        while (GameManager.Instance.GameState == GameState.ANGULARSELECTION)
        {
            ThrowAngle = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
            UiManager.Instance.SetAngleArrow(ThrowAngle);
        }


        return ThrowAngle;
    }

    public float DefineThrowDistance() 
    {
        float currentValue = 0.0f;
        int cursorDirection = 1;
        while (GameManager.Instance.GameState == GameState.DISTANCESELECTION)
        {
            currentValue += cursorDirection * Time.deltaTime;
            if (currentValue <= 0.0f || currentValue >= 1.0f) {
                cursorDirection *= -1;
            }
            UiManager.Instance.SetDistanceSlider(currentValue);
        }
        return currentValue;
    }
}
