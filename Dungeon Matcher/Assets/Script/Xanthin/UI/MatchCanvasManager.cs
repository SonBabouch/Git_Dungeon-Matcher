using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Management
{
    /// <summary>
    /// Ce script referenci les objets composant l'UI. 
    /// </summary>
    public class MatchCanvasManager : MonoBehaviour
    {
        public GameObject profilPrefab;
        public GameObject spawnPosition;

        public TextMeshProUGUI energy;


        void Start()
        {
            UpdateEnergy();
        }

        public void SwitchToListMenu()
        {
            GameManager.currentGameState = GameManager.gameState.List;
        }

        public void UpdateEnergy()
        {
            energy.text = "Energie : " + EnergyManager.energy.ToString() + "/" + EnergyManager.maxEnergy.ToString();
        }
    }
}

