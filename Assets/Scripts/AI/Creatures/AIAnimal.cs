using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Creatures/Animal")]
public class AIAnimal : AIBase {

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
    
    }

    public int getReward(AIController controller)
    {
        try
        {
            return (controller.profile as AIAnimalProfile).reward;
        }
        catch (System.Exception e)
        {
            return 0;
        }
        
    }

    public override void Die(AIController controller)
    {
        base.Die(controller);
        Destroy(controller.gameObject);
    }
}
