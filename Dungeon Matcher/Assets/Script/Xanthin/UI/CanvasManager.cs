﻿using UnityEngine;

namespace Management
{
    public class CanvasManager : MonoBehaviour
    {
        //Differents Menu
        public GameObject matchCanvas;
        public GameObject listCanvas;
        public GameObject bagCanvas;
        public GameObject shopCanvas;

        //NavigationTopButtons
        public GameObject topCanvas;
        public GameObject fullCanvas;


        //navigation
        public void GoToList()
        {
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected != null)
            {
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected = null;
                
            }

            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                fullCanvas.GetComponent<Animator>().SetInteger("State", 2);
                MenuManager.currentGameState = MenuManager.gameState.List;
            }
        }
        public void GoToMatch()
        {
           
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected != null)
            {
               
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected = null;
            }

            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                fullCanvas.GetComponent<Animator>().SetInteger("State", 1);
                MenuManager.currentGameState = MenuManager.gameState.Match;
            }
            //GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }
        public void GoToShop()
        {
           
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected != null)
            {
                
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentSelected = null;
            }

            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                fullCanvas.GetComponent<Animator>().SetInteger("State", 0);
                MenuManager.currentGameState = MenuManager.gameState.Shop;
            }
            //GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }
        public void GoToBag()
        {
            fullCanvas.GetComponent<Animator>().SetInteger("State", 3);
            MenuManager.currentGameState = MenuManager.gameState.Bag;
            //GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }

    }
}
