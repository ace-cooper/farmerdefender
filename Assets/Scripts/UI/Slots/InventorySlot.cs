using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : UIItemSlot {


    public Outline selected;

    public override void Use()
    {
       if (profile != null)
        {
            GameController.Instance.selectedChar.character.inventory.UseItem(id);
            if (profile.isEquipable)
            {
                selected.enabled = true;
                GameController.Instance.inventoryWindow.selectedIndex = id;
            }
        } 
    }

    public void Clear()
    {
        profile = null;
        id = -1;
        icoDisplay.sprite = null;
        icoDisplay.enabled = false;
    }

}
