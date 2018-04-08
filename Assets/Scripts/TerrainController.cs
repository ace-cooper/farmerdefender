using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainController : MonoBehaviour {

	void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
    
    }
}
