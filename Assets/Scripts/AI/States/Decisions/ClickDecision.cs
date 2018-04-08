using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Click Follow")]
public class ClickDecision : Decision {

    public float priorityStopTime = 5f;

    public override bool Trigger(AIController controller)
    {
        return GoTo(controller);
    }

    private bool GoTo(AIController controller)
    {
        bool priority = controller.Remember<bool>("priorityTarget");
       
        if (priority && controller.agent.remainingDistance <= 1f)
        {
            float timer = controller.Remember<float>("priorityStopTimer");
            if (timer == 0f)
            {
                controller.Remember<float>("priorityStopTimer", Time.time + priorityStopTime);
            }
            else if (timer <= Time.time)
            {
                controller.Remember("priorityTarget", false);
                controller.Remember("priorityStopTimer", 0f);

                // forçando estado anterior
                controller.TransitionToState(controller.remainState);
                return true;
            }
        }
        

        return false;
    }
}
