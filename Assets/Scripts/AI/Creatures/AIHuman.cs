using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Creatures/Human")]
public class AIHuman : AIAttacker {
    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        
    }

    public override void Select(AIController controller)
    {
        

        base.Select(controller);

        AIHumanProfile profile = controller.profile as AIHumanProfile;

        if (profile.isControlled)
        {
            //GameController.Instance.SelectPlayer(controller);
        }
    }

    public override void Die(AIController controller)
    {
        GameController.Instance.GameOver();
    }

    public override void Attack(AIController controller)
    {
        if (controller.character.inventory.currentItem)
        {
            controller.character.inventory.currentItem.LookAtTarget(controller.Remember<Creature.Health>("target").transform);
            controller.character.inventory.currentItem.Use();
        }
        else
        {
            Creature.Health target = controller.Remember<Creature.Health>("target");

            if (target != null && Vector3.Distance(controller.transform.position, target.transform.position) <= controller.profile.attackRange)
            {
                target.Damage(controller.profile.attackDamage);
            }
        }
    }

    public override float getFireRate(AIController controller)
    {
        return (controller.character.inventory.currentItem) ? controller.character.inventory.currentItem.profile.fireRate : controller.profile.attackRate;
    }
}
