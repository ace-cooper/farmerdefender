using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class EventProfile : ScriptableObject
{

    public string label;

    public enum EventType { sound=0, light=1 };
    public EventType type;

    public Vector2 amplitude = Vector2.up;

    public Vector2 duration = Vector2.up;

}
