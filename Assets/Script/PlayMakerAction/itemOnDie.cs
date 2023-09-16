using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnDie : FsmStateAction
{
    private int randomInt;//随机数
    private int resCount;//即将生成的物品数量
    private int resMax;//最多可生成的物品种类

    public override void OnEnter()
    {
        base.OnEnter();

        DoItemOnDie();

        Finish();
    }

    private void DoItemOnDie()
    {
        randomInt = UnityEngine.Random.Range(1, 70);

        //一共收获多少物品
        switch (randomInt)
        {
            case >= 1 and < 40:
                {
                    resCount = 1;
                    break;
                }
            case >= 40 and <= 60:
                {
                    resCount = 2;
                    break;
                }
            default:
                {
                    resCount = 3;
                    break;
                }
        }

        //最多可以收获多少种物品
        var res = Owner.GetComponent<BreakableParent>().res;
        resMax = res.Count;

        for(int i = 0; i < resCount; i++)
        {
            //当前物品种类
            var resID = UnityEngine.Random.Range(0, resMax);
            var resCurrent = res[resID];

            //生成物品的位置
            var playerX = GameManager.instance.player.transform.position.x;
            var itemPosition = Owner.transform.position;
            float x;
            float y;
            if (playerX - itemPosition.x >= 0)
            {
                x = itemPosition.x + UnityEngine.Random.Range(-1f, 0f);
                y = itemPosition.y + UnityEngine.Random.Range(-1f, 1f);
            }
            else
            {
                x = itemPosition.x + UnityEngine.Random.Range(0f, 1f);
                y = itemPosition.y + UnityEngine.Random.Range(-1f, 1f);
            }

            //生成物品
            var newObject = UnityEngine.Object.Instantiate(GameManager.instance.itemPrefab, new Vector3(x, y, 0), Quaternion.identity, Owner.transform.parent);

            //设置物品
            newObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.itemSprite[(int)resCurrent];
            newObject.GetComponent<Item>().type = resCurrent;
        }
    }
}
