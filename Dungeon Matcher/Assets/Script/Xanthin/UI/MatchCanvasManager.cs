using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Management
{
    /// <summary>
    /// XP_ Ce script referenci les objets composant l'UI du MENU MATCH. 
    /// </summary>
    public class MatchCanvasManager : MonoBehaviour
    {
        //Apparition du GameObject Profil à un point précis dans la scène.
        public GameObject profilPrefab;
        public GameObject spawnPosition;

        //Player Information
        public TextMeshProUGUI energy;
        public TextMeshProUGUI playerLevelText;
        public Image playerExperienceBar;

        void Start()
        {
            UpdateEnergy();
        }

        //Change le Game State selon le menu actuel
        public void SwitchToListMenu()
        {
            MenuManager.currentGameState = MenuManager.gameState.List;
        }
        
        //A appeler à chaque fois que le joueur perd ou gagne de l'energie.
        public void UpdateEnergy()
        {
            energy.text = "Energie : " + EnergyManager.energy.ToString() + "/" + EnergyManager.maxEnergy.ToString();
        }

        //Update le constamment les infos car on peut pas traquer le moment ou le joueur passe de niveau.
        private void Update()
        {
            playerLevelText.text = PlayerLevel.playerLevel.ToString();
            playerExperienceBar.fillAmount = (PlayerLevel.currentExperience / MenuManager.Instance.playerLevel.requiredExperience[PlayerLevel.playerLevel-1]);

        }
    }
}

