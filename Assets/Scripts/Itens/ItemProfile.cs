using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Itens/Item Normal")]
public class ItemProfile : ScriptableObject {

    public new string name;

    public string description;

    public Sprite Icon;

    public Transform prefab;

    public float cost;

    public float fireRate;

    public float reloadTime;

    public float coolDownTime;

    public float attackDistance;

    public enum types
    {
        use,
        equip,
        drop
    }

    public types Type;

    public bool isEquipable {
        get
        {
            return Type == types.equip;
        }
    }

    public bool isConsumable
    {
        get
        {
            return Type == types.use;
        }
    }

    public bool isDropable
    {
        get
        {
            return Type == types.drop;
        }
    }

    //public FX 


    public virtual int getPower()
    {
        return 0;
    }

    public virtual int[] getTargets()
    {
        return null;
    }

    public virtual bool isTarget(int layer)
    {
        return false;
    }


}
