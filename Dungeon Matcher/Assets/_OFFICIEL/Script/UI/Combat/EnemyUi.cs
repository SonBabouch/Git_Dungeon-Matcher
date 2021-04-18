using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUi : MonoBehaviour
{
    public Image enemyHealthBar;
    public Image enemyEnergyBar;

    public Image profilPicture;
    public TextMeshProUGUI monsterName;

    private void Update()
    {
        if (CombatManager.Instance.inCombat)
        {
            enemyHealthBar.fillAmount = Enemy.Instance.health / Enemy.Instance.maxHealth;
            enemyEnergyBar.fillAmount = Enemy.Instance.energy / Enemy.Instance.maxEnergy;

            monsterName.text = Enemy.Instance.currentMonster.GetComponent<MonsterToken>().monsterName;
            profilPicture.sprite = Enemy.Instance.currentMonster.GetComponent<MonsterToken>().profilPicture;
        }
    }
}
