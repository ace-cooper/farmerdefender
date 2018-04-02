using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UI/Data")]
public class UIData : ScriptableObject
{
    public Sprite sprite;

    public SpriteState spriteState;

    public Color defaultColor;
    public Color confirmColor;
    public Color declineColor;
    public Color warningColor;

}
