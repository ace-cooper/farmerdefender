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


    public Transform aheadPoint;

    [HideInInspector]
    public NavMeshAgent agent;

    private Dictionary<string, object> memory;

    public Crosshair crossair;

    private Health _health;

    private Character _character;


    public float fireRate
    {
        get
        {
            return core.getFireRate(this);
        }
    }


    
    public Health health
    {
        get
        {
            if (_health == null) _health = GetComponent<Health>();
            return _health;
        }
    }


    public Character character
    {
        get
        {
            if (_character == null) _character = GetComponent<Character>();
            return _character;
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

        //character.inventory._animator.SetBool("isWalking", !agent.velocity.Equals(Vector3.zero));

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

        core.onEnable(this);
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

    void OnDisable()
    {
        core.onDisable(this);
    }

    void OnTriggerEnter(Collider collider)
    {
        core.onTriggerEnter(this, collider);
    }

    void OnMouseDown()
    {
        

        core.OnMouseDown(this);
    }

    void OnMouseUp()
    {


        core.OnMouseUp(this);
    }

    void OnMouseDrag()
    {
        core.OnMouseDrag(this);
    }

    public override void Destroy()
    {
        
        base.Destroy();
    }



}
