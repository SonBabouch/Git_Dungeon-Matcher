using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowMaskGestion : MonoBehaviour
{
    public GameObject HideShadowParent;

    [Header("CardShadow")]
    public GameObject shadowParent;
    public GameObject shadowChild;

    [Header("LikeShadow")]
    public GameObject shadowLikeParent;
    public GameObject shadowLikeChild;

    [Header("DislikeShadow")]
    public GameObject shadowDislikeParent;
    public GameObject shadowDislikeChild;

    [Header("LevelShadow")]
    public GameObject shadowLevelParent;
    public GameObject shadowLevelChild;

    [Header("EnergieShadow")]
    public GameObject shadowEnergieParent;
    public GameObject shadowEnergieChild;

    [Header("SuperlikeShadow")]
    public GameObject shadowSuperlikeParent;
    public GameObject shadowSuperlikeChild;

    [Header("SuperlikeGiveShadow")]
    public GameObject shadowSuperlikeGiveParent;
    public GameObject shadowSuperlikeGiveChild;

    [Header("ListShadow")]
    public GameObject shadowListParent;
    public GameObject shadowListChild;


    [Header("BagButtonShadow")]
    public GameObject shadowBagButtonParent;
    public GameObject shadowBagButtonChild;


    [Header("ListButtonShadow")]
    public GameObject shadowListButtonParent;
    public GameObject shadowListButtonChild;

    [Header("ShowEquipeShadow")]
    public GameObject shadowShowEquipeParent;
    public GameObject shadowShowEquipeChild;

    [Header("Button")]
    public Button skipButton;

    private void Awake()
    {
        skipButton.gameObject.SetActive(false);
    }

    public IEnumerator ScreenFadeOut(float value, GameObject GO)
    {
        Image image = GO.GetComponent<Image>();
        if (image.color.a > 0)
        {
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, value);
            yield return new WaitForSeconds(0.02f);
            value -= 0.03f;
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
        skipButton.gameObject.SetActive(true);
    }
}
