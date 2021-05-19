using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using System.Collections;

public class ProfilBehaviour : MonoBehaviour
{
    public GameObject monsterPick;

    public Image profilAsset;
    public TextMeshProUGUI profilName;
    public TextMeshProUGUI description;

    public TextMeshProUGUI[] hashtag;
    public TextMeshProUGUI level;

    public GameObject certification;

    public RectTransform rectTrans;

    private void Start()
    {
        rectTrans = gameObject.GetComponent<RectTransform>();

        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;

        //rectTrans.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Initialisation(bool firstTime)
    {
        if (firstTime)
        {
            //Debug.Log("Initialisation ProfilBehvaiour");
            monsterPick = MenuManager.Instance.matchManager.monsterPresented;
            profilAsset.sprite = monsterPick.GetComponent<MonsterToken>().profilPicture;

            description.text = monsterPick.GetComponent<MonsterToken>().description;
            
            profilName.text = monsterPick.GetComponent<MonsterToken>().monsterName;

            //Reset 

            for (int i = 0; i < hashtag.Length; i++)
            {
                hashtag[i].text = "#" + monsterPick.GetComponent<MonsterToken>().monsterHashTag[i];
            }

            level.text = "Niveau : " + monsterPick.GetComponent<MonsterToken>().monsterLevel.ToString();

            if (monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
            {
                certification.SetActive(true);
            }
            else
            {
                certification.SetActive(false);
            }
        }
        else
        {
            if (monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Claim || monsterPick.GetComponent<MonsterToken>().statement == MonsterToken.statementEnum.Equipe)
            {
                certification.SetActive(true);
            }
            else
            {
                certification.SetActive(false);
            }
        }

       
    }

    public void MatchAnim(int lenght)
    {
        StartCoroutine(MatchFade(lenght));
    }

    public void DisLikeAnim(int lenght)
    {
        StartCoroutine(DislikeFade(lenght));
    }

    

    public void Destroy()
    {
        Destroy(gameObject);
    }

    IEnumerator MatchFade(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            Vector3 translate = new Vector3(100, 0, 1);
            gameObject.transform.Translate(translate);
            yield return null;
        }
        Destroy();
    }

    IEnumerator DislikeFade(int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            Vector3 translate = new Vector3(-50, 0, 1);
            gameObject.transform.Translate(translate);
            yield return null;

        }
        Destroy();
    }
}
