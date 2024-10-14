using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour
{

    [SerializeField]
    public float fishHealth;

    [SerializeField]
    public string fishName1;
    public string fishName2;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getFish(int rarityTier)
    {
        if (rarityTier == 1)
        {
            return fishName1;
        }
    return fishName2; 
    }
    
}
