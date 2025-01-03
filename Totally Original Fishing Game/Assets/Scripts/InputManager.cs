using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] float leniency;
    [SerializeField] GameObject target;
    int fishing_input;

    [SerializeField] private GameObject Controls;

    private static InputManager instance = null;
    public static InputManager Instance => instance;

    private void Awake()
    {
        //Thread.Sleep(500);
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
        target.GetComponent<RectTransform>().sizeDelta = new Vector2(leniency * 18,leniency *18);
        target.transform.position.Set(leniency*27f,0,0);
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

    void OnQuitGame()
    {
        Application.Quit();
    }

    void OnOpenControls()
    {
        Controls.SetActive(true);
        PlayerInput.SwitchCurrentActionMap("Controls");
        
    }

    void OnQuitControls()
    {
        Controls.SetActive(false);
        
        PlayerInput.SwitchCurrentActionMap("Menu");
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

            if (curr_note.Lifetime >= curr_note.Lifespan*(1- leniency/100)) {


                if (curr_note.NoteType != fishing_input)
                {
                    
                    FishHealthBar.Instance.HealthUpdate(-2);
                    curr_note.Disposal();
                    FishHealthBar.Instance.IncrementErrorCounter();

                }
                else if (curr_note.Lifetime >= curr_note.Lifespan * (1 - 0.5f*leniency/100) &&
                    curr_note.Lifetime <= curr_note.Lifespan * (1 - 0.25f * leniency / 100))
                {
                    
                    FishHealthBar.Instance.HealthUpdate(1);
                    RumbleManager.Instance.RumblePulse(0.25f, 1f, 2f);
                    curr_note.Disposal();
                    AudioManager.Instance.PlayNote(fishing_input - 1);

                }
                else
                {
                    
                    FishHealthBar.Instance.HealthUpdate(0.5f);
                    curr_note.Disposal();
                    AudioManager.Instance.PlayNote(fishing_input);
                }

            }
            else if (curr_note.Lifetime >= curr_note.Lifespan* (1 - leniency/50))
            {
                
                FishHealthBar.Instance.HealthUpdate(-1);
                curr_note.Disposal();
            }

        }
    }



    void OnStartAiming()
    {
        
        GameManager.Instance.NextGameState();
        FishingRod.Instance.Aim();
        
    }

    void OnOpenInventory()
    {
        InventoryManager.Instance.gameObject.SetActive(!InventoryManager.Instance.gameObject.activeSelf);
    }

    void OnValidateAction()
    {
       
        if (!(GameManager.Instance.GameState > GameState.DISTANCESELECTION))
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

    private void Update()
    {
        if (PlayerInput.currentActionMap.name == "Controls")
        {
            return;
        }
        if (GameManager.Instance.GameState == GameState.MENU)
        {
            PlayerInput.SwitchCurrentActionMap("Menu");
        }
        else if (GameManager.Instance.GameState == GameState.FISHING)
        {
            PlayerInput.SwitchCurrentActionMap("Fishing");
        }

        else
        {
            PlayerInput.SwitchCurrentActionMap("Aiming");
        }


    }
}
