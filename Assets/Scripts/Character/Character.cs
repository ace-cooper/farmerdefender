using Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    public Inventory inventory;
    public Transform itemPoint;

    void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (inventory.currentItem) inventory.currentItem.Initialize(inventory);
    }
}
