using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextBulle : MonoBehaviour
{
    public int currentIndex = 0;

    [TextArea]
    public string[] messages;

    public TextMeshProUGUI bubbleText;







    public IEnumerator AffichageMessage(float delay, string fullText,string currentText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            bubbleText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

}
