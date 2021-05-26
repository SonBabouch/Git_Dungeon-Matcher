using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script makes the behaviour of the BagButton.
/// It makes the Fonction : Details / Use / Upgrade Working.
/// </summary>
public class BagButtonBehaviour : MonoBehaviour
{
    public GameObject monsterContainer;

    public Color claimColor;
    public Color unclaimColor;
    public Color indisponibleColor;

    public GameObject scoreParent;
    public TextMeshProUGUI scoreText;

    public void UpdateColor()
    {
        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            gameObject.GetComponent<Image>().color = claimColor;

            if (monsterContainer.GetComponent<MonsterToken>().statement != MonsterToken.statementEnum.Equipe)
            {
                monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }
        else if(monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible)
        {
            gameObject.GetComponent<Image>().color = indisponibleColor;
        }
        else
        {
            gameObject.GetComponent<Image>().color = unclaimColor;
        }

        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            UpdateScoreInt();
        }
    }

    public void UpdateScoreInt()
    {
        scoreParent.SetActive(true);
        scoreText.text = monsterContainer.GetComponent<MonsterToken>().scoring.ToString();
    }

    public void Details()
    {
        if(MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            MenuManager.Instance.blockAction = true;
            Vector3 tweenScale = new Vector3(1, 1, 1);
            MenuManager.Instance.canvasManager.bagCanvas.detailsBackgroundBG.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.4f, Easings.Ease.SmoothStep);
            MenuManager.Instance.canvasManager.bagCanvas.detailsBackground.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);

            MenuManager.Instance.bagManager.detailShow = true;
            MenuManager.Instance.canvasManager.detailsCanvasManager.UpdateDetailsMenu();
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
                    monsterToRemove.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;

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
        MenuManager.Instance.canvasManager.bagCanvas.RemoveCross();
    }

    public void Selected()
    {
        if(MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != gameObject 
            && MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null && (gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim))
        {
            MenuManager.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManager>().currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
        }
        else if(gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviour>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = gameObject;
            MenuManager.Instance.canvasManager.bagCanvas.currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviour>().monsterContainer;
        }
    }

}
