﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// XP_Ce script régi l'entièreté du jeu. ce script aura une référence de tous les autres scripts.
    /// </summary>

    public class MenuManager : MonoBehaviour
    {
        //Référence Canvas
        public static MenuManager Instance;
        public GameObject canvasManager; //Dans ce script se trouve les 3 différents GameObjects responsables des Canvas de : Match, List & Bag.

        //Script Référence.
        public MatchManager matchManager;
        public EnergyManager energyManager;
        public ListManager listManager;
        public PlayerLevel playerLevel;
        public BagManager bagManager;
        public Monster.MonsterEncyclopedie monsterEncyclopedie;

        public enum gameState { Shop,Match,List,Bag }  //Index 0 à 3 (dans l'ordre).
        public static gameState currentGameState;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            currentGameState = gameState.Match;
        }
    }
}

