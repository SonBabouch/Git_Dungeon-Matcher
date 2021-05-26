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
    public float upgradeModifierEnergy = 1.4f;
    public float downgradeModifierEnergy = 0.7f;
    public int trueEnergy;
    public GameObject currentMonster;

    public List<GameObject> enemyMonsters = new List<GameObject>();
    [SerializeField]
    public List<Skill> enemySkills = new List<Skill>();
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
    public bool isAccelerated = false;
    public bool isSlowed = false;

    public bool isCombo = false;
    [SerializeField] private float comboTime;
    [SerializeField] private float maxComboTime;

    public bool isBoosted = false;
    public float boostAttack = 1f;

    [Header("what in hand bools")]
    public bool canUseAttack;
    public bool canUseBreak;
    public bool canUseCharm;
    public bool canUseCoupdeVent;
    public bool canUseCramp;
    public bool canUseCurse;
    public bool canUseDefense;
    public bool canUseDivinTouch;
    public bool canUseDrain;
    public bool canUseEcho;
    public bool canUseHeal;
    public bool canUseLock;
    public bool canUseMark;
    public bool canUseParalysie;
    public bool canUsePlagiat;
    public bool canUseSilence;
    public bool canUseSlowdown;
    public bool canUseAcceleration;

    [Header("Behavior Bools")]
    public bool useHeal;
    public bool useDefenseMove;

    public EnemyUi enemyUi;

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
        if (CombatManager.Instance.inCombat)
        {
            SkillFeedback.Instance.EnemiDefenseFeedback();
        }
    }
    public void InitializeMonster()
    {
        canAttack = true;
        foreach (GameObject monster in enemyMonsters)
        {
            monster.GetComponent<MonsterToken>().Initialize();
        }

        health = currentMonster.GetComponent<MonsterToken>().health;

        if (currentMonster.GetComponent<MonsterToken>().isSuperlike)
        {
            health += 20;
            currentMonster.GetComponent<MonsterToken>().isSuperlike = false;
        }

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
        Enemy.Instance.isCharging = true;
        Enemy.Instance.lastEnemyCompetence = skillToCharge;
        ConversationManager.Instance.SendMessagesEnemy(skillToCharge, 0);
        yield return null;
    }

    public IEnumerator EndEnemyChargeAttack(Skill skillToCharge)
    {
        yield return new WaitForSeconds(enemyChargingTime);

        if (ConversationManager.Instance.enemyChargingAttack.activeInHierarchy)
        {
            ConversationManager.Instance.UpdateLastMessageState(skillToCharge);
        }

        if (!skillToCharge.comesFromCurse)
        {
            if (!CombatManager.Instance.isCombatEnded)
            {
                skillToCharge.MonsterEffect();
            }
        }
        else
        {
            skillToCharge.comesFromCurse = false;
        }
        Enemy.Instance.isCharging = false;
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
        //CombatManager.Instance.ButtonsUpdate();
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
    public void EnemyBehavior()
    {

        if(energy == maxEnergy)
        {
            StartCoroutine(enemyHasMaxEnergyBehavior());
        }
        else StartCoroutine(EnemyBasicBehavior());


    }
    public IEnumerator EnemyBasicBehavior()
    {
        if (canAttack)
        {
            CheckTypeOfSkillInHand();
            UseSkill();
            yield return new WaitForSeconds(1f);
            ResetAllBools();
            EnemyBehavior();
        }
    }

    public IEnumerator enemyHasMaxEnergyBehavior()
    {
        //Debug.Log("maxMana");
        if (canAttack)
        {
            enemyIndex = Random.Range(0, enemyHand.Count);
            if ((enemyHand[enemyIndex].typeOfCapacity == Skill.capacityType.Echo && lastEnemyCompetence == null|| (enemyHand[enemyIndex].typeOfCapacity == Skill.capacityType.Plagiat &&  Player.Instance.lastPlayerCompetence == null))){

                StartCoroutine(enemyHasMaxEnergyBehavior());
            }
            else
            {
                enemyHand[enemyIndex].Use();
                yield return new WaitForSeconds(1f);
                EnemyBehavior();
            }

            
        }
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
            int chanceToLaunch = Random.Range(0, 101);
            if(chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Attack);
                return;
            }
        }
        #endregion

        #region Heal
        if (canUseHeal)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Heal);
                return;
            }
        }
        #endregion

        #region cramp
        if (canUseCramp)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Cramp);
                return;
            }
        }
        #endregion

        #region Drain
        if (canUseDrain)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Drain);
                return;
            }
        }
        #endregion

        #region Curse
        if (canUseCurse)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Curse);
                return;
            }
        }
        #endregion

        #region Mark
        if (canUseMark)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Mark);
                return;
            }
        }
        #endregion

        #region Silence
        if (canUseSilence)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Silence);
                return;
            }
        }
        #endregion

        #region Paralysie
        if (canUseParalysie)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Paralysie);
                return;
            }
        }
        #endregion

        #region Charm
        if (canUseCharm)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Charm);
                return;
            }
        }
        #endregion

        #region Lock
        if (canUseLock)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Lock);
                return;
            }
        }
        #endregion

        #region CoupDeVent
        if (canUseCoupdeVent)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.CoupDeVent);
                return;
            }
        }
        #endregion

        #region DivineTouch
        if (canUseDivinTouch)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if (chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.DivinTouch);
                return;
            }
        }
        #endregion

        #region Acceleration
        if (canUseAcceleration)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if(chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Acceleration);
            }
        }
        #endregion

        #region Slowdown
        if (canUseSlowdown)
        {
            int chanceToLaunch = Random.Range(0, 101);
            if(chanceToLaunch <= 70)
            {
                ChooseSkillToUSe(Skill.capacityType.Slowdown);
            }
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

        #region TestCode
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
        #endregion
    }

    private void ResetAllBools()
    {
        canUseAttack = false; //Done
        canUseBreak = false; 
        canUseCharm = false; //Done
        canUseCoupdeVent = false; //Done
        canUseCramp = false; //Done
        canUseCurse = false; //Done
        canUseDefense = false; 
        canUseDivinTouch = false; //
        canUseDrain = false; //Done
        canUseEcho = false;
        canUseHeal = false; //Done
        canUseLock = false; //Done
        canUseMark = false; //Done
        canUseParalysie = false; //Done
        canUsePlagiat = false;
        canUseSilence = false; //Done
        canUseSlowdown = false;
    }
}