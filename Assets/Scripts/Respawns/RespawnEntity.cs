using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnEntity : MonoBehaviour {
    public abstract void Initialize();

    public virtual void OnEnable()
    {
        Initialize();
    }

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
    }
}
