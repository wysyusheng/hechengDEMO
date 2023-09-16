using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventory : FsmStateAction
{

    [ObjectType(typeof(GameManager.ITEM))]
    public FsmEnum type;
    public int count;

    public override void OnEnter()
    {
        base.OnEnter();

        Inventory.AddInventory((GameManager.ITEM)type.Value, count);

        Finish();
    }

}
