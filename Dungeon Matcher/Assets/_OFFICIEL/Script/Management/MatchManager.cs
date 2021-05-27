using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Management
{
    /// <summary>
    /// XP_ Ce script va gérer la phase de match
    /// </summary>

    public class MatchManager : MonoBehaviour
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
        public List<int> storedIndex = new List<int>();
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
            for (int i = 0; i < MenuManager.Instance.monsterEncyclopedie.allCommonMonster.Count; i++)
            {
                commonMonsterList.Add(MenuManager.Instance.monsterEncyclopedie.allCommonMonster[i]);
            }
            for (int i = 0; i < MenuManager.Instance.monsterEncyclopedie.allRareMonster.Count; i++)
            {
                rareMonsterList.Add(MenuManager.Instance.monsterEncyclopedie.allRareMonster[i]);
            }

            commonMonsterList[0].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            commonMonsterList[1].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;


            for (int i = 0; i < nbOfProfilsMax; i++)
            {
                FirstTirage();
            }
        }
        
        #region Methods
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

                        MenuManager.Instance.canvasManager.matchCanvas.ResetBarMethod();
                        MenuManager.Instance.canvasManager.matchCanvas.ThereIsARare = true;
                       
                        //Tirage aléatoire parmis la pool de monstre rare selon le niveau du joueur.
                        int tirageIndex = Random.Range(0, numberRarePool[PlayerLevel.playerLevel]);
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
                GameObject profilSpawned = Instantiate(MenuManager.Instance.canvasManager.matchCanvas.profilPrefab, MenuManager.Instance.canvasManager.matchCanvas.profilPosition[0].transform.position, Quaternion.identity);
                profilSpawned.SetActive(false);
                profilSpawned.transform.SetParent(MenuManager.Instance.canvasManager.matchCanvas.profilPosition[0].transform);
                //profilSpawned.transform.localScale = new Vector3(1f, 1f, 1f);
                profilSpawned.GetComponent<ProfilBehaviour>().Initialisation(true);
                //Le monstre est ajouté à une liste pouvant être traquée.
                monsterSpawned.Insert(0,profilSpawned);
                
                monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                //Debug.Log(monsterPresented);

                MenuManager.Instance.canvasManager.matchCanvas.UpdateProfilPosition();
                

        }

        public void FirstTirage()
        {
            //2-Test pour savoir si un monstre rare sort ou non;
            #region RaretyTest
           

            if (MaxChanceCount == ChanceCount)
            {
                //C'est un rare.
                choosenList = rareMonsterList;
                //Reset de la chance pour ne pas re avoir de monstre Rare.
                ChanceCount = 0;
                //Tirage aléatoire parmis la pool de monstre rare selon le niveau du joueur.
                int tirageIndex = Random.Range(0, numberRarePool[PlayerLevel.playerLevel]);
                //MonsterPresented = Un monstre tiré dans la liste.
                monsterPresented = choosenList[tirageIndex];
            }
            else
            {
                //C'est pas un rare donc on augmente la chance d'avoir un rare.
                
                //La liste choisit est celles de monstres Communs.
                choosenList = commonMonsterList;
                //On lance une fonction pour verifier qu'un même index ne sort pas deux fois
                TirageIndexCommon();
            }
            #endregion

            //Instantier le gameObject avec le bon positionnement;
            GameObject profilSpawned = Instantiate(MenuManager.Instance.canvasManager.matchCanvas.profilPrefab, MenuManager.Instance.canvasManager.matchCanvas.profilPosition[0].transform.position, Quaternion.identity);
            profilSpawned.transform.SetParent(MenuManager.Instance.canvasManager.matchCanvas.profilPosition[0].transform);
            //profilSpawned.transform.localScale = new Vector3(1f, 1f, 1f);

            profilSpawned.GetComponent<ProfilBehaviour>().Initialisation(true);
            //Le monstre est ajouté à une liste pouvant être traquée.
            monsterSpawned.Insert(0, profilSpawned);
            //Debug.Log(monsterPresented);
            MenuManager.Instance.canvasManager.matchCanvas.UpdateFirstPosition();
            monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
            profilPresented = monsterSpawned[monsterSpawned.Count-1];
        }
        
        
        // Permet de répéter le test de la valeur sortie pour ne pas qu'elle soit similaire sur 3 tirages d'affiléS.
        public void TirageIndexCommon()
        {
            //Tirage aléatoire parmis la pool de monstre selon le niveau du joueur.
            int tirageIndex = Random.Range(0, numberCommonPool[PlayerLevel.playerLevel]);
            //Répéter la manip si le chiffre à déja été selectionné dans les X dernières valeurs.
            bool testGood = true;
            //Si y'a rien, ca rentre pas dedans.
            for (int i = 0; i < storedIndex.Count; i++)
            {
                if (tirageIndex == storedIndex[i])
                {
                    testGood = false;
                }
            }

            if (testGood == false)
            {
                TirageIndexCommon();
            }
            else
            {
                //Debug.Log(tirageIndex);
                monsterPresented = choosenList[tirageIndex];
                storedIndex.Add(tirageIndex);
                
                if(storedIndex.Count > maxStoredValues)
                {
                    storedIndex.Remove(storedIndex[0]);
                }
            }
        }

        //A activer quand le bouton match est préssé.
        public void Match(bool isSuperLike)
        {
            if (isSuperLike)
            {   
                if(EnergyManager.superlikeCount > 0 && MenuManager.Instance.listManager.listCurrentSize < MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && canMatch && !MenuManager.Instance.blockAction && monsterSpawned.Count != 0)
                {
                    if (monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
                    {
                        monsterPresented.GetComponent<MonsterToken>().isSuperlike = true;
                        matchList.Add(monsterPresented);
                        

                        //Augmentation de la taille de la liste actuelle.
                        MenuManager.Instance.listManager.listCurrentSize++;
                        MenuManager.Instance.canvasManager.listCanvas.UpdateList();

                        //Debug.Log("1");
                        //Instantie le profil matché dans la liste.
                        MenuManager.Instance.canvasManager.listCanvas.InstantiateProfil(true);
                        MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();
                        MenuManager.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(true);
                    }
                    else
                    {
                        //Si Indisponible => Ici
                        MenuManager.Instance.canvasManager.StartCoroutine(MenuManager.Instance.canvasManager.NoMatchFeedback());
                        MenuManager.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(false);
                    }

                    monsterSpawned.Remove(profilPresented);

                    //pour eviter la null reference d'index quand il n'y a plus de profils.
                    if (monsterSpawned.Count == 0)
                    {
                        //Trigger Animation et Destroy sur le Prefab
                        profilPresented.GetComponent<ProfilBehaviour>().MatchAnim(50,true);

                        //Reset le controle des boutons sur le profil suivant
                        monsterPresented = null;
                        profilPresented = null;
                    }
                    else
                    {
                        //Trigger Animation et Destroy sur le Prefab
                        profilPresented.GetComponent<ProfilBehaviour>().MatchAnim(50,true);
                        //Reset le controle des boutons sur le profil suivant
                        monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                        profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                    }

                    MenuManager.Instance.canvasManager.matchCanvas.IncreaseRareBar();

                    //Mettre superlikeSpend
                    //MenuManager.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

                    Tirage();
                    MenuManager.Instance.canvasManager.matchCanvas.StartCoroutine(MenuManager.Instance.playerLevel.GiveExperience(2));
                    numberOfDislike = 0;

                    EnergyManager.superlikeCount--;
                 
                    MenuManager.Instance.canvasManager.matchCanvas.UpdateSuperLike();
                }
            }
            else
            {
                if (monsterSpawned.Count != 0 && EnergyManager.energy > 0 && MenuManager.Instance.listManager.listCurrentSize < MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && canMatch && !MenuManager.Instance.blockAction)
                {
                    //Debug.Log("In");
                    //Checker si (energie > 0 && liste pas complète).
                    if (MenuManager.Instance.canvasManager.matchCanvas.ThereIsARare && monsterPresented.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                    {
                        MenuManager.Instance.canvasManager.matchCanvas.ThereIsARare = false;
                    }

                    if (monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPresented.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
                    {
                        matchList.Add(monsterPresented);

                        //Augmentation de la taille de la liste actuelle.
                        MenuManager.Instance.listManager.listCurrentSize++;
                        MenuManager.Instance.canvasManager.listCanvas.UpdateList();

                        //Debug.Log("1");
                        //Instantie le profil matché dans la liste.
                        MenuManager.Instance.canvasManager.listCanvas.InstantiateProfil(false);
                        MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();
                        MenuManager.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(true);
                    }
                    else
                    {
                        //Si Indisponible => Ici
                        MenuManager.Instance.canvasManager.StartCoroutine(MenuManager.Instance.canvasManager.NoMatchFeedback());
                        MenuManager.Instance.canvasManager.matchCanvas.matchFeedback.SpawnLikeFeedBack(false);
                    }

                    //Afficher FeedBackErreur.

                    //Baisse de l'energy
                    EnergyManager.energy--;
                    MenuManager.Instance.canvasManager.matchCanvas.UpdateEnergy();

                    monsterSpawned.Remove(profilPresented);

                    //pour eviter la null reference d'index quand il n'y a plus de profils.
                    if (monsterSpawned.Count == 0)
                    {
                        //Trigger Animation et Destroy sur le Prefab
                        profilPresented.GetComponent<ProfilBehaviour>().MatchAnim(50,false);

                        //Reset le controle des boutons sur le profil suivant
                        monsterPresented = null;
                        profilPresented = null;
                    }
                    else
                    {
                        //Trigger Animation et Destroy sur le Prefab
                        profilPresented.GetComponent<ProfilBehaviour>().MatchAnim(50,false);
                        //Reset le controle des boutons sur le profil suivant
                        monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                        profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                    }

                    MenuManager.Instance.canvasManager.matchCanvas.IncreaseRareBar();

                    MenuManager.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

                    Tirage();
                    MenuManager.Instance.canvasManager.matchCanvas.StartCoroutine(MenuManager.Instance.playerLevel.GiveExperience(2));
                    numberOfDislike = 0;

                }
            }
        }
        
        //A activer quand le bouton dislike est préssé.
        public void Dislike()
        {
            if (monsterSpawned.Count != 0 && EnergyManager.energy > 0 && MenuManager.Instance.listManager.listCurrentSize < MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && canMatch && !MenuManager.Instance.blockAction)
            {
                if (MenuManager.Instance.canvasManager.matchCanvas.ThereIsARare && monsterPresented.GetComponent<MonsterToken>().rarety == MonsterToken.raretyEnum.Rare)
                {
                    MenuManager.Instance.canvasManager.matchCanvas.ThereIsARare = false;
                }

                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    profilPresented.GetComponent<ProfilBehaviour>().DisLikeAnim(50);
                    profilPresented = null;
                    monsterPresented = null;
                }
                else
                {
                    profilPresented.GetComponent<ProfilBehaviour>().DisLikeAnim(50);
                    monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                
                }
                EnergyManager.energy--;
                MenuManager.Instance.canvasManager.matchCanvas.UpdateEnergy();
                MenuManager.Instance.canvasManager.matchCanvas.energySpend.GetComponent<Animator>().SetTrigger("Swip");

                Tirage();


                if (numberOfDislike < 5)
                {
                    numberOfDislike++;
                }
                else
                { 
                    numberOfDislike = 0;
                    MenuManager.Instance.canvasManager.matchCanvas.AlerteDislike();
                }
            }
            else
            {
                return;
            }

        }
        #endregion
    }
}

