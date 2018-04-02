using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creature
{
    [System.Serializable]
    public class Inventory
    {

        public ItemBase[] slots;

        public ItemBase currentItem;

        public AIController controller;

        public Animator _animator;


        public bool AddItem(ItemBase item)
        {
            for (int i = 0;i<slots.Length;i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = item;
                    return true;
                }
            }
            return false;
        }

        void RemoveItem(int index)
        {
            slots.SetValue(null, index);
        }

        public void UseItem(int index)
        {
            switch (slots[index].profile.Type)
            {
                case ItemProfile.types.equip:
                    currentItem = slots[index];
                    break;
                case ItemProfile.types.drop:
                case ItemProfile.types.use:
                    slots[index].Use();
                    break;
            }
        }

        public void Show()
        {
            GameController.Instance.inventoryWindow.enabled = !GameController.Instance.inventoryWindow.enabled;
        }

    }
}