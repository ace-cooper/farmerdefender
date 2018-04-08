using System;
using System.Collections;
using System.Collections.Generic;
using Creature;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticItem : ItemBase
{

    public GameObject activeDisplay;
    public GameObject enableMoveDisplay;
    public GameObject disableMoveDisplay;

    public ItemState currentState;
    public ItemState remainState;

    private bool _activated = false;
    private bool activated
    {
        get
        {
            return _activated;
        }
        set
        {
            _activated = value;
            animator.SetBool("isActive", value);
            activeDisplay.SetActive(!value);
            activeCollider.enabled = value;
        }
    }

    private bool mouseClick = false;
    private Vector3 mousePos;
    private int movedDay = -1;

    
    public Animator animator;
    public Collider activeCollider;

    public override void Initialize(Inventory inventory, ItemProfile _profile = null)
    {
        base.Initialize(inventory, _profile);
    }

    public override void OnEnable()
    {
        activeDisplay.SetActive(!activated);
    }

    public override void OnDisable()
    {
        
    }

    public override void Use()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag=="terrain")
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (activated && profile.isTarget(collider.gameObject.layer))
        {
            activated = false;

            Creature.Health temp = collider.GetComponent<Creature.Health>();
            if (temp != null)
            {
                temp.Damage(profile.getPower());
            }
        }
    }

    void OnMouseUp()
    {
        mouseClick = false;
        enableMoveDisplay.SetActive(false);
        disableMoveDisplay.SetActive(false);
        if (Input.mousePosition != mousePos)
        {
            movedDay = GameState.Instance.day;

            return;
        }
        activated = !activated;

    }

    void OnMouseDown()
    {
        mouseClick = true;
        mousePos = Input.mousePosition;
        enableMoveDisplay.SetActive(movedDay < GameState.Instance.day);
        disableMoveDisplay.SetActive(movedDay >= GameState.Instance.day);
        
    }

    void OnMouseDrag()
    {
       if (Input.mousePosition != mousePos && movedDay < GameState.Instance.day)
        {
            Vector3 position = Vector3.zero;
#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject()) return;
            position = Input.mousePosition;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
        position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
#endif
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            transform.position = new Vector3(hit.point.x, Mathf.Min(transform.position.y, 2), hit.point.z);
            


        }
    }


    public void TransitionToState(ItemState nextState)
    {

        if (nextState != null && nextState != currentState)
        {
            remainState = currentState;
            currentState = nextState;
            //OnExitState();
        }
    }
}
