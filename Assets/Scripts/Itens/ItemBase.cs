﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{

    public ItemProfile profile;

    private Creature.Inventory _controller;

    private float _coolDownTime = 0;

    protected float coolDownTime
    {
        get
        {
            return _coolDownTime;
        }
    }

    protected Creature.Inventory controller
    {
        get { return _controller; }
    }

    public abstract void OnEnable();

    public abstract void OnDisable();

    public virtual void Use()
    {
        
        _coolDownTime = Time.time + profile.coolDownTime;
        

    }

    public virtual void Initialize(Creature.Inventory inventory, ItemProfile _profile = null)
    {
 

        _controller = inventory;
    }

    public void Sell() {

    }

    public void LookAtTarget(Transform target)
    {


        Vector3 newpos = target.position - transform.position;
        newpos.y = transform.position.y;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newpos), 1);// Time.deltaTime * profile.turnSpeed);

    }

}
