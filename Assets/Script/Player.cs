using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp = 3;
    public float maxHp = 3;
    public GameObject placingItem;

    private void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
