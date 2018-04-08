using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{


    public int maxPlayerChars = 1;

    public int initialMoney = 1000;
    /**
     * <summary>Taxa de segundos do jogo para cada segundo real.</summary>
     */
    public int timeRate = 240;
    /**
     * <summary>Offset de tempo (em segundos). Usado no inicio do primeiro dia</summary>
     */
    public float initialTime = 21600;
    /**
     * <summary>Limite de aceleração do tempo.</summary>
     */
    public int maxForwardTime = 2;
    /****************************************
     * Cores e horas de cada ciclo do dia 
     ****************************************/
    // Manhã
    public Color morningColor = new Color(212, 233, 232);
    [Range(0f,23f)]
    public int morningHour = 5;
    // Inicio de tarde
    public Color middayColor = new Color(255, 244, 214);
    [Range(0f, 23f)]
    public int middayHour = 12;
    // Fim de tarde
    public Color afternoonColor = new Color(227, 80, 92);
    [Range(0f, 23f)]
    public int afternoonHour = 17;
    // Noite
    public Color nightColor = new Color(4, 7, 62);
    [Range(0f, 23f)]
    public int nightHour = 20;

    /**************************************
     *    Configurações de cada round / dia
     **************************************/
     [Serializable]
     public class Round
    {
        public string _name = "Day";

        public int survivorMoneyReward = 0;


        [Serializable]
        public class zombieRound
        {
            public AIZombieProfile profile;

            //public enum Rate {low=45, average=20, high=5};

            //public Rate rate;

            /*private float _respawnTime = 0;
            [HideInInspector]
            public float respawnTime
            {
                get {
                    if (_respawnTime==0)
                    {
                        _respawnTime = Time.time + (int)rate;
                    }
                    return _respawnTime;
                }
                set { _respawnTime = value + (int)rate; }
            }*/
        }
       
        public RespawnSettings respawn;

        public zombieRound[] zombies;

        public ItemProfile[] survivorItensReward;

        public AIHumanProfile[] survivorCharsReward;


        public zombieRound getZombie()
        {
            return zombies[(int)UnityEngine.Random.Range(0, zombies.Length - 1)];
        }
       
    }
    // Rounds
    public Round[] rounds;
}
