using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        [SerializeField] private GameObject gameTitle;
        private bool trigger = false;
        [SerializeField] private GameObject tutoButton;
        [SerializeField] private GameObject gameButton;

        //Notifications
        [SerializeField] private GameObject notificationBubble;
        [SerializeField] private TextMeshProUGUI numberNotif;

        [SerializeField] private GameObject noMatchBubble;

        public GameObject topBar;
        public GameObject shopButton;
        public GameObject matchButton;
        public GameObject listButton;
        public GameObject bagButton;

        private void Update()
        {
            if(MenuManager.Instance.listManager.listCurrentSize > 0)
            {
                notificationBubble.SetActive(true);
                numberNotif.text = MenuManager.Instance.listManager.listCurrentSize.ToString();
            }
            else
            {
                numberNotif.text = MenuManager.Instance.listManager.listCurrentSize.ToString();
                notificationBubble.SetActive(false);
                
            }
        }
        

        #region TitleScreen
        public void TitleScreen()
        {
            if(!trigger)
            {
                tutoButton.SetActive(false);
                gameButton.SetActive(false);
                trigger = true;
                float value = 1f;
                StartCoroutine(ScreenFade(value, titleScreen));
                StartCoroutine(ScreenFade(value, gameTitle));
            }
        }

        public void TutoScreen()
        {

            StartCoroutine(TutoScreenEnum());
        }

        public IEnumerator TutoScreenEnum()
        {
            MenuTransitionCombat.Instance.TransitionSlideIn();
            yield return new WaitForSeconds(1f);
            ManagerManager.Instance.menuManager.SetActive(false);
            ManagerManager.Instance.combatManager.SetActive(false);
            ManagerManager.Instance.transitionMenu.SetActive(false);
            SceneManager.LoadScene(1);
        }

        public IEnumerator ScreenFade(float value, GameObject GO)
        {
            Image image = GO.GetComponent<Image>();
            if(image.color.a > 0)
            {
                image.color = new Vector4(image.color.r, image.color.g, image.color.b, value);
                yield return new WaitForSeconds(0.1f);
                value -= 0.1f;
                StartCoroutine(ScreenFade(value, GO));
            }
            else
            {

                GO.SetActive(false);
            }    
        }

        public IEnumerator TextFade(float value, GameObject GO)
        {
            TextMeshProUGUI text = GO.GetComponent<TextMeshProUGUI>();
            if (text.color.a > 0)
            {
                text.color = new Vector4(text.color.r, text.color.g, text.color.b, value);
                yield return new WaitForSeconds(0.1f);
                value -= 0.1f;
                StartCoroutine(TextFade(value, GO));
            }
            else 
            { 
            
                GO.SetActive(false);
            }
        }
        #endregion

        #region GoTo...
        //navigation
        public void GoToList()
        {
            if (MenuManager.Instance.canvasManager.matchCanvas.switchExp)
            {
                MenuManager.Instance.canvasManager.matchCanvas.ShowExpérience();
            }

            if (MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
            {
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
                
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
          
                pageSwiper.GetComponent<PageSwiper>().GoToList();
                //fullCanvas.GetComponent<Animator>().SetInteger("State", 2);
                MenuManager.currentGameStateMenu = MenuManager.Menu.List;
            }

            
        }
        public void GoToMatch()
        {
            
            if (MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
            {
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                
                pageSwiper.GetComponent<PageSwiper>().GoToMatch();
                //fullCanvas.GetComponent<Animator>().SetInteger("State", 1);
                MenuManager.currentGameStateMenu = MenuManager.Menu.Match;
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
                MenuManager.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
            }

            //Faire le changement de Panel;
            if (MenuManager.Instance.bagManager.detailShow == false)
            {
                pageSwiper.GetComponent<PageSwiper>().GoToShop();
              
                //fullCanvas.GetComponent<Animator>().SetInteger("State", 0);
                MenuManager.currentGameStateMenu = MenuManager.Menu.Shop;

                
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
            MenuManager.currentGameStateMenu = MenuManager.Menu.Bag;



        }
        #endregion

        public IEnumerator NoMatchFeedback()
        {
            Tweener t = noMatchBubble.GetComponent<Tweener>();
            Vector3 tweenScale = new Vector3 (-1, 1f, 1f);
            t.TweenScaleTo(tweenScale,1f,Easings.Ease.SmoothStep);
            yield return new WaitForSeconds(2f);
            Vector3 initialTween = new Vector3(-0.01f, 0.01f, 0.01f);
            t.TweenScaleTo(initialTween, 1f, Easings.Ease.SmoothStep);
        }

    }
}

