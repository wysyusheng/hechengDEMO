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

    //�򿪱���ʱ���Լ������ɵĲ�ۣ����� slots
    private void OnEnable()
    {
        slots.Add(this);
    }

    //�رձ���ʱ���Լ���slots���Ƴ�
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

    //��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!dragging)
        {
            dragging = true;

            text.transform.SetParent(icon.transform);//����λ�ø������
            icon.transform.SetParent(transform.parent.parent);//��ͼ����ʾ�����в������
            SyncIconToMousePos();
        }
    }

    //ͼ��λ�ø������
    private void SyncIconToMousePos()
    {
        var mousePosition = Input.mousePosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform.parent as RectTransform, mousePosition, null, out var mouseWorldPoint))
        {
            icon.transform.position = mouseWorldPoint;
        }
    }

    //��ק��
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
                //����ڲ����
                if (RectTransformUtility.RectangleContainsScreenPoint(targetSlot.transform as RectTransform, Input.mousePosition))
                {
                    //������ڲ���ǵ�ǰ���
                    if (targetSlot == this)
                    {
                        break;
                    }
                    else
                    {
                        //ͬ���Ͳ����������
                        if (targetSlot.slotItem.Type == this.slotItem.Type)
                        {
                            targetSlot.slotItem.Count += this.slotItem.Count;
                            //�������ܳ���������۵���������
                            if (targetSlot.slotItem.Count <= SlotItem.Limit)
                            {
                                this.slotItem.Count=0;
                            }
                            else
                            {
                                this.slotItem.Count = targetSlot.slotItem.Count - SlotItem.Limit;//������ķų���
                                targetSlot.slotItem.Count = SlotItem.Limit;
                            }
                        }
                        //��ͬ���ۣ�����λ��
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

            //��겻���κβ���ϣ��Ż�ԭ�����
            icon.transform.SetParent(this.transform);
            icon.transform.localPosition = Vector3.zero;
            text.transform.SetParent(this.transform);
        }
    }
}
