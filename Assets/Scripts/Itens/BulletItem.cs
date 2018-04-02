using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletItem : RespawnEntity {

    public BulletProfile profile;

    private Rigidbody _rigidbody;

    private Vector3 direction;

    private float timer = 10;

	// Use this for initialization
	void Awake () {
        _rigidbody = GetComponent<Rigidbody>();
	}

    void Start()
    {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (timer<=Time.time)
        {
            Destroy();
        }
    }


    public void Initialize(Vector3 dir)
    {
        direction = dir;
        _rigidbody.velocity = direction * profile.speed;
    
    }

    public override void Initialize()
    {
        _rigidbody.velocity = Vector3.zero;
        timer = Time.time + profile.life;
    }

    void OnTriggerEnter(Collider col)
    {
    
        Creature.Health temp = col.GetComponent<Creature.Health>();
        if (temp)
        {
            temp.Damage((int)profile.damage);
        }

        Destroy();
    }


}
