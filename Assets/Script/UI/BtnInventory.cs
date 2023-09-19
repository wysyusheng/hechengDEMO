using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnInventory : MonoBehaviour
{
    private SlotItem mItem;
    public Image Icon;
    public Text Text;

    //初始化
    public void InitMyItem(SlotItem item)
    {
        mItem = item;
        mItem.OnChanged += UpdateView;
    }

    public void OnDestroy()
    {
        mItem.OnChanged -= UpdateView;
    }

    //更新样式
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
        //移除物品
        var hover = GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("hover") as FsmBool;
        var rightClick = Input.GetMouseButtonDown(1);

        if (hover.Value && rightClick)
        {
            //丢弃
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

            //移除
            Inventory.RemoveInventory(mItem);
        }

    }
}
