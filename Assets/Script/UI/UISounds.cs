using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    public static UISounds Get;

    public AudioSource UIClick;
    public AudioSource Crafting;
    public AudioSource CraftingSuccess;

    private void Awake()
    {
        Get = this;
    }

    private void OnDestroy()
    {
        Get = null;
    }
}
