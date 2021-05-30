using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CombatManagerTuto : MonoBehaviour
{
    public static CombatManagerTuto Instance;
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
    public GameObject skipButton;

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
        MenuTransitionTuto.Instance.topOfBG.GetComponent<Tweener>().TweenPositionTo(MenuTransitionTuto.Instance.topOfBGTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);

        EnemyTuto.Instance.enemyMonsters = MenuManagerTuto.Instance.matchManager.matchList;
        EnemyTuto.Instance.currentMonster = MenuManagerTuto.Instance.matchManager.matchList[MenuTransitionTuto.Instance.numberOfBattle];
        secondsLeft = maxSecondsLeft;

        CharacterSkillInitialisation();

        PlayerTuto.Instance.health = PlayerTuto.Instance.minHealth;

        PlayerTuto.Instance.energy = 50;
        PlayerTuto.Instance.trueEnergy = 5;
        EnemyTuto.Instance.energy = 50;
        EnemyTuto.Instance.trueEnergy = 5;

        StartCoroutine(PlayerEnergyGenerator());
        StartCoroutine(EnemyEnergyGenerator());

        //Faire que tout le monde puisse jouer.
        PlayerTuto.Instance.canAttack = true;
        EnemyTuto.Instance.canAttack = true;
        ConversationManagerTuto.Instance.canAttack = true;
        inCombat = true;
        isCombatEnded = false;

        EnemyTuto.Instance.EnemyBehavior();
        PlayerTuto.Instance.lastPlayerCompetence = null;
        EnemyTuto.Instance.lastEnemyCompetence = null;
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

            if (PlayerTuto.Instance.health >= PlayerTuto.Instance.maxHealth)
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
        PlayerTuto.Instance.canAttack = false;
        EnemyTuto.Instance.canAttack = false;
        ConversationManagerTuto.Instance.canAttack = false;
        ResetBools();

        float maxWaitingTime = 0f;

        //Tween Les message du Player;
        for (int i = 0; i < ConversationManagerTuto.Instance.allMsg.Length; i++)
        {
            if (ConversationManagerTuto.Instance.allMsg[i] != null)
            {
                if (ConversationManagerTuto.Instance.allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
                {
                    float currentWaitingTime = Random.Range(1f, 2f);
                    ConversationManagerTuto.Instance.allMsg[i].GetComponent<Tweener>().TweenPositionTo(ConversationManagerTuto.Instance.tweenPositionPlayer.transform.localPosition, currentWaitingTime, Easings.Ease.SmoothStep, true);
                    Vector3 rotationVector = new Vector3(0, 0, Random.Range(-10f, 10f));
                    ConversationManagerTuto.Instance.allMsg[i].GetComponent<Tweener>().TweenEulerTo(rotationVector, 0.2f, Easings.Ease.SmoothStep, true);

                    if (maxWaitingTime == 0)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }

                    if (currentWaitingTime >= maxWaitingTime)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }
                }
            }
        }

        //Tween Les message du Enemy;
        for (int i = 0; i < ConversationManagerTuto.Instance.allMsg.Length; i++)
        {
            if (ConversationManagerTuto.Instance.allMsg[i] != null)
            {
                if (ConversationManagerTuto.Instance.allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Enemy)
                {
                    float currentWaitingTime = Random.Range(1f, 2f);
                    ConversationManagerTuto.Instance.allMsg[i].GetComponent<Tweener>().TweenPositionTo(ConversationManagerTuto.Instance.tweenPositionPlayer.transform.localPosition, currentWaitingTime, Easings.Ease.SmoothStep, true);
                    Vector3 rotationVector = new Vector3(0, 0, Random.Range(-10f, 10f));
                    ConversationManagerTuto.Instance.allMsg[i].GetComponent<Tweener>().TweenEulerTo(rotationVector, 0.2f, Easings.Ease.SmoothStep, true);


                    if (currentWaitingTime >= maxWaitingTime)
                    {
                        maxWaitingTime = currentWaitingTime;
                    }
                }
            }
        }

        yield return new WaitForSeconds(maxWaitingTime);        //Tous les messages sont tombés

        //SetActive l'écran noir & musique de défaite se met en route
        FightSoundManager.Instance.deathScreen = true;
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        breakingHeart.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        conseillere.GetComponent<Tweener>().TweenPositionTo(tweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        bubble.SetActive(true);
        Vector3 scaleVector = new Vector3(1f, 1f, 1f);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 0.5f, Easings.Ease.SmoothStep);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(AffichageMessageEnum(0.05f, MessageToTell));
        yield return new WaitForSeconds(5.5f);
        //popButton to skip;
        skipButton.SetActive(true);
    }

    public void PlayerDeathGoToMenu()
    {
        StartCoroutine(PlayerDeathGoToMenuEnum());
    }

    public IEnumerator PlayerDeathGoToMenuEnum()
    {
        skipButton.SetActive(false);
        Debug.Log("Here");

        //Reset Combat
        CombatManagerTuto.Instance.ResetBools();
        PlayerTuto.Instance.playerSkills.Clear();
        EnemyTuto.Instance.enemySkills.Clear();
        ConversationManagerTuto.Instance.emojis.Clear();
        PlayerTuto.Instance.lastPlayerCompetence = null;
        EnemyTuto.Instance.lastEnemyCompetence = null;

        //Destructions des Messages;
        for (int i = 0; i < ConversationManagerTuto.Instance.allMsg.Length; i++)
        {
            if (ConversationManagerTuto.Instance.allMsg[i] != null)
            {
                Destroy(ConversationManagerTuto.Instance.allMsg[i].gameObject);
                ConversationManagerTuto.Instance.allMsg[i] = null;
            }
        }

        MenuTransitionTuto.Instance.storedValue.Clear();
        EnemyTuto.Instance.currentMonster = null;
        EnemyTuto.Instance.enemyMonsters.Clear();

        MenuTransitionTuto.Instance.TransitionSlideIn();
        yield return new WaitForSeconds(1f);

        //Reset les textes etc.
        conseillere.transform.localPosition = initialPosition.transform.localPosition;
        bubble.transform.localScale = new Vector3(0, 0, 0);
        bubbleText.text = "";
        currentText = "";
        blackScreen.SetActive(false);
        breakingHeart.SetActive(false);
        FightSoundManager.Instance.deathScreen = false;


        TutorielManager.Instance.MenuGO.SetActive(true);
        MenuTransitionTuto.Instance.startCombatButton.SetActive(false);
        MenuManagerTuto.Instance.canvasManager.listCanvas.BackGroundResultat.SetActive(true);
        MenuTransitionCombat.Instance.TransitionSlideOut();
        TutorielManager.Instance.CombatGO.SetActive(false);


        //Mettre la transition sur Transition Menu.
        MenuTransitionTuto.Instance.EndPlayerDeath();
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
        PlayerTuto.Instance.InitializePlayer();
        EnemyTuto.Instance.InitializeMonster();
        EnemyTuto.Instance.SetEnemyHandAndDraw();
    }

    public IEnumerator PlayerEnergyGenerator()
    {
        yield return new WaitForSeconds(0.1f);

        //Vitesse de rechargement de la Jauge d'Inspiration en fonction des effets en jeu
        if (!PlayerTuto.Instance.isAccelerated && !PlayerTuto.Instance.isSlowed)
        {
            PlayerTuto.Instance.energy += energyPerSeconds * PlayerTuto.Instance.modifierEnergy;
            //Debug.Log("Normal");
        }
        else if (PlayerTuto.Instance.isSlowed && PlayerTuto.Instance.isAccelerated)
        {
            PlayerTuto.Instance.energy += energyPerSeconds * PlayerTuto.Instance.modifierEnergy;
            //Debug.Log("Normal");
        }
        else if (PlayerTuto.Instance.isAccelerated && !PlayerTuto.Instance.isSlowed)
        {
            PlayerTuto.Instance.energy += energyPerSeconds * PlayerTuto.Instance.upgradeModifierEnergy;
            //Debug.Log("Accelerated");
        }
        else if (!PlayerTuto.Instance.isAccelerated && PlayerTuto.Instance.isSlowed)
        {
            PlayerTuto.Instance.energy += energyPerSeconds * PlayerTuto.Instance.downgradeModifierEnergy;
            //Debug.Log("Slowed");
        }

        if (PlayerTuto.Instance.energy > PlayerTuto.Instance.maxEnergy)
        {
            PlayerTuto.Instance.energy = PlayerTuto.Instance.maxEnergy;
        }

        GetPlayerTrueEnergy();

        if (!isCombatEnded)
        {
            StartCoroutine(PlayerEnergyGenerator());
        }
    }

    public void GetPlayerTrueEnergy()
    {
        float energy = PlayerTuto.Instance.energy;

        if (energy < 10)
        {
            PlayerTuto.Instance.trueEnergy = 0;
        }
        else if (energy >= 10 && energy < 20)
        {
            PlayerTuto.Instance.trueEnergy = 1;
        }
        else if (energy >= 20 && energy < 30)
        {
            PlayerTuto.Instance.trueEnergy = 2;
        }
        else if (energy >= 30 && energy < 40)
        {
            PlayerTuto.Instance.trueEnergy = 3;
        }
        else if (energy >= 40 && energy < 50)
        {
            PlayerTuto.Instance.trueEnergy = 4;
        }
        else if (energy >= 50 && energy < 60)
        {
            PlayerTuto.Instance.trueEnergy = 5;
        }
        else if (energy == 60)
        {
            PlayerTuto.Instance.trueEnergy = 6;
        }
    }
    public IEnumerator EnemyEnergyGenerator()
    {
        yield return new WaitForSeconds(0.1f);

        if (!EnemyTuto.Instance.isAccelerated)
        {
            EnemyTuto.Instance.energy += energyPerSeconds * EnemyTuto.Instance.energyModifierEnemy;
        }
        else if (!EnemyTuto.Instance.isSlowed)
        {
            EnemyTuto.Instance.energy += energyPerSeconds * EnemyTuto.Instance.energyModifierEnemy;
        }
        else if (EnemyTuto.Instance.isSlowed && EnemyTuto.Instance.isAccelerated)
        {
            EnemyTuto.Instance.energy += energyPerSeconds * EnemyTuto.Instance.energyModifierEnemy;
        }
        else if (EnemyTuto.Instance.isAccelerated)
        {
            EnemyTuto.Instance.energy += energyPerSeconds * EnemyTuto.Instance.upgradeModifierEnergy;
        }
        else if (EnemyTuto.Instance.isSlowed)
        {
            EnemyTuto.Instance.energy += energyPerSeconds * EnemyTuto.Instance.downgradeModifierEnergy;
        }

        if (EnemyTuto.Instance.energy >= EnemyTuto.Instance.maxEnergy)
        {
            EnemyTuto.Instance.energy = EnemyTuto.Instance.maxEnergy;
        }

        GetEnemyTrueEnergy();

        if (!isCombatEnded)
        {
            StartCoroutine(EnemyEnergyGenerator());
        }

    }

    public void GetEnemyTrueEnergy()
    {
        float energy = EnemyTuto.Instance.energy;

        if (energy < 10)
        {
            EnemyTuto.Instance.trueEnergy = 0;
        }
        else if (energy >= 10 && energy < 20)
        {
            EnemyTuto.Instance.trueEnergy = 1;
        }
        else if (energy >= 20 && energy < 30)
        {
            EnemyTuto.Instance.trueEnergy = 2;
        }
        else if (energy >= 30 && energy < 40)
        {
            EnemyTuto.Instance.trueEnergy = 3;
        }
        else if (energy >= 40 && energy < 50)
        {
            EnemyTuto.Instance.trueEnergy = 4;
        }
        else if (energy >= 50 && energy < 60)
        {
            EnemyTuto.Instance.trueEnergy = 5;
        }
        else if (energy == 60)
        {
            EnemyTuto.Instance.trueEnergy = 6;
        }
    }
    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        EnemyTuto.Instance.health -= 0.5f;
        secondsLeft -= 1;

        if (secondsLeft == 20 || secondsLeft == 10)
        {

            StartCoroutine(AlerteMessage(secondsLeft));
        }

        if (secondsLeft < 10)
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
        else if (secondsLeft <= minSecondsLeft)
        {
            //Stoper les Coroutines des attacks
            if (PlayerTuto.Instance.isCharging)
            {
                StopCoroutine(PlayerTuto.Instance.PlayerChargeAttack(PlayerTuto.Instance.lastPlayerCompetence));
                StopCoroutine(PlayerTuto.Instance.EndPlayerChargeAttack(PlayerTuto.Instance.lastPlayerCompetence));
                ConversationManagerTuto.Instance.playerChargingAttack.SetActive(false);
            }

            if (EnemyTuto.Instance.isCharging)
            {
                StopCoroutine(EnemyTuto.Instance.EnemyChargeAttack(EnemyTuto.Instance.lastEnemyCompetence));
                StopCoroutine(EnemyTuto.Instance.EndEnemyChargeAttack(EnemyTuto.Instance.lastEnemyCompetence));
                ConversationManagerTuto.Instance.enemyChargingAttack.SetActive(false);
            }

            isCombatEnded = true;
            inCombat = false;
            OnCombatEnd();
        }
    }

    void UpdateHealthPoint()
    {
        if (EnemyTuto.Instance.health < 0)
        {
            EnemyTuto.Instance.health = 0;
        }

        if (EnemyTuto.Instance.health > 100)
        {
            EnemyTuto.Instance.health = 100;
        }
    }
    //Se lance à la fin d'un combat.
    public void OnCombatEnd()
    {
        ConversationManagerTuto.Instance.canAttack = false;
        EnemyTuto.Instance.canAttack = false;
        PlayerTuto.Instance.canAttack = false;
        isCombatEnded = false;
        MenuTransitionTuto.Instance.numberOfBattle++;
        MenuTransitionTuto.Instance.ShowCombatDetails(EnemyTuto.Instance.health);
    }

    public void ContinueCombat()
    {
        if (combatInt < EnemyTuto.Instance.enemyMonsters.Count)
        {
            //Lancer une autre Coroutine puis celle ci;
            MenuTransitionTuto.Instance.DisableDetails();
        }
        else
        {
            //Pour garder les references et pas être coupé dans l'animation.
            MenuTransitionTuto.Instance.GoToMenuTransition();
        }
    }

    public void NoEchoFeedback()
    {
        for (int i = 0; i < PlayerTuto.Instance.playerHand.Count; i++)
        {
            if (PlayerTuto.Instance.playerHand[i].isEcho && PlayerTuto.Instance.lastPlayerCompetence == null)
            {
                CombatManagerTuto.Instance.combatButtons[i].gameObject.GetComponent<Image>().color = Color.grey;
            }
            else if (PlayerTuto.Instance.playerHand[i].isPlagiat && EnemyTuto.Instance.lastEnemyCompetence == null)
            {
                CombatManagerTuto.Instance.combatButtons[i].gameObject.GetComponent<Image>().color = Color.grey;
            }
        }
    }

    public void ResetBools()
    {
        PlayerTuto.Instance.isSkillUsed = false;
        PlayerTuto.Instance.isBurn = false;
        PlayerTuto.Instance.isCurse = false;
        PlayerTuto.Instance.isCharging = false;
        PlayerTuto.Instance.isCramp = false;
        PlayerTuto.Instance.isCombo = false;
        PlayerTuto.Instance.isDefending = false;
        PlayerTuto.Instance.isBoosted = false;
        PlayerTuto.Instance.isAccelerated = false;
        PlayerTuto.Instance.isSlowed = false;

        EnemyTuto.Instance.isCurse = false;
        EnemyTuto.Instance.isCharging = false;
        EnemyTuto.Instance.isCramp = false;
        EnemyTuto.Instance.isCombo = false;
        EnemyTuto.Instance.isDefending = false;
        EnemyTuto.Instance.isBoosted = false;
        EnemyTuto.Instance.isAccelerated = false;
        EnemyTuto.Instance.isSlowed = false;
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

        for (int i = 0; i < 250; i += 25)
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

        for (int i = 0; i < PlayerTuto.Instance.playerDraw.Count; i++)
        {
            if (PlayerTuto.Instance.playerDraw[i].isEcho)
            {
                PlayerTuto.Instance.playerDraw[i].typeOfCapacity = Skill.capacityType.Echo;
                PlayerTuto.Instance.playerDraw[i].messageType = Skill.typeOfMessage.Small;
                PlayerTuto.Instance.playerDraw[i].isComboSkill = false;
                PlayerTuto.Instance.playerDraw[i].chargingAttack = false;
            }
        }

        for (int i = 0; i < EnemyTuto.Instance.enemyDraw.Count; i++)
        {
            if (EnemyTuto.Instance.enemyDraw[i].isEcho)
            {
                EnemyTuto.Instance.enemyDraw[i].typeOfCapacity = Skill.capacityType.Echo;
                EnemyTuto.Instance.enemyDraw[i].messageType = Skill.typeOfMessage.Small;
                EnemyTuto.Instance.enemyDraw[i].isComboSkill = false;
                EnemyTuto.Instance.enemyDraw[i].chargingAttack = false;
            }
        }

        for (int i = 0; i < PlayerTuto.Instance.playerHand.Count; i++)
        {
            if (PlayerTuto.Instance.playerHand[i].isEcho && Player.Instance.lastPlayerCompetence != null)
            {
                PlayerTuto.Instance.playerHand[i].typeOfCapacity = PlayerTuto.Instance.lastPlayerCompetence.typeOfCapacity;
                PlayerTuto.Instance.playerHand[i].initialEnergyCost = PlayerTuto.Instance.lastPlayerCompetence.initialEnergyCost;
                PlayerTuto.Instance.playerHand[i].trueEnergyCost = PlayerTuto.Instance.lastPlayerCompetence.trueEnergyCost;
                PlayerTuto.Instance.playerHand[i].crampEnergyCost = PlayerTuto.Instance.lastPlayerCompetence.crampEnergyCost;
                PlayerTuto.Instance.playerHand[i].messageType = PlayerTuto.Instance.lastPlayerCompetence.messageType;
                PlayerTuto.Instance.playerHand[i].isComboSkill = PlayerTuto.Instance.lastPlayerCompetence.isComboSkill;
                PlayerTuto.Instance.playerHand[i].chargingAttack = PlayerTuto.Instance.lastPlayerCompetence.chargingAttack;
                PlayerTuto.Instance.playerHand[i].isComboSkill = PlayerTuto.Instance.lastPlayerCompetence.isComboSkill;
                PlayerTuto.Instance.playerHand[i].comboEffectValue = PlayerTuto.Instance.lastPlayerCompetence.comboEffectValue;
            }

            if (PlayerTuto.Instance.playerHand[i].typeOfCapacity == Skill.capacityType.Plagiat && Enemy.Instance.lastEnemyCompetence != null)
            {
                PlayerTuto.Instance.playerHand[i].typeOfCapacity = EnemyTuto.Instance.lastEnemyCompetence.typeOfCapacity;
                PlayerTuto.Instance.playerHand[i].initialEnergyCost = EnemyTuto.Instance.lastEnemyCompetence.initialEnergyCost;
                PlayerTuto.Instance.playerHand[i].trueEnergyCost = EnemyTuto.Instance.lastEnemyCompetence.trueEnergyCost;
                PlayerTuto.Instance.playerHand[i].crampEnergyCost = EnemyTuto.Instance.lastEnemyCompetence.crampEnergyCost;
                PlayerTuto.Instance.playerHand[i].messageType = EnemyTuto.Instance.lastEnemyCompetence.messageType;
                PlayerTuto.Instance.playerHand[i].isComboSkill = EnemyTuto.Instance.lastEnemyCompetence.isComboSkill;
                PlayerTuto.Instance.playerHand[i].chargingAttack = EnemyTuto.Instance.lastEnemyCompetence.chargingAttack;
                PlayerTuto.Instance.playerHand[i].chargingAttack = EnemyTuto.Instance.lastEnemyCompetence.chargingAttack;
                PlayerTuto.Instance.playerHand[i].isComboSkill = EnemyTuto.Instance.lastEnemyCompetence.isComboSkill;
                PlayerTuto.Instance.playerHand[i].comboEffectValue = EnemyTuto.Instance.lastEnemyCompetence.comboEffectValue;
            }
        }


        if (Player.Instance.isCramp)
        {
            int cost1 = PlayerTuto.Instance.playerHand[0].trueEnergyCost + 1;
            int cost2 = PlayerTuto.Instance.playerHand[1].trueEnergyCost + 1;
            int cost3 = PlayerTuto.Instance.playerHand[2].trueEnergyCost + 1;
            int cost4 = PlayerTuto.Instance.playerHand[3].trueEnergyCost + 1;

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
            energyCostText[0].text = PlayerTuto.Instance.playerHand[0].trueEnergyCost.ToString();
            energyCostText[1].text = PlayerTuto.Instance.playerHand[1].trueEnergyCost.ToString();
            energyCostText[2].text = PlayerTuto.Instance.playerHand[2].trueEnergyCost.ToString();
            energyCostText[3].text = PlayerTuto.Instance.playerHand[3].trueEnergyCost.ToString();

            energyCostText[0].color = Color.white;
            energyCostText[1].color = Color.white;
            energyCostText[2].color = Color.white;
            energyCostText[3].color = Color.white;
        }

        damageText[0].text = PlayerTuto.Instance.playerHand[0].skillName;
        damageText[1].text = PlayerTuto.Instance.playerHand[1].skillName;
        damageText[2].text = PlayerTuto.Instance.playerHand[2].skillName;
        damageText[3].text = PlayerTuto.Instance.playerHand[3].skillName;

        ButtonIcon();
        ButtonTypeOf();

        PlayerTuto.Instance.UpdateComboVisuel();
    }

    public void ButtonIcon()
    {
        for (int i = 0; i < PlayerTuto.Instance.playerHand.Count; i++)
        {
            Sprite spriteToShow = null;

            switch (PlayerTuto.Instance.playerHand[i].typeOfCapacity)
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

    public void EmojiIcon(Image image, Skill skill)
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
    public void MessageIcon(GameObject message, Skill skill)
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
        for (int i = 0; i < PlayerTuto.Instance.playerHand.Count; i++)
        {
            Sprite spriteToShow = null;

            switch (PlayerTuto.Instance.playerHand[i].messageType)
            {
                case Skill.typeOfMessage.Small:
                    if (PlayerTuto.Instance.playerHand[i].isComboSkill)
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
        PlayerTuto.Instance.UpdateComboVisuel();
    }
    public void ButtonsUpdate()
    {
        ResetButtons();
        PlayerTuto.Instance.PlayerSwapSkill(index);
        ButtonsInfos();
    }

    public void UseAttack()
    {
        if (selectedSkill != null && !PlayerTuto.Instance.isCharging)
        {
            if (PlayerTuto.Instance.isCramp && PlayerTuto.Instance.energy >= selectedSkill.crampEnergyCost)
            {
                selectedSkill.Use();
                selectedSkill = null;
                UpdateDescription();
            }
            else if (PlayerTuto.Instance.energy >= selectedSkill.initialEnergyCost)
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
        if (combatButtons[0].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = PlayerTuto.Instance.playerHand[0];
            index = 0;

        }
        else if (combatButtons[1].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = PlayerTuto.Instance.playerHand[1];
            index = 1;
        }
        else if (combatButtons[2].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = PlayerTuto.Instance.playerHand[2];
            index = 2;
        }
        else if (combatButtons[3].GetComponent<MyButton>().isPressed == true && inCombat)
        {
            selectedSkill = PlayerTuto.Instance.playerHand[3];
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
        if (selectedSkill != null)
        {
            if (selectedSkill.isEcho)
            {
                attackDetails.text = selectedSkill.skillDescription + "(" + (PlayerTuto.Instance.lastPlayerCompetence.skillName) + ")";
            }
            else if (selectedSkill.isPlagiat)
            {
                attackDetails.text = selectedSkill.skillDescription + "(" + (EnemyTuto.Instance.lastEnemyCompetence.skillName) + ")";
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

