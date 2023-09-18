using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotItem 
{
    public GameManager.ITEM Type;
    public int Count;
    public const int Limit = 3;

    public SlotItem(GameManager.ITEM type,int count)
    {
        Type = type;
        Count = count;
    }

    public SlotItem() { }
}
