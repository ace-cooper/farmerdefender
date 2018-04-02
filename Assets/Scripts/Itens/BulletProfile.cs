using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Bala")]
public class BulletProfile : ScriptableObject {

    public new string name;

    public string description;

    public Sprite Icon;

    public BulletItem prefab;

    public float cost;

    public float speed;

    public float damage;

    public float life;
}
