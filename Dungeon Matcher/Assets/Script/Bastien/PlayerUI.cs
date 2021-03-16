using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public Image playerEnergyBar;

    private void Update()
    {
        playerHealthBar.fillAmount = Player.Instance.health / Player.Instance.maxHealth;
        playerEnergyBar.fillAmount = Player.Instance.energy / Player.Instance.maxEnergy;
    }
}
