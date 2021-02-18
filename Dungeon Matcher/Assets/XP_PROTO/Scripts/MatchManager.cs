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
        
        [Header("Sécurité")]
        //Cette Liste permet de stocker X valeurs pour ne pas qu'il retombe.
        public List<int> storedIndex = new List<int>();
        //Cette int permet de ne pas avoir le même index de sortie sur X tirages (ou X = maxStoredValues).
        [SerializeField] private int maxStoredValues;

        [Header("Chance")]
        [SerializeField] private int rareChance;

        [Header("Profil")]
        public GameObject monsterPresented;
        public GameObject profilPresented;

        // Start is called before the first frame update
        void Start()
        {
            choosenList = null;
            Tirage();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        #region Methods
        public void Tirage()
        {
            //Test pour savoir si un monstre rare sort ou non;
            bool isRare = false;
            int testRare = Random.Range(0, 100);
            
            if(testRare >= rareChance)
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
            }
            else
            {
                choosenList = commonMonsterList;
            }

            int tirageIndex = Random.Range(0,choosenList.Count-1);
            monsterPresented = choosenList[tirageIndex];

            GameObject profilSpawned = Instantiate(GameManager.Instance.canvasManager.profilPrefab, transform.position, Quaternion.identity);
            profilSpawned.transform.SetParent(GameManager.Instance.canvas.transform);
            profilSpawned.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            isRare = false;
            choosenList = null;
            profilSpawned.GetComponent<ProfilBehaviour>().Initialisation();
            profilPresented = profilSpawned;
        }
        
        public void Match()
        {
            if (monsterPresented !=null)
            {
                //Checker si (energie > 0 && liste pas complète).
                //Dépenser energie
                profilPresented.GetComponent<Animator>().SetTrigger("Like");
                Tirage();
                
            }
           
        }

        public void Dislike()
        {
            if (monsterPresented != null)
            {
                profilPresented.GetComponent<Animator>().SetTrigger("Dislike");
                Tirage();
                //energy --;
            }


        }
        #endregion
    }
}

