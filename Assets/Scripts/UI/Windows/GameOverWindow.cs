using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : UIWindow {

    public Text daysDisplay;
    public Text zombiesDisplay;


    void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        daysDisplay.text = GameState.Instance.day.ToString("D2") + " / " + GameState.Instance.hour.ToString("D2") + ":" + GameState.Instance.minute.ToString("D2");
        zombiesDisplay.text = GameState.Instance.killedzombies.ToString("D2");
        GameState.Instance.paused = true;
    }
}
