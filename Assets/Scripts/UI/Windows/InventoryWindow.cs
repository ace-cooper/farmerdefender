using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InventoryWindow : UIWindow {

    public InventorySlot[] slots;

    public int selectedIndex = 0;

    private Animator animator;

    public override void Awake()
    {
        base.Awake();
        if (animator == null) animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    void OnEnable()
    {
        
        Load(GameController.Instance.selectedChar.character.inventory);

        animator.SetBool("isOpen", true);
    }

    void OnDisable()
    {
        animator.SetBool("isOpen", false);
    }

    public void Load(Creature.Inventory _inventory)
    {

        int tot = _inventory.slots.Length;
        for (int i=0;i<slots.Length;i++)
        {
            if (i<tot && _inventory.slots[i] != null)
            {
                slots[i].Initialize(_inventory.slots[i].profile,i);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }


    public void Active()
    {
        enabled = !enabled;
    }
}
