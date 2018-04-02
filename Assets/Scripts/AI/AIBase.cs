using System.Collections;
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


}
