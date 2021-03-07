using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField]
    private List<GameObject> monsters;
    public List<Skill> playerSkills;
    public float health;
    public float maxHealth;
    public float minHealth;
    public Image playerHealthBar;
    public float energy = 10f;

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
        foreach (GameObject monster in monsters)
        {
            foreach (Skill skill in monster.GetComponent<MonsterToken>().allySkills)
            {
                skill.side = Skill.monsterSide.Ally;
                playerSkills.Add(skill);
            }
        }
        CombatManager.Instance.combatButtons[0].onClick.AddListener(playerSkills[0].Use);
        CombatManager.Instance.combatButtons[1].onClick.AddListener(playerSkills[1].Use);
        CombatManager.Instance.combatButtons[2].onClick.AddListener(playerSkills[2].Use);
        CombatManager.Instance.combatButtons[3].onClick.AddListener(playerSkills[3].Use);
    }
    private void Update()
    {
        playerHealthBar.fillAmount = health / maxHealth;
    }
}
