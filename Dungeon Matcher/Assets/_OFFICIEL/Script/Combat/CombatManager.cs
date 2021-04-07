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

    public bool isCombatEnded = false;

    public List<Button> combatButtons;
    [Header("BoutonsUI")]
    public Image[] capacityIcon;
    public Image[] capacityType;
    public TextMeshProUGUI[] energyCostText;
    public TextMeshProUGUI[] damageText;

    public Sprite[] iconRessource;

    //Combo = 0 /  
    public Sprite[] typeRessource;

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
        CharacterSkillInitialisation();
        ButtonsInitialization();

        StartCoroutine(PlayerEnergyGenerator());
        StartCoroutine(EnemyEnergyGenerator());
    }

    private void FixedUpdate()
    {
        RunningTimer();
        ButtonIndex();
    }

    public void CharacterSkillInitialisation()
    {
        Player.Instance.InitializePlayer();
        Player.Instance.SetPlayerHandAndDraw();
        Enemy.Instance.InitializeMonster();
        Enemy.Instance.SetEnemyHandAndDraw();
    }
    public IEnumerator PlayerEnergyGenerator()
    {
       
        yield return new WaitForSeconds(0.1f);
        
        Player.Instance.energy += 1 * Player.Instance.modifierEnergy;

        if(Player.Instance.energy > Player.Instance.maxEnergy)
        {
            Player.Instance.energy = Player.Instance.maxEnergy;
        }

        GetPlayerTrueEnergy();

        if (!isCombatEnded)
        {
          StartCoroutine(PlayerEnergyGenerator());
        }
    }

    public void GetPlayerTrueEnergy()
    { float energy = Player.Instance.energy;

        if(energy < 10)
        {
            Player.Instance.trueEnergy = 0;
        }
        else if (energy >= 10 && energy <20)
        {
            Player.Instance.trueEnergy = 1;
        }
        else if (energy >= 20 && energy < 30)
        {
            Player.Instance.trueEnergy = 2;
        }
        else if (energy >= 30 && energy < 40)
        {
            Player.Instance.trueEnergy = 3;
        }
        else if (energy >= 40 && energy < 50)
        {
            Player.Instance.trueEnergy = 4;
        }
        else if (energy >= 50 && energy < 60)
        {
            Player.Instance.trueEnergy = 5;
        }
        else if (energy == 60)
        {
            Player.Instance.trueEnergy = 6;
        }
    }
    public IEnumerator EnemyEnergyGenerator()
    {
        yield return new WaitForSeconds(0.1f);
        Enemy.Instance.energy += 1 * Enemy.Instance.energyModifierEnemy;
        if (Enemy.Instance.energy >= Enemy.Instance.maxEnergy)
        {
            Enemy.Instance.energy = Enemy.Instance.maxEnergy;
        }
        GetEnemyTrueEnergy();

        if (!isCombatEnded)
        {
            StartCoroutine(EnemyEnergyGenerator());
        }
    
    }

    public void GetEnemyTrueEnergy()
    {

        float energy = Enemy.Instance.energy;

        if (energy < 10)
        {
            Enemy.Instance.trueEnergy = 0;
        }
        else if (energy >= 10 && energy < 20)
        {
            Enemy.Instance.trueEnergy = 1;
        }
        else if (energy >= 20 && energy < 30)
        {
            Enemy.Instance.trueEnergy = 2;
        }
        else if (energy >= 30 && energy < 40)
        {
            Enemy.Instance.trueEnergy = 3;
        }
        else if (energy >= 40 && energy < 50)
        {
            Enemy.Instance.trueEnergy = 4;
        }
        else if (energy >= 50 && energy < 60)
        {
            Enemy.Instance.trueEnergy = 5;
        }
        else if (energy == 60)
        {
            Enemy.Instance.trueEnergy = 6;
        }
    }
    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        Enemy.Instance.health -= 1f;
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

    #region Buttons
    void ButtonsInfos()
    {
        for (int i = 0; i < Player.Instance.playerDraw.Count; i++)
        {
            if (Player.Instance.playerDraw[i].isEcho)
            {
                Player.Instance.playerDraw[i].typeOfCapacity = Skill.capacityType.Echo;
                Player.Instance.playerDraw[i].messageType = Skill.typeOfMessage.Small;
                Player.Instance.playerDraw[i].isComboSkill = false;
                Player.Instance.playerDraw[i].chargingAttack = false;
            }
        }

        for (int i = 0; i < Enemy.Instance.enemyDraw.Count; i++)
        {
            if (Enemy.Instance.enemyDraw[i].isEcho)
            {
                Enemy.Instance.enemyDraw[i].typeOfCapacity = Skill.capacityType.Echo;
                Enemy.Instance.enemyDraw[i].messageType = Skill.typeOfMessage.Small;
                Enemy.Instance.enemyDraw[i].isComboSkill = false;
                Enemy.Instance.enemyDraw[i].chargingAttack = false;
            }
        }

        for (int i = 0; i < Player.Instance.playerHand.Count; i++)
        {
            if(Player.Instance.playerHand[i].isEcho && Player.Instance.lastPlayerCompetence != null)
            {
                Player.Instance.playerHand[i].typeOfCapacity = Player.Instance.lastPlayerCompetence.typeOfCapacity;
                Player.Instance.playerHand[i].initialEnergyCost = Player.Instance.lastPlayerCompetence.initialEnergyCost;
                Player.Instance.playerHand[i].trueEnergyCost = Player.Instance.lastPlayerCompetence.trueEnergyCost;
                Player.Instance.playerHand[i].crampEnergyCost = Player.Instance.lastPlayerCompetence.crampEnergyCost;
                Player.Instance.playerHand[i].messageType = Player.Instance.lastPlayerCompetence.messageType;
                Player.Instance.playerHand[i].isComboSkill = Player.Instance.lastPlayerCompetence.isComboSkill;
                Player.Instance.playerHand[i].chargingAttack = Player.Instance.lastPlayerCompetence.chargingAttack;
            }

            if (Player.Instance.playerHand[i].typeOfCapacity == Skill.capacityType.Plagiat && Enemy.Instance.lastEnemyCompetence != null)
            {
                Player.Instance.playerHand[i].typeOfCapacity = Enemy.Instance.lastEnemyCompetence.typeOfCapacity;
                Player.Instance.playerHand[i].initialEnergyCost = Enemy.Instance.lastEnemyCompetence.initialEnergyCost;
                Player.Instance.playerHand[i].trueEnergyCost = Enemy.Instance.lastEnemyCompetence.trueEnergyCost;
                Player.Instance.playerHand[i].crampEnergyCost = Enemy.Instance.lastEnemyCompetence.crampEnergyCost;
                Player.Instance.playerHand[i].messageType = Enemy.Instance.lastEnemyCompetence.messageType;
                Player.Instance.playerHand[i].isComboSkill = Enemy.Instance.lastEnemyCompetence.isComboSkill;
                Player.Instance.playerHand[i].chargingAttack = Enemy.Instance.lastEnemyCompetence.chargingAttack;
            }
        }


        if (Player.Instance.isCramp)
        {
            int cost1 = Player.Instance.playerHand[0].trueEnergyCost + 1;
            int cost2 = Player.Instance.playerHand[1].trueEnergyCost + 1;
            int cost3 = Player.Instance.playerHand[2].trueEnergyCost + 1;
            int cost4 = Player.Instance.playerHand[3].trueEnergyCost + 1;

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
            energyCostText[0].text =  Player.Instance.playerHand[0].trueEnergyCost.ToString();
            energyCostText[1].text =  Player.Instance.playerHand[1].trueEnergyCost.ToString();
            energyCostText[2].text =  Player.Instance.playerHand[2].trueEnergyCost.ToString();
            energyCostText[3].text =  Player.Instance.playerHand[3].trueEnergyCost.ToString();

            energyCostText[0].color = Color.black;
            energyCostText[1].color = Color.black;
            energyCostText[2].color = Color.black;
            energyCostText[3].color = Color.black;
        }

        damageText[0].text = Player.Instance.playerHand[0].skillDescription;
        damageText[1].text = Player.Instance.playerHand[1].skillDescription;
        damageText[2].text = Player.Instance.playerHand[2].skillDescription;
        damageText[3].text = Player.Instance.playerHand[3].skillDescription;

        ButtonIcon();
        ButtonTypeOf();

        Player.Instance.UpdateComboVisuel();
    }

    public void ButtonIcon()
    {
        for (int i = 0; i < Player.Instance.playerHand.Count; i++)
        {
            Sprite spriteToShow = null;

            switch (Player.Instance.playerHand[i].typeOfCapacity)
            {
                case Skill.capacityType.Attack:
                    spriteToShow = iconRessource[0];
                break;
                case Skill.capacityType.CoupDeVent:
                    spriteToShow = iconRessource[1];
                    break;
                case Skill.capacityType.Defense:
                    spriteToShow = iconRessource[2];
                    break;
                case Skill.capacityType.DivinTouch:
                    spriteToShow = iconRessource[3];
                    break;
                case Skill.capacityType.Drain:
                    spriteToShow = iconRessource[4];
                    break;
                case Skill.capacityType.Echo:
                    spriteToShow = iconRessource[5];
                    break;
                case Skill.capacityType.Heal:
                    spriteToShow = iconRessource[6];
                    break;
                case Skill.capacityType.Paralysie:
                    spriteToShow = iconRessource[7];
                    break;
                case Skill.capacityType.Plagiat:
                    spriteToShow = iconRessource[8];
                    break;
                case Skill.capacityType.Mark:
                    spriteToShow = iconRessource[9];
                    break;
                case Skill.capacityType.Curse:
                    spriteToShow = iconRessource[10];
                    break;
                case Skill.capacityType.Cramp:
                    spriteToShow = iconRessource[11];
                    break;
                case Skill.capacityType.Charm:
                    spriteToShow = iconRessource[12];
                    break;
                case Skill.capacityType.Silence:
                    spriteToShow = iconRessource[13];
                    break;
                case Skill.capacityType.Lock:
                    spriteToShow = iconRessource[14];
                    break;
                case Skill.capacityType.Break:
                    spriteToShow = iconRessource[15];
                    break;
                default:
                    break;
            }

            capacityIcon[i].sprite = spriteToShow;
        }
    }

    public void ButtonTypeOf()
    {
        for (int i = 0; i < Player.Instance.playerHand.Count; i++)
        {
            Sprite spriteToShow = null;

            switch (Player.Instance.playerHand[i].messageType)  
            {
                case Skill.typeOfMessage.Small:
                    if (Player.Instance.playerHand[i].isComboSkill)
                    {
                        spriteToShow = typeRessource[0];
                    }
                    else
                    {
                        spriteToShow = null; //Ou mettre icon qui indique que c'est instant.
                    }
                    break;
                case Skill.typeOfMessage.Big:
                    spriteToShow = typeRessource[1];
                    break;
                case Skill.typeOfMessage.Emoji:
                    spriteToShow = typeRessource[2];
                    break;
                default:
                    break;
            }

            capacityType[i].sprite = spriteToShow;
        }
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
    #endregion
}
