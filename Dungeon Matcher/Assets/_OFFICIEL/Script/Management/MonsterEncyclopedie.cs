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

        for (int i = MenuManager.Instance.matchManager.numberCommonPool[PlayerLevel.playerLevel - 1]; i < MenuManager.Instance.matchManager.numberCommonPool[PlayerLevel.playerLevel]; i++)
        {
            allCommonMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible;
        }

        for (int i = 0; i < MenuManager.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel - 1]; i++)
        {
            allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
        }

        for (int i = MenuManager.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel - 1]; i < MenuManager.Instance.matchManager.numberRarePool[PlayerLevel.playerLevel]; i++)
        {
            allRareMonster[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Indisponible;
        }

        //redonner les monstres claim;
        for (int i = 0; i < MenuManager.Instance.matchManager.commonMonsterList.Count; i++)
        {
            if (MenuManager.Instance.matchManager.commonMonsterList[i].GetComponent<MonsterToken>().isGet)
            {
                MenuManager.Instance.matchManager.commonMonsterList[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }

        for (int i = 0; i < MenuManager.Instance.matchManager.rareMonsterList.Count; i++)
        {
            if (MenuManager.Instance.matchManager.rareMonsterList[i].GetComponent<MonsterToken>().isGet)
            {
                MenuManager.Instance.matchManager.rareMonsterList[i].GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }

        //Update le visuel du sac;
        for (int i = 0; i < MenuManager.Instance.canvasManager.bagCanvas.bagButtonList.Count; i++)
        {
            MenuManager.Instance.canvasManager.bagCanvas.bagButtonList[i].GetComponent<BagButtonBehaviour>().UpdateColor();
        }
    }

}





