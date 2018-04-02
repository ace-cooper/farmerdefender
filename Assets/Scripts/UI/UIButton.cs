using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class UIButton : UIBase {

    private Button button;
    private Image image;
    

    public override void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        base.Awake();
    }

    protected override void editorUpdate()
    {
        base.editorUpdate();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = data.sprite;
        image.type = Image.Type.Sliced;
        button.spriteState = data.spriteState;
    }
}
