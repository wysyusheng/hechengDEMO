using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject itemPrefab;
    public GameObject InventoryMenu;
    public GameObject BtnInventory;
    public GameObject BtnCraft;
    public GameObject BreakbleItem;
    public GameObject ChestMenu;
    public GameObject ButtonMovable;

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
        BLOCK_IRON,
        RED_RING,

        WOODEN_CHEST,
        WOODEN_CHEST_LARGE
    }

    public List<Sprite> itemSprite;
    public List<string> itemName;
    public List<GameObject> itemPlaceable;

    public ArrayList craftingMenu = new ArrayList()
    {
        "合成",
        "数值",
        ITEM.POIION,
        ITEM.SYRUP,

        "建造",
        ITEM.BLOCK_WOOD,
        ITEM.BLOCK_IRON,
        ITEM.RED_RING,

        "宝箱",
        ITEM.WOODEN_CHEST,
        ITEM.WOODEN_CHEST_LARGE,
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
        },
        new()
        {
            new SlotItem(ITEM.GOLD,2),
            new SlotItem(ITEM.RED_GEM,1)
        },
        new()
        {
            new SlotItem(ITEM.WOOD,3)
        },
        new()
        {
            new SlotItem(ITEM.WOOD,6)
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
        ChestMenu.SetActive(false);
    }

    private void Update()
    {
        if(player)
        {
            if(player.GetComponent<Player>().hp<=0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
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

    public void ShowChestMenu()
    {
        ChestMenu.SetActive(true);

        //背包
        var invMovable = ChestMenu.transform.Find("InvMovable");

        //清空克隆体
        var invButtonCount = invMovable.childCount;
        for(int i=invButtonCount-1;i>=0;i--)
        {
            var child = invMovable.GetChild(i);
            Destroy(child.gameObject);
        }

        //生成插槽
        for (int i = 0; i < invList.Count; i++)
        {
            var newObject = Object.Instantiate(ButtonMovable, invMovable);
            var item = invList[i];
            var uiSlot = newObject.GetComponent<UISlot>();
            uiSlot.slotItem = item;
            uiSlot.UpdateView();
        }

        //宝箱
        var chestMovable = ChestMenu.transform.Find("ChestMovable");
        var chest = GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("chest") as FsmGameObject;
        var chestList = chest.Value.transform.GetComponent<ChestParent>().chestList;

        //清空克隆体
        var chestButtonCount = chestMovable.childCount;
        for (int i = chestButtonCount - 1; i >= 0; i--)
        {
            var child = chestMovable.GetChild(i);
            Destroy(child.gameObject);
        }

        //生成插槽
        for (int i = 0; i < chestList.Count; i++)
        {
            var newObject = Object.Instantiate(ButtonMovable, chestMovable);
            var item = chestList[i];
            var uiSlot = newObject.GetComponent<UISlot>();
            uiSlot.slotItem = item;
            uiSlot.UpdateView();
        }

    }
}
