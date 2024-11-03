using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishHealthBar : MonoBehaviour
{


    private Slider slider;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private int difficulty;

    private int ErrorCounter = 0; 

    public float MaxHealth {  get { return maxHealth; } set { maxHealth = value; } }
    public float Health { get; set; }

    private static FishHealthBar instance = null;
    public static FishHealthBar Instance => instance;

    public void IncrementErrorCounter()
    {

    ErrorCounter++; 
    }

    public int GetErrorCounter() { return ErrorCounter; }

    public void HealthUpdate(float value)
    {
        Health += value * GameManager.Instance.Damage * (1 - 0.2f * (difficulty - 1));
    }


    public void Init(float healthPoints, int diff)
    {
        maxHealth = healthPoints;
        difficulty = diff;
        slider = GetComponent<Slider>();
        slider.maxValue = maxHealth;
        slider.minValue = 0;
        Health = 0.4f * maxHealth;
        
    }
    
    // Start is called before the first frame update
    void Awake()
    {

        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Health -= Time.deltaTime*2;
        slider.value = Health;
        
    }
}
