using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

public class ProfilBehaviour : MonoBehaviour
{
    public MonsterToken monsterPick;

    public Image profilAsset;
    public TextMeshProUGUI profilName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI health;

    public void Initialisation()
    {
        //Debug.Log("Initialisation ProfilBehvaiour");
        monsterPick = MenuManager.Instance.matchManager.monsterPresented.GetComponent<MonsterToken>();
        profilAsset.sprite = monsterPick.profilPicture;
       
        description.text = monsterPick.description;
        health.text = monsterPick.health.ToString();
        profilName.text = monsterPick.monsterName;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
