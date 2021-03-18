using UnityEngine;
using TMPro;

namespace Management
{
    public class CanvasManager : MonoBehaviour
    {
        public PageSwiper pageSwiper;

        //Differents Menu
        public MatchCanvasManager matchCanvas;
        public ListCanvasManager listCanvas;
        public BagCanvasManager bagCanvas;
        public GameObject shopCanvas;
        public DetailsCanvasManager detailsCanvasManager;

        //NavigationTopButtons
        public GameObject topCanvas;
        public GameObject fullCanvas;

        //Title Screen
        [SerializeField] private GameObject titleScreen;

        //Notifications
        [SerializeField] private GameObject notificationBubble;
        [SerializeField] private TextMeshProUGUI numberNotif;


        private void Update()
        {
            if(MenuManager.Instance.listManager.listCurrentSize > 0)
            {
                notificationBubble.SetActive(true);
                numberNotif.text = MenuManager.Instance.listManager.listCurrentSize.ToString();
            }
        }


        public void TitleScreen()
        {
            titleScreen.SetActive(false);
        }

        //navigation
        public void GoToList()
        {

            
            if (MenuManager.Instance.canvasManager.matchCanvas.switchExp)
            {
                MenuManager.Instance.canvasManager.matchCanvas.ShowExpérience();
                
            }

            if (MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
            {
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
                
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
            
            if (MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
            {
               
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
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
            if (MenuManager.Instance.canvasManager.matchCanvas.switchExp)
            {
                MenuManager.Instance.canvasManager.matchCanvas.ShowExpérience();
            }

            if (MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
            {
                
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected.GetComponent<BagButtonBehaviour>().UnSelected();
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
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
            if (MenuManager.Instance.canvasManager.matchCanvas.switchExp)
            {
                MenuManager.Instance.canvasManager.matchCanvas.ShowExpérience();
            }
            pageSwiper.GetComponent<PageSwiper>().GoToBag();
            //Faire le changement de Panel;
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 3);
            MenuManager.currentGameState = MenuManager.gameState.Bag;

            

        }



    }
}

