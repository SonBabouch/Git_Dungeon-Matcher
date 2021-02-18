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
        public MatchManager matchManager;
        public CanvasManager canvasManager;

        public GameObject canvas;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

