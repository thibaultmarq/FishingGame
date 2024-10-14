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



    public float MaxHealth {  get { return maxHealth; } set { maxHealth = value; } }
    public float Health { get; set; }

    private static FishHealthBar instance = null;
    public static FishHealthBar Instance => instance;



    public void HealthUpdate(float value)
    {
        Health += value * GameManager.Instance.Damage * (1 - 0.2f * (difficulty - 1));
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


        slider = GetComponent<Slider>();
        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = 0.4f*maxHealth;
        Health = slider.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Health;
        
    }
}
