using UnityEngine;

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
            //fullCanvas.GetComponent<Animator>().SetInteger("State",2);
            GameManager.currentGameState = GameManager.gameState.List;
            Debug.Log(GameManager.currentGameState);
            GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }
        public void GoToMatch()
        {
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 1);
            GameManager.currentGameState = GameManager.gameState.Match;
            GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }
        public void GoToShop()
        {
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 0);
            GameManager.currentGameState = GameManager.gameState.Shop;
            GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }
        public void GoToBag()
        {
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 3);
            GameManager.currentGameState = GameManager.gameState.Bag;
            GameManager.Instance.GetComponent<CameraController>().MoveCamera();
        }

    }
}

