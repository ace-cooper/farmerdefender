using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action
{

    public override void Trigger(AIController controller)
    {

        Walk(controller);
    }

    private void Walk(AIController controller)
    {


        Vector3 temp = controller.Remember<Vector3>("lastTargetPos");
        Creature.Health target = controller.Remember<Creature.Health>("target");

        if (target && temp != target.transform.position)
        {


            controller.agent.destination = target.transform.position;
            controller.Remember<Vector3>("lastTargetPos", target.transform.position);

            controller.agent.isStopped = false;

        }
    }
}
