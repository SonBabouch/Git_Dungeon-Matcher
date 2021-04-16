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

    public List<GameObject> enemyMonsters = new List<GameObject>();
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
    public bool canAttack = true;

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

    private void Start()
    {
        StartCoroutine(EnemyBasicBehavior());
    }

    public void InitializeMonster()
    {
        canAttack = true;
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

            if (skill.isEcho)
            {
                skill.typeOfCapacity = Skill.capacityType.Echo;
                skill.messageType = Skill.typeOfMessage.Small;
                skill.isComboSkill = false;
            }
            if (skill.isPlagiat)
            {
                skill.typeOfCapacity = Skill.capacityType.Plagiat;
                skill.messageType = Skill.typeOfMessage.Small;
                skill.isComboSkill = false;
            }
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

        if (!skillToCharge.comesFromCurse)
        {
            skillToCharge.MonsterEffect();
        }
        else
        {
            skillToCharge.comesFromCurse = false;
        }
        
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
        CombatManager.Instance.ButtonsUpdate();
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

    int minValue;
    int maxValue;
    int averageValue;
    public IEnumerator EnemyBasicBehavior()
    {
        CheckTypeOfSkillInHand();
        UseSkill();
        yield return new WaitForSeconds(3f);
        ResetAllBools();
        StartCoroutine(EnemyBasicBehavior());

    }

    public void CheckTypeOfSkillInHand()
    {
        foreach(Skill skillInHand in enemyHand)
        {
            skillInHand.SetEnemyBoolType();
        }
         minValue = enemyHand[0].energyCost;
         maxValue = enemyHand[0].energyCost;
         averageValue = (enemyHand[0].energyCost + enemyHand[1].energyCost + enemyHand[2].energyCost + enemyHand[3].energyCost) / enemyHand.Count; ;
        for (int i = 1; i < enemyHand.Count; i++)
        {
            if(enemyHand[i].energyCost < minValue)
            {
                minValue = enemyHand[i].energyCost;
            }
            if(enemyHand[i].energyCost > maxValue)
            {
                maxValue = enemyHand[i].energyCost;
            }
        }
    }

    public void UseSkill()
    {
        #region attack
        if (canUseAttack)
        {
            ChooseSkillToUSe(Skill.capacityType.Attack);
            return;
        }
        #endregion

        #region Heal
        if (canUseHeal)
        {
            ChooseSkillToUSe(Skill.capacityType.Heal);
            return;
        }
        #endregion
    }

    public void ChooseSkillToUSe(Skill.capacityType skillType)
    {
        enemyIndex = Random.Range(0, enemyHand.Count);

        if (enemyHand[enemyIndex].typeOfCapacity == skillType)
        {
            enemyHand[enemyIndex].Use();
        }


        //ResetAllBools();
        //bool _running = true;
        //while(_running)
        //{
        //List<Skill> allSkillsOfThisType = new List<Skill>();

        //foreach (Skill skillInHand in enemyHand)
        //{
        //    if (skillInHand.typeOfCapacity == skillType)
        //    {
        //        allSkillsOfThisType.Add(skillInHand);
        //    }
        //}


        //enemyIndex = Random.Range(0, allSkillsOfThisType.Count);
        //Debug.Log("enemyIndex : " + enemyIndex + " allSkillsOfThisType Count " + allSkillsOfThisType.Count);

        //if (trueEnergy >= allSkillsOfThisType[enemyIndex].trueEnergyCost && canAttack)
        //{
        //    allSkillsOfThisType[enemyIndex].Use();
        //    _running = false;
        //}
        //allSkillsOfThisType.Clear();
        //yield return null;
        //}
    }

    private void ResetAllBools()
    {
        canUseAttack = false;
        canUseBreak = false;
        canUseCharm = false;
        canUseCoupdeVent = false;
        canUseCramp = false;
        canUseCurse = false;
        canUseDefense = false;
        canUseDivinTouch = false;
        canUseDrain = false;
        canUseEcho = false;
        canUseHeal = false;
        canUseLock = false;
        canUseMark = false;
        canUseParalysie = false;
        canUsePlagiat = false;
        canUseSilence = false;
    }
}
