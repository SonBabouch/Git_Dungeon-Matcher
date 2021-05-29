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

    [Header("Messages")]
    [TextArea]
    public string[] messages;
    private string currentText;

    public TextMeshProUGUI bubbleText;

    public bool isTyping = false;
    

    public enum MessageStatement {typing, needToSkip, needNextMessage}
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

        StartCoroutine(GoInConseillere());
    }

    //Faire Rentrer la conseillère dans l'écran.
    public IEnumerator GoInConseillere()
    {
        conseillere.GetComponent<Tweener>().TweenPositionTo(tweenPosition.transform.localPosition, 0.8f, Easings.Ease.SmoothStep, true);

        Vector3 scaleVector = new Vector3(1, 1, 1);
        bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);

        yield return new WaitForSeconds(1f);
        AffichageMessage();
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

    public void ResetButton()
    {
        skipButton.onClick.RemoveAllListeners();
    }

    public void SkipMessage()
    {
        if (statement == MessageStatement.typing)
        {
            StopCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
            currentText = messages[TutorielManager.Instance.currentIndex];
            bubbleText.text = currentText;
            statement = MessageStatement.needNextMessage;
            skipButton.onClick.AddListener(AffichageMessage);
        }
    }

    //Le mettre lorsque le joueur appuie sur le bouton.
    public void AffichageMessage()
    {
        ResetButton();
        bool needToSkip = false;
        if (TutorielManager.Instance.shadowMask.skipButton.isActiveAndEnabled)
        {
            needToSkip = true;
            TutorielManager.Instance.shadowMask.skipButton.enabled = false;
            StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeOut(TutorielManager.Instance.shadowMask.shadowChild.GetComponent<Image>().color.a, TutorielManager.Instance.shadowMask.shadowChild));
            Vector3 scaleVector = new Vector3(1, 1, 1);
            bubble.GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
            Debug.Log("good");

            //on lance le message suivant
            skipButton.onClick.AddListener(SkipMessage);
            StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
            skipButton.gameObject.SetActive(true);
        }
        else if (TutorielManager.Instance.currentIndex == 2 && !needToSkip)
        {
                //Appeler la Méthode pour finir;
                StartCoroutine(DropShadow());
            Debug.Log("Dedans");
        }else
        {
            skipButton.onClick.AddListener(SkipMessage);
            StartCoroutine(AffichageMessageEnum(0.05f, messages[TutorielManager.Instance.currentIndex]));
        }
    }

    public IEnumerator DropShadow()
    {
        skipButton.gameObject.SetActive(false);
        skipArrow.SetActive(false);
        skipText.gameObject.SetActive(false);
        StartCoroutine(WaitInfoBubble());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TutorielManager.Instance.shadowMask.ScreenFadeIn(0f, TutorielManager.Instance.shadowMask.shadowChild));
    }

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

        TutorielManager.Instance.currentIndex++;
        statement = MessageStatement.needNextMessage;
    }

}
