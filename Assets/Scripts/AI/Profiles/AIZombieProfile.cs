using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Profiles/Zombie")]
public class AIZombieProfile : AIProfile {

    /**
     * <summary>Taxa de perda de hp para cada minuto exposto ao sol</summary> 
     */
    public float hpDecay=1;
}
