using System.Collections.Generic;
using UnityEngine;

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


        public List<GameObject> monsterSpawned = new List<GameObject>();

        [Header("Sécurité")]
        //Cette Liste permet de stocker X valeurs pour ne pas qu'il retombe.
        public List<int> storedIndex = new List<int>();
        //Cette int permet de ne pas avoir le même index de sortie sur X tirages (ou X = maxStoredValues).
        [SerializeField] private int maxStoredValues;

        [Header("Chance")]
        [SerializeField] private int rareChance;

        public int nbOfProfilsMax;

        [Header("Profil")]
        public GameObject monsterPresented;
        public GameObject profilPresented;

        // Start is called before the first frame update
        void Start()
        {
            rareChance = 5;
            choosenList = null;

            foreach (Transform child in  MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().commonMonsterParents.transform)
            {
                commonMonsterList.Add(child.gameObject);
            }

            foreach (Transform child in MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().rareMonsterParents.transform)
            {
                rareMonsterList.Add(child.gameObject);
            }

            Tirage();
        }
        
        #region Methods
        public void Tirage()
        {
            //1-On fait boucler X fois selon la variable nbOfProfilsMax;
            for (int i = 0; i < nbOfProfilsMax; i++)
            {
                //2-Test pour savoir si un monstre rare sort ou non;
                #region RaretyTest
                bool isRare = false;
                int testRare = Random.Range(0, 100);
                //Debug.Log(testRare);

                if (testRare >= rareChance)
                {
                    isRare = false;
                }
                else
                {
                    isRare = true;
                }

                if (isRare)
                {
                    //La liste choisit est celle des monstres Rares.
                    choosenList = rareMonsterList;
                    //Reset de la chance.
                    rareChance = 5;
                    //Tirage aléatoire parmis la pool de monstre rare selon le niveau du joueur.
                    int tirageIndex = Random.Range(0, numberRarePool[PlayerLevel.playerLevel - 1]);
                    monsterPresented = choosenList[tirageIndex];
                }
                else
                {
                    //C'est pas un rare donc on augmente la chance d'avoir un rare.
                    rareChance++;
                    //La liste choisit est celles de monstres Communs.
                    choosenList = commonMonsterList;

                    TirageIndexCommon();
                }
                #endregion

                //Instantier le gameObject avec le bon positionnement;
                Debug.Log("Initialisation");
                GameObject profilSpawned = Instantiate(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().profilPrefab, transform.position, Quaternion.identity);
                profilSpawned.transform.SetParent(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().spawnPosition.transform);
                
                //Besoin de Set le Scale de l'objet pour le faire feet au reste de l'écran.
                //profilSpawned.GetComponent<RectTransform>().transform.position = new Vector3(0f, 0f, 0f);

                //Afficher les valeurs.
                profilPresented = profilSpawned;
                profilSpawned.GetComponent<ProfilBehaviour>().Initialisation();
                
                //Le monstre est ajouté à une liste pouvant être traquée.
                monsterSpawned.Add(profilPresented);
                //Debug.Log(monsterPresented);
            }
           
        }
        // Permet de répéter le test de la valeur sortie pour ne pas qu'elle soit similaire sur 3 tirages d'affiléS.
        public void TirageIndexCommon()
        {
            //Tirage aléatoire parmis la pool de monstre selon le niveau du joueur.
            int tirageIndex = Random.Range(0, numberCommonPool[PlayerLevel.playerLevel - 1]);
            //Répéter la manip si le chiffre à déja été selectionné dans les X dernières valeurs.

            bool testGood = true;
            //Si y'a rien, ca rentre pas dedans.
            for (int i = 0; i < storedIndex.Count; i++)
            {
                if (storedIndex[i] == tirageIndex)
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
                if (storedIndex.Count < 3)
                {
                    storedIndex.Add(tirageIndex);
                }
                else
                {
                    storedIndex[2] = storedIndex[1];
                    storedIndex[1] = storedIndex[0];
                    storedIndex[0] = tirageIndex;
                }
                monsterPresented = choosenList[tirageIndex];
            }


        }

        //A activer quand le bouton match est préssé.
        public void Match()
        {
            if (monsterSpawned.Count != 0 && EnergyManager.energy >0 && MenuManager.Instance.listManager.listCurrentSize < MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel-1])
            {
                //Debug.Log("In");
                //Checker si (energie > 0 && liste pas complète).
                

                matchList.Add(monsterPresented);

                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    Destroy(profilPresented);
                    profilPresented = null;
                    monsterPresented = null;
                }
                else
                {
                    //Reset le controle des boutons sur le profil suivant
                    Destroy(profilPresented);
                    profilPresented = monsterSpawned[monsterSpawned.Count-1];
                    monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                }

                //Baisse de l'energy
                EnergyManager.energy--;
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();
                
                //Augmentation de la taille de la liste actuelle.
                MenuManager.Instance.listManager.listCurrentSize++;

                //Instantie le profil matché dans la liste
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().listCanvas.GetComponent<ListCanvasManager>().InstantiateProfil();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().listCanvas.GetComponent<ListCanvasManager>().UpdateList();


                
            }
            else
            {
                return;
            }

        }

        //A activer quand le bouton dislike est préssé.
        public void Dislike()
        {
            if (monsterSpawned.Count !=0 && EnergyManager.energy > 0 && MenuManager.Instance.listManager.listCurrentSize < MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1])
            {

                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    Destroy(profilPresented);
                    profilPresented = null;
                    monsterPresented = null;
                }
                else
                {
                    Destroy(profilPresented);
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                    monsterPresented = monsterSpawned[monsterSpawned.Count - 1].GetComponent<ProfilBehaviour>().monsterPick;
                }
                
                rareChance++;
                EnergyManager.energy--;
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();
                
            }
            else
            {
                return;
            }


        }

       
        #endregion
    }
}

