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
    [SerializeField] private GameObject monsterContainer;


    //S'appele quand le profil est matché.
    public void UpdateVisualMatch()
    {
        //Permet de set les visuel lors du match.
        numberCombat = MenuManager.Instance.matchManager.GetComponent<MatchManager>().matchList.Count;
        chanceDrop.enabled = false;

        profilAsset = MenuManager.Instance.matchManager.GetComponent<MatchManager>().matchList[numberCombat-1].GetComponent<Monster.MonsterToken>().profilPicture;

        profilImage.sprite = profilAsset;
        numberCombatText.text = "Combat " + numberCombat.ToString() + ".";

        monsterContainer = MenuManager.Instance.matchManager.GetComponent<MatchManager>().matchList[numberCombat - 1];
    }

    //Fonction on click
    public void OnClickButton()
    {
        //Checker cb de combat on été fait.
        //Si c'est celui qui doit être fait :
        //Checker si le joueur a des monstres :
        //Si il en a pas deux, on lance pas => Afficher un message.
        //Si y'en 
    }

    public void UpdateVisualCombat()
    {
        chanceDrop.enabled = true;
        chanceDrop.text = chanceClaim.ToString() + " %";
    }
}
