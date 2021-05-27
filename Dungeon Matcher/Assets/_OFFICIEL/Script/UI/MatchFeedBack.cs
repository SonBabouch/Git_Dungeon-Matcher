using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFeedBack : MonoBehaviour
{
    //MatchFeedback
    public GameObject initialPositionFB;
    public GameObject tweenPositionFB;
    public GameObject FB;

    public void SpawnLikeFeedBack(bool goesWell)
    {
        if (goesWell)
        {
            StartCoroutine(FBAnim());
        }
    }
    private IEnumerator FBAnim()
    {
        Management.MenuManager.Instance.blockAction = true;
        FB.GetComponent<Tweener>().TweenPositionTo(tweenPositionFB.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1.5f);
        FB.GetComponent<Tweener>().TweenPositionTo(initialPositionFB.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1);
        Management.MenuManager.Instance.blockAction = false;
    }
}

