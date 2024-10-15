using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] float leniency;
    [SerializeField] GameObject target;
    int fishing_input;

    private static InputManager instance = null;
    public static InputManager Instance => instance;

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




    private void Start()
    {
        target = GameManager.Instance.target;
        target.GetComponent<RectTransform>().sizeDelta = new Vector2(leniency*100,leniency * 100);
    }

    void OnUp()
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
        fishing_input = 1;
        InputProcess();

        }
    }

    void OnDown()
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            fishing_input = 2;
            InputProcess();

        }
    }


    void OnLeft()
    {
        if (GameManager.Instance.GameState == GameState.FISHING)
        {
            fishing_input = 3;
            InputProcess();

        }
    }

    void OnRight()
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

            Note curr_note = GameManager.Instance.noteQueue[0].GetComponent<Note>();


            if (curr_note.Lifetime >= curr_note.Lifespan-leniency) {

                if (curr_note.NoteType != fishing_input)
                {
                    Debug.Log("Ahah t'es nul");
                    FishHealthBar.Instance.HealthUpdate(-2);
                    
                }
                else if (curr_note.Lifetime >= curr_note.Lifespan - 0.5f*leniency &&
                    curr_note.Lifetime <= curr_note.Lifespan-0.25f*leniency)
                {
                    Debug.Log("Perfect !");
                    FishHealthBar.Instance.HealthUpdate(1);
                    
                }
                else
                {
                    Debug.Log("Ok");
                    FishHealthBar.Instance.HealthUpdate(0.5f);
                    
                }

            }
            else
            {
                Debug.Log("Trop tôt !");
                FishHealthBar.Instance.HealthUpdate(-1);
            }

               curr_note.Disposal();
        }
    }



    void OnStartAiming()
    {
        Debug.Log("OnStartAiming");
        GameManager.Instance.NextGameState();
        FishingRod.Instance.Aim();
        PlayerInput.SwitchCurrentActionMap("Aiming");
        
    }

    void OnValidateAction()
    {
        Debug.Log("OnValidateAngle");
        GameManager.Instance.NextGameState();
    }

    void OnChangeAngleRight()
    {
        FishingRod.Instance.SelectAngle(-1);
    }
    void OnChangeAngleLeft()
    {
        FishingRod.Instance.SelectAngle(1);
    }

    public void ChangePlayerInput(string playerInput)
    {
        PlayerInput.SwitchCurrentActionMap(playerInput);
    }
}
