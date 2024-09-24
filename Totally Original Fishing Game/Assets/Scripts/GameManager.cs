using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fishingSpotPrefab;
    private GameObject fishingSpot;
    private GameState gameState = GameState.MAIN;

    private float fishingSpotCooldown;
    [SerializeField] private float maxFishingSpotCooldown;

    public GameState GameState { get { return gameState; } set { gameState = value; } }



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
    }


    private void Update()
    {
        fishingSpotCooldown -= Time.deltaTime;
        if (fishingSpotCooldown < 0)
        {
            fishingSpotCooldown = maxFishingSpotCooldown;
            DestroyOldSpot();
            fishingSpot = GetNextSpot();
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

}
