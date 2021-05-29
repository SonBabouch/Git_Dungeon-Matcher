﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFeedbackTuto : MonoBehaviour
{
    public static SkillFeedbackTuto Instance;

    private Color32 green = new Color32(50, 202, 31, 255);
    private Color32 red = new Color32(202, 50, 31, 255);


    #region Player statement
    [Header("Player Heal")]
    [Header("Player")]
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
    [SerializeField] private Animator playerAnim;

    [Header("Player Break")]
    [SerializeField] private GameObject playerBreak;
    #endregion

    #region Enemi Statement
    [Header("Enemi Heal")]
    [Header("Enemi")]
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

    [Header("Enemi Accelerate/Decelerate")]
    [SerializeField] private GameObject enemiAccelerateDecelerate;
    [SerializeField] private Animator enemiAnim;

    [Header("Enemi Break")]
    [SerializeField] private GameObject enemiBreak;
    #endregion

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

    #region Player methods
    public void PlayerDefenseFeedback()
    {
        if (PlayerTuto.Instance.isDefending)
        {
            playerDefense.SetActive(true);
            playerDefenseShield.GetComponent<Tweener>().TweenPositionTo(playerShieldTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
            playerGlow.GetComponent<Tweener>().TweenPositionTo(playerGlowTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);

        }
        else if (PlayerTuto.Instance.isDefending == false && playerDefense.activeSelf == true)
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
            PlayerTuto.Instance.health -= 1f;

            if (PlayerTuto.Instance.health < 0)
            {
                PlayerTuto.Instance.health = 0;
            }

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        playerHeal.SetActive(false);
    }

    public void PlayerAccelerateDecelerateFeedback()
    {
        if (!PlayerTuto.Instance.isAccelerated && !PlayerTuto.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(false);
            playerAnim.SetBool("isAccelerated", false);
            playerAnim.SetBool("isSlowed", false);
        }
        else if (PlayerTuto.Instance.isAccelerated && PlayerTuto.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(false);
            playerAnim.SetBool("isAccelerated", false);
            playerAnim.SetBool("isSlowed", false);
        }
        if (PlayerTuto.Instance.isAccelerated && !PlayerTuto.Instance.isSlowed)
        {
            playerAccelerateDecelerate.SetActive(true);
            playerAnim.SetBool("isAccelerated", true);
            playerAnim.SetBool("isSlowed", false);
        }
        if (PlayerTuto.Instance.isSlowed && !PlayerTuto.Instance.isAccelerated)
        {
            playerAccelerateDecelerate.SetActive(true);
            playerAnim.SetBool("isSlowed", true);
            playerAnim.SetBool("isAccelerated", false);
        }

    }

    public IEnumerator PlayerAttackFeedBack()
    {
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
        yield return new WaitForSeconds(0.1f);
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
        yield return new WaitForSeconds(0.1f);
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
        yield return new WaitForSeconds(0.1f);
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
        yield return new WaitForSeconds(0.1f);
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
        yield return new WaitForSeconds(0.1f);
        PlayerTuto.Instance.playerHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
    }

    public IEnumerator PlayerBreakFeedback()
    {
        enemiBreak.SetActive(true);
        yield return new WaitForSeconds(1f);
        enemiBreak.SetActive(false);
    }
    #endregion

    #region Enemi methods
    public IEnumerator EnemiHealFeedback(float numberOfDamage)
    {
        enemiHeal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfDamage; i++)
        {
            EnemyTuto.Instance.health -= 1f;
            if (EnemyTuto.Instance.health < 0)
            {
                EnemyTuto.Instance.health = 0;
            }

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        enemiHeal.SetActive(false);
    }

    public void EnemiDefenseFeedback()
    {
        if (EnemyTuto.Instance.isDefending)
        {
            enemiDefense.SetActive(true);
            enemiDefenseShield.GetComponent<Tweener>().TweenPositionTo(enemiShieldTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
            enemiGlow.GetComponent<Tweener>().TweenPositionTo(enemiGlowTweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);
        }
        else if (EnemyTuto.Instance.isDefending == false && enemiDefense.activeSelf == true)
        {
            enemiDefenseShield.transform.localPosition = enemiShieldInitialPos.transform.localPosition;
            enemiGlow.transform.localPosition = enemiGlowInitialPos.transform.localPosition;
            enemiDefense.SetActive(false);
        }
    }

    public void EnemiAccelerateDecelerateFeedback()
    {
        if (!EnemyTuto.Instance.isAccelerated && !EnemyTuto.Instance.isSlowed)
        {
            enemiAccelerateDecelerate.SetActive(false);
            enemiAnim.SetBool("isAccelerated", false);
            enemiAnim.SetBool("isSlowed", false);
        }
        else if (EnemyTuto.Instance.isAccelerated && EnemyTuto.Instance.isSlowed)
        {
            enemiAccelerateDecelerate.SetActive(false);
            enemiAnim.SetBool("isAccelerated", false);
            enemiAnim.SetBool("isSlowed", false);
        }
        if (EnemyTuto.Instance.isAccelerated && !EnemyTuto.Instance.isSlowed)
        {
            enemiAccelerateDecelerate.SetActive(true);
            enemiAnim.SetBool("isAccelerated", true);
            enemiAnim.SetBool("isSlowed", false);
        }
        if (EnemyTuto.Instance.isSlowed && !EnemyTuto.Instance.isAccelerated)
        {
            enemiAccelerateDecelerate.SetActive(true);
            enemiAnim.SetBool("isSlowed", true);
            enemiAnim.SetBool("isAccelerated", false);
        }

    }

    public IEnumerator EnemiAttackFeedBack()
    {
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
        yield return new WaitForSeconds(0.1f);
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
        yield return new WaitForSeconds(0.1f);
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
        yield return new WaitForSeconds(0.1f);
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
        yield return new WaitForSeconds(0.1f);
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", green);
        yield return new WaitForSeconds(0.1f);
        EnemyTuto.Instance.enemiHealthBar.GetComponent<Image>().material.SetColor("Color_9DC95DD3", red);
    }

    public IEnumerator EnemiBreakFeedback()
    {
        playerBreak.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerBreak.SetActive(false);
    }
    #endregion


    public void EndCombatReset()
    {
        enemiDefenseShield.transform.localPosition = enemiShieldInitialPos.transform.localPosition;
        enemiGlow.transform.localPosition = enemiGlowInitialPos.transform.localPosition;
        enemiDefense.SetActive(false);

        playerDefenseShield.transform.localPosition = playerShieldInitialPos.transform.localPosition;
        playerGlow.transform.localPosition = playerGlowInitialPos.transform.localPosition;
        playerDefense.SetActive(false);
    }

}
