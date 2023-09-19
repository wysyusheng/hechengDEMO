using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCraft : MonoBehaviour
{
    public GameManager.ITEM type;
    public GameObject recipeItem;
    public GameObject uiRecipe;
    public GameObject uiCrafting;

    private int recipeSize;
    private float craftingAnimation = 0f;

    private void Start()
    {
        recipeSize = GameManager.instance.itemRecipe[(int)type].Count;
        uiRecipe.SetActive(false);

    }

    private void Update()
    {
        //�˵�
        //���
        var childCount = uiRecipe.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            var child = uiRecipe.transform.GetChild(i);
            Destroy(child.gameObject);

        }

        //���ɲ˵�
        for(int i=0;i<recipeSize;i++)
        {
            var newRecipeItem = Object.Instantiate(recipeItem, uiRecipe.transform);
            var itemType = GameManager.instance.itemRecipe[(int)type][i].Type;
            var itemCount = GameManager.instance.itemRecipe[(int)type][i].Count;

            newRecipeItem.transform.Find("Sprite").GetComponent<Image>().sprite = GameManager.instance.itemSprite[(int)itemType];
            newRecipeItem.transform.Find("Count").GetComponent<Text>().text = itemCount.ToString();
        }

        //�ϳ�
        //��ȡ��������
        var hover = GetComponent<PlayMakerFSM>().FsmVariables.GetVariable("hover") as FsmBool;//��PlayMaker״̬����ȡ����
        var held = hover.Value && Input.GetMouseButton(0);
        //����Ƿ���Ժϳ�
        if(held && craftingAnimation<=0)
        {
            var canCraft = true;
            for(int i=0;i<recipeSize;i++)
            {
                var itemType = GameManager.instance.itemRecipe[(int)type][i].Type;
                var itemCount = GameManager.instance.itemRecipe[(int)type][i].Count;
                canCraft=Inventory.CheckCanCraft(itemType, itemCount);

                if (!canCraft) break;

            }

            //��ʼ�ϳ�
            if(canCraft)
            {
                craftingAnimation = 0.02f;

            }
        }

        //�ϳ���
        if(held && craftingAnimation>0)
        {
            craftingAnimation += 0.02f;
            //���
            if (craftingAnimation >= 1)
            {
                craftingAnimation = 0;
                CraftNewInventory();
                GameManager.instance.ShowInventoryMenu();
            }
        }

        else
        {
            craftingAnimation = 0;
        }

        var scalc = uiCrafting.transform.localScale;
        scalc.x = craftingAnimation;
        uiCrafting.transform.localScale = scalc;

    }

    private void CraftNewInventory()
    {
        //����ԭ����
        for(int i=0;i<recipeSize;i++)
        {
            var itemType = GameManager.instance.itemRecipe[(int)type][i].Type;
            var itemCount = GameManager.instance.itemRecipe[(int)type][i].Count;

            Inventory.DepleteInventory(itemType, itemCount);

        }
        //�������Ʒ������
        Inventory.AddInventory(type, 1);

    }
}
