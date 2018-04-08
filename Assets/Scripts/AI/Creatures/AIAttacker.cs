using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    

}
