using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class TextBulle : MonoBehaviour
{
    [SerializeField] private GameObject blackShader;
    [SerializeField] private GameObject highlightPoint;
    [SerializeField] private Button skipButton;

    [TextArea]
    public string[] messages;
    private string currentText;

    public TextMeshProUGUI bubbleText;

    public bool isTyping = false;
    public int currentIndex = 0;

    public enum MessageStatement {typing, needToSkip, needNextMessage}
    public MessageStatement statement;


    private void Start()
    {
        statement = MessageStatement.needNextMessage;
        AffichageMessage();
    }

    public void ResetButton()
    {
        skipButton.onClick.RemoveAllListeners();
    }

    public void SkipMessage()
    {
        if (statement == MessageStatement.typing)
        {
            StopCoroutine(AffichageMessageEnum(0.05f, messages[currentIndex]));
            currentText = messages[currentIndex];
            bubbleText.text = currentText;
            statement = MessageStatement.needNextMessage;
            skipButton.onClick.AddListener(AffichageMessage);
        }
    }

    //Le mettre lorsque le joueur appuie sur le bouton.
     void AffichageMessage()
     {
        ResetButton();

        if(currentIndex == messages.Length)
        {
            //Appeler la Méthode pour finir;
            skipButton.gameObject.SetActive(false);
        }
        else
        {
            skipButton.onClick.AddListener(SkipMessage);
            StartCoroutine(AffichageMessageEnum(0.05f, messages[currentIndex]));
        }
     }

    public IEnumerator AffichageMessageEnum(float delay, string fullText)
    {
        statement = MessageStatement.typing;
        for (int i = 0; i < fullText.Length; i++)
        { 
            if(statement == MessageStatement.typing)
            {
                currentText = fullText.Substring(0, i);
                bubbleText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
        currentIndex++;
        statement = MessageStatement.needNextMessage;
    }

}
