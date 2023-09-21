using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp = 3;
    public float maxHp = 3;
    public GameObject placingItem;
    public GameObject iconE;

    private void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        //IconE animation
        if(iconE.activeSelf)
        {
            var durationFrames = 30;
            var radius = Mathf.Lerp(0, Mathf.PI * 2, (Time.frameCount % durationFrames) / (durationFrames * 1f));
            var newScale = 1 + Mathf.Sin(radius) * 0.2f;
            iconE.transform.localScale = new Vector3(newScale, newScale, 1);
        }
    }
}
