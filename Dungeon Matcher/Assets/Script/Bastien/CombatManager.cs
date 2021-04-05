using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
        ButtonsInitialization();
    }

    private void FixedUpdate()
    {
        RunningTimer();
        ButtonIndex();
    }

    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        Enemy.Instance.health -= 1f;
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

    void ButtonsInfos()
    {
        if (Player.Instance.isCramp)
        {
            int cost1 = Player.Instance.playerHand[0].energyCost + 1;
            int cost2 = Player.Instance.playerHand[1].energyCost + 1;
            int cost3 = Player.Instance.playerHand[2].energyCost + 1;
            int cost4 = Player.Instance.playerHand[3].energyCost + 1;

            energyCostText[0].text = "Cost = " + cost1;
            energyCostText[1].text = "Cost = " + cost2;
            energyCostText[2].text = "Cost = " + cost3;
            energyCostText[3].text = "Cost = " + cost4;

            energyCostText[0].color = Color.red;
            energyCostText[1].color = Color.red;
            energyCostText[2].color = Color.red;
            energyCostText[3].color = Color.red;
        }
        else
        {
            energyCostText[0].text = "Cost = " + Player.Instance.playerHand[0].energyCost.ToString();
            energyCostText[1].text = "Cost = " + Player.Instance.playerHand[1].energyCost.ToString();
            energyCostText[2].text = "Cost = " + Player.Instance.playerHand[2].energyCost.ToString();
            energyCostText[3].text = "Cost = " + Player.Instance.playerHand[3].energyCost.ToString();

            energyCostText[0].color = Color.black;
            energyCostText[1].color = Color.black;
            energyCostText[2].color = Color.black;
            energyCostText[3].color = Color.black;
        }

       

        damageText[0].text = Player.Instance.playerHand[0].skillDescription;
        damageText[1].text = Player.Instance.playerHand[1].skillDescription;
        damageText[2].text = Player.Instance.playerHand[2].skillDescription;
        damageText[3].text = Player.Instance.playerHand[3].skillDescription;

        Player.Instance.UpdateComboVisuel();
    }

    public void ButtonsInitialization()
    {
        for (int i = 0; i < combatButtons.Count; i++)
        {
            combatButtons[i].onClick.AddListener(Player.Instance.playerHand[i].Use);
        }
        ButtonsInfos();
        Player.Instance.UpdateComboVisuel();

    }
    public void ButtonsUpdate()
    {
        ResetButtons();
        Player.Instance.PlayerSwapSkill(index);
        
        for (int i = 0; i < combatButtons.Count; i++)
        {
            combatButtons[i].onClick.AddListener(Player.Instance.playerHand[i].Use);
        }
        ButtonsInfos();
        
    }

    public void ResetButtons()
    {
        for (int i = 0; i < combatButtons.Count; i++)
        {
            combatButtons[i].onClick.RemoveAllListeners();
        }
    }

    public int index;
    public void ButtonIndex()
    {
        if(combatButtons[0].GetComponent<MyButton>().isPressed == true)
        {
            index = 0;
        }
        if (combatButtons[1].GetComponent<MyButton>().isPressed == true)
        {
            index = 1;
        }
        if (combatButtons[2].GetComponent<MyButton>().isPressed == true)
        {
            index = 2;
        }
        if (combatButtons[3].GetComponent<MyButton>().isPressed == true)
        {
            index = 3;
        }
    }

}
