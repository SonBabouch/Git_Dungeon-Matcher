using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class ListCanvasManager : MonoBehaviour
    {
        public void SwitchToMatchMenu()
        {
            GameManager.currentGameState = GameManager.gameState.Match;
        }
    }
}

