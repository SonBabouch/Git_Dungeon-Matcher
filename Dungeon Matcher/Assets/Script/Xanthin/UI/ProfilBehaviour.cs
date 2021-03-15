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
    public GameObject certification;

    public void Initialisation()
    {
        //Debug.Log("Initialisation ProfilBehvaiour");
        monsterPick = MenuManager.Instance.matchManager.monsterPresented;
        profilAsset.sprite = monsterPick.GetComponent<MonsterToken>().profilPicture;
       
        description.text = monsterPick.GetComponent<MonsterToken>().description;
        health.text = monsterPick.GetComponent<MonsterToken>().health.ToString();
        profilName.text = monsterPick.GetComponent<MonsterToken>().monsterName;

        if (monsterPick.GetComponent<MonsterToken>().isGet)
        {
            certification.SetActive(true);
        }
        else
        {
            certification.SetActive(false);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
