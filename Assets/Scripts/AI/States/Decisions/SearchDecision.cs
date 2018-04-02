using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Search")]
public class SearchDecision : Decision {

    public float searchInterval = 2f;

	public override bool Trigger(AIController controller)
    {
        return Search(controller);
    }

    private bool Search(AIController controller)
    {
        float timer = controller.Remember<float>("searchInterval");

        if (timer<Time.time) {
            controller.Remember<float>("searchInterval",Time.time+searchInterval);
  
            Collider[] hitColliders = Physics.OverlapSphere(controller.aheadPoint.position, controller.profile.lookSphereCastRadius);
            
            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {

                    if ((controller.core as AIAttacker).isTarget(hitColliders[i].tag))
                    {
                        Creature.Health target = hitColliders[i].GetComponent<Creature.Health>();
                        if (target && !target.isDead)
                        {

                            controller.Remember<Creature.Health>("target", target);
                            return true;
                        }
                    }

                }
                return false;
            }

            controller.Remember<Creature.Health>("target", null);
        }
        return false;
    }
}
