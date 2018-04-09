using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Actions/Aim")]
public class AimAction : Action
{
    public override void Trigger(AIController controller)
    {

        LookAt(controller);
    }



    private void LookAt(AIController controller)
    {
        Vector3 temp = controller.Remember<Vector3>("lastTargetPos");
        Creature.Health target = controller.Remember<Creature.Health>("target");
        bool priority = controller.Remember<bool>("priorityTarget");

        if (!priority && target)
        {

            float distance = Mathf.Abs(Vector3.Distance(controller.transform.position, target.transform.position));
            if (distance > controller.profile.lookRange)
            {
                temp = Vector3.Lerp(target.transform.position, controller.transform.position, controller.profile.lookRange * 100 / distance);
                controller.Remember<Vector3>("lastTargetPos", temp);
                controller.agent.isStopped = false;
            } else if (!controller.agent.isStopped)
            {
                controller.agent.isStopped = true;


            }

           
            controller.core.LookAt(controller,target.transform.position);

        }
    }
}
