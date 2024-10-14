using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishingSpotPrefab;
    private GameObject fishingSpot;

    [SerializeField]
    private GameState gameState = GameState.MENU;


    private float fishingSpotCooldown;
    [SerializeField] private float maxFishingSpotCooldown;

    private float noteCooldown;
    [SerializeField] private float maxNoteCooldown;
    [SerializeField] private float noteSpeed;
    [SerializeField] private int damage;

    public int Damage {  get { return damage; } set { damage = value; } }

    [SerializeField] private List<GameObject> notePrefabList;
    [SerializeField] private GameObject noteLinePrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject healthBarPrefab;

    [SerializeField] private Canvas canvas;
    private GameObject spawner;
    public GameObject target;

    public GameState GameState { get { return gameState; } set { gameState = value; } }

    public List<GameObject> noteQueue = new List<GameObject>();

    private static GameManager instance = null;
    public static GameManager Instance => instance;


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


        fishingSpotCooldown = maxFishingSpotCooldown;
        GameObject noteLine = Instantiate(noteLinePrefab, canvas.transform);
        spawner = noteLine.transform.GetChild(0).gameObject;
        target = Instantiate(targetPrefab, canvas.transform);
        Instantiate(healthBarPrefab, canvas.transform);

    }


    private void Update()
    {
        if (gameState == GameState.MENU)
        {
            fishingSpotCooldown -= Time.deltaTime;
            if (fishingSpotCooldown < 0)
            {
                fishingSpotCooldown = maxFishingSpotCooldown;
                DestroyOldSpot();
                fishingSpot = GetNextSpot();
            }
        }
        else if (gameState == GameState.FISHING)
        {

            noteCooldown -= Time.deltaTime;
            if (noteCooldown < 0)
            {
                noteCooldown = maxNoteCooldown;
                int noteType = Random.Range(0, 4);
                GameObject new_note = Instantiate(notePrefabList[noteType], spawner.transform);
                new_note.GetComponent<Note>().Speed = noteSpeed;

                noteQueue.Add(new_note);

            }

        }

        
    }

    private GameObject GetNextSpot()
    {
        return Instantiate(fishingSpotPrefab, new Vector3(Random.Range(-5,5),Random.Range(0,5),0), Quaternion.identity);
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

    public Vector3 GetCurrentFishingSpotPosition()
    {
        return fishingSpot.transform.position;
    }

}
