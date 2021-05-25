using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFeedback : MonoBehaviour
{
    public static SkillFeedback Instance;
    public GameObject enemiHeal;
    public GameObject playerHeal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }


    public IEnumerator PlayerHealFeedback(float numberOfDamage)
    {
        playerHeal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfDamage; i++)
        {
            Player.Instance.health -= 1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        playerHeal.SetActive(false);
    }

    public IEnumerator EnemiHealFeedback(float numberOfDamage)
    {
        enemiHeal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfDamage; i++)
        {
            Enemy.Instance.health -= 1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        enemiHeal.SetActive(false);
    }
}
