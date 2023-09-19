using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

    public static bool CheckCanCraft(GameManager.ITEM type,int count)
    {
        var list = GameManager.instance.invList;
        var listSize = list.Count;
        var totalItemCount = 0;
        
        //获取物品总数
        for(int i=0;i<listSize;i++)
        {
            var item = list[i];
            //有同类物品
            if(item.Type==type &&item.Count!=0)
            {
                totalItemCount += item.Count;
            }
        }
        return totalItemCount >= count;

    }

    public static void DepleteInventory(GameManager.ITEM type,int count)
    {
        var List = GameManager.instance.invList;
        var ListSize = List.Count;

        for(int i=ListSize-1;i>=0;i--)
        {
            var item = List[i];
            //当前格子中的物品数量小于或等于消耗的物品数量
            if(item.Type==type && item.Count!=0 &&item.Count<=count)
            {
                count -= item.Count;
                RemoveInventory(item);
            }

            //当前格子中的物品数量大于消耗的物品数量
            if(item.Type==type && item.Count!=0 && item.Count>count)
            {
                item.Count -= count;
                break;
            }
        }
    }

    public static void RemoveInventory(SlotItem item)
    {
        item.Count = 0;
        item.TriggerOnChanged();

    }
}
