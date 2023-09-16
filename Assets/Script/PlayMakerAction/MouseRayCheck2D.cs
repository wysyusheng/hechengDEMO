using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using Unity.VisualScripting;
using UnityEngine;

public class MouseRayCheck2D : FsmStateAction
{
    public FsmGameObject Target;
    public FsmBool hit;

    public override void OnEnter()
    {  
        base.OnEnter();

        var result = Check(hit2D =>
        {
            if (hit2D && hit2D.collider && Target.Value && hit2D.collider.gameObject == Target.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        );
        hit.Value = result; 
        Finish();
    }

    //创建数组，用来存放检测到的所有物体
    private static RaycastHit2D[] mResult = new RaycastHit2D[12];

    public static bool Check(Func<RaycastHit2D, bool> condition)
    {
        //清空数组
        Array.Clear(mResult ,0, mResult.Length);

        //获取鼠标坐标
        var mouseScreenPosition = Input.mousePosition;

        //通过摄像机，将鼠标坐标转化为射线
        var ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        //射线检测
        Physics2D.RaycastNonAlloc(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, mResult,Mathf.Infinity) ;

        //遍历数组，获取符合条件的物体
        foreach (var hit2D in mResult)
        {
            if (condition(hit2D))
            {
                return true;
            }
        }

        return false;
    }
}
