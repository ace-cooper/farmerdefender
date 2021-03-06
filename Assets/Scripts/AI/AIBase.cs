﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase : ScriptableObject
{



    public virtual void Initialize(AIController controller)
    {
        
    }

    public virtual void AIUpdate(AIController controller)
    {
       
        
    }
    
    public virtual void DrawGizmos(AIController controller) {

        Gizmos.color = controller.profile.entityColor;
        Gizmos.DrawWireSphere(controller.transform.position + new Vector3(0,2.5f,0), controller.profile.lookSphereCastRadius);
    }

    public virtual void Select(AIController controller)
    {

    }

    public virtual void Die(AIController controller) { }

    public virtual void Attack(AIController controller) { }

    public virtual void LookAt(AIController controller, Vector3 lastTargetPos) {
        if (controller.profile.canTurn && !lastTargetPos.Equals(Vector3.zero))
        {


            Vector3 newpos = lastTargetPos - controller.transform.position;
            newpos.y = 0;

            controller.body.transform.rotation = Quaternion.Slerp(controller.body.transform.rotation, Quaternion.LookRotation(newpos), 1);// Time.deltaTime * profile.turnSpeed);


        }
    }

    public virtual void OnMouseDown(AIController controller) { }

    public virtual void OnMouseUp(AIController controller) { }

    public virtual void OnMouseDrag(AIController controller) { }

    public virtual float getFireRate(AIController controller) { return 0; }

    public virtual void onTriggerEnter(AIController controller, Collider collider) { }

    public virtual void onEnable(AIController controller) { }

    public virtual void onDisable(AIController controller) { }


}
