using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int fishHealth;
    public string fishName;
    public int difficulty;
    public int noteSpeed;
    public float noteCooldown;
    [SerializeField] private Sprite sprite;


    public Fish GetFish()
    {
        return this;
    }
}
