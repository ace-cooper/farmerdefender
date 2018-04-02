using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Enemies/Zombie")]
public class AIZombie : AIAttacker {

    public Vector3 initialTarget;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);

        controller.Remember<Vector3>("lastTargetPos", GameController.Instance.selectedChar.transform.position);

    }

    public override void Select(AIController controller)
    {
        base.Select(controller);

        GameController.Instance.selectedChar.Remember<Creature.Health>("target", controller.health);
    }

    public override void Die(AIController controller)
    {
        base.Die(controller);

        GameState.Instance.killedzombies++;
    }
}
