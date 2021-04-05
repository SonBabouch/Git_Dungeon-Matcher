using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField]
    private List<GameObject> allyMonsters;
    public List<Skill> playerSkills;
    public float health;
    public float maxHealth;
    public float minHealth;

    public float energy;
    public float maxEnergy;
    public List<Skill> playerHand = new List<Skill>();
    public List<Skill> playerDraw = new List<Skill>();

    public bool isSkillUsed;
    public bool isBurn;
    public bool isCharging;
    public bool isCramp = false;

    public bool isCombo = false;
    public bool isDefending = false;

    public bool isBoosted = false;
    public float boostAttack = 1f;

    


    public Skill lastPlayerCompetence;

    public float chargingTime;
    public float comboTime;

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
    private void Start()
    {
        InitializePlayer();
        SetPlayerHandAndDraw();
    }
    private void Update()
    {
        limitHealthAndEnergy();
    }

    public void InitializePlayer()
    {
        foreach (GameObject monster in allyMonsters)
        {
            foreach (Skill skill in monster.GetComponent<MonsterToken>().allySkills)
            {
                skill.side = Skill.monsterSide.Ally;
                playerSkills.Add(skill);
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
    }

    public void SetPlayerHandAndDraw()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i] = playerSkills[i];
            playerDraw[i] = playerSkills[i + 4];
        }
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

    public IEnumerator ChargeAttack(Skill skillToCharge)
    {
        Player.Instance.lastPlayerCompetence = skillToCharge;
        Player.Instance.isCharging = true;
        CombatManager.Instance.ButtonsUpdate();
        ConversationManager.Instance.SendMessagesPlayer(skillToCharge,0);
        yield return new WaitForSeconds(chargingTime);
        //Debug.Log("End");
        Player.Instance.isCharging = false;
        ConversationManager.Instance.UpdateLastMessageState();
        skillToCharge.PlayerEffect();
    }

    //Failed Attack Feedback
    public void FailedAttack()
    {
        Debug.Log("The Attack has failed");
    }


}
