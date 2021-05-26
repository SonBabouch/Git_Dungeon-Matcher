using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFeedback : MonoBehaviour
{
    public static SkillFeedback Instance;
    [SerializeField] private Animator playerAnim;
    //[SerializeField] private Animator enemiAnim;


    #region Player
    [Header("Player Heal")] [Header("Player")]
    [SerializeField] private GameObject playerHeal;

    [Header("Player Defense")]
    [SerializeField] private GameObject playerDefense;
    [SerializeField] private GameObject playerDefenseShield;
    [SerializeField] private GameObject playerGlow;

    [Header("Player Defense Tweening Pos")]
    [SerializeField] private GameObject playerShieldInitialPos;
    [SerializeField] private GameObject playerShieldTweenPos;
    [Space]
    [SerializeField] private GameObject playerGlowInitialPos;
    [SerializeField] private GameObject playerGlowTweenPos;

    [Header("Player Accelerate/Decelerate")]
    [SerializeField] private GameObject playerAccelerateDecelerate;
    #endregion

    [Header("Enemi Heal")] [Header("Enemi")]
    [SerializeField] private GameObject enemiHeal;

    [Header("Enemi Defense")]
    [SerializeField] private GameObject enemiDefense;
    [SerializeField] private GameObject enemiDefenseShield;
    [SerializeField] private GameObject enemiGlow;

    [Header("Enemi Tweening Pos")]
    [SerializeField] private GameObject enemiShieldInitialPos;
    [SerializeField] private GameObject enemiShieldTweenPos;
    [Space]
    [SerializeField] private GameObject enemiGlowInitialPos;
    [SerializeField] private GameObject enemiGlowTweenPos;


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

    public void PlayerDefenseFeedback()
    {
        if (Player.Instance.isDefending)
        {
            playerDefense.SetActive(true);
            playerDefenseShield.GetComponent<Tweener>().TweenPositionTo(playerShieldTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
            playerGlow.GetComponent<Tweener>().TweenPositionTo(playerGlowTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);

        }
        else if (Player.Instance.isDefending == false && playerDefense.activeSelf == true)
        {
            playerDefenseShield.transform.localPosition = playerShieldInitialPos.transform.localPosition;
            playerGlow.transform.localPosition = playerGlowInitialPos.transform.localPosition;
            playerDefense.SetActive(false);
        }
    }

    public IEnumerator PlayerHealFeedback(float numberOfDamage)
    {
        playerHeal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfDamage; i++)
        {
            Player.Instance.health -= 1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        playerHeal.SetActive(false);
    }

    public void PlayerAccelerateDecelerateFeedback()
    {
        if(!Player.Instance.isAccelerated && !Player.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(false);
            playerAnim.SetBool("isAccelerated", false);
            playerAnim.SetBool("isSlowed", false);
        }
        else if (Player.Instance.isAccelerated && Player.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(false);
            playerAnim.SetBool("isAccelerated", false);
            playerAnim.SetBool("isSlowed", false);
        }
        if(Player.Instance.isAccelerated && !Player.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(true);
            playerAnim.SetBool("isAccelerated", true);
            playerAnim.SetBool("isSlowed", false);
        }
        if(Player.Instance.isSlowed && !Player.Instance.isAccelerated)
        {
            playerAccelerateDecelerate.SetActive(true);
            playerAnim.SetBool("isSlowed", true);
            playerAnim.SetBool("isAccelerated", false);
        }

    }

    public IEnumerator EnemiHealFeedback(float numberOfDamage)
    {
        enemiHeal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfDamage; i++)
        {
            Enemy.Instance.health -= 1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        enemiHeal.SetActive(false);
    }

    public void EnemiDefenseFeedback()
    {
        if(Enemy.Instance.isDefending)
        {
            enemiDefense.SetActive(true);
            enemiDefenseShield.GetComponent<Tweener>().TweenPositionTo(enemiShieldTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
            enemiGlow.GetComponent<Tweener>().TweenPositionTo(enemiGlowTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
        }
        else if(Enemy.Instance.isDefending == false && enemiDefense.activeSelf == true)
        {
            enemiDefenseShield.transform.localPosition = enemiShieldInitialPos.transform.localPosition;
            enemiGlow.transform.localPosition = enemiGlowInitialPos.transform.localPosition;
            enemiDefense.SetActive(false);
        }
    }
}
