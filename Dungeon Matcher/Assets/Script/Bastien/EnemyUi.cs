using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUi : MonoBehaviour
{
    public Image enemyHealthBar;

    private void Update()
    {
        enemyHealthBar.fillAmount = Enemy.Instance.health / Enemy.Instance.maxHealth;
    }
}
