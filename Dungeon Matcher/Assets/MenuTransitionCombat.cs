using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTransitionCombat : MonoBehaviour
{
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
    private void Awake()
    {
        topSliderInitialPosition = topSlider0.transform.localPosition;
        botSliderInitialPosition = botSlider0.transform.localPosition;
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Z))
        {
           
        }
    }

    public void StartCombatCoroutine()
    {
        StartCoroutine(CombatTransition(Management.MenuManager.Instance.bagManager.monsterTeam[0].GetComponent<MonsterToken>().fullMonsterImage
            ,  Management.MenuManager.Instance.matchManager.matchList[numberOfBattle].GetComponent<MonsterToken>().fullMonsterImage));
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
        numberOfBattle++;
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
}
