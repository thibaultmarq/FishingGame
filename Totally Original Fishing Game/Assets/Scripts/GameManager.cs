using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishingSpotPrefab;
    private GameObject fishingSpot;
    private FishingSpot fishingSpotFunctions;

    [SerializeField] private GameObject bait;
    [SerializeField] private GameObject player;


    private float timer = 0f;

    [SerializeField] private GameObject VictoryImage;

    [SerializeField]
    private GameState gameState = GameState.MENU;


    private float fishingSpotCooldown;
    [SerializeField] private float maxFishingSpotCooldown;

    private float noteCooldown;
    [SerializeField] private float maxNoteCooldown;
    public float MaxNoteCooldown { get { return maxNoteCooldown; } set { maxNoteCooldown = value; } }

    [SerializeField] private float noteSpeed;
    public float NoteSpeed {  get { return noteSpeed; } set { noteSpeed = value; } }

    [SerializeField] private int damage;

    public int Damage {  get { return damage; } set { damage = value; } }

    [SerializeField] private List<GameObject> notePrefabList;
    [SerializeField] private GameObject noteLinePrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject healthBarPrefab;

    [SerializeField] private GameObject fish1;
    [SerializeField] private GameObject fish2;
    [SerializeField] private GameObject fish3;
    [SerializeField] private GameObject fish4;
    [SerializeField] private GameObject fish5;


    [SerializeField] private Canvas canvas;
    private GameObject spawner;
    private GameObject noteLine;

    public GameObject target;

    
    public GameState GameState { get { return gameState; } set { gameState = value; } }

    public List<GameObject> noteQueue = new List<GameObject>();

    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private Dictionary<string, int> fishStock = new Dictionary<string,int>();


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


        fishingSpotCooldown = 1;
        noteLine = Instantiate(noteLinePrefab, canvas.transform);

        spawner = noteLine.transform.GetChild(0).gameObject;
        target = Instantiate(targetPrefab, noteLine.transform);
        Instantiate(healthBarPrefab, canvas.transform);

    }


    private void Update()
    {
        if (timer < 0) {
            VictoryImage.SetActive(false);
        }
        else
        {
            foreach (Transform child in VictoryImage.transform)
            {
                if (child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    Color color = child.GetComponent<UnityEngine.UI.Image>().color;
                    color.a -= Time.deltaTime/3;
                    child.GetComponent<UnityEngine.UI.Image>().color = color;
                }
            }
        }
        timer -= Time.deltaTime;

        if (gameState == GameState.FISHING)
        {
            ChangeSceneMenuToFishing();

            noteCooldown -= Time.deltaTime;
            if (noteCooldown < 0)
            {
                noteCooldown = maxNoteCooldown;
                int noteType = Random.Range(0, 4);
                GameObject new_note = Instantiate(notePrefabList[noteType], spawner.transform);
                new_note.GetComponent<Note>().Speed = noteSpeed;

                noteQueue.Add(new_note);

            }
            if (FishHealthBar.Instance.Health <= 0)
            {
              
                foreach (GameObject note in noteQueue)
                {
                    note.GetComponent<Note>().Disposal();
                }
                AudioManager.Instance.PlayAudio(4);


                gameState = GameState.MENU;
            }
            else if(FishHealthBar.Instance.Health >= FishHealthBar.Instance.MaxHealth) 
            {
              
                timer = 2;
                foreach (Transform child in VictoryImage.transform)
                {
                    if (child.GetComponent<UnityEngine.UI.Image>() != null)
                    {
                        Destroy(child.gameObject);
                    }
                }

                foreach (GameObject note in noteQueue)
                {
                    note.GetComponent<Note>().Disposal();
                }
                if (FishHealthBar.Instance.GetErrorCounter() < 3 && bait.GetComponent<Bait>().score == 1)
                {
                    InventoryManager.Instance.AddFish(fish1);
                    AudioManager.Instance.PlayAudio(0);
                    UiManager.Instance.AddScore(1000);
                    
                    Instantiate(fish1, VictoryImage.transform);
                    TextMeshProUGUI text = VictoryImage.GetComponent<RectTransform>().Find("Text").GetComponent<TextMeshProUGUI>();
                    text.text = fish1.GetComponent<Fish>().fishName;
                }
                
                else if (FishHealthBar.Instance.GetErrorCounter() > 3 && bait.GetComponent<Bait>().score == 1)
                {
                    InventoryManager.Instance.AddFish(fish2);
                    AudioManager.Instance.PlayAudio(1);
                    UiManager.Instance.AddScore(600);
                    Instantiate(fish2, VictoryImage.transform);
                    TextMeshProUGUI text = VictoryImage.GetComponent<RectTransform>().Find("Text").GetComponent<TextMeshProUGUI>();
                    text.text = fish2.GetComponent<Fish>().fishName;
                }

                else if (FishHealthBar.Instance.GetErrorCounter() < 3 && bait.GetComponent<Bait>().score == 2)
                {
                    InventoryManager.Instance.AddFish(fish3);
                    AudioManager.Instance.PlayAudio(2);
                    UiManager.Instance.AddScore(800);
                    Instantiate(fish3, VictoryImage.transform);
                    TextMeshProUGUI text = VictoryImage.GetComponent<RectTransform>().Find("Text").GetComponent<TextMeshProUGUI>();
                    text.text = fish3.GetComponent<Fish>().fishName;
                }
                
                else if (FishHealthBar.Instance.GetErrorCounter() > 3 && bait.GetComponent<Bait>().score == 2)

                {
                    InventoryManager.Instance.AddFish(fish4);
                    UiManager.Instance.AddScore(400);
                    Instantiate(fish4,VictoryImage.transform);
                    TextMeshProUGUI text = VictoryImage.GetComponent<RectTransform>().Find("Text").GetComponent<TextMeshProUGUI>();
                    text.text = fish4.GetComponent<Fish>().fishName;
                }

                VictoryImage.SetActive(true);

                RumbleManager.Instance.RumblePulse(1, 4, 2);
                gameState = GameState.MENU;
                FishHealthBar.Instance.Health = 0;
            }

        }
        else if (gameState != GameState.PAUSE)
        {
            ChangeSceneFishingToMenu();

            fishingSpotCooldown -= Time.deltaTime;
            if (fishingSpotCooldown < 0)
            {
                fishingSpotCooldown = maxFishingSpotCooldown;
                DestroyOldSpot();
                fishingSpot = GetNextSpot();
                fishingSpotFunctions = fishingSpot.GetComponent<FishingSpot>();
            }
        }


    }

    private GameObject GetNextSpot()
    {
        return Instantiate(fishingSpotPrefab, new Vector3(Random.Range(-8f,7f),Random.Range(1f,4f),0), Quaternion.identity);
    }


    public void DestroyOldSpot()
    {
        Destroy(fishingSpot);
    }



    public void NextGameState()
    {
        gameState++;
        if (gameState > GameState.FISHING)
        {
            gameState = GameState.MENU;
        }
    }

    public void GoToState(GameState gamestate)
    {
        gameState = gamestate;
    }

    public Vector3 GetCurrentFishingSpotPosition()
    {
        return fishingSpot.transform.position;
    }

    public void StockFish(int rarityTier)
    {
        string fishName = fishingSpotFunctions.getFish(rarityTier);
        if (fishStock.ContainsKey(fishName))
        {
            fishStock[fishName]++;
        } else
        {
            fishStock.Add(fishName, 1);
        }

    }

    public void ChangeSceneFishingToMenu()
    {
        FishHealthBar.Instance.gameObject.SetActive(false); 
        noteLine.SetActive(false);
        target.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void ChangeSceneMenuToFishing()
    {
        target.SetActive(true);
        bait.SetActive(false);
        FishHealthBar.Instance.gameObject.SetActive(true);
        noteLine.SetActive(true);
        //player.GetComponent<SpriteRenderer>().enabled = false;
    }

}


