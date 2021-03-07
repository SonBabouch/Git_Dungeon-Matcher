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
        //Debug.Log("Initialisation ProfilBehvaiour");
        monsterPick = MenuManager.Instance.matchManager.monsterPresented;
        profilAsset.sprite = monsterPick.GetComponent<Monster.MonsterToken>().profilPicture;
        profilName.text = monsterPick.GetComponent<Monster.MonsterToken>().name;
        description.text = monsterPick.GetComponent<Monster.MonsterToken>().description;
        health.text = monsterPick.GetComponent<Monster.MonsterToken>().health.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
