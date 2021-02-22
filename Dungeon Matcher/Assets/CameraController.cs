using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera shopCamera;
    [SerializeField] private Camera matchCamera;
    [SerializeField] private Camera listCamera;
    [SerializeField] private Camera bagCamera;


    public void MoveCamera()
    {
        switch (GameManager.currentGameState)
        {
            case GameManager.gameState.Shop:
                shopCamera.enabled = true;
                matchCamera.enabled = false;
                listCamera.enabled = false;
                bagCamera.enabled = false;

                break;
            case GameManager.gameState.Match:
                shopCamera.enabled = false;
                matchCamera.enabled = true;
                listCamera.enabled = false;
                bagCamera.enabled = false;

                break;
            case GameManager.gameState.List:
                shopCamera.enabled = false;
                matchCamera.enabled = false;
                listCamera.enabled = true;
                bagCamera.enabled = false;

                break;
            case GameManager.gameState.Bag:
                shopCamera.enabled = false;
                matchCamera.enabled = false;
                listCamera.enabled = false;
                bagCamera.enabled = true;
                break;
           
            default:
                matchCamera.enabled = true;
                break;


        }
    }
}
