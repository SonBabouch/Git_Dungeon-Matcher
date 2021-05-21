using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFeedback : MonoBehaviour
{
    public GameObject enemiHeal;
    public GameObject playerHeal;


    public IEnumerator HealFeedback()
    {
        switch(Skill.monsterSide)
        {
            case Skill.monsterSide.Ally:
                playerHeal.SetActive(true);
                yield return new WaitForSeconds(0.1f)
                break;
        }
    }
}
