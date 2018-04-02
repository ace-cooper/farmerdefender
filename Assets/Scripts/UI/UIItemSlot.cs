using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIItemSlot : MonoBehaviour {

    public Text nameDisplay;
    public Image icoDisplay;

    public GameObject parent;

    protected int id;

    protected ItemProfile profile;

    public virtual void Initialize(ItemProfile _profile, int _id) {
        profile = _profile;
        id = _id;

        if (nameDisplay != null) nameDisplay.text = profile.name;
        icoDisplay.sprite = profile.Icon;
        icoDisplay.enabled = profile.Icon != null;
    }

    public virtual void Hide()
    {
        if (parent != null) parent.SetActive(false);
    }

    public virtual void Use()
    {

    }
}
