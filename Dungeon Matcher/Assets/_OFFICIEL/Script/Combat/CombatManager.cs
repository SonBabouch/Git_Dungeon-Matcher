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
    public bool inCombat = false;
    public int combatInt = 0;

    public List<Button> combatButtons;
    [Header("BoutonsUI")]
    public Image[] capacityIcon;
    public Image[] capacityType;
    public TextMeshProUGUI[] energyCostText;
    public TextMeshProUGUI[] damageText;

    public float energyPerSeconds;
    public Skill selectedSkill = null;

    public Sprite[] iconRessource;

    //Combo = 0 /  
    public Sprite[] typeRessource;

    public Color initialButtonColor;
    public Color selectedButton;

    public TextMeshProUGUI attackDetails;

    [Header("PlayerDeath")]
    public GameObject blackScreen;
    public GameObject breakingHeart;
    public GameObject conseillere;
    public GameObject initialPosition;
    public GameObject tweenPosition;
    private string currentText;
    [SerializeField] private string MessageToTell;
    public TextMeshProUGUI bubbleText;
    public GameObject bubble;

    [Header("Alerte")]
    [SerializeField] private GameObject alerteText;
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


    public void InitializeBattle()
    {
        MenuTransitionCombat.Instance.topOfBG.GetComponent<Tweener>().TweenPositionTo(MenuTransitionCombat.Instance.topOfBGTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        
        Enemy.Instance.enemyMonsters = Management.MenuManager.Instance.matchManager.matchList;
        Enemy.Instance.currentMonster = Management.MenuManager.Instance.matchManager.matchList[MenuTransitionCombat.Instance.numberOfBattle];
        secondsLeft = maxSecondsLeft;
        
        CharacterSkillInitialisation();

        Player.Instance.health = Player.Instance.minHealth;

        Player.Instance.energy = 50;
        Player.Instance.trueEnergy = 5;
        Enemy.Instance.energy = 50;
        Enemy.Instance.trueEnergy = 5;

        StartCoroutine(PlayerEnergyGenerator());
        StartCoroutine(EnemyEnergyGenerator());

        //Faire que tout le monde puisse jouer.
        Player.Instance.canAttack = true;
        Enemy.Instance.canAttack = true;
        ConversationManager.Instance.canAttack = true;
        inCombat = true;
        isCombatEnded = false;

        Enemy.Instance.EnemyBehavior();
        Player.Instance.lastPlayerCompetence = null;
        Enemy.Instance.lastEnemyCompetence = null;
        ButtonsInfos();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitializeBattle();
        }

        if (inCombat)
        {
            RunningTimer();
            ButtonIndex();
            UpdateHealthPoint();

            if(Player.Instance.health >= Player.Instance.maxHealth)
            {
                PlayerDeath();
            }
        }
    }

    public void PlayerDeath()
    {
        inCombat = false;
        StartCoroutine(PlayerDeathEnum());
    }

    public IEnumerator PlayerDeathEnum()
    {
        //Bloquer les Compétences/
        isCombatEnded = true;
        Player.Instance.canAttack = false;
        Enemy.Instance.canAttack = false;
        ConversationManager.Instance.canAttack = false;
        ResetBools();

        float maxWaitingTime = 0f;

        //Tween Les message du Player;
        for (int i = 0; i < ConversationManager.Instance.allMsg.Length; i++)
        {
            if (ConversationManager.Instance.allMsg[i] != null)
            {
                if (ConversationManager.Instance.allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
                {
                    float currentWaitingTime = Random.Range(1f, 2f);
                    ConversationManager.Instance.allMsg[i].GetComponent<Tweener>().TweenPositionTo(ConversationManager.Instance.tweenPositionPlayer.transform.localPosition, currentWaitingTime, Easings.Ease.SmoothStep, true);
                    Vector3 rotationVector = new Vector3(0, 0, Random.Range(-10f,10f));
                    ConversationManager.Instance.allMsg[i].GetComponent<Tweener>().TweenEulerTo(rotationVector,0.2f,Easings.Ease.SmoothStep,true);

                    if (maxWaitingTime == 0)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }
                    
                    if(currentWaitingTime >= maxWaitingTime)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }
                }
            }
        }

        //Tween Les message du Enemy;
        for (int i = 0; i < ConversationManager.Instance.allMsg.Length; i++)
        {
            if (ConversationManager.Instance.allMsg[i] != null)
            {
                if (ConversationManager.Instance.allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Enemy)
                {
                    float currentWaitingTime = Random.Range(1f, 2f);
                    ConversationManager.Instance.allMsg[i].GetComponent<Tweener>().TweenPositionTo(ConversationManager.Instance.tweenPositionPlayer.transform.localPosition, currentWaitingTime, Easings.Ease.SmoothStep, true);
                    Vector3 rotationVector = new Vector3(0, 0, Random.Range(-10f, 10f));
                    ConversationManager.Instance.allMsg[i].GetComponent<Tweener>().TweenEulerTo(rotationVector, 0.2f, Easings.Ease.SmoothStep, true);


                    if (currentWaitingTime >= maxWaitingTime)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }
                }
            }
        }

        yield return new WaitForSeconds(maxWaitingTime);        //Tous les messages sont tombés

        //SetActive l'écran noir
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        breakingHeart.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        conseillere.GetComponent<Tweener>().TweenPositionTo(tweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        bubble.SetActive(true);
        Vector3 scaleVector = new Vector3(1f, 1f, 1f);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector,0.5f,Easings.Ease.SmoothStep);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AffichageMessageEnum(0.03f, MessageToTell));
        yield return new WaitForSeconds(3.2f);
        //popButton to skip;
    }

    public IEnumerator AffichageMessageEnum(float delay, string fullText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            bubbleText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }


    public void CharacterSkillInitialisation()
    {
        Player.Instance.InitializePlayer();
        Enemy.Instance.InitializeMonster();
        Enemy.Instance.SetEnemyHandAndDraw();
    }

    public IEnumerator PlayerEnergyGenerator()
    {
        yield return new WaitForSeconds(0.1f);

        //Vitesse de rechargement de la Jauge d'Inspiration en fonction des effets en jeu
        if (!Player.Instance.isAccelerated && !Player.Instance.isSlowed)
        {
            Player.Instance.energy += energyPerSeconds * Player.Instance.modifierEnergy;
            //Debug.Log("Normal");
        }
        else if (Player.Instance.isSlowed && Player.Instance.isAccelerated)
        {
            Player.Instance.energy += energyPerSeconds * Player.Instance.modifierEnergy;
            //Debug.Log("Normal");
        }
        else if (Player.Instance.isAccelerated && !Player.Instance.isSlowed)
        {
            Player.Instance.energy += energyPerSeconds * Player.Instance.upgradeModifierEnergy;
            //Debug.Log("Accelerated");
        }
        else if (!Player.Instance.isAccelerated && Player.Instance.isSlowed)
        {
            Player.Instance.energy += energyPerSeconds * Player.Instance.downgradeModifierEnergy;
            //Debug.Log("Slowed");
        }

        if (Player.Instance.energy > Player.Instance.maxEnergy)
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

        if (!Enemy.Instance.isAccelerated)
        {
            Enemy.Instance.energy += energyPerSeconds * Enemy.Instance.energyModifierEnemy;
        }
        else if (!Enemy.Instance.isSlowed)
        {
            Enemy.Instance.energy += energyPerSeconds * Enemy.Instance.energyModifierEnemy;
        }
        else if (Enemy.Instance.isSlowed && Enemy.Instance.isAccelerated)
        {
            Enemy.Instance.energy += energyPerSeconds * Enemy.Instance.energyModifierEnemy;
        }
        else if (Enemy.Instance.isAccelerated)
        {
            Enemy.Instance.energy += energyPerSeconds * Enemy.Instance.upgradeModifierEnergy;
        }
        else if (Enemy.Instance.isSlowed)
        {
            Enemy.Instance.energy += energyPerSeconds * Enemy.Instance.downgradeModifierEnergy;
        }

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
        Enemy.Instance.health -= 0.5f;
        secondsLeft -= 1;

        if(secondsLeft == 20 || secondsLeft == 10) {

            StartCoroutine(AlerteMessage(secondsLeft));
        }

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
            //Stoper les Coroutines des attacks
            if (Player.Instance.isCharging)
            {
                StopCoroutine(Player.Instance.PlayerChargeAttack(Player.Instance.lastPlayerCompetence));
                StopCoroutine(Player.Instance.EndPlayerChargeAttack(Player.Instance.lastPlayerCompetence));
                ConversationManager.Instance.playerChargingAttack.SetActive(false);
            }

            if (Enemy.Instance.isCharging)
            {
                StopCoroutine(Enemy.Instance.EnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));
                StopCoroutine(Enemy.Instance.EndEnemyChargeAttack(Enemy.Instance.lastEnemyCompetence));
                ConversationManager.Instance.enemyChargingAttack.SetActive(false);
            }

            isCombatEnded = true;
            inCombat = false;
            OnCombatEnd();
        }
    }

    void UpdateHealthPoint()
    {
        if(Enemy.Instance.health < 0)
        {
            Enemy.Instance.health = 0;
        }
        
        if(Enemy.Instance.health > 100)
        {
            Enemy.Instance.health = 100;
        }
    }
    //Se lance à la fin d'un combat.
    public void OnCombatEnd()
    {
        ConversationManager.Instance.canAttack = false;
        Enemy.Instance.canAttack = false;
        Player.Instance.canAttack = false;
        isCombatEnded = false;
        MenuTransitionCombat.Instance.numberOfBattle++;
        MenuTransitionCombat.Instance.ShowCombatDetails(Enemy.Instance.health);
    }

    public void ContinueCombat()
    {
        if (combatInt < Enemy.Instance.enemyMonsters.Count)
        {
            //Lancer une autre Coroutine puis celle ci;
            MenuTransitionCombat.Instance.DisableDetails();
        }
        else
        {
            //Pour garder les references et pas être coupé dans l'animation.
            MenuTransitionCombat.Instance.GoToMenuTransition(); 
        }
    }

    public void NoEchoFeedback()
    {
        for (int i = 0; i < Player.Instance.playerHand.Count; i++)
        {
            if (Player.Instance.playerHand[i].isEcho && Player.Instance.lastPlayerCompetence ==null)
            {
                CombatManager.Instance.combatButtons[i].gameObject.GetComponent<Image>().color= Color.grey;
            }
            else if (Player.Instance.playerHand[i].isPlagiat && Enemy.Instance.lastEnemyCompetence == null)
            {
                CombatManager.Instance.combatButtons[i].gameObject.GetComponent<Image>().color = Color.grey;
            }
        }
    }

    public void ResetBools()
    {
        Player.Instance.isSkillUsed = false;
        Player.Instance.isBurn = false;
        Player.Instance.isCurse = false;
        Player.Instance.isCharging = false;
        Player.Instance.isCramp = false;
        Player.Instance.isCombo = false;
        Player.Instance.isDefending = false;
        Player.Instance.isBoosted = false;
        Player.Instance.isAccelerated = false;
        Player.Instance.isSlowed = false;

        Enemy.Instance.isCurse = false;
        Enemy.Instance.isCharging = false;
        Enemy.Instance.isCramp = false;
        Enemy.Instance.isCombo = false;
        Enemy.Instance.isDefending = false;
        Enemy.Instance.isBoosted = false;
        Enemy.Instance.isAccelerated = false;
        Enemy.Instance.isSlowed = false;
    }

    public IEnumerator AlerteMessage(int timer)
    {
        alerteText.GetComponent<TextMeshProUGUI>().text = "Il reste " + timer.ToString() + " secondes !";

        //Debug.Log("Middle");
        alerteText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 250);
        byte alphaText = 250;

        for (int i = 250; i > 0; i -= 25)
        {
            alphaText -= 25;
            yield return new WaitForSeconds(0.1f);
            alerteText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, alphaText);
        }

        for(int i = 0; i<250; i += 25)
        {
            alphaText += 25;
            yield return new WaitForSeconds(0.1f);
            alerteText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, alphaText);
        }
    

        yield return new WaitForSeconds(0.3f);
        alphaText = 0;
        alerteText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
        //Debug.Log("End");
    }

    #region Buttons
    void ButtonsInfos()
    {
        NoEchoFeedback();

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
                Player.Instance.playerHand[i].isComboSkill = Player.Instance.lastPlayerCompetence.isComboSkill;
                Player.Instance.playerHand[i].comboEffectValue = Player.Instance.lastPlayerCompetence.comboEffectValue;
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
                Player.Instance.playerHand[i].chargingAttack = Enemy.Instance.lastEnemyCompetence.chargingAttack;
                Player.Instance.playerHand[i].isComboSkill = Enemy.Instance.lastEnemyCompetence.isComboSkill;
                Player.Instance.playerHand[i].comboEffectValue = Enemy.Instance.lastEnemyCompetence.comboEffectValue;
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

            energyCostText[0].color = Color.white;
            energyCostText[1].color = Color.white;
            energyCostText[2].color = Color.white;
            energyCostText[3].color = Color.white;
        }

        damageText[0].text = Player.Instance.playerHand[0].skillName;
        damageText[1].text = Player.Instance.playerHand[1].skillName;
        damageText[2].text = Player.Instance.playerHand[2].skillName;
        damageText[3].text = Player.Instance.playerHand[3].skillName;

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
                case Skill.capacityType.Slowdown:
                    spriteToShow = iconRessource[16];
                    break;
                case Skill.capacityType.Acceleration:
                    spriteToShow = iconRessource[17];
                    break;
                case Skill.capacityType.Confuse:
                    spriteToShow = iconRessource[18];
                    break;
                default:
                    break;
            }

            capacityIcon[i].sprite = spriteToShow;
        }
    }

    public void EmojiIcon(Image image,Skill skill)
    {
       Sprite spriteToShow = null;

            switch (skill.typeOfCapacity)
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
                case Skill.capacityType.Slowdown:
                    spriteToShow = iconRessource[16];
                    break;
                case Skill.capacityType.Acceleration:
                    spriteToShow = iconRessource[17];
                    break;
                case Skill.capacityType.Confuse:
                    spriteToShow = iconRessource[18];
                    break;
                    default:
                    break;
            }
        image.sprite = spriteToShow;
        spriteToShow = null;
    }
    public void MessageIcon(GameObject message,Skill skill)
    {
        
            Sprite spriteToShow = null;

            switch (skill.typeOfCapacity)
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
                case Skill.capacityType.Slowdown:
                    spriteToShow = iconRessource[16];
                    break;
                case Skill.capacityType.Acceleration:
                    spriteToShow = iconRessource[17];
                    break;
                case Skill.capacityType.Confuse:
                    spriteToShow = iconRessource[18];
                    break;
                    default:
                    break;
            }

        if (skill.messageType == Skill.typeOfMessage.Big || skill.messageType == Skill.typeOfMessage.Small)
        {
            message.GetComponent<MessageBehaviour>().iconeImage.sprite = spriteToShow;
        }

        spriteToShow = null;
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
                        spriteToShow = typeRessource[0]; //Combo = 0
                    }
                    else
                    {
                        spriteToShow = typeRessource[1]; //basic = 0
                    }
                    break;
                case Skill.typeOfMessage.Big:
                    spriteToShow = typeRessource[2]; // Chargée = 2
                    break;
                case Skill.typeOfMessage.Emoji:
                    spriteToShow = typeRessource[3]; //Emoji = 3
                    break;
                
                default:
                    break;
            }

            capacityType[i].sprite = spriteToShow;
        }
    }

    public void ButtonsInitialization()
    {
        Player.Instance.UpdateComboVisuel();
    }
    public void ButtonsUpdate()
    {
        ResetButtons();
        Player.Instance.PlayerSwapSkill(index);
        ButtonsInfos();
    }

    public void UseAttack()
    {
        if(selectedSkill != null && !Player.Instance.isCharging)
        {
            if(Player.Instance.isCramp && Player.Instance.energy >= selectedSkill.crampEnergyCost)
            {
                selectedSkill.Use();
                selectedSkill = null;
                UpdateDescription();
            }
            else if(Player.Instance.energy >= selectedSkill.initialEnergyCost)
            {
                selectedSkill.Use();
                selectedSkill = null;
                UpdateDescription();
            }
        }
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
        if(combatButtons[0].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = Player.Instance.playerHand[0];
            index = 0;

        }
        else if (combatButtons[1].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = Player.Instance.playerHand[1];
            index = 1;
        }
        else if (combatButtons[2].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = Player.Instance.playerHand[2];
            index = 2;
        }
        else if (combatButtons[3].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = Player.Instance.playerHand[3];
            index = 3;
        }
        UpdateDescription();
        UpdateSelectedVisuel();
        NoEchoFeedback();
    }

    public void UpdateSelectedVisuel()
    {
        for (int i = 0; i < combatButtons.Count; i++)
        {
            combatButtons[i].gameObject.GetComponent<Image>().color = initialButtonColor;
        }

        combatButtons[index].gameObject.GetComponent<Image>().color = selectedButton;
    }

    public void UpdateDescription()
    {
        if(selectedSkill != null)
        {
            if(selectedSkill.isEcho)
            {
                attackDetails.text = selectedSkill.skillDescription + "(" +(Player.Instance.lastPlayerCompetence.skillName) + ")";
            }
            else if(selectedSkill.isPlagiat)
            {
                attackDetails.text = selectedSkill.skillDescription + "(" + (Enemy.Instance.lastEnemyCompetence.skillName) + ")";
            }
            else if (selectedSkill.isComboSkill)
            {
                attackDetails.text = selectedSkill.skillDescription + "(Combo: " + selectedSkill.comboEffectValue + ")";
            }
            else
            {
                attackDetails.text = selectedSkill.skillDescription;
            }

        }
        else
        {
            attackDetails.text = "";
        }
    }

        
    #endregion
}
