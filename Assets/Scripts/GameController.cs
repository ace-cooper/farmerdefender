using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Advertisements;

public class GameController : MonoBehaviour
{
    /***************************************
     * 
     *              UI Objects
     * 
     * *************************************/
    /**
     * <summary>Exibe o tempo no formato HH:MM</summary>
     */
    public Text timeDisplay;
    /**
     * <summary>Exibe o dia corrente</summary> 
     */
    public Text dayDisplay;
    private Animation dayDisplayAnimation;
    /**
     * <summary>Exibe a grana total do jogador</summary>
     */
    public Text moneyDisplay;
    /**
     * <summary>Tela exibida antes do início de cada round</summary>
     */
    public UIWindow levelClearedWindow;

    public UIWindow gameOverWindow;

    public GameObject pauseWindow;

    public InventoryWindow inventoryWindow;
    /***************************************
     * 
     *              System
     * 
     ***************************************/
    /**
     * <summary>Período de intervalo entre as mudanças de início e fim de round</summary>
     */
    public float roundInitDelayTime = 5f;
    private WaitForSeconds roundInitDelay;
    public float roundEndDelayTime = 5f;
    private WaitForSeconds roundEndDelay;

    public Settings settings;

    public Light dayLight;

    public Transform[] respawnPoints;

    public Transform itemRespawnPoint;


    //public RespawnSettings respawnSettings;

    /*=========================
     *        Internas
     *=========================*/
    // ciclos
    private Color currentDayCycleColor;
    private Color nextDayCycleColor;
    private int currentDayCycleHour;
    private int nextDayCycleHour;

    // controle
    public AIController selectedChar;

    // respawn
    private float respawnTimer = 0;

    private static GameController _instance;
    public static GameController Instance {
        get {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameController").GetComponent<GameController>();
            }
            return _instance;
        }
    }
    public AIController[] animals;

    private Dictionary<string, List<RespawnEntity>> pooledAI = new Dictionary<string, List<RespawnEntity>>();
    private List<RespawnEntity> pooledBullets = new List<RespawnEntity>();

    //========================================================================================

    void Awake()
    {
        Advertisement.Initialize("1760565");


        GameState.Initialize(settings);
        

        

        roundInitDelay = new WaitForSeconds(roundInitDelayTime);
        roundEndDelay = new WaitForSeconds(roundEndDelayTime);
        // buscando chars
        AIController[] players = FindObjectsOfType<AIController>();
        
        for (int i=0;i<players.Length;i++)
        {
            if (players[i].core.GetType() == typeof(AIHuman)) {
                GameState.Instance.addChar(players[i]);
            }
        }
        // definindo ciclos do dia
        currentDayCycleHour = settings.morningHour;
        nextDayCycleHour = settings.middayHour;

        currentDayCycleColor = settings.morningColor;
        nextDayCycleColor = settings.middayColor;


        dayDisplayAnimation = dayDisplay.GetComponent<Animation>();

        prepareZombies();
    }

    void Start()
    {
       StartCoroutine(Loop());
       
    }

    
    void FixedUpdate()
    {
        GameState.Instance.time = Time.deltaTime;
        updateDayLight();
    }

    void Update()
    {
        UIUpdate();
    }

    void UIUpdate()
    {
        // atualizando display de tempo
        timeDisplay.text = "<color="+ ((Time.timeScale > 1) ? "red" : "white" )+">" + GameState.Instance.hour.ToString("D2") + ":" + GameState.Instance.minute.ToString("D2") + "</color>";
        moneyDisplay.text = "<color=white>x "+GameState.Instance.money+"</color>";
    }


    private IEnumerator Loop()
    {
        // iniciando round
        yield return StartCoroutine(RoundInit());
        // round...
        yield return StartCoroutine(Round());
        // encerrando
        yield return StartCoroutine(RoundEnd());
        // repete o ciclo enquanto a partida não for encerrada
        if (!GameState.Instance.gameFinished)
        {
            yield return StartCoroutine(Loop());
        }

        
    }


    private IEnumerator RoundInit()
    {
        // atualizando dia
        GameState.Instance.updateDay();
        
        dayDisplay.text = "Day " + GameState.Instance.day.ToString();

        // exbindo janela com informações do round anterior
        if (GameState.Instance.day > 1)
        {
            roundGifts();
            levelClearedWindow.gameObject.SetActive(true);
        }
        // aguardando o fechamento da janela de fim de round
        while(levelClearedWindow.gameObject.activeSelf)
        {
            
            yield return null;
        }
        
        dayDisplayAnimation.Play();

        yield return roundInitDelay;
    }

    private IEnumerator Round()
    {
        while (!GameState.Instance.roundFinished)
        {
            respawnEnemy();
            yield return null;
        }

    }

    private IEnumerator RoundEnd()
    {
        yield return roundEndDelay;
    }



    /**
     * <summary>Acelera a passagem do tempo</summary>
     */
    public void Forward()
    {
        Time.timeScale = (Time.timeScale == settings.maxForwardTime) ? 1 : ++Time.timeScale;
        
    }

    public void Pause()
    {
        GameState.Instance.paused = !GameState.Instance.paused;

        pauseWindow.SetActive(GameState.Instance.paused);
    }

    // Alterando ciclos do dia de acordo com a hora do jogo
    private void verifyDayCycle()
    {
       
        if (GameState.Instance.hour == settings.morningHour)
        {
            currentDayCycleHour = settings.morningHour;
            nextDayCycleHour = settings.middayHour;

            currentDayCycleColor = settings.morningColor;
            nextDayCycleColor = settings.middayColor;
        } 
        else if (GameState.Instance.hour == settings.middayHour)
        {
            currentDayCycleHour = settings.middayHour;
            nextDayCycleHour = settings.afternoonHour;

            currentDayCycleColor = settings.middayColor;
            nextDayCycleColor = settings.middayColor;
        }
        else if (GameState.Instance.hour == settings.afternoonHour-1)
        {
            currentDayCycleHour = settings.afternoonHour-1;
            nextDayCycleHour = settings.afternoonHour;

            currentDayCycleColor = settings.middayColor;
            nextDayCycleColor = settings.afternoonColor;
        }
        else if (GameState.Instance.hour == settings.afternoonHour)
        {
            currentDayCycleHour = settings.afternoonHour;
            nextDayCycleHour = settings.nightHour;

            currentDayCycleColor = settings.afternoonColor;
            nextDayCycleColor = settings.nightColor;
        }
        else if (GameState.Instance.hour == settings.nightHour)
        {
            currentDayCycleHour = settings.nightHour;
            nextDayCycleHour = settings.morningHour-1;

            currentDayCycleColor = settings.nightColor;
            nextDayCycleColor = settings.nightColor;
        }
        else if (GameState.Instance.hour == settings.morningHour-1)
        {
            currentDayCycleHour = settings.morningHour - 1;
            nextDayCycleHour = settings.morningHour;

            currentDayCycleColor = settings.nightColor;
            nextDayCycleColor = settings.morningColor;
        }
    }

    // Atualiza luz do dia
    private void updateDayLight()
    {

        verifyDayCycle();

        int timeDiff = nextDayCycleHour - currentDayCycleHour;
        if (timeDiff < 0) timeDiff += 24;

        int currentTime = GameState.Instance.hour - currentDayCycleHour;
        if (currentTime < 0) currentTime += 24;

       
        dayLight.color = Color.Lerp(currentDayCycleColor,nextDayCycleColor, (timeDiff>0) ? (float)currentTime / (float)timeDiff : 0);
    }



    /*public void SelectPlayer(AIController entity)
    {
        
    }*/

    public void GameOver()
    {
       
     
        gameOverWindow.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Init");
    }


    private void roundGifts()
    {


        // Somando prêmio de sobrevivência do round
        GameState.Instance.money += GameState.Instance.settings.rounds[GameState.Instance.roundDay].survivorMoneyReward;

        // Buscando e somando prêmios para cada animal vivo
        int total = animals.Length;
        int money = 0;
        for (int i=0;i<total;i++)
        {
           if (animals[i] != null && animals[i].gameObject.activeInHierarchy) money += (animals[i].core as AIAnimal).getReward(animals[i]);
        }
        GameState.Instance.money += money;
    }

    public void buyItem(ItemProfile item)
    {
        if (GameState.Instance.money >= item.cost)
        {
            GameState.Instance.money -= (int)item.cost;
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-5f,5f),0, UnityEngine.Random.Range(-5f, 5f));
            Instantiate(item.prefab, itemRespawnPoint.transform.position+offset, itemRespawnPoint.transform.rotation, transform);

        } else
        {
            // exibir janela de erro
        }
    }
    public void buyItem(ItemProfile item, int player)
    {
       if (GameState.Instance.money >= item.cost && selectedChar._inventory.AddItem(item))
        {
            GameState.Instance.money -= (int) item.cost;
        } else
        {
            // exibir janela de erro
        }
    }

    /*************************************************************
     * 
     *                Controle de balas e zumbis
     *
     *************************************************************/
    private void respawnEnemy()
    {
        if (respawnTimer < Time.time)
        {
            respawnTimer = Time.time + UnityEngine.Random.Range(settings.rounds[GameState.Instance.roundDay].respawn.minInterval, settings.rounds[GameState.Instance.roundDay].respawn.maxInterval);
            

            AIZombieProfile profile = GameState.Instance.settings.rounds[GameState.Instance.roundDay].getZombie().profile;

            Transform respawn = respawnPoints[UnityEngine.Random.Range(0, respawnPoints.Length - 1)];

            InstantiateEntity(profile._name, profile.prefab, respawn.position, respawn.rotation);
        }
    }
    private void prepareZombies()
    {
      for (int i = 0;i<settings.rounds.Length;i++)
        {
            for (int j = 0;j<settings.rounds[i].zombies.Length;j++)
            {
                List<RespawnEntity> result;
                if (!pooledAI.TryGetValue(settings.rounds[i].zombies[j].profile.prefab.name, out result))
                {
                    pooledAI[settings.rounds[i].zombies[j].profile.prefab.name] = new List<RespawnEntity>();
                }
            }
            

        }
    }
    private RespawnEntity getEntity(string label)
    {


        for (int i = 0; i < pooledAI[label].Count; i++)
        {

    
            if (!pooledAI[label][i].gameObject.activeInHierarchy)
            {

                return pooledAI[label][i];
            }
        }

        return null;
    }
    
    public RespawnEntity InstantiateEntity(RespawnEntity obj, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {


            if (!pooledBullets[i].gameObject.activeInHierarchy)
            {

                pooledBullets[i].transform.position = position;
                pooledBullets[i].transform.rotation = rotation;
                pooledBullets[i].gameObject.SetActive(true);
                return pooledBullets[i];
            }
        }


        RespawnEntity newobj = Instantiate(obj.transform, position, rotation, transform).GetComponent<RespawnEntity>();
        pooledBullets.Add(newobj);
        

        newobj.transform.position = position;
        newobj.transform.rotation = rotation;

        newobj.gameObject.SetActive(true);

        return newobj;
    }

    public RespawnEntity InstantiateEntity(string label, RespawnEntity obj, Vector3 position, Quaternion rotation)
    {
        

        RespawnEntity newobj = getEntity(label);

        if (newobj==null)
        {
            newobj = Instantiate(obj.transform, position, rotation, transform).GetComponent<RespawnEntity>();
            pooledAI[label].Add(newobj);
        }

        newobj.transform.position = position;
        newobj.transform.rotation = rotation;

        newobj.gameObject.SetActive(true);

        return newobj; 
    }


    /*************************************************
     * 
     *              Monetization
     *
     *************************************************/
    public void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = (ShowResult result) =>
        {
            if (result == ShowResult.Finished)
            {
                Debug.Log("Video completed - Offer a reward to the player");
                // Reward your player here.
                GameState.Instance.money += 100;

            }
        };

        Advertisement.Show("rewardedVideo", options);
    }
}
