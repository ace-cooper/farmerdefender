using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AIProfile : ScriptableObject
{

    public string _name="";

    public string description="";

    public bool canMove = true;
    public bool canTurn = true;

    public int hp = 10;
    public float moveSpeed = 1;
    public float turnSpeed = 1;
    public float lookRange = 40f;
    public float lookSphereCastRadius = 1f;

    public float attackRange = 1f;


    public float attackRate = 1f;
    public int attackDamage = 50;

    public float searchDuration = 4f;
    public float searchingTurnSpeed = 120f;

    public Vector2 rangedIdleTargetTime = Vector2.up;

    public Vector2 maxWaypointRange = Vector2.one;
    public Vector2 minWaypointRange = -Vector2.one;

    public Color entityColor = Color.grey;

    public float hearMagnitude = 10f;

    public Sprite profileImage;

    public AIController prefab;

}