using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChestMenu : FsmStateAction
{
    public override void OnEnter()
    {
        base.OnEnter();

        GameManager.instance.ShowChestMenu();


        Finish();
    }
}
