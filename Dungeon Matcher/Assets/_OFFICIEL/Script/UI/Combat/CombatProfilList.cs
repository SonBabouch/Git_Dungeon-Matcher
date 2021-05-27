using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using System.Collections;

public class CombatProfilList : MonoBehaviour
{
    //Valeurs à set lors du match.
    public Sprite profilAsset;
    
    [SerializeField] private GameObject heartTweenPosition;
    [SerializeField] private GameObject ppTweenPosition;
    [SerializeField] private GameObject initialPosition;

    //Valeur à set à la fin du Combat.
    public float chanceClaim;

    //Référence des GameObjects
    [SerializeField] private Image profilImage;
    [SerializeField] private TextMeshProUGUI chanceDrop;
    public GameObject monsterContainer;

    public GameObject claimFeedback;
    public GameObject noClaimFeedback;
    public GameObject[] emptyHeart;
    public GameObject[] fullHeart;
    public GameObject PPParent;
    public GameObject HeartParent;
    public GameObject sparkles;
    public GameObject bonusText;

    public bool isClaim = false;

    public bool firstHeartDone = false;
    public bool secondHeartDone = false;
    public bool thirdHeartDone = false;


    [SerializeField] private RectTransform rectTransform;

    //S'appele quand le profil est matché.
    public void UpdateVisualMatch()
    {
        rectTransform.sizeDelta = new Vector2(1000, 500);
      
        monsterContainer = MenuManager.Instance.matchManager.matchList[MenuManager.Instance.matchManager.matchList.Count - 1];
        profilAsset = monsterContainer.GetComponent<MonsterToken>().profilPicture;
        //Permet de set les visuel lors du match.
        chanceDrop.enabled = false;

        profilImage.sprite = profilAsset;
    }

    public IEnumerator UpdateVisualEndCombat()
    {
        chanceDrop.enabled = true;
        float trueChanceClaim = chanceClaim;
        chanceClaim = 0;

        PPParent.GetComponent<Tweener>().TweenPositionTo(ppTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep,true);
        HeartParent.GetComponent<Tweener>().TweenPositionTo(heartTweenPosition.transform.localPosition, 1f, Easings.Ease.SmootherStep, true);
        
        //Donner Le Bonus
        //Animation Bonus
        if(MenuManager.Instance.listManager.currentTest <1)
        {
            yield return new WaitForSeconds(1f);
            bonusText.SetActive(true);
            bonusText.GetComponent<TextMeshProUGUI>().text = "BONUS :  " + ((MenuManager.Instance.listManager.currentTest - 1) * 5).ToString();

            for (int i = 0; i < (MenuManager.Instance.listManager.currentTest - 1) * 3; i++)
            {
                chanceClaim++;
                chanceDrop.text = chanceClaim.ToString() + " %";
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(2f);

        if(trueChanceClaim == 0)
        {
            chanceDrop.text = "0 %";
        }

        //Increase Value + Spawn Heart
        #region Value
        for (int i = 0; i < trueChanceClaim; i++)
        {
            chanceClaim++;
            chanceDrop.text = chanceClaim.ToString() + " %";
            yield return new WaitForSeconds(0.05f);

            if (monsterContainer.GetComponent<MonsterToken>().isGet)
            {
                if (chanceClaim >= 25 && !firstHeartDone)
                {
                    firstHeartDone = true;
                    Vector3 tweenPosition = new Vector3(1f, 1f, 1f);
                    fullHeart[0].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
                }

                if (chanceClaim >= 50 && !secondHeartDone)
                {
                    secondHeartDone = true;
                    Vector3 tweenPosition = new Vector3(1f, 1f, 1f);
                    fullHeart[1].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
                }

                if(chanceClaim >= 75 && !thirdHeartDone)
                {
                    thirdHeartDone = true;
                    Vector3 tweenPosition = new Vector3(1f, 1f, 1f);
                    fullHeart[2].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
                }
            }
        }

        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            Vector3 tweenPosition = new Vector3(1f, 1f, 1f);
            if (!firstHeartDone)
            {
                emptyHeart[0].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
            }

            if (!secondHeartDone)
            {
                emptyHeart[1].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
            }

            if (!thirdHeartDone)
            {
                emptyHeart[2].GetComponent<Tweener>().TweenScaleTo(tweenPosition, 0.1f, Easings.Ease.SmootherStep);
            }
        }
        #endregion

        if (chanceClaim >= 10)
        {
            isClaim = true;
           
        }


        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            //Dans le cas ou le monstre est déja Get.
            //Augementer du nombre de score sur ce monstre
            MenuManager.Instance.canvasManager.listCanvas.popButton.SetActive(true);
            MenuManager.Instance.playerLevel.GiveExperience(5);

            if (firstHeartDone)
            {
                monsterContainer.GetComponent<MonsterToken>().scoring++;
            }

            if (secondHeartDone)
            {
                monsterContainer.GetComponent<MonsterToken>().scoring++;
            }

            if (thirdHeartDone)
            {
                 monsterContainer.GetComponent<MonsterToken>().scoring++;
            }
            MenuManager.Instance.monsterEncyclopedie.GetNewMonsters();
            MenuManager.Instance.canvasManager.bagCanvas.SortBag();
        }
        else
        {
            //Dans le cas ou le monstre n'est pas encore Get.
            if (isClaim)
            {
                monsterContainer.GetComponent<MonsterToken>().isGet = true;
                MenuManager.Instance.monsterEncyclopedie.GetNewMonsters();
                MenuManager.Instance.canvasManager.bagCanvas.SortBag();
                PlayerLevel.currentExperience += 7;
                //Dans le cas ou ca se passe bien.
                claimFeedback.SetActive(true);
                Vector3 tweenVector = new Vector3(1f, 1f, 1f);
                claimFeedback.GetComponent<Tweener>().TweenScaleTo(tweenVector, 0.3f, Easings.Ease.SmoothStep);
                yield return new WaitForSeconds(0.25f);
                sparkles.SetActive(true);
                sparkles.GetComponent<Animator>().SetTrigger("Sparkles");
                MenuManager.Instance.canvasManager.listCanvas.popButton.SetActive(true);
            }
            else
            {
                //Dans le cas ou ca se passe pas bien.
                noClaimFeedback.SetActive(true);
                Vector3 twwenVector = new Vector3(1f, 1f, 1f);
                noClaimFeedback.GetComponent<Tweener>().TweenScaleTo(twwenVector, 0.5f, Easings.Ease.SmoothStep);
                yield return new WaitForSeconds(0.5f);
                MenuManager.Instance.canvasManager.listCanvas.popButton.SetActive(true);
            }
        }

        //A la fin mettre une bool qui permet de vérifier que toute l'anim à été joué.

    }

    public IEnumerator DispawnPrefab()
    {
        MenuManager.Instance.listManager.currentTest++;
        chanceDrop.enabled = false;
        PPParent.GetComponent<Tweener>().TweenPositionTo(initialPosition.transform.localPosition,0.5f,Easings.Ease.SmoothStep,true);
        HeartParent.GetComponent<Tweener>().TweenPositionTo(initialPosition.transform.localPosition, 0.5f,Easings.Ease.SmoothStep,true);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Tweener>().TweenScaleTo(Vector3.zero, 0.3f, Easings.Ease.SmoothStep);
    }
}
