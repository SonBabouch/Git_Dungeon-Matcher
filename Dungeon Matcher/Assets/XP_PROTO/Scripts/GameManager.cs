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
        //Référence Canvas
        public GameObject canvasManager; //Dans ce script se trouve les 3 différents GameObjects responsables des Canvas de : Match, List & Bag.

        public GameObject matchCanvas;
        public GameObject listCanvas;

        public CameraController cameraMain;

        //Script Référence.
        public MatchManager matchManager;
        public EnergyManager energyManager;
        

        public enum gameState { Shop,Match,List,Bag }  //Index 0 à 3 (dans l'ordre).
        public static gameState currentGameState;

        private void Start()
        {
            currentGameState = gameState.Match;
        }
    }
}

