using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

public class CombatProfilList : MonoBehaviour
{
    //Valeurs à set lors du match.
    public Sprite profilAsset;
    public int numberCombat;
    

    //Valeur à set à la fin du Combat.
    public float chanceClaim;

    //Référence des GameObjects
    [SerializeField] private Image profilImage;
    [SerializeField] private TextMeshProUGUI numberCombatText;
    [SerializeField] private TextMeshProUGUI chanceDrop;

    public void UpdateVisualMatch()
    {
        //Permet de set les visuel lors du match.
        numberCombat = MenuManager.Instance.matchManager.GetComponent<MatchManager>().matchList.Count;
        chanceDrop.enabled = false;

        profilAsset = MenuManager.Instance.matchManager.GetComponent<MatchManager>().matchList[numberCombat-1].GetComponent<Monster.MonsterToken>().profilPicture;

        profilImage.sprite = profilAsset;
        numberCombatText.text = "Combat " + numberCombat.ToString() + ".";
    }

    public void UpdateVisualCombat()
    {
        chanceDrop.enabled = true;
        chanceDrop.text = chanceClaim.ToString() + " %";
    }
}
