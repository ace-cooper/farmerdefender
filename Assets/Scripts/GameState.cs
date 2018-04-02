using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameState : ScriptableObject
{

    private static GameState _instance;
    public static GameState Instance
    {
        get
        {
            if (!_instance) { _instance = Resources.FindObjectsOfTypeAll<GameState>().FirstOrDefault(); }

            return _instance;
        }
    }
    // Configurações do modo de jogo
    private Settings _settings;
    public Settings settings
    {
        get { return _settings; }
    }
    /**
     * <summary>Offset de tempo. Usado no inicio do primeiro dia</summary>
     */
    private float initialTime = 21600;
    /**
     * <summary>Taxa de segundos do jogo para cada segundo real.</summary>
     */
    private float timeRate = 240;
    // Controle de atualização dos dias
    private int lastDayUpdate = 1;
    // Tempo do jogo
    private float _time;
    /**
     * <summary>Retorna o tempo corrente ou define o mesmo de acordo com o time rate configurado.</summary>
     */
    public float time
    {
        get { return _time; }
        set
        {
            _time += value * timeRate;

            if (day>lastDayUpdate && hour>_settings.morningHour)
            {

                _roundFinished = true;
            }
        }
    }

    /**
     <summary>Retorna a hora corrente</summary>
    */
    public int hour
    {
        get { return (int) _time % 86400 / 3600;  }
    }
    /**
     <summary>Retorna o minuto corrente</summary>
    */
    public int minute
    {
        get { return (int) _time % 86400 % 3600 / 60; }
    }
    /**
     <summary>Retorna o dia corrente</summary>
    */
    public int day
    {
        get { return (int)_time / 86400 + 1;  }
    }
    /**
     <summary>Retorna a quantidade de dinheiro que o jogador possui</summary>
    */
    public int money = 1000;
    /**
     <summary>Quantidade de zumbis mortos</summary>
    */
    public int killedzombies = 0;
    /**
     * <summary>Animais da fazenda</summary>
     */
    public int animalsCount
    {
        get
        {
            int _return = 0;
            for (int i = 0; i< GameController.Instance.animals.Length;i++)
            {
                if (GameController.Instance.animals[i]!=null)
                {
                    _return++;
                }
            }

            return _return;
        }
    } 
    //public Dictionary<int,AIController> animals = new Dictionary<int,AIController>();

    private bool _paused = false;
    /**
     <summary>Pausa o jogo</summary>
    */
    public bool paused
    {
        get
        {
            return _paused;
        }
        set
        {
            _paused = value;
            Time.timeScale = (_paused) ? 0 : 1;
        }
    }
    // controle de fim de round
    private bool _roundFinished = false;

    public bool roundFinished
    {
        get { return _roundFinished; }
    }
    // controle de fim de jogo
    private bool _gameFinished = false; 
    public bool gameFinished
    {
        get { return _gameFinished; }
        set
        {
            if (value)
            {
                GameController.Instance.GameOver();
            }
            _gameFinished = value;
        }
    }
    // Chars utilizados pelo jogador
    private Dictionary<int, AIController> chars = new Dictionary<int, AIController>();


  
    /**
     * <summary>Adiciona um novo char para ser utilizado pelo jogador</summary>
     */
    public bool addChar(AIController controller)
    {
        AIController result;
        int hash = controller.profile.GetHashCode();
        if (chars.TryGetValue(hash, out result))
        {
            return false;
        }

        chars[hash] = controller;

        return true;
    }
    // Remove?!
    public void removeChar()
    {

    }


    public static void Initialize(Settings _settings)
    {
        _instance = CreateInstance<GameState>();
        _instance.hideFlags = HideFlags.HideAndDontSave;

        _instance._time = _instance.initialTime;
        _instance._settings = _settings;

        _instance.timeRate = _settings.timeRate;
        _instance.initialTime = _settings.initialTime;

        _instance.money = _settings.initialMoney;

        _instance._roundFinished = false;

        _instance.lastDayUpdate = 1;

        _instance.killedzombies = 0;

        _instance.paused = false;

    }

    public void updateDay()
    {
        lastDayUpdate = day;
        _roundFinished = false;

    }

    public void loadSettings(Settings _settings)
    {
        this._settings = _settings;
    }

    public void getAnimalGift()
    {
        
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Window/Game State")]
    public static void Show()
    {
        UnityEditor.Selection.activeObject = Instance;
    }
#endif
}

