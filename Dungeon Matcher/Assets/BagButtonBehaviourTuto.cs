using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEngine.UI;
using TMPro;

public class BagButtonBehaviourTuto : MonoBehaviour
{
    public GameObject monsterContainer;

    public Color claimColor;
    public Color unclaimColor;
    public Sprite unknownAssets;

    public GameObject scoreParent;
    public TextMeshProUGUI scoreText;

    public void UpdateColor()
    {
        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            gameObject.GetComponent<Image>().sprite = monsterContainer.GetComponent<MonsterToken>().profilPicture;
            gameObject.GetComponent<Image>().color = claimColor;


            if (monsterContainer.GetComponent<MonsterToken>().statement != MonsterToken.statementEnum.Equipe)
            {
                monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
            }
        }
        else if (monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible)
        {
            gameObject.GetComponent<Image>().sprite = unknownAssets;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = monsterContainer.GetComponent<MonsterToken>().profilPicture;
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
        if (MenuManagerTuto.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || MenuManagerTuto.Instance.canvasManager.bagCanvas.currentMonsterSelected.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            MenuManagerTuto.Instance.blockAction = true;
            Vector3 tweenScale = new Vector3(1, 1, 1);
            MenuManagerTuto.Instance.canvasManager.bagCanvas.detailsBackgroundBG.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.4f, Easings.Ease.SmoothStep);
            MenuManagerTuto.Instance.canvasManager.bagCanvas.detailsBackground.GetComponent<Tweener>().TweenScaleTo(tweenScale, 0.5f, Easings.Ease.SmoothStep);

            MenuManagerTuto.Instance.bagManager.detailShow = true;
            MenuManagerTuto.Instance.canvasManager.detailsCanvasManager.UpdateDetailsMenu();
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentMonsterSelected = null;
        }
    }

    public void Equip()
    {
        if (monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Indisponible || monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
        {
            return;
        }
        else if (monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            switch (MenuManagerTuto.Instance.bagManager.monsterTeam.Count)
            {
                case 0:
                    MenuManagerTuto.Instance.bagManager.monsterTeam.Add(monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                case 1:
                    MenuManagerTuto.Instance.bagManager.monsterTeam.Insert(1, monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                case 2:
                    GameObject monsterToRemove = MenuManagerTuto.Instance.bagManager.monsterTeam[1];
                    monsterToRemove.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;

                    MenuManagerTuto.Instance.bagManager.monsterTeam.Remove(MenuManagerTuto.Instance.bagManager.monsterTeam[1]);
                    MenuManagerTuto.Instance.bagManager.monsterTeam.Insert(1, monsterContainer);
                    monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Equipe;
                    break;
                default:
                    break;
            }
        }
        MenuManagerTuto.Instance.canvasManager.bagCanvas.UpdateEquipeButton();
        MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateCombatButton();
        MenuManagerTuto.Instance.canvasManager.bagCanvas.RemoveCross();
    }

    public void Selected()
    {
        if (MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected != gameObject
            && MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected != null && (gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim))
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManagerTuto>().currentButtonSelected = gameObject;
            MenuManagerTuto.Instance.canvasManager.bagCanvas.GetComponent<BagCanvasManagerTuto>().currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer;
        }
        else if (gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe || gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected = gameObject;
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentMonsterSelected = gameObject.GetComponent<BagButtonBehaviourTuto>().monsterContainer;
        }
    }

}
