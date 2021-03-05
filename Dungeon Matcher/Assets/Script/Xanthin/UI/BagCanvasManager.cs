using UnityEngine;
using System.Collections.Generic;
using Management;

/// <summary>
/// Ce script permet de gérer le visuel de l'inventaire.
/// </summary>
public class BagCanvasManager : MonoBehaviour
{
    public GameObject currentSelected;
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
        currentSelected = null;

        for (int i = 0; i < bagButtonList.Count; i++)
        {
            bagButtonList[i].GetComponent<BagButtonBehaviour>().monsterContainer = MenuManager.Instance.monsterEncyclopedie.GetComponent<Monster.MonsterEncyclopedie>().allCommonMonster[i].gameObject;
        }
    }

    public void closeDetails()
    {
        detailsBackground.SetActive(false);
        MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = false;
    }
}
