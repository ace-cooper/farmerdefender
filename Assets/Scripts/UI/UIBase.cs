using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UIBase : MonoBehaviour
{
    public UIData data;

    protected virtual void editorUpdate() { }

    public virtual void Awake() { editorUpdate(); }

    public virtual void Update()
    {
        if (Application.isEditor)
            editorUpdate();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

}
