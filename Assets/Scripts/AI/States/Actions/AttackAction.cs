using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Trigger(AIController controller)
    {

        Attack(controller);
    }



    private void Attack(AIController controller)
    {
        float temp = controller.Remember<float>("attackTime");
        Creature.Health target = controller.Remember<Creature.Health>("target");
        if (target != null && !target.isDead && temp <= Time.time)
        {
            controller.Attack();

            controller.Remember<float>("attackTime", Time.time + controller.fireRate);
        }
    }
}
