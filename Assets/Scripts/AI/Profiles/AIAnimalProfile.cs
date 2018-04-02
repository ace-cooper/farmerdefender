using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Profiles/Animal")]
public class AIAnimalProfile : AIProfile
{

    /**
     * <summary>Preço para contratar</summary>
     */
    public int cost = 1000;
    /**
     * <summary>Prêmio por round</summary>
     */
    public int reward = 50;
}
