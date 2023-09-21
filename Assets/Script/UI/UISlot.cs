using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour,IBeginDragHandler,IDeselectHandler,IEndDragHandler
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
            SyncIconToMousePos();
        }
    }

    //ͼ��λ�ø������
    private void SyncIconToMousePos()
    {
        var mousePosition = Input.mousePosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.parent as RectTransform, mousePosition, null, out var mouseWorldPoint))
        {
            icon.transform.position = mouseWorldPoint;
        }
    }

    //��ק��
    public void OnDeselect(BaseEventData eventData)
    {
        if (dragging)
        {
            SyncIconToMousePos();
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
