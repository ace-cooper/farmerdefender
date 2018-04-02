using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Profiles/Human")]
public class AIHumanProfile : AIProfile {

    /**
     * <summary>Preço para contratar</summary>
     */
    public float cost = 1000;
    /**
     * <summary>Define se o char pode ou não ser controlado</summary>
     */
    public bool isControlled = true;
}
