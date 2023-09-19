using HutongGames.PlayMaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject itemPrefab;
    public GameObject InventoryMenu;
    public GameObject BtnInventory;
    public GameObject BtnCraft;
    public GameObject BreakbleItem;

   public enum ITEM
    {
        APPLE,
        PLUM,
        WOOD,
        IRON,
        GOLD,
        RED_GEM,

        POIION,
        SYRUP,
        BLOCK_WOOD,
        BLOCK_IRON
    }

    public List<Sprite> itemSprite;
    public List<string> itemName;

    public ArrayList craftingMenu = new ArrayList()
    {
        "合成",
        "数值",
        ITEM.POIION,
        ITEM.SYRUP,

        "建造",
        ITEM.BLOCK_WOOD,
        ITEM.BLOCK_IRON,
    };

    public List<List<SlotItem>> itemRecipe = new List<List<SlotItem>>()
    {
        new(),
        new(),
        new(),
        new(),
        new(),
        new(),
        new()
        {
            new SlotItem(ITEM.APPLE,4)
        },
        new()
        {
            new SlotItem(ITEM.PLUM,4),
            new SlotItem(ITEM.APPLE,2)
        },
        new()
        {
            new SlotItem(ITEM.WOOD,2)
        },
        new()
        {
            new SlotItem(ITEM.IRON,4)
        }
    };

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
        //Inventory
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
            var newButton = UnityEngine.Object.Instantiate(BtnInventory,Inventory);
            newButton.GetComponent<BtnInventory>().InitMyItem(item);
            newButton.GetComponent<BtnInventory>().UpdateView();
        }

        //Crafting
        var Craft = InventoryMenu.transform.Find("Scroll View").Find("Viewport").Find("Craft");

        var craChildCount = Craft.childCount;
        for(int i=craChildCount-1;i>=0;i--)
        {
            var child = Craft.GetChild(i);
            Destroy(child.gameObject);
        }

        //生成合成列表
        var craSize = craftingMenu.Count;
        for(int i=0;i<craSize;i++)
        {
            var newButton = UnityEngine.Object.Instantiate(BtnCraft, Craft);
            var arr = craftingMenu[i];
            //标题
            if (arr is String)
            {
                newButton.GetComponent<Image>().enabled = false;
                newButton.transform.Find("Bg").gameObject.SetActive(false);
                newButton.transform.Find("Sprite").gameObject.SetActive(false);
                newButton.transform.Find("Name").gameObject.SetActive(false);
                newButton.transform.Find("Title").GetComponent<Text>().text = arr.ToString();
            }
            //物品
            else
            {
                newButton.transform.Find("Title").gameObject.SetActive(false);
                newButton.transform.Find("Sprite").GetComponent<Image>().sprite = itemSprite[(int)arr];
                newButton.transform.Find("Name").GetComponent<Text>().text = itemName[(int)arr];
                newButton.GetComponent<BtnCraft>().type = (ITEM)arr;
            }
        }
    }
}
