using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<GameObject, int> fishList = new Dictionary<GameObject, int>();

    [SerializeField] private GameObject itemSlot;

    [SerializeField] private GameObject fish1;
    [SerializeField] private GameObject fish2;
    [SerializeField] private GameObject fish3;
    [SerializeField] private GameObject fish4;
    [SerializeField] private GameObject fish5;
    [SerializeField] private GameObject background;


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

        AddFish(fish1);
        AddFish(fish2);
        AddFish(fish3);
        AddFish(fish4);
        AddFish(fish5);
 
        refreshInventoryItem();
        gameObject.SetActive(false);
    }


    public void AddFish(GameObject fish)
    {
        if (fishList.ContainsKey(fish))
        {
            fishList[fish]++;
        }
        else
        {
            fishList.Add(fish, 0);
        }
        refreshInventoryItem();

    }


    private void refreshInventoryItem()
    {
        foreach (Transform child in transform)
        {
            if (child != background.transform)
            {
                Destroy(child.gameObject);
            }
        }



        int x = 0;
        int y = 0;
        float itemSlotSize = 4f;
        foreach (GameObject fish in fishList.Keys)
        {
            GameObject curItemSlot = Instantiate(itemSlot,this.transform);
            curItemSlot.transform.position += new Vector3(x * itemSlotSize, y * itemSlotSize);
            curItemSlot.SetActive(true);
            TextMeshProUGUI text = curItemSlot.GetComponent<RectTransform>().Find("Text").GetComponent<TextMeshProUGUI>();
            text.text = fish.GetComponent<Fish>().fishName + ": "+ fishList[fish];
            Instantiate(fish, curItemSlot.transform);
            x++;
            if (x >= 3)
            {
                x = 0;
                y--;
            }
        }
    }
}
