using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class CircleScale : MonoBehaviour
{
    private bool currentlyOnAnimation = false;

    // Update is called once per frame
    void Update()
    {
       if(MenuManager.Instance.matchManager.matchList.Count == MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && !currentlyOnAnimation && MenuManager.currentGameStateMenu != MenuManager.Menu.List)
       {
            Debug.Log("Start");
            currentlyOnAnimation = true;
            StartCoroutine(Scale());
       }
       else if(MenuManager.currentGameStateMenu == MenuManager.Menu.List)
       {
            StopCoroutine(Scale());
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            currentlyOnAnimation = false;
       }
    }

    private IEnumerator Scale()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        Vector3 scaleVector = new Vector3(1, 1, 1);
        gameObject.GetComponent<Tweener>().TweenScaleTo(scaleVector,1f,Easings.Ease.SmootherStep);
        yield return null;
        yield return new WaitForSeconds(1f);
        if (currentlyOnAnimation)
        {
            StartCoroutine(Scale());
        }
    }
}
