using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour
{

    [SerializeField]
    public float fishHealth;
    public int difficulty;
    public int noteSpeed;
    public float noteCooldown;

    [SerializeField]
    public string fishName1;
    public string fishName2;


    public string getFish(int rarityTier)
    {
        FishHealthBar.Instance.Init(fishHealth, difficulty);
        GameManager.Instance.NoteSpeed = noteSpeed;
        GameManager.Instance.MaxNoteCooldown = noteCooldown;
        if (rarityTier == 1)
        {
            
            return fishName1;
        }
    return fishName2; 
    }
    
}
