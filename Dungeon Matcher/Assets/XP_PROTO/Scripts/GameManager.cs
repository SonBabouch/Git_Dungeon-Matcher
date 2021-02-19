using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// XP_Ce script régi l'entièreté du jeu. ce script aura une référence de tous les autres scripts.
    /// </summary>

    public class GameManager : Singleton<GameManager>
    {
        //Script Référence.
        public MatchManager matchManager;
        public MatchCanvasManager canvasManager;
        public ListCanvasManager listCanvasManager;
        public EnergyManager energyManager;
        public CameraManager cameraManager;

        public GameObject matchCanvas;
        public GameObject listCanvas;
        
        public enum gameState { Match,List }
        public static gameState currentGameState;

        private void Start()
        {
            currentGameState = gameState.Match;
        }
    }
}

