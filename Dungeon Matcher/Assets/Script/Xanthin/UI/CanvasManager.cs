using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class CanvasManager : MonoBehaviour
    {
        public PageSwiper pageSwiper;

        //Differents Menu
        public GameObject matchCanvas;
        public GameObject listCanvas;
        public GameObject bagCanvas;
        public GameObject shopCanvas;
        public DetailsCanvasManager detailsCanvasManager;

        //NavigationTopButtons
        public GameObject topCanvas;
        public GameObject fullCanvas;

        //Tittel Screen
        [SerializeField] private GameObject titleScreen;

        public void TitleScreen()
        {
            titleScreen.SetActive(false);
        }

        //navigation
        public void GoToList()
        {
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().switchExp)
            {
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().ShowExpérience();
            }

            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != null)
            {
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = null;
                
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                pageSwiper.GetComponent<PageSwiper>().GoToList();
                //fullCanvas.GetComponent<Animator>().SetInteger("State", 2);
                MenuManager.currentGameState = MenuManager.gameState.List;
            }
        }
        public void GoToMatch()
        {
           
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != null)
            {
               
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = null;
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                pageSwiper.GetComponent<PageSwiper>().GoToMatch();
                //fullCanvas.GetComponent<Animator>().SetInteger("State", 1);
                MenuManager.currentGameState = MenuManager.gameState.Match;
            }
            
        }
        public void GoToShop()
        {
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().switchExp)
            {
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().ShowExpérience();
            }

            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected != null)
            {
                
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().bagCanvas.GetComponent<BagCanvasManager>().currentButtonSelected = null;
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                pageSwiper.GetComponent<PageSwiper>().GoToShop();

                //fullCanvas.GetComponent<Animator>().SetInteger("State", 0);
                MenuManager.currentGameState = MenuManager.gameState.Shop;
            }
            
        }
        public void GoToBag()
        {
            if (MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().switchExp)
            {
                MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().ShowExpérience();
            }
            pageSwiper.GetComponent<PageSwiper>().GoToBag();
            //Faire le changement de Panel;
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 3);
            MenuManager.currentGameState = MenuManager.gameState.Bag;
           
        }

    }
}

