using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject itemPrefab;
    public GameObject InventoryMenu;
    public GameObject BtnInventory;

   public enum ITEM
    {
        APPLE,
        PLUM,
        WOOD,
        IRON,
        GOLD,
        RED_GEM
    }

    public List<Sprite> itemSprite;

    [SerializeField]
    public List<SlotItem> invList = new List<SlotItem>()
    {
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
        new SlotItem(),
    };

    private void Awake()
    {
        instance = this;
        InventoryMenu.SetActive(false);
    }

    public void ShowInventoryMenu()
    {
        InventoryMenu.SetActive(true);

        //获取按纽父节点
        var Inventory = InventoryMenu.transform.Find("背包格子");

        //清空克隆体
        var invChildCount = Inventory.childCount;
        for(int i=invChildCount-1;i>=0;i--)
        {
            var child = Inventory.GetChild(i);
            Destroy(child.gameObject);
        }

        //生成背包
        var invSize = invList.Count;
        for (int i = 0; i < invSize; i++)
        {
            var item = invList[i];
            var newButton = Object.Instantiate(BtnInventory,Inventory);
            newButton.GetComponent<BtnInventory>().InitMyItem(item);
            newButton.GetComponent<BtnInventory>().UpdateView();
        }
    }
}
