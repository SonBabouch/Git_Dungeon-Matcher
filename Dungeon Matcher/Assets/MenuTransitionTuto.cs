using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransitionTuto : MonoBehaviour
{
    [Header("Transition")]
    public GameObject topSlider0;
    public GameObject topSlider1;
    public GameObject botSlider0;
    public GameObject botSlider1;
    public GameObject topSliderInitialPosition;
    public GameObject botSliderInitialPosition;
    public GameObject topSliderTweenPosition;
    public GameObject botSliderTweenPosition;


    public void TransitionSlideOut()
    {
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderInitialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderInitialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
    }

    public void TransitionSlideIn()
    {
        topSlider0.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider0.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        topSlider1.GetComponent<Tweener>().TweenPositionTo(topSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
        botSlider1.GetComponent<Tweener>().TweenPositionTo(botSliderTweenPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
    }
}
