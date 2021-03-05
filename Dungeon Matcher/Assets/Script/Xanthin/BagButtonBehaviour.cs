using UnityEngine;
using Management;
using UnityEngine.UI;

/// <summary>
/// This script makes the behaviour of the BagButton.
/// It makes the Fonction : Details / Use / Upgrade Working.
/// </summary>
public class BagButtonBehaviour : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject detailsButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject upgradeButton;

    public GameObject monsterContainer;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        detailsButton.SetActive(false);
        equipButton.SetActive(false);
        upgradeButton.SetActive(false);
    }


    public void Details()
    {
        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().detailsBackground.SetActive(true);
        MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = true;
        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().detailsCanvasManager.GetComponent<DetailsCanvasManager>().UpdateDetailsMenu();
        UnSelected();
        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = null;
        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = null;
    }

    public void Equip()
    {

    }

    public void Upgrade()
    {

    }

    public void Selected()
    {
        if(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != gameObject && MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != null)
        {
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
            animator.SetTrigger("Selected");
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
        }
        else
        {
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
            animator.SetTrigger("Selected");
        }
    }

    public void UnSelected()
    {
        animator.SetTrigger("Unselected");
    }
}
