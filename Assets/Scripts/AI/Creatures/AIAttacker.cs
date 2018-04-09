using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creature;


public class AIAttacker : AIBase
{
    [Header("Numero da camada alvo desta entidade")]
    public int[] targetsTags;
    protected Dictionary<int, bool> targets;

    public override void AIUpdate(AIController controller)
    {
        base.AIUpdate(controller);

        Debug.DrawRay(controller.aheadPoint.position, controller.aheadPoint.forward.normalized * controller.profile.lookRange, controller.currentState.color);

    }

    public override void Initialize(AIController controller)
    {
        targets = new Dictionary<int, bool>();
        for (int i = 0; i < targetsTags.Length; i++)
        {

            targets[targetsTags[i]] = true;
        }

        base.Initialize(controller);
    }

    public bool isTarget(int tag)
    {
        bool result;
        return targets.TryGetValue(tag, out result);
    }

    /*public override void Attack(AIController controller)
    {
        if (controller.character.inventory.currentItem)
        {
            controller.character.inventory.currentItem.LookAtTarget(controller.Remember<Health>("target").transform);
            controller.character.inventory.currentItem.Use();
        }
        else
        {
            Health target = controller.Remember<Health>("target");

            if (target != null && Vector3.Distance(controller.transform.position, target.transform.position) <= controller.profile.attackRange)
            {
                target.Damage(controller.profile.attackDamage);
            }
        }
    }*/


}
