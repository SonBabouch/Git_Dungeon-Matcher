using System.Collections;
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
        public List<GameObject> rareMonsterList = new List<GameObject>();
        private List<GameObject> choosenList = new List<GameObject>();

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
            choosenList = null;
            Tirage();
        }
        
        #region Methods
        public void Tirage()
        {
            //On fait boucler;
            for (int i = 0; i < nbOfProfilsMax; i++)
            {
                //Test pour savoir si un monstre rare sort ou non;
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
                    choosenList = rareMonsterList;
                    rareChance = 5;
                }
                else
                {
                    choosenList = commonMonsterList;
                }
                #endregion

                //Tirage au sort.
                int tirageIndex = Random.Range(0, choosenList.Count - 1);
                monsterPresented = choosenList[tirageIndex];

                //Stocker la Valeur dans le tableau.
                if (isRare == false)
                {
                    storedIndex.Add(tirageIndex);
                    isRare = false;
                }

                //Instantier le gameObject avec le bon positionnement;
                GameObject profilSpawned = Instantiate(GameManager.Instance.canvasManager.profilPrefab, transform.position, Quaternion.identity);
                profilSpawned.transform.SetParent(GameManager.Instance.canvasManager.GetComponent<MatchCanvasManager>().spawnPosition.transform);
                profilSpawned.GetComponent<RectTransform>().localScale = new Vector3(8f, 8f, 8f);
                
                //Reset la liste de pick.
                choosenList = null;

                //Afficher les valeurs.
                profilSpawned.GetComponent<ProfilBehaviour>().Initialisation();
                profilPresented = profilSpawned;
                monsterSpawned.Add(profilPresented);
            }
           
        }
        
        public void Match()
        {
            if (monsterSpawned.Count != 0 && EnergyManager.energy >0)
            {
                //Checker si (energie > 0 && liste pas complète).
                profilPresented.GetComponent<Animator>().SetTrigger("Like");
                
                monsterSpawned.Remove(profilPresented);
                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    profilPresented = null;
                }
                else
                {
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                }

                rareChance++;
                EnergyManager.energy--;
                GameManager.Instance.matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();

            }
            else
            {
                return;
            }

        }

        public void Dislike()
        {
            if (monsterSpawned.Count !=0 && EnergyManager.energy > 0)
            {
                profilPresented.GetComponent<Animator>().SetTrigger("Dislike");
                monsterSpawned.Remove(profilPresented);

                //pour eviter la null reference d'index quand il n'y a plus de profils.
                if (monsterSpawned.Count == 0)
                {
                    profilPresented = null;
                }
                else
                {
                    profilPresented = monsterSpawned[monsterSpawned.Count - 1];
                }
                
                rareChance++;
                EnergyManager.energy--;
                GameManager.Instance.matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();
            }
            else
            {
                return;
            }


        }
        #endregion
    }
}

