using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Respawn Settings")]
public class RespawnSettings : ScriptableObject {
    
    public float minInterval = 5f;
    public float maxInterval = 10f;

    public float minDay = 1;
    
}
