using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PlayerInput PlayerInput;

    void OnUp(InputValue value)
    {
        Debug.Log("truc");
    }

    void OnDown(InputValue value)
    {
        Debug.Log("bidule");
    }


    void OnLeft(InputValue value)
    {

    }

    void OnRight(InputValue value)
    {

    }



    void InputProcess(int value)
    {

    }


    void OnTestAction(InputValue value)
    {
        Debug.Log(value.Get());
    }

    void OnStartAiming(InputValue value)
    {
        Debug.Log("OnStartAiming");
        GameManager.Instance.NextGameState();
        FishingRod.Instance.Aim();
        PlayerInput.SwitchCurrentActionMap("Aiming");
        
    }

    void OnValidateAction(InputValue value)
    {
        Debug.Log("OnValidateAction");
        GameManager.Instance.NextGameState();
    }

    void OnChangeAngleRight(InputValue value)
    {
        FishingRod.Instance.SelectAngle(-1);
    }
    void OnChangeAngleLeft(InputValue value)
    {
        FishingRod.Instance.SelectAngle(1);
    }

}
