using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Turn")]
public class TurnAction : Action
{

    public override void Trigger(AIController controller)
    {

        Turn(controller);
    }

    private void Turn(AIController controller)
    {


        Vector3 temp = controller.Remember<Vector3>("lastTargetPos");

        if (temp != controller.agent.destination)
        {


            /*float turn = controller.profile.turnSpeed * Time.deltaTime;


            Quaternion rotation = Quaternion.Euler(0f, turn, 0f);

            Quaternion targetRotation = Quaternion.LookRotation(temp - controller.transform.position);
            float str = Mathf.Min(controller.profile.turnSpeed * Time.deltaTime, 1);

            controller. = Quaternion.Lerp(transform.rotation, targetRotation, str);*/


        }
    }
}