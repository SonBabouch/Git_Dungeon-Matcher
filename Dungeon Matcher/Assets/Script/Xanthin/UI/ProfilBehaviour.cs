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
        profilAsset.sprite = monsterPick.GetComponent<MonsterToken>().profilPicture;
       
        description.text = monsterPick.GetComponent<MonsterToken>().description;
        health.text = monsterPick.GetComponent<MonsterToken>().health.ToString();
        profilName.text = monsterPick.GetComponent<MonsterToken>().monsterName;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
