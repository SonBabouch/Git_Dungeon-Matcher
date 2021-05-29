using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEncyclopedieTuto : MonoBehaviour
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

    public void GetNewMonsters()
    {
        for (int i = 0; i < allCommonMonster.Count; i++)
        {
            if (allCommonMonster[i].GetComponent<MonsterToken>().isGet)
            {
                if (allCommonMonster[i].GetComponent<MonsterToken>().statement != MonsterToken.statementEnum.Equipe)
                {
                    allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
                }
            }
        }


        for (int i = 0; i < allRareMonster.Count; i++)
        {
            if (allRareMonster[i].GetComponent<MonsterToken>().isGet)
            {
                allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }

        //Update le visuel du sac;
        for (int i = 0; i < MenuManagerTuto.Instance.canvasManager.bagCanvas.bagButtonList.Count; i++)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }
    }

    public void UpdateMonsterEncyclopedie()
    {
        Debug.Log(PlayerLevelTuto.playerLevelTuto);

        for (int i = 0; i < MenuManagerTuto.Instance.matchManager.numberCommonPool[PlayerLevelTuto.playerLevelTuto]; i++)
        {
            allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
        }

        for (int i = MenuManagerTuto.Instance.matchManager.numberCommonPool[PlayerLevelTuto.playerLevelTuto - 1]; i < MenuManagerTuto.Instance.matchManager.numberCommonPool[PlayerLevelTuto.playerLevelTuto]; i++)
        {
            allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible;
        }

        for (int i = 0; i < MenuManagerTuto.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel - 1]; i++)
        {
            allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
        }

        for (int i = MenuManagerTuto.Instance.matchManager.numberRarePool[PlayerLevelTuto.playerLevelTuto - 1]; i < MenuManagerTuto.Instance.matchManager.numberRarePool[PlayerLevelTuto.playerLevelTuto-1]; i++)
        {
            allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible;
        }

        //redonner les monstres claim;
        for (int i = 0; i < MenuManagerTuto.Instance.matchManager.commonMonsterList.Count; i++)
        {
            if (MenuManagerTuto.Instance.matchManager.commonMonsterList[i].GetComponent<MonsterToken>().isGet)
            {
                MenuManagerTuto.Instance.matchManager.commonMonsterList[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }

        for (int i = 0; i < MenuManagerTuto.Instance.matchManager.rareMonsterList.Count; i++)
        {
            if (MenuManagerTuto.Instance.matchManager.rareMonsterList[i].GetComponent<MonsterToken>().isGet)
            {
                MenuManagerTuto.Instance.matchManager.rareMonsterList[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }

        //Update le visuel du sac;
        for (int i = 0; i < MenuManagerTuto.Instance.canvasManager.bagCanvas.bagButtonList.Count; i++)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }
    }
}
