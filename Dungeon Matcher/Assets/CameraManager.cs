using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera matchCamera;
        [SerializeField] private Camera listCamera;

        // Start is called before the first frame update
        void Start()
        {
            matchCamera.enabled = true;
            listCamera.enabled = false;

            GameManager.Instance.matchCanvas.GetComponent<Canvas>().enabled = true;
            GameManager.Instance.listCanvas.GetComponent<Canvas>().enabled = false;
        }

        public void UpdateCamera()
        {
            switch (GameManager.currentGameState)
            {
                case GameManager.gameState.List:
                    GameManager.Instance.matchCanvas.GetComponent<Canvas>().enabled = false;
                    GameManager.Instance.listCanvas.GetComponent<Canvas>().enabled = true;
                    matchCamera.enabled = false;
                    listCamera.enabled = true;
                    break;

                case GameManager.gameState.Match:
                    GameManager.Instance.matchCanvas.GetComponent<Canvas>().enabled = true;
                    GameManager.Instance.listCanvas.GetComponent<Canvas>().enabled = false;
                    matchCamera.enabled = true;
                    listCamera.enabled = false;
                    break;

                default:
                    break;
            }
        }
    }
}


