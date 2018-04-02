using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public EventProfile profile;

    void Awake()
    {
        if (!profile) enabled = false;


    }
	
	void FixedUpdate () {
		
	}

    public virtual void Initialization()
    {

    }
}
