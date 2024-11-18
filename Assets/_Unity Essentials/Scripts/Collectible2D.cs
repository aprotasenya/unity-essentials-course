﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectible2D : MonoBehaviour, ICollectible
{
    [SerializeField] GameObject onCollectPrefab;

    [SerializeField] private CollectibleConfig _type;
    public CollectibleConfig Type { get => _type; set => _type = value; }

    [SerializeField] private int _collectibleValue = 1;
    public int collectibleValue { get => _collectibleValue; set => _collectibleValue = value; }


    void Start()
    {
        ICollectible.RaiseOnCreated(this.Type, this.collectibleValue);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ImmediatePickup();
        }

    }

    protected void ImmediatePickup()
    {
        ICollectible.RaiseOnCollected(this.Type, this.collectibleValue);

        Instantiate(onCollectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}