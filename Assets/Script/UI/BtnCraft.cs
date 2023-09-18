using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCraft : MonoBehaviour
{
    public GameManager.ITEM type;
    public GameObject recipeItem;
    public GameObject uiRecipe;

    private int recipeSize;

    private void Start()
    {
        recipeSize = GameManager.instance.itemRecipe[(int)type].Count;
        uiRecipe.SetActive(false);

    }

    private void Update()
    {
        //清空
        var childCount = uiRecipe.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            var child = uiRecipe.transform.GetChild(i);
            Destroy(child.gameObject);

        }

        //生成菜单
        for(int i=0;i<recipeSize;i++)
        {
            var newRecipeItem = Object.Instantiate(recipeItem, uiRecipe.transform);
            var itemType = GameManager.instance.itemRecipe[(int)type][i].Type;
            var itemCount = GameManager.instance.itemRecipe[(int)type][i].Count;

            newRecipeItem.transform.Find("Sprite").GetComponent<Image>().sprite = GameManager.instance.itemSprite[(int)itemType];
            newRecipeItem.transform.Find("Count").GetComponent<Text>().text = itemCount.ToString();
        }
    }
}
