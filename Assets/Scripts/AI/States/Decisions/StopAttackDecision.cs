using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Stop Attack")]
public class StopAttackDecision : Decision {

    public override bool Trigger(AIController controller)
    {
        return Verify(controller);
    }

    private bool Verify(AIController controller)
    {
        

        Creature.Health target = controller.Remember<Creature.Health>("target");
        
        if (target==null || target!=null && target.isDead)
        {
         
            controller.Remember<Creature.Health>("target", null);

            return true;
        }
        
        return false;
    }
}
