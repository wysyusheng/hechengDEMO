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
}
