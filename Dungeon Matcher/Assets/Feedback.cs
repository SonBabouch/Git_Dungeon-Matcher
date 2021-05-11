using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public byte alphaColor = 250;

    public IEnumerator FadeOut()
    {
        for (int i = 250; i > 0; i -= 25)
        {
            alphaColor -= 25;
            yield return new WaitForSeconds(0.15f);
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, alphaColor);
        }

        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }


}
