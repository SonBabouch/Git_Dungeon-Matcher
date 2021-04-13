using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public float health;
    public float maxHealth;
    public float minHealth;
    public float energy;
    public float maxEnergy;
    public float energyModifierEnemy;
    public int trueEnergy;
    public GameObject currentMonster;
    [SerializeField]
    private List<GameObject> enemyMonsters = new List<GameObject>();
    [SerializeField]
    private List<Skill> enemySkills = new List<Skill>();
    [SerializeField]
    public List<Skill> enemyHand = new List<Skill>();
    [SerializeField]
    public List<Skill> enemyDraw = new List<Skill>();
    [HideInInspector]
    public int enemyIndex;

    public Skill lastEnemyCompetence;
    public float enemyChargingTime;

    public bool isCramp = false;
    public bool isCharging = false;
    public bool isDefending = false;
    public bool isCurse = false;
    public bool canAttack;

    public bool isCombo = false;
    [SerializeField] private float comboTime;
    [SerializeField] private float maxComboTime;

    public bool isBoosted = false;
    public float boostAttack = 1f;

    [Header("what in hand bools")]
    [SerializeField] public bool canUseAttack;
    [SerializeField] public bool canUseBreak;
    [SerializeField] public bool canUseCharm;
    [SerializeField] public bool canUseCoupdeVent;
    [SerializeField] public bool canUseCramp;
    [SerializeField] public bool canUseCurse;
    [SerializeField] public bool canUseDefense;
    [SerializeField] public bool canUseDivinTouch;
    [SerializeField] public bool canUseDrain;
    [SerializeField] public bool canUseEcho;
    [SerializeField] public bool canUseHeal;
    [SerializeField] public bool canUseLock;
    [SerializeField] public bool canUseMark;
    [SerializeField] public bool canUseParalysie;
    [SerializeField] public bool canUsePlagiat;
    [SerializeField] public bool canUseSilence;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        EnemyBasicBehavior();
    }

    public void InitializeMonster()
    {
        foreach (GameObject monster in enemyMonsters)
        {
            monster.GetComponent<MonsterToken>().Initialize();
        }

        currentMonster = enemyMonsters[0];
        health = currentMonster.GetComponent<MonsterToken>().health;
        maxHealth = currentMonster.GetComponent<MonsterToken>().maxHealth;
        minHealth = currentMonster.GetComponent<MonsterToken>().minHealth;

        foreach (Skill skill in currentMonster.GetComponent<MonsterToken>().ennemySkills)
        {
            //skill.side = Skill.monsterSide.Enemy;
            enemySkills.Add(skill);
        }
        ShuffleEnemySkill();
    }

    public IEnumerator EnemyChargeAttack(Skill skillToCharge)
    {
        Enemy.Instance.lastEnemyCompetence = skillToCharge;
        Enemy.Instance.isCharging = true;
        CombatManager.Instance.ButtonsUpdate();
        ConversationManager.Instance.SendMessagesPlayer(skillToCharge, 0);
        yield return new WaitForSeconds(enemyChargingTime);
        //Debug.Log("End");
        Enemy.Instance.isCharging = false;

        if (ConversationManager.Instance.allMsg[0] != null)
        {
            ConversationManager.Instance.UpdateLastMessageState(skillToCharge);
        }
        skillToCharge.MonsterEffect();
    }

    #region Shuffle
    public void ShuffleEnemySkill()
    {
        enemySkills.ShuffleFisherYates();
    }
    public void SetEnemyHandAndDraw()
    {
        for (int i = 0; i < enemyHand.Count; i++)
        {
            enemyHand[i] = enemySkills[i];
            enemyDraw[i] = enemySkills[i + 4];
        }
        CheckTypeOfSkillInHand();
    }

    
    public void EnemySwapSkill(int index)
    {
        enemyDraw.Add(enemyHand[index]);
        enemyHand.RemoveAt(index);
        enemyHand.Insert(index, enemyDraw[0]);
        enemyDraw.RemoveAt(0);
    }
    #endregion

    #region Combo
    public IEnumerator TimerCombo()
    {
        if (isCombo)
        {
            if (comboTime < maxComboTime)
            {
                comboTime++;
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(TimerCombo());
            }
            else
            {
                isCombo = false;
                comboTime = 0;
            }
        }
    }

    public IEnumerator EnemyCombo()
    {
        isCombo = true;
        comboTime = 0;
        StopCoroutine(TimerCombo());
        StartCoroutine(TimerCombo());
        yield return null;
    }
    #endregion


    public void EnemyBasicBehavior()
    {
        if(trueEnergy >= 3f && canAttack)
        {
            canAttack = false;
            enemyIndex = Random.Range(0, enemyHand.Count);
            enemyHand[enemyIndex].InUse();
        }
        //yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    public void CheckTypeOfSkillInHand()
    {
        foreach(Skill skillInHand in enemyHand)
        {
            skillInHand.SetEnemyBoolType();
        }
    }
}
