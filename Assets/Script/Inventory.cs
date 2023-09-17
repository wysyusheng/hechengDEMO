using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public static bool AddInventory(GameManager.ITEM type,int count)
    {
        var list = GameManager.instance.invList;
        var listSize = list.Count;

        //已经有了同类物品
        for(int i=0; i < listSize;i++)
        {
            var item = list[i];
            if(item.Type == type && item.Count !=0 && item.Count<SlotItem.Limit)
            {
                item.Count += 1;
                return true;
            }
        }

        //没有同类物品
        for(int i=0; i <listSize;i++)
        {
            var item = list[i];
            if(item.Count==0)
            {
                item.Type = type;
                item.Count = count;
                return true;
            }
        }

        //背包满了
        return false;
    }
}
