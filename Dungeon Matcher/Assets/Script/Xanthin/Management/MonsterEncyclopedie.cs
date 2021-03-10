using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;


    public class MonsterEncyclopedie : MonoBehaviour
    {
        

        public GameObject commonMonsterParents;
        public GameObject rareMonsterParents;

        public List<GameObject> allCommonMonster = new List<GameObject>();
        public List<GameObject> allRareMonster = new List<GameObject>();

        // Start is called before the first frame update
        void Awake()
        {

            //Initialisation des valeurs pour chaque Monstres.
            foreach (Transform child in commonMonsterParents.transform)
            {
                allCommonMonster.Add(child.gameObject);
                child.gameObject.GetComponent<MonsterToken>().Initialize();
            }

            foreach (Transform child in rareMonsterParents.transform)
            {
                allRareMonster.Add(child.gameObject);
                child.gameObject.GetComponent<MonsterToken>().Initialize();
            }

            for (int i = 0; i < allCommonMonster.Count; i++)
            {
                allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible; 
            }
            for (int i = 0; i < allRareMonster.Count; i++)
            {
                allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible;
            }

            
        }
       
        public void UpdateMonsterEncyclopedie()
        {
            for (int i = 0; i < MenuManager.Instance.matchManager.numberCommonPool[PlayerLevel.playerLevel - 1]; i++)
            {
                allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
            }

            for (int i = 0; i < MenuManager.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel - 1]; i++)
            {
                allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
            }

        }
       
    }

    



