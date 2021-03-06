﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MatchCanvasManagerTuto : MonoBehaviour
{
    public MatchFeedBackTuto matchFeedback;

    //Apparition du GameObject Profil à un point précis dans la scène.
    public GameObject profilPrefab;
    public GameObject[] profilPosition;

    //Player Information
    public TextMeshProUGUI energy;
    public TextMeshProUGUI superLike;
    public GameObject energySpend;
    public TextMeshProUGUI playerLevelText;
    public Image playerExperienceBar;

    //ShowExpérience
    public bool switchExp = false;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Image experienceShower;

    //Animation Finish;
    private bool zeroFinish = false;
    private bool oneFinish = false;

    //rareStuff;
    [SerializeField] private Image rareBar;
    public bool ThereIsARare = false;

    //Marker
    public GameObject dislikeMarker;
    public GameObject likeMarker;

    //Advertissing dislike to much.
    [SerializeField] private GameObject advertising;
    public Tweener tweener;
    [SerializeField] private GameObject textBubble;
    [SerializeField] private TextMeshProUGUI conseillereMsg;
    [SerializeField] private GameObject buttonSkip;
    [SerializeField] private GameObject initialPosition;
    [SerializeField] private GameObject tweenPosition;

    void Start()
    {
        tweener = advertising.GetComponent<Tweener>();

        UpdateEnergy();

        UpdateExperience();
    }

    //Change le Game State selon le menu actuel
    public void SwitchToListMenu()
    {
        MenuManagerTuto.currentGameStateMenu = MenuManagerTuto.Menu.List;
    }

    //A appeler à chaque fois que le joueur perd ou gagne de l'energie.
    public void UpdateEnergy()
    {
        energy.text = EnergyManagerTuto.energy.ToString() + "/" + EnergyManagerTuto.maxEnergy.ToString();
    }

    public void UpdateSuperLike()
    {
        superLike.text = EnergyManagerTuto.superlikeCount.ToString();
    }

    public void IncreaseRareBar()
    {
        int NeedToFill = 10;

        StartCoroutine(IncreaseBar(NeedToFill));
    }

    //Update le constamment les infos car on peut pas traquer le moment ou le joueur passe de niveau.
    private void Update()
    {
        if (zeroFinish && oneFinish)
        {
            zeroFinish = false;
            oneFinish = false;
            MenuManagerTuto.Instance.matchManager.monsterSpawned[0].SetActive(true);
            MenuManagerTuto.Instance.matchManager.canMatch = true;
        }
    }

    public void UpdateExperience()
    {
        playerLevelText.text = PlayerLevelTuto.playerLevelTuto.ToString();
        playerExperienceBar.fillAmount = (PlayerLevelTuto.currentExperienceTuto / MenuManagerTuto.Instance.playerLevel.requiredExperience[PlayerLevelTuto.playerLevelTuto - 1]);
    }

    public void ResetBarMethod()
    {
        StartCoroutine(ResetBar());
    }

    public void ShowExpérience()
    {
        experienceText.text = PlayerLevelTuto.currentExperienceTuto.ToString() + " / " + MenuManagerTuto.Instance.playerLevel.requiredExperience[PlayerLevelTuto.playerLevelTuto - 1].ToString();

        if (!switchExp)
        {
            switchExp = true;
        }
        else if (switchExp)
        {
            switchExp = false;
        }

        if (switchExp)
        {
            experienceShower.GetComponent<Animator>().SetTrigger("Show");

        }
        else
        {
            experienceShower.GetComponent<Animator>().SetTrigger("Unshow");

        }


    }

    public void UpdateFirstPosition()
    {
        for (int i = 0; i < MenuManagerTuto.Instance.matchManager.monsterSpawned.Count; i++)
        {
            MenuManagerTuto.Instance.matchManager.monsterSpawned[i].transform.SetParent(profilPosition[i].transform);
            MenuManagerTuto.Instance.matchManager.monsterSpawned[i].transform.localPosition = Vector3.zero;
        }
    }

    public void UpdateProfilPosition()
    {
        StartCoroutine(StarterCoroutine());
    }


    IEnumerator IncreaseBar(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            MenuManagerTuto.Instance.matchManager.ChanceCount++;
            rareBar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.05f);

        }
    }

    IEnumerator ResetBar()
    {
        for (int i = 0; i < 100; i++)
        {
            rareBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.001f);
        }

    }

    IEnumerator StarterCoroutine()
    {
        StartCoroutine(ProfilMovement(MenuManagerTuto.Instance.matchManager.monsterSpawned[2], 2));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ProfilMovement(MenuManagerTuto.Instance.matchManager.monsterSpawned[1], 1));
    }

    IEnumerator ProfilMovement(GameObject currentProfil, int currentLoop)
    {
        if (currentProfil.transform.position.y < profilPosition[currentLoop].transform.position.y)
        {
            Vector3 translateVector = new Vector3(0f, 20f, 0f);
            currentProfil.transform.Translate(translateVector);
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(ProfilMovement(currentProfil, currentLoop));
        }
        else
        {
            if (currentLoop == 1)
            {
                zeroFinish = true;
            }

            if (currentLoop == 2)
            {
                oneFinish = true;
            }

            currentProfil.transform.SetParent(profilPosition[currentLoop].transform);
            currentProfil.transform.SetAsFirstSibling();
            currentProfil.transform.localPosition = Vector3.zero;

            yield return null;
        }
    }

    public IEnumerator alerteDislike(string message)
    {
        //Debug.Log("wallah match");
        MenuManagerTuto.Instance.blockAction = true;
        PageSwiper.canChange = false;
        //Changer la boule pour désactiver les boutons.
        conseillereMsg.text = message;
        tweener.TweenPositionTo(tweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1f);
        textBubble.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        buttonSkip.SetActive(true);
        //Debug.Log("End");
    }

    public void AlerteDislike()
    {
        StartCoroutine(alerteDislike("Si tu ne matches pas, tu ne pourras pas devenir plus fort..."));
    }
    public void EndAlerteDislike()
    {
        StartCoroutine(endAlerteDislike());
    }

    public IEnumerator endAlerteDislike()
    {
        buttonSkip.SetActive(false);
        textBubble.SetActive(false);
        tweener.TweenPositionTo(initialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1f);
        MenuManagerTuto.Instance.blockAction = false;
        PageSwiper.canChange = true;
        //changer la bool pour pouvoir réutiliser les boutons.
    }
}
