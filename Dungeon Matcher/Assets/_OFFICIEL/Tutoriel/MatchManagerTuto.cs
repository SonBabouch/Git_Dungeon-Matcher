﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManagerTuto : MonoBehaviour
{
    [Header("Lists")]
    //Liste comprenant tous les monstres. Les monstres sont rangés à des index précis.
    public List<GameObject> commonMonsterList = new List<GameObject>();
    //Permis de selectionner la bonne liste de monstre.

    public List<GameObject> rareMonsterList = new List<GameObject>();
    public List<GameObject> choosenList = new List<GameObject>();
    public List<GameObject> matchList = new List<GameObject>();
    public int[] numberCommonPool;
    public int[] numberRarePool;

    public bool canMatch = true;

    public List<GameObject> monsterSpawned = new List<GameObject>();

    [Header("Sécurité")]
    //Cette Liste permet de stocker X valeurs pour ne pas qu'il retombe.
    //Cette int permet de ne pas avoir le même index de sortie sur X tirages (ou X = maxStoredValues).
    [SerializeField] private int maxStoredValues;

    [Header("Chance")]
    public int ChanceCount;
    public int MaxChanceCount;

    public int nbOfProfilsMax;

    public int numberOfDislike = 0;

    [Header("Profil")]
    public GameObject monsterPresented;
    public GameObject profilPresented;

    // Start is called before the first frame update
    void Start()
    {
        MaxChanceCount = 100;
        ChanceCount = 0;
        choosenList = null;

        //update la liste de Monstre via l'encyclopédie.
        for (int i = 0; i < MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
        {
            commonMonsterList.Add(MenuManagerTuto.Instance.monsterEncyclopedie.allCommonMonster[i]);
        }
        for (int i = 0; i < MenuManagerTuto.Instance.monsterEncyclopedie.allRareMonster.Count; i++)
        {
            rareMonsterList.Add(MenuManagerTuto.Instance.monsterEncyclopedie.allRareMonster[i]);
        }

        commonMonsterList[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
        commonMonsterList[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;


        for (int i = 0; i < nbOfProfilsMax; i++)
        {
            FirstTirage();
        }
    }

    
    public void Tirage()
    {

        canMatch = false;
        //2-Test pour savoir si un monstre rare sort ou non;
        #region RaretyTest

        //Debug.Log(testRare);

        if (ChanceCount >= 100)
        {
            //C'est un rare.
            choosenList = rareMonsterList;
            //Reset de la chance pour ne pas re avoir de monstre Rare.
            ChanceCount = 0;

            MenuManagerTuto.Instance.canvasManager.matchCanvas.ResetBarMethod();
            MenuManagerTuto.Instance.canvasManager.matchCanvas.ThereIsARare = true;

            //Tirage aléatoire parmis la pool de monstre rare selon le niveau du joueur.
            int tirageIndex = Random.Range(0, numberRarePool[PlayerLevelTuto.playerLevelTuto]);
            //MonsterPresented = Un monstre tiré dans la liste.
            monsterPresented = choosenList[tirageIndex];
        }
        else
        {
            //La liste choisit est celles de monstres Communs.
            choosenList = commonMonsterList;
            //On lance une fonction pour verifier qu'un même index ne sort pas deux fois
            TirageIndexCommon();
        }
        #endregion

        //Instantier le gameObject avec le bon positionnement;
        GameObject profilSpawned = Instantiate(MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPrefab, MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPosition[0].transform.position, Quaternion.identity);
        profilSpawned.SetActive(false);
        profilSpawned.transform.SetParent(MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPosition[0].transform);
        //profilSpawned.transform.localScale = new Vector3(1f, 1f, 1f);
        profilSpawned.GetComponent<ProfilBehaviourTuto>().Initialisation(true);
        //Le monstre est ajouté à une liste pouvant être traquée.
        monsterSpawned.Insert(0, profilSpawned);

        monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviourTuto>().monsterPick;
        profilPresented = monsterSpawned[monsterSpawned.Count - 1];
        //Debug.Log(monsterPresented);

        MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateProfilPosition();


    }

    public void FirstTirage()
    {
        //La liste choisit est celles de monstres Communs.
        choosenList = commonMonsterList;
        //On lance une fonction pour verifier qu'un même index ne sort pas deux fois
        TirageIndexCommon();

        //Instantier le gameObject avec le bon positionnement;
        GameObject profilSpawned = Instantiate(MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPrefab, MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPosition[0].transform.position, Quaternion.identity);
        profilSpawned.transform.SetParent(MenuManagerTuto.Instance.canvasManager.matchCanvas.profilPosition[0].transform);
        //profilSpawned.transform.localScale = new Vector3(1f, 1f, 1f);

        profilSpawned.GetComponent<ProfilBehaviourTuto>().Initialisation(true);
        //Le monstre est ajouté à une liste pouvant être traquée.
        monsterSpawned.Insert(0, profilSpawned);
        //Debug.Log(monsterPresented);
        MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateFirstPosition();
        monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviourTuto>().monsterPick;
        profilPresented = monsterSpawned[monsterSpawned.Count - 1];
    }


    // Permet de répéter le test de la valeur sortie pour ne pas qu'elle soit similaire sur 3 tirages d'affiléS.
    public void TirageIndexCommon()
    {
        int tirageIndex = 0;
        if (monsterSpawned.Count == 0)
        {
            tirageIndex = 0;
        }
        else if (monsterSpawned.Count == 1)
        {
            tirageIndex = 2;
        }
        else if (monsterSpawned.Count == 2)
        {
            tirageIndex = 1;
        }

        monsterPresented = choosenList[tirageIndex];
    }

    //A activer quand le bouton match est préssé.
    public void Match(bool isSuperLike)
    {
        if (isSuperLike)
        {
            if (EnergyManagerTuto.superlikeCount > 0 && MenuManagerTuto.Instance.listManager.listCurrentSize < MenuManagerTuto.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && canMatch && !MenuManagerTuto.Instance.blockAction && monsterSpawned.Count != 0)
            {
                if (monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
                {
                    monsterPresented.GetComponent<MonsterToken>().isSuperlike = true;
                    matchList.Add(monsterPresented);


                    //Augmentation de la taille de la liste actuelle.
                    MenuManagerTuto.Instance.listManager.listCurrentSize++;
                    MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateList();

                    //Debug.Log("1");
                    //Instantie le profil matché dans la liste.
                    MenuManagerTuto.Instance.canvasManager.listCanvas.InstantiateProfil(true);
                    MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
                    MenuManagerTuto.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(true);
                }
                else
                {
                    //Si Indisponible => Ici
                    MenuManagerTuto.Instance.canvasManager.StartCoroutine(MenuManagerTuto.Instance.canvasManager.NoMatchFeedback());
                    MenuManagerTuto.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(false);
                }

                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    //Trigger Animation et Destroy sur le Prefab
                    profilPresented.GetComponent<ProfilBehaviourTuto>().MatchAnim(50, true);

                    //Reset le controle des boutons sur le profil suivant
                    monsterPresented = null;
                    profilPresented = null;
                }
                else
                {
                    //Trigger Animation et Destroy sur le Prefab
                    profilPresented.GetComponent<ProfilBehaviourTuto>().MatchAnim(50, true);
                    //Reset le controle des boutons sur le profil suivant
                    monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviourTuto>().monsterPick;
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                }

                MenuManagerTuto.Instance.canvasManager.matchCanvas.IncreaseRareBar();

                //Mettre superlikeSpend
                //MenuManager.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

                Tirage();
                MenuManagerTuto.Instance.canvasManager.matchCanvas.StartCoroutine(MenuManagerTuto.Instance.playerLevel.GiveExperience(2));
                numberOfDislike = 0;

                EnergyManagerTuto.superlikeCount--;

                MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateSuperLike();
            }
        }
        else
        {
            if (monsterSpawned.Count != 0 && EnergyManagerTuto.energy > 0 && MenuManagerTuto.Instance.listManager.listCurrentSize < MenuManagerTuto.Instance.listManager.listMaxSize[PlayerLevelTuto.playerLevelTuto - 1] && canMatch && !MenuManagerTuto.Instance.blockAction)
            {
                //Debug.Log("In");
                //Checker si (energie > 0 && liste pas complète).
                if (MenuManagerTuto.Instance.canvasManager.matchCanvas.ThereIsARare && monsterPresented.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    MenuManagerTuto.Instance.canvasManager.matchCanvas.ThereIsARare = false;
                }

                if (monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
                {
                    matchList.Add(monsterPresented);

                    //Augmentation de la taille de la liste actuelle.
                    MenuManagerTuto.Instance.listManager.listCurrentSize++;
                    MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateList();

                    //Debug.Log("1");
                    //Instantie le profil matché dans la liste.
                    MenuManagerTuto.Instance.canvasManager.listCanvas.InstantiateProfil(false);
                    MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
                    MenuManagerTuto.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(true);
                }
                else
                {
                    //Si Indisponible => Ici
                    MenuManagerTuto.Instance.canvasManager.StartCoroutine(MenuManagerTuto.Instance.canvasManager.NoMatchFeedback());
                    MenuManagerTuto.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(false);
                }

                //Afficher FeedBackErreur.

                //Baisse de l'energy
                EnergyManagerTuto.energy--;
                MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateEnergy();

                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    //Trigger Animation et Destroy sur le Prefab
                    profilPresented.GetComponent<ProfilBehaviourTuto>().MatchAnim(50, false);

                    //Reset le controle des boutons sur le profil suivant
                    monsterPresented = null;
                    profilPresented = null;
                }
                else
                {
                    //Trigger Animation et Destroy sur le Prefab
                    profilPresented.GetComponent<ProfilBehaviourTuto>().MatchAnim(50, false);
                    //Reset le controle des boutons sur le profil suivant
                    monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviourTuto>().monsterPick;
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                }

                MenuManagerTuto.Instance.canvasManager.matchCanvas.IncreaseRareBar();

                MenuManagerTuto.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

                Tirage();
                MenuManagerTuto.Instance.canvasManager.matchCanvas.StartCoroutine(MenuManagerTuto.Instance.playerLevel.GiveExperience(2));
                numberOfDislike = 0;

            }
        }
    }

    //A activer quand le bouton dislike est préssé.
    public void Dislike()
    {
        if (monsterSpawned.Count != 0 && EnergyManagerTuto.energy > 0 && MenuManagerTuto.Instance.listManager.listCurrentSize < MenuManagerTuto.Instance.listManager.listMaxSize[PlayerLevelTuto.playerLevelTuto - 1] && canMatch && !MenuManagerTuto.Instance.blockAction)
        {
            if (MenuManagerTuto.Instance.canvasManager.matchCanvas.ThereIsARare && monsterPresented.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
            {
                MenuManagerTuto.Instance.canvasManager.matchCanvas.ThereIsARare = false;
            }

            monsterSpawned.Remove(profilPresented);

            //pour eviter la null reference d'index quand il n'y a plus de profils.
            if (monsterSpawned.Count == 0)
            {
                profilPresented.GetComponent<ProfilBehaviourTuto>().DisLikeAnim(50);
                profilPresented = null;
                monsterPresented = null;
            }
            else
            {
                profilPresented.GetComponent<ProfilBehaviourTuto>().DisLikeAnim(50);
                monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviourTuto>().monsterPick;
                profilPresented = monsterSpawned[monsterSpawned.Count - 1];

            }
            EnergyManagerTuto.energy--;
            MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateEnergy();
            MenuManagerTuto.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

            Tirage();


            if (numberOfDislike < 5)
            {
                numberOfDislike++;
            }
            else
            {
                numberOfDislike = 0;
                MenuManagerTuto.Instance.canvasManager.matchCanvas.AlerteDislike();
            }
        }
        else
        {
            return;
        }

    }
    

}
