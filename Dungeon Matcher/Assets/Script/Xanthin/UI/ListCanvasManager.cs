using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Management
{
    public class ListCanvasManager : MonoBehaviour
    {
        public TextMeshProUGUI listeState;

        public void SwitchToMatchMenu()
        {
            GameManager.currentGameState = GameManager.gameState.Match;
        }

        private void UpdateList()
        {
            listeState.text = "Taille " + GameManager.Instance.listManager.GetComponent<ListManager>().listCurrentSize + " / " + GameManager.Instance.listManager.GetComponent<ListManager>().listMaxSize[PlayerLevel.playerLevel];
        }
    }

    

}

