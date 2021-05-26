using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public Image playerEnergyBar;
    public TextMeshProUGUI healthPointText;
    public Image[] comboSkillFeedback;

    private void Update()
    {
        playerHealthBar.fillAmount = Player.Instance.health / Player.Instance.maxHealth;
        playerEnergyBar.fillAmount = Player.Instance.energy / Player.Instance.maxEnergy;

        healthPointText.text = Player.Instance.health.ToString();
    }

   
}
