using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<Fish, int> fishList = new Dictionary<Fish, int>();

    [SerializeField] private GameObject itemSlot;

    [SerializeField] private GameObject fish1;
    [SerializeField] private GameObject fish2;

    private static InventoryManager instance = null;
    public static InventoryManager Instance => instance;

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
        Fish fishe1 = fish1.GetComponent<Fish>();
        Fish fishe2 = fish2.GetComponent<Fish>();

        AddFish(fishe1);
        AddFish(fishe2);
        AddFish(fishe1);
        AddFish(fishe1);
        AddFish(fishe2);
        refreshInventoryItem();
        gameObject.SetActive(false);
    }


    public void AddFish(Fish fish)
    {
        Debug.Log(fish);
        Debug.Log(fish.fishName);
        if (fishList.ContainsKey(fish))
        {
            fishList[fish]++;
        }
        else
        {
            fishList.Add(fish, 1);
        }
        Debug.Log(fishList[fish]);
    }


    private void refreshInventoryItem()
    {
        int x = 0;
        int y = 0;
        float itemSlotSize = 5f;
        foreach (Fish fish in fishList.Keys)
        {
            Debug.Log(this.transform.position);
            GameObject curItemSlot = Instantiate(itemSlot,this.transform);
            curItemSlot.transform.position += new Vector3(x * itemSlotSize, y * itemSlotSize);
            curItemSlot.SetActive(true);

            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }
}
