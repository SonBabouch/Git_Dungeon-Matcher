using UnityEngine;
using System.Collections.Generic;
using Management;
using UnityEngine.UI;

/// <summary>
/// Ce script permet de gérer le visuel de l'inventaire.
/// </summary>
public class BagCanvasManager : MonoBehaviour
{
    public GameObject currentButtonSelected;
    public GameObject currentMonsterSelected;

    public GameObject detailsBackground;

    [SerializeField] private GameObject bagButtonParent;
    public List<GameObject> bagButtonList = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in bagButtonParent.transform)
        {
            bagButtonList.Add(child.gameObject);
        }
    }

    private void Start()
    {
        currentButtonSelected = null;
        currentMonsterSelected = null;

        for (int i = 0; i < MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster[i].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<Monster.MonsterToken>().profilPicture;


        }

        for (int i = MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster.Count; i < MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allRareMonster.Count + MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allRareMonster[i- MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster.Count].gameObject;
            bagButtonList[i].GetComponent<Image>().sprite = bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<Monster.MonsterToken>().profilPicture;
        }
    }

    public void closeDetails()
    {
        detailsBackground.SetActive(false);
        MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = false;
    }
}
