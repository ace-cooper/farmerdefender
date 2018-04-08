using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Itens/Item Estático")]
public class StaticItemProfile : ItemProfile
{

    public int attackPower = 10;
    public int[] targets;

    public bool disarmOnUse = false;

    public override int getPower()
    {
        return attackPower;
    }

    public override int[] getTargets()
    {
        return targets;
    }

    public override bool isTarget(int layer)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == layer) return true;
        }
        return false;
    }

}
