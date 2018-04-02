using UnityEngine.UI;

public class RewardSlot : UIItemSlot {

    public Text valueDisplay;

    public override void Initialize(ItemProfile _profile, int _id)
    {
        base.Initialize(_profile, _id);

        valueDisplay.text = profile.cost.ToString();
    }

}
