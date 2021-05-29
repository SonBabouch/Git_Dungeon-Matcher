using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUITuto : MonoBehaviour
{
    public Image playerHealthBar;
    public Image playerEnergyBar;
    public TextMeshProUGUI healthPointText;
    public Image[] comboSkillFeedback;

    private void Update()
    {
        playerHealthBar.fillAmount = PlayerTuto.Instance.health / PlayerTuto.Instance.maxHealth;
        playerEnergyBar.fillAmount = PlayerTuto.Instance.energy / PlayerTuto.Instance.maxEnergy;

        healthPointText.text = PlayerTuto.Instance.health.ToString();
    }


}
