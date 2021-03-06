﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    public static Library instance;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject playerCameraPrefab;

    [Header("Physics Materials")]
    public PhysicMaterial defaultPhysicMaterial;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
