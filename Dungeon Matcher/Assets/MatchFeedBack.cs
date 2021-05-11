using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFeedBack : MonoBehaviour
{
   [SerializeField] private GameObject matchPrefab;
   [SerializeField] private GameObject noMatchPrefab;
   [SerializeField] private GameObject dislikePrefab;

   [SerializeField] private GameObject likePosition;
   [SerializeField] private GameObject dislikePosition;

    public void SpawnLikeFeedBack(bool goesWell)
    {
        if (goesWell)
        {
            //feedback = match
            Vector3 scale = new Vector3(1, 1, 1);
            GameObject instantiateObject = Instantiate(matchPrefab, likePosition.transform.position, Quaternion.identity);
            instantiateObject.transform.SetParent(likePosition.transform.parent);
            instantiateObject.GetComponent<Tweener>().TweenPositionTo(dislikePosition.transform.localPosition, 3f, Easings.Ease.SmoothStep, true);
            instantiateObject.GetComponent<Tweener>().TweenScaleTo(scale, 2f, Easings.Ease.SmoothStep);
            instantiateObject.GetComponent<Feedback>().StartCoroutine(instantiateObject.GetComponent<Feedback>().FadeOut());
        }
        else
        {
            Vector3 scale = new Vector3(1, 1, 1);
            GameObject instantiateObject = Instantiate(noMatchPrefab, likePosition.transform.position, Quaternion.identity);
            instantiateObject.transform.SetParent(likePosition.transform.parent);
            instantiateObject.GetComponent<Tweener>().TweenPositionTo(dislikePosition.transform.localPosition, 3f, Easings.Ease.SmoothStep, true);
            instantiateObject.GetComponent<Tweener>().TweenScaleTo(scale, 2f, Easings.Ease.SmoothStep);
            instantiateObject.GetComponent<Feedback>().StartCoroutine(instantiateObject.GetComponent<Feedback>().FadeOut());
        }
    }

    public void SpawnDislikeFeedback()
    {
        Vector3 scale = new Vector3(1, 1, 1);
        GameObject instantiateObject = Instantiate(dislikePrefab, dislikePosition.transform.position, Quaternion.identity);
        instantiateObject.transform.SetParent(dislikePosition.transform.parent);
        instantiateObject.GetComponent<Tweener>().TweenPositionTo(likePosition.transform.localPosition, 3f, Easings.Ease.SmoothStep, true);
        instantiateObject.GetComponent<Tweener>().TweenScaleTo(scale, 2f, Easings.Ease.SmoothStep);
        instantiateObject.GetComponent<Feedback>().StartCoroutine(instantiateObject.GetComponent<Feedback>().FadeOut());
    }
}

