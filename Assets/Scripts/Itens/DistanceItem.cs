using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceItem : ItemBase
{

    public Transform[] instantiatePoint;

    public BulletProfile currentBullet;

    public override void OnDisable()
    {

    }

    public override void OnEnable()
    {

    }

    public override void Use()
    {
        if (Time.time > coolDownTime)
        {
            base.Use();

            Fire();

        }
    }

    private void Fire()
    {
        int tot = instantiatePoint.Length;
     
        for (int i=0;i<tot;i++) {


            //Instantiate(currentBullet.prefab, instantiatePoint[i].position, instantiatePoint[i].rotation)
            BulletItem temp =  GameController.Instance.InstantiateEntity(currentBullet.prefab, instantiatePoint[i].position, instantiatePoint[i].rotation) as BulletItem;

            temp.Initialize(instantiatePoint[i].forward);
        }
    }
}
