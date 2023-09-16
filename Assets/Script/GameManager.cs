using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject itemPrefab;
   public enum ITEM
    {
        APPLE,
        PLUM,
        WOOD,
        IRON,
        GOLD,
        RED_GEM
    }

    public List<Sprite> itemSprite;

    private void Awake()
    {
        instance = this;
    }
}
