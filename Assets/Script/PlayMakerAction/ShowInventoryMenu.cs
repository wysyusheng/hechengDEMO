using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventoryMenu : FsmStateAction
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.instance.ShowInventoryMenu();
        Finish();
    }
}
