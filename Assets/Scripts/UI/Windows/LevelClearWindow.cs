using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClearWindow : UIWindow {

    public Text moneyText;

    public Text zombieStats;

    public Text animalStats;

    public RewardSlot[] rewardSlots;

    public void Initialize()
    {
        int day = GameState.Instance.day - 1;
        int rounds = GameState.Instance.settings.rounds.Length;

        if (day >= rounds) day = rounds - 1;

        moneyText.text = "<color=white>x "+GameState.Instance.settings.rounds[day].survivorMoneyReward.ToString()+ "</color>";
        zombieStats.text = "<color=white>x " + GameState.Instance.killedzombies.ToString()+ "</color>";
        animalStats.text = "<color=white>x " + GameState.Instance.animalsCount.ToString() + "</color>";

        for (int i=0;i<rewardSlots.Length;i++)
        {

          if (i >= GameState.Instance.settings.rounds[day].survivorItensReward.Length)
            {
                rewardSlots[i].parent.SetActive(false);
            }
          else
            {
                rewardSlots[i].Initialize(GameState.Instance.settings.rounds[day].survivorItensReward[i], i);
                rewardSlots[i].parent.SetActive(true);
            }

          
            
        }
    }

    void OnEnable()
    {
        GameState.Instance.paused = true;
        Initialize();
    }

    void OnDisable()
    {
        GameState.Instance.paused = false;
    }
}
