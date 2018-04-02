using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Idle")]
public class IdleAction : Action
{
    public float targetOffset = 1.5f;
    public bool precise = false;

    public override void Trigger(AIController controller)
    {
        Act(controller);
    }

    private Vector3 randomLocation(AIController controller)
    {
        return controller.transform.position + new Vector3(Random.Range(controller.profile.minWaypointRange.x, controller.profile.maxWaypointRange.x), 0, Random.Range(controller.profile.minWaypointRange.y, controller.profile.maxWaypointRange.y));
    }

    private void Act(AIController controller)
    {
        Vector3 temp = controller.Remember<Vector3>("lastTargetPos");
        float time = controller.Remember<float>("idleTime");
        bool priority = controller.Remember<bool>("priorityTarget");
        
        if (temp == Vector3.zero || (precise && controller.agent.remainingDistance <= targetOffset) || (Time.time >= time))
        {
            
            Vector3 random = randomLocation(controller);
            controller.Remember<Vector3>("lastTargetPos", random);
            controller.Remember<float>("idleTime", Time.time + Random.Range(controller.profile.rangedIdleTargetTime.x, controller.profile.rangedIdleTargetTime.y));
        } 
    }
}
