using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUiTuto : MonoBehaviour
{
    public Image enemyHealthBar;
    public Image enemyEnergyBar;

    public Image profilPicture;
    public TextMeshProUGUI monsterName;

    public TextMeshProUGUI healthPointText;

    private void Update()
    {
        if (CombatManagerTuto.Instance.inCombat)
        {
            enemyHealthBar.fillAmount = EnemyTuto.Instance.health / EnemyTuto.Instance.maxHealth;
            enemyEnergyBar.fillAmount = EnemyTuto.Instance.energy / EnemyTuto.Instance.maxEnergy;

            monsterName.text = EnemyTuto.Instance.currentMonster.GetComponent<MonsterToken>().monsterName;
            profilPicture.sprite = EnemyTuto.Instance.currentMonster.GetComponent<MonsterToken>().profilPicture;

            healthPointText.text = EnemyTuto.Instance.health.ToString();
        }
    }

    public void UpdateBarreResultat()
    {
        healthPointText.text = EnemyTuto.Instance.health.ToString();
        enemyHealthBar.fillAmount = EnemyTuto.Instance.health / EnemyTuto.Instance.maxHealth;
    }
}     

