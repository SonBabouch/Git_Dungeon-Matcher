﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFeedback : MonoBehaviour
{
    public static SkillFeedback Instance;
    [Header("Player")]
    public GameObject playerHeal;
    public GameObject playerDefense;
    public GameObject playerDefenseShield;
    [Header("Object To Tween")]
    [SerializeField] private GameObject initialPos;
    [SerializeField]  private GameObject tweenPos;

    [Header("Enemi")]
    public GameObject enemiHeal;
    //public GameObject enemiDefense;


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
            playerDefenseShield.GetComponent<Tweener>().TweenPositionTo(tweenPos.transform.localPosition, 0.1f, Easings.Ease.SmoothStep, true);

        }
        else if (Player.Instance.isDefending == false && playerDefense.activeSelf == true)
        {
            playerDefenseShield.transform.localPosition = initialPos.transform.localPosition;
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
}
