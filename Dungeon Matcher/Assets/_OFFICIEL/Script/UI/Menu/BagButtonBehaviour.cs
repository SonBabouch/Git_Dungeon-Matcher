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
        if(MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Disponible || MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
        {
            MenuManager.Instance.canvasManager.bagCanvas.detailsBackground.SetActive(true);
            MenuManager.Instance.bagManager.detailShow = true;
            MenuManager.Instance.canvasManager.detailsCanvasManager.UpdateDetailsMenu();
            UnSelected();
            MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
            MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected = null;
        }    
    }

    public void Equip()
    {
        if(monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible || monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
        {
            return;
        }
        else if (monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            switch (MenuManager.Instance.bagManager.monsterTeam.Count)
            {
                case 0:
                    MenuManager.Instance.bagManager.monsterTeam.Add(monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                case 1:
                    MenuManager.Instance.bagManager.monsterTeam.Insert(1, monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                case 2:
                    GameObject monsterToRemove = MenuManager.Instance.bagManager.monsterTeam[1];
                    monsterToRemove.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;

                    MenuManager.Instance.bagManager.monsterTeam.Remove(MenuManager.Instance.bagManager.monsterTeam[1]);
                    MenuManager.Instance.bagManager.monsterTeam.Insert(1, monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                default:
                    break;
            }
        }
        MenuManager.Instance.canvasManager.bagCanvas.UpdateEquipeButton();
        MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();
    }

    public void Upgrade()
    {

    }

    public void Selected()
    {
        if(MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != gameObject 
            && MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null && (gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim))
        {
            MenuManager.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
            animator.SetTrigger("Selected");
            MenuManager.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
        }
        else if(gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
            animator.SetTrigger("Selected");
        }
    }

    public void UnSelected()
    {
        animator.SetTrigger("Unselected");
    }
}
