using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFeedBackTuto : MonoBehaviour
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
        MenuManagerTuto.Instance.blockAction = true;
        FB.GetComponent<Tweener>().TweenPositionTo(tweenPositionFB.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1.5f);
        FB.GetComponent<Tweener>().TweenPositionTo(initialPositionFB.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1);
        MenuManagerTuto.Instance.blockAction = false;
    }
}
