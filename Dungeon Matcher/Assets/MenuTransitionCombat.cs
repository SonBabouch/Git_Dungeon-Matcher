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
    [SerializeField] private Image enemyMonster;
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

    public void StartCombatCoroutine()
    {
        if(numberOfBattle < Management.MenuManager.Instance.matchManager.matchList.Count)
        {
            StartCoroutine(CombatTransition(Management.MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().fullMonsterImage
                , Management.MenuManager.Instance.matchManager.matchList[numberOfBattle].GetComponent<MonsterToken>().fullMonsterImage));
        }
        else
        {
            GoToMenuTransition();
        }

    }

    public void EndCombatCoroutine()
    {
        StartCoroutine(EndCombatTransition());
    }

    public IEnumerator CombatTransition(Sprite leaderMonsterAsset, Sprite enemyMonsterAsset)
    {
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1.5f);
        detailsGO.SetActive(true);
        leaderMonster.sprite = leaderMonsterAsset;
        enemyMonster.sprite = enemyMonsterAsset;
        yield return new WaitForSeconds(0.1f);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        button.SetActive(true);
    }

    public IEnumerator EndCombatTransition()
    {
        button.SetActive(false);
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1.5f);
        detailsGO.SetActive(false);
        leaderMonster.sprite = null;
        enemyMonster.sprite = null;
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        ManagerManager.Instance.menuManager.SetActive(false);
        ManagerManager.Instance.combatManager.SetActive(true);
        yield return new WaitForSeconds(1f);
        initialPosition = consiellere.transform.localPosition;
        consiellere.GetComponent<Tweener>().TweenPositionTo(tweenPositionCt.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.5f);
        number3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        number2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        number1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        matchText.SetActive(true);
        consiellere.GetComponent<Tweener>().TweenPositionTo(initialPosition, 1f, Easings.Ease.SmoothStep, true);
        
        yield return new WaitForSeconds(1f);
        CombatManager.Instance.InitializeBattle();
        number3.SetActive(false);
        number2.SetActive(false);
        number1.SetActive(false);
        matchText.SetActive(false);
    }

    

    public void ShowCombatDetails(float value)
    {
        StartCoroutine(EnumShowDetails(value));
    }

    public IEnumerator EnumShowDetails(float value)
    {
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
        heart.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        heartBG.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        results.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Button>().enabled = false;

        for (int i = 0; i <= value; i++)
        {
            results.text = i.ToString() + "%";
        }

        storedValue.Add(value);
        yield return new WaitForSeconds(1f);
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
        heart.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        heartBG.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        results.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 1f, Easings.Ease.SmoothStep);
        skipButton.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1f);
        results.text = "0%";
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
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);

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

        for (int i = 0; i < Management.MenuManager.Instance.listManager.listPrefab.Count; i++)
        {
            Management.MenuManager.Instance.listManager.listPrefab[i].GetComponent<CombatProfilList>().chanceClaim = storedValue[i];
            Management.MenuManager.Instance.listManager.listPrefab[i].GetComponent<CombatProfilList>().UpdateClaimChance();
        }

        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition, 1f, Easings.Ease.SmoothStep, true);

        yield return new WaitForSeconds(0.1f);
        numberOfBattle = 0;
        Management.MenuManager.Instance.listManager.TestClaim();
    }
}
