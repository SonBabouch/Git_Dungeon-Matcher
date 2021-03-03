using UnityEngine;
using Management;

/// <summary>
/// Ce script permet de gérer le visuel de l'inventaire.
/// </summary>
public class BagCanvasManager : MonoBehaviour
{
    public GameObject currentSelected;
    public GameObject detailsBackground;

    private void Start()
    {
        currentSelected = null;
    }

    public void closeDetails()
    {
        detailsBackground.SetActive(false);
        MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = false;
    }
}
