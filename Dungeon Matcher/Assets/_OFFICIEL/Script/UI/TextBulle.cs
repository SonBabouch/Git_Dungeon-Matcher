using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class TextBulle : MonoBehaviour
{
    public static TextBulle Instance;

    [Header("SkipButton")]
    [SerializeField] private Button skipButton;
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private GameObject skipArrow;

    [Header("Conseillère")]
    [SerializeField] private GameObject conseillere;
    [SerializeField] private GameObject initialPosition;
    [SerializeField] private GameObject tweenPosition;
    [SerializeField] private GameObject bubble;

    public bool needToSkip = false;

    [Header("Messages")]
    [TextArea]
    public string[] messages;
    private string currentText;

    public TextMeshProUGUI bubbleText;

    public bool isTyping = false;


    public enum MessageStatement { typing, needToSkip, needNextMessage }
    public MessageStatement statement;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialisaition()
    {
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        statement = MessageStatement.needNextMessage;

        StartCoroutine(GoInConseillere(true));
    }

    //Faire Rentrer la conseillère dans l'écran.
    public IEnumerator GoInConseillere(bool isContinue)
    {
        conseillere.GetComponent<Tweener>().TweenPositionTo(tweenPosition.transform.localPosition, 0.8f, Easings.Ease.SmoothStep, true);

        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);

        yield return new WaitForSeconds(1f);
        if (isContinue)
        {
            AffichageMessage();
        }

    }

    //Faire sortir la conseillère de l'écran.
    public IEnumerator GoOutConseillere()
    {
        bubbleText.text = "";

        conseillere.GetComponent<Tweener>().TweenPositionTo(initialPosition.transform.localPosition, 0.8f, Easings.Ease.SmoothStep, true);

        Vector3 scaleVector = new Vector3(0, 0, 0);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);

        yield return new WaitForSeconds(1f);
    }

    public void ForceNext()
    {
        skipButton.onClick.AddListener(SkipMessage);
        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
    }

    public void ResetButton()
    {
        skipButton.onClick.RemoveAllListeners();
    }

    public void SkipMessage()
    {
        if (statement == MessageStatement.needNextMessage)
        {
            TutorielManager.Instance.currentIndex++;
            AffichageMessage();
        }
        else if (statement == MessageStatement.typing)
        {
            StopCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
            currentText = messages[TutorielManager.Instance.currentIndex];
            bubbleText.text = currentText;
            statement = MessageStatement.needNextMessage;
        }
        
    }

    //Le mettre lorsque le joueur appuie sur le bouton.
    public void AffichageMessage()
    {
        ResetButton();

        switch (TutorielManager.Instance.currentIndex)
        {
            case 0:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 1:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 2:
                if (!needToSkip)
                {
                    StartCoroutine(DropShadow());
                    needToSkip = true;
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeCardShadow());
                    break;
                }
            case 3:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 4:
                if (!needToSkip)
                {
                    needToSkip = true;
                    StartCoroutine(DropShadowLikeDislike());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeOutLikeShadow());
                    break;
                }
            case 5:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 6:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 7:
                if (!needToSkip)
                {
                    needToSkip = true;
                    TutorielManager.Instance.shadowMask.HideShadowParent.SetActive(false);
                    StartCoroutine(GoOutConseillere());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(GoInConseillere(false));
                    skipButton.onClick.AddListener(SkipMessage);
                    StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                    break;
                }
            case 8:
                if (!needToSkip)
                {
                    needToSkip = true;
                    TutorielManager.Instance.shadowMask.HideShadowParent.SetActive(true);
                    StartCoroutine(DropShadowLevel());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeLevelShadow());
                    break;
                }
            case 9:
                if (!needToSkip)
                {
                    needToSkip = true;
                    StartCoroutine(DropShadowEnergy());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeEnergieShadow());
                    EnergyManagerTuto.energy = EnergyManagerTuto.maxEnergy;
                    TutorielManager.Instance.menuManager.canvasManager.matchCanvas.UpdateEnergy();
                    break;
                }
            case 10:
                if (!needToSkip)
                {
                    needToSkip = true;
                    StartCoroutine(DropShadowSuperlike());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeSuperlikeShadow());
                    break;
                }
            case 11:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 12:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 13:
                if (!needToSkip)
                {
                    needToSkip = true;
                    StartCoroutine(GoOutConseillere());
                    TutorielManager.Instance.shadowMask.HideShadowParent.SetActive(false);
                    break;
                }
                else
                {
                    needToSkip = false;
                    skipButton.onClick.AddListener(SkipMessage);
                    StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                    break;
                }
            case 14:
                skipButton.onClick.AddListener(SkipMessage);
                StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
                break;
            case 15:
                if (!needToSkip)
                {
                    needToSkip = true;
                    TutorielManager.Instance.shadowMask.HideShadowParent.SetActive(true);
                    StartCoroutine(DropShadowGiveSuperlike());
                    break;
                }
                else
                {
                    needToSkip = false;
                    StartCoroutine(FadeGiveSuperlikeShadow());
                    TutorielManager.Instance.shadowMask.HideShadowParent.SetActive(false);
                    break;
                }

            default:
                break;
        }
    }

    #region Card
    public IEnumerator DropShadow()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowChild));
    }
    public IEnumerator FadeCardShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowChild));
        yield return new WaitForSeconds(1f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        skipButton.onClick.AddListener(SkipMessage);
        yield return new WaitForSeconds(1f);
        skipButton.gameObject.SetActive(true);
        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);

        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
    }
    #endregion

    #region Like
    public IEnumerator DropShadowLikeDislike()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowDislikeChild));
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowLikeChild));
    }

    public IEnumerator FadeOutLikeShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowLikeChild));
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowDislikeChild));
        yield return new WaitForSeconds(1f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
      
        yield return new WaitForSeconds(1f);
        skipButton.gameObject.SetActive(true);
        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);
        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
        skipButton.onClick.AddListener(SkipMessage);
    }

    #endregion

    #region Level
    public IEnumerator DropShadowLevel()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowLevelChild));
    }

    public IEnumerator FadeLevelShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowLevelChild));
        yield return new WaitForSeconds(1f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
     
        yield return new WaitForSeconds(1f);
        skipButton.gameObject.SetActive(true);
        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);

        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
        skipButton.onClick.AddListener(SkipMessage);
    }
    #endregion

    #region Energie
    public IEnumerator DropShadowEnergy()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowEnergieChild));
    }

    public IEnumerator FadeEnergieShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowEnergieChild));
        yield return new WaitForSeconds(1f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);

        yield return new WaitForSeconds(1f);
        skipButton.gameObject.SetActive(true);
        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);

        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
        skipButton.onClick.AddListener(SkipMessage);
    }
    #endregion

    #region Superlike
    public IEnumerator DropShadowSuperlike()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowSuperlikeChild));
    }

    public IEnumerator FadeSuperlikeShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowSuperlikeChild));
        yield return new WaitForSeconds(1f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);

        yield return new WaitForSeconds(1f);
        skipButton.gameObject.SetActive(true);
        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);

        StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
        skipButton.onClick.AddListener(SkipMessage);
    }
    #endregion

    #region GiveSuperlike
    public IEnumerator DropShadowGiveSuperlike()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowSuperlikeGiveChild));
    }

    public IEnumerator FadeGiveSuperlikeShadow()
    {
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(0.5f, TutorielManager.Instance.shadowMask.shadowSuperlikeGiveChild));
        yield return new WaitForSeconds(1f);
    }
    #endregion

    public IEnumerator WaitInfoBubble()
    {
        bubbleText.text = "";
        Vector3 scaleVector = new Vector3(0, 0, 0);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
        yield return null;
    }

    public IEnumerator AffichageMessageEnum(float delay, string fullText)
    {
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);

        statement = MessageStatement.typing;

        bubbleText.text = "";
        currentText = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            if (statement == MessageStatement.typing)
            {
                currentText = fullText.Substring(0, i);
                bubbleText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }

        skipArrow.SetActive(true);
        skipText.gameObject.SetActive(true);
        statement = MessageStatement.needNextMessage;
        
    }
}

