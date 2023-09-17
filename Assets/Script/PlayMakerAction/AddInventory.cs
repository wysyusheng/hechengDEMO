using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventory : FsmStateAction
{

    [ObjectType(typeof(GameManager.ITEM))]
    public FsmEnum type;
    public int count;
    public FsmBool collected;

    public override void OnEnter()
    {
        base.OnEnter();

        collected.Value=Inventory.AddInventory((GameManager.ITEM)type.Value, count);

        Finish();
    }

}
