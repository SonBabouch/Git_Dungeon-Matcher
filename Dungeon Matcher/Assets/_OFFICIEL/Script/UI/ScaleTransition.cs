using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleTransition : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] private float speed;

    private float widthInitial;
    private float heightInitial;

    private RectTransform goRect;

    private bool switchBool;

    private void Awake()
    {
        goRect = gameObject.GetComponent<RectTransform>();

        widthInitial = goRect.rect.width;
        heightInitial = goRect.rect.height;
    }


    private IEnumerator Scale()
    {
        if (switchBool)
        {
            


        }
        else
        {

        }


        yield return new WaitForSeconds(0.1f);
    }


}
