﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilBehaviourTuto : MonoBehaviour
{
    public GameObject monsterPick;

    public Image profilAsset;
    public TextMeshProUGUI profilName;
    public TextMeshProUGUI description;

    public TextMeshProUGUI[] hashtag;
    public TextMeshProUGUI level;

    public GameObject certification;

    public RectTransform rectTrans;

    public Button superlikeButton;

    public GameObject yesTampon;
    public GameObject nopeTampon;

    public GameObject rareSparkles;

    private void Start()
    {
        rectTrans = gameObject.GetComponent<RectTransform>();

        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;

        rectTrans.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    public void Superlike()
    {
        MenuManagerTuto.Instance.matchManager.Match(true);
    }

    public void Initialisation(bool firstTime)
    {
        if (firstTime)
        {
            //Debug.Log("Initialisation ProfilBehvaiour");
            monsterPick = MenuManagerTuto.Instance.matchManager.monsterPresented;
            profilAsset.sprite = monsterPick.GetComponent<MonsterToken>().profilPicture;

            description.text = monsterPick.GetComponent<MonsterToken>().description;

            profilName.text = monsterPick.GetComponent<MonsterToken>().monsterName;

            //Reset 

            if (monsterPick.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                rareSparkles.SetActive(true);
            }
            else
            {
                rareSparkles.SetActive(false);
            }

            for (int i = 0; i < hashtag.Length; i++)
            {
                hashtag[i].text = "#" + monsterPick.GetComponent<MonsterToken>().monsterHashTag[i];
            }

            level.text = "Niveau : " + monsterPick.GetComponent<MonsterToken>().monsterLevel.ToString();

            if (monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
            {
                certification.SetActive(true);
            }
            else
            {
                certification.SetActive(false);
            }
        }
        else
        {
            if (monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
            {
                certification.SetActive(true);
            }
            else
            {
                certification.SetActive(false);
            }
        }


    }

    public void MatchAnim(int lenght, bool isSuperlike)
    {
        StartCoroutine(MatchFade(lenght, isSuperlike));
    }

    public void DisLikeAnim(int lenght)
    {
        StartCoroutine(DislikeFade(lenght));
    }



    public void Destroy()
    {
        Destroy(gameObject);
    }

    IEnumerator MatchFade(int lenght, bool isSuperlike)
    {
        if (isSuperlike)
        {
            for (int i = 0; i < lenght; i++)
            {
                Vector3 translate = new Vector3(60f, 0, 1);
                gameObject.transform.Translate(translate);
                yield return null;
            }
        }
        else
        {
            for (int i = 0; i < lenght; i++)
            {
                Vector3 translate = new Vector3(100, 0, 1);
                gameObject.transform.Translate(translate);
                yield return null;
            }
        }


        Destroy();
    }

    IEnumerator DislikeFade(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            Vector3 translate = new Vector3(-50, 0, 1);
            gameObject.transform.Translate(translate);
            yield return null;

        }
        Destroy();
    }
}
