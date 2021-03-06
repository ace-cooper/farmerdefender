﻿using System;
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
        controller.crossair.gameObject.SetActive(false);
    }

    public override void Select(AIController controller)
    {
        base.Select(controller);
        Creature.Health target = GameController.Instance.selectedChar.Remember<Creature.Health>("target");
        if (target != null) target.controller.crossair.gameObject.SetActive(false);
        GameController.Instance.selectedChar.Remember<Creature.Health>("target", controller.health);
        controller.crossair.gameObject.SetActive(true);

        GameController.Instance.selectedChar.Remember("priorityTarget", false);
        GameController.Instance.selectedChar.Remember("priorityStopTimer", 0f);

        GameController.Instance.selectedChar.TransitionToState(GameController.Instance.selectedChar.profile.attackState);
    }

    public override void Die(AIController controller)
    {
        base.Die(controller);

        GameState.Instance.killedzombies++;
    }

    public override void OnMouseDown(AIController controller)
    {
        base.OnMouseDown(controller);
        Select(controller);
    }


    public override void Attack(AIController controller)
    {

            Creature.Health target = controller.Remember<Creature.Health>("target");

            if (target != null && Vector3.Distance(controller.transform.position, target.transform.position) <= controller.profile.attackRange)
            {
                target.Damage(controller.profile.attackDamage);
            }
        
    }

    public override float getFireRate(AIController controller)
    {
        return controller.profile.attackRate;
    }
}
