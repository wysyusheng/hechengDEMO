using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BtnInventory : MonoBehaviour
{
    private SlotItem mItem;
    public Image Icon;
    public Text Text;

    //��ʼ��
    public void InitMyItem(SlotItem item)
    {
        mItem = item;
        mItem.OnChanged += UpdateView;
    }

    public void OnDestroy()
    {
        mItem.OnChanged -= UpdateView;
    }

    //������ʽ
    public void UpdateView()
    {
        if(mItem.Count!=0)
        {
            var type = mItem.Type;
            var count = mItem.Count;
            var spr = GameManager.instance.itemSprite[(int)type];
            var color = Icon.color;
            color.a = 1;
            Icon.color = color;
            Icon.sprite = spr;
            Text.text = count.ToString();
        }
        else
        {
            var color = Icon.color;
            color.a = 0;
            Icon.color = color;
            Text.text = null;
        }
    }

    private void Update()
    {
        //�Ƴ���Ʒ
        //��������
        var hover = GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("hover") as FsmBool;
        var rightClick = Input.GetMouseButtonDown(1);

        if (hover.Value && rightClick)
        {
            //��Ч
            UISounds.Get.UIClick.Play();

            //����
            var type = mItem.Type;
            var count = mItem.Count;
            for (int i = 0; i < count; i++)
            {
                var x = GameManager.instance.player.transform.position.x + Random.Range(-2f, 2f);
                var y = GameManager.instance.player.transform.position.y + Random.Range(-2f, 2f);
                var parent = GameManager.instance.BreakbleItem.transform;

                var newObject = Object.Instantiate(GameManager.instance.itemPrefab, new Vector3(x, y, 0), Quaternion.identity, parent);
                newObject.SetActive(true);
                newObject.GetComponent<Item>().type = type;
                newObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.itemSprite[(int)type];
            }

            //�Ƴ�
            Inventory.RemoveInventory(mItem);
        }

        //ʹ����Ʒ
        var LeftClick = Input.GetMouseButtonDown(0);
        var playerS = GameManager.instance.player.GetComponent<Player>();
        if ( hover.Value && LeftClick)
        {
            //��Ч
            UISounds.Get.UIClick.Play();

            var used = false;
            switch(mItem.Type)
            {
                case GameManager.ITEM.POIION:
                    {
                        if (playerS.hp < playerS.maxHp)
                        {
                            playerS.hp++;
                            used = true;
                            if (playerS.hp > playerS.maxHp) playerS.hp = playerS.maxHp;
                        }
                    };
                    break;
                case GameManager.ITEM.APPLE:
                    {
                        if (playerS.hp < playerS.maxHp)
                        {
                            playerS.hp+=0.5f;
                            used = true;
                            if (playerS.hp > playerS.maxHp) playerS.hp = playerS.maxHp;
                        }
                    };
                    break;
                default:
                    if (GameManager.instance.itemPlaceable[(int)mItem.Type])
                    {
                        //�� GameManger Pause ״̬�������¼� PauseToggle
                        GameManager.instance.GetComponent<PlayMakerFSM>().SendEvent("PauseToggle");
                        playerS.placingItem = GameManager.instance.itemPlaceable[(int)mItem.Type];
                        used = true;

                    };
                    break;
            }

            //�ù���
            if (used)
            {
                mItem.Count--;
                mItem.TriggerOnChanged();
            }
        }
    }
}
