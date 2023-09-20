using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public GameObject player;
    public GameObject heartPrefab;
    public Sprite halfHeart;

    private void Update()
    {
        //清空子节点
        var childCount = transform.childCount;
        for(int i= childCount -1;i>=0;i--)
        {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);

        }

        //显示血量
        if (!player) return;
        var hp = player.GetComponent<Player>().hp;
        for(int i=0;i<hp;i++)
        {
            var heart = Object.Instantiate(heartPrefab,transform);
            if(hp-i<=0.5)
            {
                heart.GetComponent<Image>().sprite = halfHeart;
            }

        }
    }
}
