using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;

public class ProfilBehaviour : MonoBehaviour
{
    public Image profilAsset;
    public TextMeshProUGUI profilName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI health;

    public void Initialisation()
    {
        profilAsset.sprite = GameManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().profilPicture;
        profilName.text = GameManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().name;
        description.text = GameManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().description;
        health.text = GameManager.Instance.matchManager.monsterPresented.GetComponent<Monster.MonsterToken>().health.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
