using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using Monster;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public TextMeshProUGUI timerDisplay;
    public int secondsLeft = 60;
    public int maxSecondsLeft;
    public int minSecondsLeft;
    [HideInInspector]
    public bool takingTimeAway = false;
    public List<GameObject> combatList = new List<GameObject>();
    public List<Button> combatButtons = new List<Button>();
    public TextMeshProUGUI[] energyCostText;
    public TextMeshProUGUI[] damageText;
    [SerializeField]
    private Image enemyhealthBar;
    public float enemyEnergy;


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
        //combatList = MenuManager.Instance.matchManager.matchList;
    }

    private void Start()
    {
        foreach(GameObject monster in combatList)
        {
            monster.GetComponent<MonsterToken>().Initialize();
        }
        Debug.Log(combatList[0].GetComponent<MonsterToken>().health);
        timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
    }

    private void Update()
    {
        InfoButtons();

        if (combatList[0].GetComponent<MonsterToken>().health >= combatList[0].GetComponent<MonsterToken>().maxHealth)
        {
            combatList[0].GetComponent<MonsterToken>().health = combatList[0].GetComponent<MonsterToken>().maxHealth;
        }
        RunningTimer();
        enemyhealthBar.fillAmount = combatList[0].GetComponent<MonsterToken>().health / 100;

    }

    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        combatList[0].GetComponent<MonsterToken>().health -= 0.5f;
        Player.Instance.energy++;
        Debug.Log(combatList[0].GetComponent<MonsterToken>().health);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
        }
        else
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
        }
        takingTimeAway = false;
    }

    void RunningTimer()
    {
        if (!takingTimeAway && secondsLeft > 0)
        {
            StartCoroutine(CombatTimer());
        }
    }

    void InfoButtons()
    {
        energyCostText[0].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[0].energyCost.ToString();
        energyCostText[1].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[1].energyCost.ToString();
        energyCostText[2].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[2].energyCost.ToString();
        energyCostText[3].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[3].energyCost.ToString();

        damageText[0].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[0].damage.ToString();
        damageText[1].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[1].damage.ToString();
        damageText[2].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[2].damage.ToString();
        damageText[3].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[3].damage.ToString();
    }
}
