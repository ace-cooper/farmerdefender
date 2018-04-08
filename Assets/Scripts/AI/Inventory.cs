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


        public bool AddItem(ItemProfile item)
        {
            for (int i = 0;i<slots.Length;i++)
            {

                if (slots[i] == null)
                {

                    Transform temp = GameObject.Instantiate(item.prefab, controller.itemPoint.position, controller.itemPoint.rotation, controller.itemPoint);
                    temp.gameObject.SetActive(false);
                    slots[i] = temp.GetComponent<ItemBase>();
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
                    currentItem.gameObject.SetActive(false);
                    slots[index].gameObject.SetActive(true);
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