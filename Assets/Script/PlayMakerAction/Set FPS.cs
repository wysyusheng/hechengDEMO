using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

public class SetFPS : FsmStateAction
{
    public FsmInt targetFPS;

    public override void OnEnter()
    {  
        base.OnEnter();

        Application.targetFrameRate = targetFPS.Value;

        Finish();
    }
}
