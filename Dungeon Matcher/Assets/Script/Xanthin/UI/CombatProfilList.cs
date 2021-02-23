using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        profilImage.sprite = profilAsset;
        numberCombatText.text = "Combat " + numberCombat.ToString() + ".";
    }

    public void UpdateVisualCombat()
    {
        chanceDrop.text = chanceClaim.ToString() + " %";
    }
}
