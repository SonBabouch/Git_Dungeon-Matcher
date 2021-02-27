using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

public class ProfilBehaviour : MonoBehaviour
{
    public GameObject monsterPick;

    public Image profilAsset;
    public TextMeshProUGUI profilName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI health;

    public void Initialisation()
    {
        monsterPick = MenuManager.Instance.matchManager.monsterPresented;
        profilAsset.sprite = MenuManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().profilPicture;
        profilName.text = MenuManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().name;
        description.text = MenuManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().description;
        health.text = MenuManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().health.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
