using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;


public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public TextMeshProUGUI timerDisplay;
    public int secondsLeft;
    public int maxSecondsLeft;
    public int minSecondsLeft;
    public bool takingTimeAway = false;
    public List<Button> combatButtons;
    public TextMeshProUGUI[] energyCostText;
    public TextMeshProUGUI[] damageText;
    public bool isCombatEnded = false;


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
        //timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
    }

    private void FixedUpdate()
    {
        RunningTimer();
    }

    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        Enemy.Instance.health -= 0.5f;
        Player.Instance.energy++;
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
        if (!takingTimeAway && secondsLeft > minSecondsLeft && !isCombatEnded)
        {
            StartCoroutine(CombatTimer());
        }
        else if(secondsLeft <= minSecondsLeft)
        {
            isCombatEnded = true;
        }
    }

    void InfoButtons()
    {

        energyCostText[0].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[0].energyCost.ToString();
        energyCostText[1].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[1].energyCost.ToString();
        energyCostText[2].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[2].energyCost.ToString();
        energyCostText[3].GetComponent<TextMeshProUGUI>().text = "Cost = " + Player.Instance.playerSkills[3].energyCost.ToString();

        damageText[0].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[0].healthAmount.ToString();
        damageText[1].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[1].healthAmount.ToString();
        damageText[2].GetComponent<TextMeshProUGUI>().text = "Heal = " + Player.Instance.playerSkills[2].healthAmount.ToString();
        damageText[3].GetComponent<TextMeshProUGUI>().text = "Damage = " + Player.Instance.playerSkills[3].healthAmount.ToString();
    }
}
