using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnDie : FsmStateAction
{
    private int randomInt;//�����
    private int resCount;//�������ɵ���Ʒ����
    private int resMax;//�������ɵ���Ʒ����

    public override void OnEnter()
    {
        base.OnEnter();

        DoItemOnDie();

        Finish();
    }

    private void DoItemOnDie()
    {
        randomInt = UnityEngine.Random.Range(1, 70);

        //һ���ջ������Ʒ
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

        //�������ջ��������Ʒ
        var res = Owner.GetComponent<BreakableParent>().res;
        resMax = res.Count;

        for(int i = 0; i < resCount; i++)
        {
            //��ǰ��Ʒ����
            var resID = UnityEngine.Random.Range(0, resMax);
            var resCurrent = res[resID];

            //������Ʒ��λ��
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

            //������Ʒ
            var newObject = UnityEngine.Object.Instantiate(GameManager.instance.itemPrefab, new Vector3(x, y, 0), Quaternion.identity, Owner.transform.parent);

            //������Ʒ
            newObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.itemSprite[(int)resCurrent];
            newObject.GetComponent<Item>().type = resCurrent;
        }
    }
}
