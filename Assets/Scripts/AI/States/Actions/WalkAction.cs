using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Walk")]
public class WalkAction : Action
{

    public override void Trigger(AIController controller)
    {

        Walk(controller);
    }

    private void Walk(AIController controller)
    {


        Vector3 temp = controller.Remember<Vector3>("lastTargetPos");

        if (temp != controller.agent.destination)
        {


            controller.agent.destination = temp;

            controller.agent.isStopped = false;

        }
    }
}