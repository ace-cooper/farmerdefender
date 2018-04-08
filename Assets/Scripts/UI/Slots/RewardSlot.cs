using UnityEngine.UI;

public class RewardSlot : UIItemSlot {

    public Text valueDisplay;

    public override void Initialize(ItemProfile _profile, int _id)
    {
        base.Initialize(_profile, _id);

        valueDisplay.text = profile.cost.ToString();
    }

    public override void Use()
    {
        if (profile.Type.Equals(ItemProfile.types.drop))
        {
            GameController.Instance.buyItem(profile);
        } else
        {
            Select(0);
        }
    }

    // escolhendo o char
    public void Select(int player)
    {

            GameController.Instance.buyItem(profile, player);
        
    } 

}
