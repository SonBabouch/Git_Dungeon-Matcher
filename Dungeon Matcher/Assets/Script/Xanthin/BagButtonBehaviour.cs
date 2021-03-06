using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        if(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected.GetComponent<Monster.MonsterToken>().statement == Monster.MonsterToken.statementEnum.Disponible || MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected.GetComponent<Monster.MonsterToken>().statement == Monster.MonsterToken.statementEnum.Equipe)
        {
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().detailsBackground.SetActive(true);
            MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow = true;
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().detailsCanvasManager.GetComponent<DetailsCanvasManager>().UpdateDetailsMenu();
            UnSelected();
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = null;
            MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = null;
        }    

       
    }

    public void Equip()
    {
        if(monsterContainer.GetComponent<Monster.MonsterToken>().statement == Monster.MonsterToken.statementEnum.Indisponible || monsterContainer.GetComponent<Monster.MonsterToken>().statement == Monster.MonsterToken.statementEnum.Equipe)
        {
            return;
        }

        //Ajouter à l'équipe si Count pas = à 2
        if(MenuManager.Instance.bagManager.monsterTeam.Count < 2)
        {
            MenuManager.Instance.bagManager.monsterTeam.Insert(0, monsterContainer);
            monsterContainer.GetComponent<Monster.MonsterToken>().statement = Monster.MonsterToken.statementEnum.Equipe;
        }
        else
        {
            GameObject monsterToRemove = MenuManager.Instance.bagManager.monsterTeam[1];
            monsterToRemove.GetComponent<Monster.MonsterToken>().statement = Monster.MonsterToken.statementEnum.Disponible;

            MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
            MenuManager.Instance.bagManager.monsterTeam.Insert(0,monsterContainer);
            monsterContainer.GetComponent<Monster.MonsterToken>().statement = Monster.MonsterToken.statementEnum.Equipe;
        }

        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().UpdateEquipeButton();

    }

            
        
    

    public void Upgrade()
    {

    }

    public void Selected()
    {
        if(MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != gameObject 
            && MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != null 
            )
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
