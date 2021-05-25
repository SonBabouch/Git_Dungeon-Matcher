using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

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

    public void UpdateVisualCombat()
    {
        chanceDrop.enabled = true;
        chanceDrop.text = chanceClaim.ToString() + " %";
    }
}
