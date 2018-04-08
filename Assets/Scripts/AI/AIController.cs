using Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : RespawnEntity
{

    public AIBase core;
    public AIProfile profile;
    public State currentState;
    public State remainState;
    private State initState;

    public Rigidbody body;

    [SerializeField]
    public Inventory _inventory;


    public Transform aheadPoint;
    public Transform itemPoint;

    [HideInInspector]
    public NavMeshAgent agent;

    private Dictionary<string, object> memory;

    public Crosshair crossair;



    public float fireRate
    {
        get
        {
            return (_inventory.currentItem) ? _inventory.currentItem.profile.fireRate : profile.attackRate;
        }
    }


    private Health _health;
    public Health health
    {
        get
        {
            if (_health == null) _health = GetComponent<Health>();
            return _health;
        }
    }

    public T Remember<T>(string key)
    {
        object result;
        if (!memory.TryGetValue(key, out result))
            return default(T);
        return (T)result;
    }

    public void Remember<T>(string key, T value)
    {
        memory[key] = value;
    }

    void Awake()
    {
        //enabled = false;
        agent = GetComponent<NavMeshAgent>();
        initState = currentState;



    }


    void FixedUpdate()
    {
        core.AIUpdate(this);
        currentState.StateUpdate(this);

        //_inventory._animator.SetBool("isWalking", !agent.velocity.Equals(Vector3.zero));

    }

    public override void OnEnable()
    {
        agent.enabled = true;
        base.OnEnable();
        if (!core || !currentState)
        {
            enabled = false;
            return;
        }

        memory = new Dictionary<string, object>();
        core.Initialize(this);
        Initialize(transform.position);
    }

    public void Initialize(Vector3 spawnPosition, AIBase ai = null)
    {
        transform.position = spawnPosition;
        if (ai)
        {
            core = ai;
        }

        Initialize();
    }

    public override void Initialize()
    {

        currentState = initState;
        agent.speed = profile.moveSpeed;

        if (_inventory.currentItem) _inventory.currentItem.Initialize(_inventory);

        enabled = true;


    }


    void OnDrawGizmos()
    {
        core.DrawGizmos(this);
    }


    public void TransitionToState(State nextState)
    {

        if (nextState != null && nextState != currentState)
        {
            remainState = currentState;
            currentState = nextState;
            //OnExitState();
        }
    }

    public void Attack()
    {
        if (_inventory.currentItem)
        {
            _inventory.currentItem.LookAtTarget(Remember<Health>("target").transform);
            _inventory.currentItem.Use();
        } else
        {
            Health target = Remember<Health>("target");

            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= profile.attackRange)
            {
                target.Damage(profile.attackDamage);
            }
        }
    }

    public void Attack(AIBase target)
    {

    }

    public void LookAt(Vector3 lastTargetPos)
    {
    

        if (profile.canTurn && !lastTargetPos.Equals(Vector3.zero))
        {
            

            Vector3 newpos = lastTargetPos - transform.position;
            newpos.y = 0;

            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, Quaternion.LookRotation(newpos), 1);// Time.deltaTime * profile.turnSpeed);
            

        }
    }

    void OnMouseDown()
    {
        

        core.Select(this);
    }


    public override void Destroy()
    {
        
        base.Destroy();
    }



}
