using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManagerTuto : MonoBehaviour
{
    public PageSwiper pageSwiper;

    //Differents Menu
    public MatchCanvasManagerTuto matchCanvas;
    public ListCanvasManagerTuto listCanvas;
    public BagCanvasManagerTuto bagCanvas;
    public GameObject shopCanvas;
    public DetailsCanvasManagerTuto detailsCanvasManager;

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
        if (MenuManagerTuto.Instance.listManager.listCurrentSize > 0)
        {
            notificationBubble.SetActive(true);
            numberNotif.text = MenuManagerTuto.Instance.listManager.listCurrentSize.ToString();
        }
        else
        {
            numberNotif.text = MenuManagerTuto.Instance.listManager.listCurrentSize.ToString();
            notificationBubble.SetActive(false);

        }
    }


   

 
    public IEnumerator ScreenFade(float value, GameObject GO)
    {
        Image image = GO.GetComponent<Image>();
        if (image.color.a > 0)
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

    #region GoTo...
    //navigation
    public void GoToList()
    {
        if (MenuManagerTuto.Instance.canvasManager.matchCanvas.switchExp)
        {
            MenuManagerTuto.Instance.canvasManager.matchCanvas.ShowExpérience();
        }

        if (MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected = null;

        }

        //Faire le changement de Panel;
        if (MenuManagerTuto.Instance.bagManager.detailShow == false)
        {
            pageSwiper.GetComponent<PageSwiper>().GoToList();
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 2);
            MenuManagerTuto.currentGameStateMenu = MenuManagerTuto.Menu.List;
        }


    }
    public void GoToMatch()
    {

        if (MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
        }

        //Faire le changement de Panel;
        if (MenuManagerTuto.Instance.bagManager.detailShow == false)
        {

            pageSwiper.GetComponent<PageSwiper>().GoToMatch();
            //fullCanvas.GetComponent<Animator>().SetInteger("State", 1);
            MenuManagerTuto.currentGameStateMenu = MenuManagerTuto.Menu.Match;
        }

    }
    public void GoToShop()
    {
        if (MenuManagerTuto.Instance.canvasManager.matchCanvas.switchExp)
        {
            MenuManagerTuto.Instance.canvasManager.matchCanvas.ShowExpérience();
        }

        if (MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected != null)
        {
            MenuManagerTuto.Instance.canvasManager.bagCanvas.currentButtonSelected = null;
        }

        //Faire le changement de Panel;
        if (MenuManagerTuto.Instance.bagManager.detailShow == false)
        {
            pageSwiper.GetComponent<PageSwiper>().GoToShop();

            //fullCanvas.GetComponent<Animator>().SetInteger("State", 0);
            MenuManagerTuto.currentGameStateMenu = MenuManagerTuto.Menu.Shop;


        }

    }
    public void GoToBag()
    {
        if (MenuManagerTuto.Instance.canvasManager.matchCanvas.switchExp)
        {
            MenuManagerTuto.Instance.canvasManager.matchCanvas.ShowExpérience();
        }
        pageSwiper.GetComponent<PageSwiper>().GoToBag();
        //Faire le changement de Panel;
        //fullCanvas.GetComponent<Animator>().SetInteger("State", 3);
        MenuManagerTuto.currentGameStateMenu = MenuManagerTuto.Menu.Bag;



    }
    #endregion

    public IEnumerator NoMatchFeedback()
    {
        Tweener t = noMatchBubble.GetComponent<Tweener>();
        Vector3 tweenScale = new Vector3(-1, 1f, 1f);
        t.TweenScaleTo(tweenScale, 1f, Easings.Ease.SmoothStep);
        yield return new WaitForSeconds(2f);
        Vector3 initialTween = new Vector3(-0.01f, 0.01f, 0.01f);
        t.TweenScaleTo(initialTween, 1f, Easings.Ease.SmoothStep);
    }
}
