using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Image icon;
    public Text text;
    public SlotItem slotItem;
    public static List<UISlot> slots = new List<UISlot>();

    private bool dragging = false;

    //打开宝箱时把自己（生成的插槽）加入 slots
    private void OnEnable()
    {
        slots.Add(this);
    }

    //关闭宝箱时把自己从slots中移除
    private void OnDisable()
    {
        slots.Remove(this);
    }

    public void UpdateView()
    {
        if (slotItem.Count != 0)
        {
            var type = slotItem.Type;
            var count = slotItem.Count;
            var spr = GameManager.instance.itemSprite[(int)type];
            var color = icon.color;
            color.a = 1;
            icon.color = color;
            icon.sprite = spr;
            text.text = count.ToString();
        }
        else
        {
            var color = icon.color;
            color.a = 0;
            icon.color = color;
            text.text = null;
        }
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!dragging)
        {
            dragging = true;

            text.transform.SetParent(icon.transform);//数量位置跟随鼠标
            icon.transform.SetParent(transform.parent.parent);//让图标显示在所有插槽上面
            SyncIconToMousePos();
        }
    }

    //图标位置跟随鼠标
    private void SyncIconToMousePos()
    {
        var mousePosition = Input.mousePosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform.parent as RectTransform, mousePosition, null, out var mouseWorldPoint))
        {
            icon.transform.position = mouseWorldPoint;
        }
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            SyncIconToMousePos();
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            dragging = false;
            foreach (var targetSlot in slots)
            {
                //鼠标在插槽上
                if (RectTransformUtility.RectangleContainsScreenPoint(targetSlot.transform as RectTransform, Input.mousePosition))
                {
                    //鼠标所在插槽是当前插槽
                    if (targetSlot == this)
                    {
                        break;
                    }
                    else
                    {
                        //同类型插槽数量增加
                        if (targetSlot.slotItem.Type == this.slotItem.Type)
                        {
                            targetSlot.slotItem.Count += this.slotItem.Count;
                            //数量不能超过单个插槽的限制数量
                            if (targetSlot.slotItem.Count <= SlotItem.Limit)
                            {
                                this.slotItem.Count=0;
                            }
                            else
                            {
                                this.slotItem.Count = targetSlot.slotItem.Count - SlotItem.Limit;//多出来的放出来
                                targetSlot.slotItem.Count = SlotItem.Limit;
                            }
                        }
                        //非同类插槽，调换位置
                        else
                        {
                            var count = targetSlot.slotItem.Count;
                            var type = targetSlot.slotItem.Type;
                            targetSlot.slotItem.Count = this.slotItem.Count;
                            targetSlot.slotItem.Type = this.slotItem.Type;
                            this.slotItem.Count = count;
                            this.slotItem.Type = type;
                        }
                        targetSlot.UpdateView();
                        this.UpdateView();

                        break;
                    }
                }
            }

            //鼠标不在任何插槽上，放回原来插槽
            icon.transform.SetParent(this.transform);
            icon.transform.localPosition = Vector3.zero;
            text.transform.SetParent(this.transform);
        }
    }
}
