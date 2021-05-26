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

    public bool isClaim = false;

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

    public IEnumerator UpdateVisualCombat()
    {
        chanceDrop.enabled = true;
        float trueChanceClaim = chanceClaim;
        chanceClaim = 0;

        PPParent.GetComponent<Tweener>().TweenPositionTo(ppTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep,true);
        HeartParent.GetComponent<Tweener>().TweenPositionTo(heartTweenPosition.transform.localPosition, 1f, Easings.Ease.SmootherStep, true);
        
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < trueChanceClaim; i++)
        {
            chanceClaim++;
            chanceDrop.text = chanceClaim.ToString() + " %";
            yield return new WaitForSeconds(0.1f);
        }

        if(chanceClaim >= 50)
        {
            isClaim = true;
        }


        if (monsterContainer.GetComponent<MonsterToken>().isGet)
        {
            //Dans le cas ou le monstre est déja Get.
            
        }
        else
        {
            //Dans le cas ou le monstre n'est pas encore Get.
            if (isClaim)
            {
                //Dans le cas ou ca se passe bien.
                claimFeedback.SetActive(true);
                Vector3 twwenVector = new Vector3(1f, 1f, 1f);
                claimFeedback.GetComponent<Tweener>().TweenScaleTo(twwenVector, 0.5f, Easings.Ease.SmoothStep);
                yield return new WaitForSeconds(0.5f);
                //Activer l'animation de Sparkles;
            }
            else
            {
                //Dans le cas ou ca se passe pas bien.
                Vector3 twwenVector = new Vector3(1f, 1f, 1f);
                noClaimFeedback.GetComponent<Tweener>().TweenScaleTo(twwenVector, 0.5f, Easings.Ease.SmoothStep);
                yield return new WaitForSeconds(0.5f);
            }
        }

        //A la fin mettre une bool qui permet de vérifier que toute l'anim à été joué.

    }
}
