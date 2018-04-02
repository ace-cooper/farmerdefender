using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Creatures/Human")]
public class AIHuman : AIAttacker {
    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        
    }

    public override void Select(AIController controller)
    {
        

        base.Select(controller);

        AIHumanProfile profile = controller.profile as AIHumanProfile;

        if (profile.isControlled)
        {
            //GameController.Instance.SelectPlayer(controller);
        }
    }

    public override void Die(AIController controller)
    {
        GameController.Instance.GameOver();
    }
}
