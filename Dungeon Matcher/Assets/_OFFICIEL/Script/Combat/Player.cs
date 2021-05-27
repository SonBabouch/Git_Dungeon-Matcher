using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerUI playerUi;

    
    public List<GameObject> allyMonsters;
    public List<Skill> playerSkills;
    public float health;
    public float maxHealth;
    public float minHealth;

    public float energy;
    public float maxEnergy;
    public float modifierEnergy = 1f;
    public float upgradeModifierEnergy = 1.4f;
    public float downgradeModifierEnergy = 0.7f;
    public float trueEnergy=0;
    public List<Skill> playerHand = new List<Skill>();
    public List<Skill> playerDraw = new List<Skill>();

    public bool isSkillUsed;
    public bool isBurn;
    public bool isCurse;
    public bool isCharging;
    public bool isCramp = false;
    public bool isAccelerated = false;
    public bool isSlowed = false;

    public bool isCombo = false;
    public bool isDefending = false;

    public bool isBoosted = false;
    public float boostAttack = 1f;

    public Skill lastPlayerCompetence;

    public float playerChargingTime;
    
    public float comboTime;
    public float maxComboTime;

    public bool canAttack = true;

    public bool playerUsedBreak;

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
        //allyMonsters = MenuManager.Instance.bagManager.monsterTeam;
    }

    private void Update()
    {
        limitHealthAndEnergy();
        SkillFeedback.Instance.PlayerDefenseFeedback();
        SkillFeedback.Instance.PlayerAccelerateDecelerateFeedback();
    }

    public void InitializePlayer()
    {

        allyMonsters = MenuManager.Instance.bagManager.monsterTeam;

        foreach (GameObject monster in allyMonsters)
        {
            foreach (Skill skill in monster.GetComponent<MonsterToken>().allySkills)
            {
                //skill.side = Skill.monsterSide.Ally;
                playerSkills.Add(skill);

                if (skill.isEcho)
                {
                    skill.typeOfCapacity = Skill.capacityType.Echo;
                    skill.messageType = Skill.typeOfMessage.Small;
                }

                if (skill.isPlagiat)
                {
                    skill.typeOfCapacity = Skill.capacityType.Plagiat;
                }
            }
        }
        ShufflePlayerSkills();
    }

    void limitHealthAndEnergy()
    {
        if (energy >= maxEnergy)
        {
            energy = maxEnergy;
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void ShufflePlayerSkills()
    {
        playerSkills.ShuffleFisherYates();

        SetPlayerHandAndDraw();
    }

    public void SetPlayerHandAndDraw()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i] = playerSkills[i];
            playerDraw[i] = playerSkills[i + 4];
        }

       CombatManager.Instance.ButtonsInitialization();
       CombatManager.Instance.NoEchoFeedback();
    }

    

    public void PlayerSwapSkill(int index)
    {
        playerDraw.Add(playerHand[index]);
        playerHand.RemoveAt(index);
        playerHand.Insert(index, playerDraw[0]);
        playerDraw.RemoveAt(0);
    }

    public void AllyAlteration()
    {
        //Creer une méthode qui gere un type d'altération et l'appeller ici

        //Attack Boosté;
        if (isBoosted)
        {
            boostAttack = 1.2f;
        }
        else
        {
            boostAttack = 1f;
        }
    }

    public IEnumerator PlayerChargeAttack(Skill skillToCharge)
    {
        Player.Instance.isCharging = true;
        Player.Instance.lastPlayerCompetence = skillToCharge;
        ConversationManager.Instance.SendMessagesPlayer(skillToCharge,0);
        yield return null;

    }

    public IEnumerator EndPlayerChargeAttack(Skill skillToCharge)
    {
        yield return new WaitForSeconds(playerChargingTime);
        yield return new WaitForSeconds(0.01f);

        if (ConversationManager.Instance.playerChargingAttack.activeInHierarchy)
        {
            ConversationManager.Instance.UpdateLastMessageState(skillToCharge);
        }

        if (!skillToCharge.comesFromCurse)
        {

            if (!CombatManager.Instance.isCombatEnded && !Enemy.Instance.enemiUsedBreak)
            {
                skillToCharge.PlayerEffect();
            }
            
        }
        else
        {
            skillToCharge.comesFromCurse = false;
        }
        Enemy.Instance.enemiUsedBreak = false;
        Player.Instance.isCharging = false;
    }

    #region Combo
    public void UpdateComboVisuel()
    {
        if (isCombo)
        {
            //Check de toutes la main.
            for (int i = 0; i < playerHand.Count; i++)
            {
                if (playerHand[i].isComboSkill)
                {
                    playerUi.comboSkillFeedback[i].enabled = true;
                }
                else
                {
                   playerUi.comboSkillFeedback[i].enabled = false;
                    
                }

            }
        }
    }

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
                UpdateComboVisuel();
            }
        }
    }

    public IEnumerator PlayerCombo()
    {
        isCombo = true;
        UpdateComboVisuel();
        comboTime = 0;
        StopCoroutine(TimerCombo()); 
        StartCoroutine(TimerCombo());
        yield return null;
    }
    #endregion

    //Failed Attack Feedback
    public void FailedAttack()
    {
        Debug.Log("The Attack has failed");
    }


}
