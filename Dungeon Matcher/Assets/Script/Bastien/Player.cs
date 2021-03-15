using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image playerHealthBar;
    public Image playerEnergyBar;
    public float energy;
    public float maxEnergy;
    public Skill[] playerHand = new Skill[4];
    public Skill[] playerDraw = new Skill[4];

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
        CombatManager.Instance.combatButtons[0].onClick.AddListener(playerSkills[0].Use);
        CombatManager.Instance.combatButtons[1].onClick.AddListener(playerSkills[1].Use);
        CombatManager.Instance.combatButtons[2].onClick.AddListener(playerSkills[2].Use);
        CombatManager.Instance.combatButtons[3].onClick.AddListener(playerSkills[3].Use);
    }
    private void Update()
    {
        limitHealthAndEnergy();
        playerHealthBar.fillAmount = health / maxHealth;
        playerEnergyBar.fillAmount = energy / maxEnergy;
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
        playerSkills[0] = playerHand[0];
        playerSkills[1] = playerHand[1];
        playerSkills[2] = playerHand[2];
        playerSkills[3] = playerHand[3];

        playerSkills[4] = playerDraw[0];
        playerSkills[5] = playerDraw[1];
        playerSkills[6] = playerDraw[2];
        playerSkills[7] = playerDraw[3];
    }
}
