using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuTransitionCombat : MonoBehaviour
{
    public static MenuTransitionCombat Instance;

    #region transitionCombat
    [SerializeField] private GameObject detailsGO;
    [SerializeField] private Image leaderMonster;
    [SerializeField] private Image leaderMonsterBG;
    [SerializeField] private Image enemyMonster;
    [SerializeField] private Image enemyMonsterBG;
    [SerializeField] private GameObject VS;
    [SerializeField] private GameObject thunder;
    [SerializeField] private GameObject button;
    

    public int numberOfBattle = 0;

    public GameObject topSlider0;
    public GameObject topSlider1;
    public GameObject botSlider0;
    public GameObject botSlider1;

     private Vector3 topSliderInitialPosition;
     private Vector3 botSliderInitialPosition;

    [SerializeField] private GameObject topSliderTweenPosition;
    [SerializeField] private GameObject botSliderTweenPosition;

    public GameObject topOfBG;
    public GameObject topOfBGInitialPosition;
    public GameObject topOfBGTweenPosition;
    
    
    #endregion

    #region CountdownCombat
    [SerializeField] private GameObject consiellere;

    private Vector3 initialPosition;
    [SerializeField] private GameObject tweenPositionCt;

    [SerializeField] private GameObject number3;
    [SerializeField] private GameObject number2;
    [SerializeField] private GameObject number1;
    [SerializeField] private GameObject matchText;
    #endregion

    #region CombatResultats
    [SerializeField] private Image heart;
    [SerializeField] private Image heartBG;
    [SerializeField] private TextMeshProUGUI results;
    [SerializeField] private Button skipButton;
    [SerializeField] private GameObject buttonToPass;
    #endregion

    public GameObject startCombatButton;

    public List<float> storedValue = new List<float>();

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

        
        topSliderInitialPosition = topSlider0.transform.localPosition;
        botSliderInitialPosition = botSlider0.transform.localPosition;
    }

    #region SlideTransition
    public void TransitionSlideIn()
    {
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
    }

    public void TransitionSlideOut()
    {
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
    }
    #endregion

    //1- Le joueur appuie sur le bouton "Lancer Combat".
    public void StartCombatCoroutine()
    {
        //Si il reste des combats à faire, on lance ca.
        if(numberOfBattle < Management.MenuManager.Instance.matchManager.matchList.Count)
        {
            StartCoroutine(AnnonceMonstreEnum(Management.MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().fullMonsterImage
                , Management.MenuManager.Instance.matchManager.matchList[numberOfBattle].GetComponent<MonsterToken>().fullMonsterImage));
        }
        else
        {
            //Sinon on va au menu.
            GoToMenuTransition();
        }

    }

    public void EndPlayerDeath()
    {
        StartCoroutine(EndPlayerDeathEnum());
    }

    public IEnumerator EndPlayerDeathEnum()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Start");
        Management.MenuManager.Instance.blockAction = true;
        //Faire le Resultat des monstres;

        for (int i = 0; i < Management.MenuManager.Instance.listManager.listPrefab.Count; i++)
        {
            
            
            Vector3 scaleVector = new Vector3 (1, 1, 1);
            Management.MenuManager.Instance.listManager.listPrefab[i].GetComponent<CombatProfilList>().noClaimFeedback.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
            Debug.Log("i");
            
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < Management.MenuManager.Instance.listManager.listPrefab.Count; i++)
        {
            Destroy(Management.MenuManager.Instance.listManager.listPrefab[i].gameObject);
        }

        Management.MenuManager.Instance.listManager.listPrefab.Clear();
        Management.MenuManager.Instance.listManager.listCurrentSize = 0;
        Management.MenuManager.Instance.canvasManager.listCanvas.UpdateList();
        Management.MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();


        yield return new WaitForSeconds(0.5f);
        Debug.Log("Finish");
        StartCoroutine(Management.MenuManager.Instance.canvasManager.ScreenFade(1f, Management.MenuManager.Instance.canvasManager.listCanvas.BackGroundResultat));
        StartCoroutine(Management.MenuManager.Instance.canvasManager.TextFade(1f, Management.MenuManager.Instance.canvasManager.listCanvas.BackGroundResultatText));
        yield return new WaitForSeconds(1f);
        Management.MenuManager.Instance.blockAction = false;
    }

    //2- Affichage des monstres si il reste des combats à faire.
    public IEnumerator AnnonceMonstreEnum(Sprite leaderMonsterAsset, Sprite enemyMonsterAsset)
    {
        TransitionSlideIn();        //Le menu slide vers l'avant.
        yield return new WaitForSeconds(1.5f);
        detailsGO.SetActive(true);
        leaderMonster.sprite = leaderMonsterAsset;
        leaderMonsterBG.sprite = leaderMonsterAsset;
        enemyMonster.sprite = enemyMonsterAsset;
        enemyMonsterBG.sprite = enemyMonsterAsset;
        yield return new WaitForSeconds(0.1f);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1f);
        thunder.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        VS.SetActive(true);
        button.SetActive(true);
    }


    //3- Le joueur appuie sur le bouton lors de l'affichage des monstres.
    public void StartCombatAfterAnnonce()
    {
        StartCoroutine(StartCombatAfterAnnonceEnum());
    }
   
    //4- On lance le décompte pour le combat.
    public IEnumerator StartCombatAfterAnnonceEnum()
    {
        #region EndTransition
        //Désactive le bouton
        button.SetActive(false);
        //transition
        TransitionSlideIn();

        yield return new WaitForSeconds(1.5f);
        
        //Désactiver les images et l'affichage d'annonce des messages à la fin de la transiton.
        detailsGO.SetActive(false);
        leaderMonster.sprite = null;
        enemyMonster.sprite = null;

        TransitionSlideOut();
        #endregion

        ManagerManager.Instance.menuManager.SetActive(false);
        ManagerManager.Instance.combatManager.SetActive(true);

        yield return new WaitForSeconds(1f);

        initialPosition = consiellere.transform.localPosition;
        consiellere.GetComponent<Tweener>().TweenPositionTo(tweenPositionCt.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);

        #region Countdown
        yield return new WaitForSeconds(0.5f);
        number3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        number2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        number1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        matchText.SetActive(true);
        #endregion

        //tween de la conseillère.
        consiellere.GetComponent<Tweener>().TweenPositionTo(initialPosition, 1f, Easings.Ease.SmoothStep, true);
        
        yield return new WaitForSeconds(1f);
        
        #region DisableCountdown
        number3.SetActive(false);
        number2.SetActive(false);
        number1.SetActive(false);
        matchText.SetActive(false);
        #endregion

        //5- Le combat se lance.
        ClearCombat();
        yield return new WaitForSeconds(0.1f);
        
        CombatManager.Instance.InitializeBattle();
    }

    public void ClearCombat()
    {
        CombatManager.Instance.ResetBools();
        Player.Instance.playerSkills.Clear();
        Enemy.Instance.enemySkills.Clear();
        ConversationManager.Instance.emojis.Clear();
        Player.Instance.lastPlayerCompetence = null;
        Enemy.Instance.lastEnemyCompetence = null;
       
        //Destructions des Messages;
        for (int i = 0; i < ConversationManager.Instance.allMsg.Length; i++)
        {
            if (ConversationManager.Instance.allMsg[i] != null)
            {
                Destroy(ConversationManager.Instance.allMsg[i].gameObject);
                ConversationManager.Instance.allMsg[i] = null;
            }
        }
    }

    public void ShowCombatDetails(float value)
    {
        StartCoroutine(EnumShowDetails(value));
    }

    public IEnumerator EnumShowDetails(float value)
    {
        CombatManager.Instance.isCombatEnded = true;
        CombatManager.Instance.inCombat = false;
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < ConversationManager.Instance.allMsg.Length; i++)
        {
            if (ConversationManager.Instance.allMsg[i] != null)
            {
                Destroy(ConversationManager.Instance.allMsg[i]);
                ConversationManager.Instance.allMsg[i] = null;
            }
        }

        //reset Bool;

        Vector3 scaleVector = new Vector3(1f, 1f, 1f);
        buttonToPass.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        heart.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        heartBG.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        results.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Button>().enabled = false;

        storedValue.Add(value);

        for (int i = 0; i <= value; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Enemy.Instance.health--;
            results.text = ((i / Enemy.Instance.maxHealth)*100).ToString() + "%";
            Enemy.Instance.enemyUi.UpdateBarreResultat();
        }

        skipButton.GetComponent<Tweener>().TweenScaleTo(scaleVector, 0.3f, Easings.Ease.SmoothStep);
        yield return new WaitForSeconds(0.3f);
        skipButton.GetComponent<Button>().enabled = true;
    }



    //Disable les visuels et relancer le match;
    public void DisableDetails()
    {
        StartCoroutine(EnumDisableDetails());
    }

    private IEnumerator EnumDisableDetails()
    {
        skipButton.GetComponent<Button>().enabled = false;
        topOfBG.GetComponent<Tweener>().TweenPositionTo(topOfBGInitialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        buttonToPass.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        heart.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        heartBG.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        results.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1f);
        SkillFeedback.Instance.EndCombatReset();
        results.text = " ";
        StartCombatCoroutine();
    }


    //retour vers les menus quand plus de combat.
    public void GoToMenuTransition()
    {
        StartCoroutine(FinishCombatGoToMenu());
    }

    public IEnumerator FinishCombatGoToMenu()
    {
        button.SetActive(false);
        
        TransitionSlideIn();

        //Détruire tous les messages.
        for (int i = 0; i < ConversationManager.Instance.allMsg.Length; i++)
        {
            if (ConversationManager.Instance.allMsg[i] != null)
            {
                Destroy(ConversationManager.Instance.allMsg[i]);
                ConversationManager.Instance.allMsg[i] = null;
            }
        }

        yield return new WaitForSeconds(1f);

        ManagerManager.Instance.combatManager.SetActive(false);
        ManagerManager.Instance.menuManager.SetActive(true);
        yield return new WaitForSeconds(1f);
        startCombatButton.SetActive(false);
        Management.MenuManager.Instance.canvasManager.listCanvas.BackGroundResultat.SetActive(true);
        MatchSoundManager.Instance.resultsScreen = true;
        for (int i = 0; i < Management.MenuManager.Instance.listManager.listPrefab.Count; i++)
        {
            Management.MenuManager.Instance.listManager.listPrefab[i].GetComponent<CombatProfilList>().chanceClaim = storedValue[i];

        }
        storedValue.Clear();
        TransitionSlideOut();

        yield return new WaitForSeconds(0.1f);
        numberOfBattle = 0;
        
        Enemy.Instance.health = Enemy.Instance.minHealth;
        Management.MenuManager.Instance.listManager.TestClaim();
    }

    
}
