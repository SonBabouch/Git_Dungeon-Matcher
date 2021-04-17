using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUi : MonoBehaviour
{
    public Image enemyHealthBar;
    public Image enemyEnergyBar;

    private void Update()
    {
        if (CombatManager.Instance.inCombat)
        {
            enemyHealthBar.fillAmount = Enemy.Instance.health / Enemy.Instance.maxHealth;
            enemyEnergyBar.fillAmount = Enemy.Instance.energy / Enemy.Instance.maxEnergy;
        }
    }
}
