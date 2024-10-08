using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PlayerInput PlayerInput;
    int fishing_input;

    void OnUp(InputValue value)
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
        fishing_input = 1;
        InputProcess();

        }
    }

    void OnDown(InputValue value)
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            fishing_input = 2;
            InputProcess();

        }
    }


    void OnLeft(InputValue value)
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            fishing_input = 3;
            InputProcess();

        }
    }

    void OnRight(InputValue value)
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            fishing_input = 4;
            InputProcess();
        }
    }



    void InputProcess()
    {
        if (GameManager.Instance.noteQueue.Count > 0) {

            GameObject curr_note = GameManager.Instance.noteQueue[0];
            if (curr_note != null && curr_note.GetComponent<Note>().Lifetime >= curr_note.GetComponent<Note>().Lifespan-1.5) {

                if (curr_note.GetComponent<Note>().NoteType != fishing_input)
                {
                    Debug.Log("Ahah t'es nul");
                    GameManager.Instance.noteQueue.RemoveAt(0);
                    Destroy(curr_note.gameObject);
                }
                else
                {
                    Debug.Log("Ok");
                    GameManager.Instance.noteQueue.RemoveAt(0);
                    Destroy(curr_note.gameObject);
                }

            }
        }
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
