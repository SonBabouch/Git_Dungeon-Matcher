using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Monster
{
    public class MonsterEncyclopedie : MonoBehaviour
    {
        

        public GameObject commonMonsterParents;
        public GameObject rareMonsterParents;

        public List<MonsterToken> allCommonMonster = new List<MonsterToken>();
        public List<MonsterToken> allRareMonster = new List<MonsterToken>();

        // Start is called before the first frame update
        void Awake()
        {
            foreach (Transform child in commonMonsterParents.transform)
            {
                allCommonMonster.Add(child.gameObject.GetComponent<MonsterToken>());
                child.gameObject.GetComponent<MonsterToken>().Initialize();
            }

            foreach (Transform child in rareMonsterParents.transform)
            {
                allRareMonster.Add(child.gameObject.GetComponent<MonsterToken>());
                child.gameObject.GetComponent<MonsterToken>().Initialize();
            }



            for (int i = 0; i < allCommonMonster.Count; i++)
            {
                allCommonMonster[i].statement = MonsterToken.statementEnum.Indisponible; 
            }
            for (int i = 0; i < allRareMonster.Count; i++)
            {
                allRareMonster[i].statement = MonsterToken.statementEnum.Indisponible;
            }

            
        }
       
        public void UpdateMonsterEncyclopedie()
        {
            for (int i = 0; i < MenuManager.Instance.matchManager.numberCommonPool[PlayerLevel.playerLevel - 1]; i++)
            {
                allCommonMonster[i].statement = MonsterToken.statementEnum.Disponible;
            }

            for (int i = 0; i < MenuManager.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel - 1]; i++)
            {
                allRareMonster[i].statement = MonsterToken.statementEnum.Disponible;
            }

        }
       
    }
}
    



