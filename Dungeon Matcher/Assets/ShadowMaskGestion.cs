using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowMaskGestion : MonoBehaviour
{
    public GameObject shadowParent;
    public GameObject shadowChild;
    public Button skipButton;
    public Sprite spriteToShow;

    private void Awake()
    {
        skipButton = shadowChild.GetComponent<Button>();
        skipButton.enabled = false;
    }

    public IEnumerator ScreenFadeOut(float value, GameObject GO)
    {
        Image image = GO.GetComponent<Image>();
        if (image.color.a > 0)
        {
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, value);
            yield return new WaitForSeconds(0.1f);
            value -= 0.1f;
            StartCoroutine(ScreenFadeOut(value, GO));
        }
        else
        {

            GO.SetActive(false);
        }
    }

    public IEnumerator ScreenFadeIn(float value, GameObject GO)
    {
        Image image = GO.GetComponent<Image>();
        if (image.color.a < 0.55)
        {
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, value);
            yield return new WaitForSeconds(0.02f);
            value += 0.03f;
            StartCoroutine(ScreenFadeIn(value, GO));
        }
        skipButton.enabled = true;
    }
}
