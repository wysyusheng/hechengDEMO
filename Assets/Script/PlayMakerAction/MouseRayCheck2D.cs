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

    //�������飬������ż�⵽����������
    private static RaycastHit2D[] mResult = new RaycastHit2D[12];

    public static bool Check(Func<RaycastHit2D, bool> condition)
    {
        //�������
        Array.Clear(mResult ,0, mResult.Length);

        //��ȡ�������
        var mouseScreenPosition = Input.mousePosition;

        //ͨ������������������ת��Ϊ����
        var ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        //���߼��
        Physics2D.RaycastNonAlloc(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, mResult,Mathf.Infinity) ;

        //�������飬��ȡ��������������
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
